using System;
using CoreAudio;
using CoreAudio.Interfaces;
using System.Windows.Forms;
using System.Drawing;

namespace CoreAudioForms.Framework.Sessions {
    public partial class FormMain : Form {
        private MMDevice selDevice;
        private VU.Modes vuMode = VU.Modes.Bar;

        #region Theme colors
        private readonly Color formBackColor = Color.FromArgb(41, 48, 69);
        private readonly Color formForeColor = Color.Gainsboro;
        private readonly Color sessionBackColor = Color.FromArgb(9, 28, 50);
        private readonly Color vuBorderColor = Color.FromArgb(74, 100, 136);
        private readonly Color vuBackColor = Color.FromArgb(33, 33, 33);
        private readonly Color vuForeColor = Color.FromArgb(34, 60, 91);
        private readonly Color sessionTrackBarKnobColor = Color.FromArgb(34, 68, 91);
        private readonly Color sessionTrackBarLineColor = Color.FromArgb(1, 1, 2);

        private Color[] ledsColorsOff = new Color[] { ControlPaint.Dark(Color.DarkGreen), ControlPaint.Dark(Color.DarkGoldenrod), ControlPaint.Dark(Color.DarkRed) };
        private Color[] ledsFullColorsOn = new Color[] { ControlPaint.Dark(Color.LightGreen), ControlPaint.Dark(Color.Yellow), ControlPaint.Dark(Color.Red) };
        private Color[] ledsAdjColorsOn = new Color[] { Color.LightGreen, Color.Yellow, Color.Red };
        #endregion

        private class RenderDevice {
            public readonly string Name;
            public readonly MMDevice Device;

            public RenderDevice(MMDevice device) {
                Device = device;
                Name = $"{device.Properties[PKey.DeviceDescription].Value} ({device.DeviceInterfaceFriendlyName})";
            }

            public override string ToString() {
                return Name;
            }
        }

        public FormMain() {
            InitializeComponent();

            ComboBoxDevices.SelectedIndexChanged += (_, __) => EnumerateSessions();

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator(Guid.NewGuid());
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            int selectedIndex = 0;
            for(int i = 0; i < devCol.Count; i++) {
                RenderDevice rdev = new RenderDevice(devCol[i]);
                rdev.Device.AudioSessionManager2.OnSessionCreated += HandleSessionCreated;
                if(rdev.Device.Selected) selectedIndex = i;
                ComboBoxDevices.Items.Add(rdev);
            }

            if(ComboBoxDevices.Items.Count > 0) ComboBoxDevices.SelectedIndex = selectedIndex;

            this.FlowLayoutPanelSessions.Resize += (_, __) => ResizeUI();

            this.BackColor = formBackColor;
            this.ForeColor = formForeColor;
        }

        private void EnumerateSessions() {
            selDevice = ((RenderDevice)ComboBoxDevices.SelectedItem).Device;
            selDevice.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(UpdateSessionsMasterVolume);
            SessionCollection sessions = selDevice.AudioSessionManager2.Sessions;

            while(FlowLayoutPanelSessions.Controls.Count > 0) {
                FlowLayoutPanelSessions.Controls[0].Dispose();
            }

            foreach(AudioSessionControl2 session in sessions) {
                AddSeesion(session);
            }
        }

        private void AddSeesion(AudioSessionControl2 session) {
            if(session.State != AudioSessionState.AudioSessionStateExpired) {
                session.OnStateChanged += HandleSessionStateChanged;

                SessionUI sui = new SessionUI();
                sui.BackColor = sessionBackColor;

                sui.VUDisplay.ForeColor = vuForeColor;
                sui.VUDisplay.BorderColor = vuBorderColor;
                sui.VUDisplay.LedsColorsOff = ledsColorsOff;
                sui.VUDisplay.LedsFullColorsOn = ledsFullColorsOn;
                sui.VUDisplay.LedsAdjColorsOn = ledsAdjColorsOn;
                sui.VUDisplay.Mode = vuMode;

                sui.TrackBarVol.TrackerColor = sessionTrackBarKnobColor;
                sui.TrackBarVol.TrackLineColor = sessionTrackBarLineColor;

                sui.SetSession(session, selDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
                sui.Click += (_, __) => {
                    if(vuMode == VU.Modes.Bar) {
                        vuMode = VU.Modes.Leds;
                    } else {
                        vuMode = VU.Modes.Bar;
                    }

                    ApplyGlobalVUMode(this);
                };
                sui.OnSessionVolumeChanged += (float newVolume) => { // FIXME: The volume handling between the UI,
                                                                     // the master volume and the sessions' volumes
                                                                     // should be simplified. Right now it's a spaghetti of a mess.
                    if(newVolume > 1.0f) {
                        newVolume = 1.0f;
                    } else if(newVolume < 0.0f) {
                        newVolume = 0.0f;
                    }
                    selDevice.AudioEndpointVolume.MasterVolumeLevelScalar = newVolume;
                    UpdateSessionsMasterVolume(new AudioVolumeNotificationData(Guid.Empty, false, newVolume, new float[] { newVolume }));
                };

                ApplyVUMode(sui.VUDisplay);
                FlowLayoutPanelSessions.Controls.Add(sui);
                sui.Width = FlowLayoutPanelSessions.Width - FlowLayoutPanelSessions.Padding.Horizontal - sui.Margin.Horizontal;
            }
        }

        private void HandleSessionStateChanged(object sender, AudioSessionState newState) {
            AudioSessionControl2 session = (AudioSessionControl2)sender;

            switch(newState) {
                case AudioSessionState.AudioSessionStateActive:
                    foreach(SessionUI sui in FlowLayoutPanelSessions.Controls) {
                        if(sui.Session.ProcessID == session.ProcessID) {
                            sui.SetSession(session, selDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
                            break;
                        }
                    }
                    break;
                case AudioSessionState.AudioSessionStateInactive:
                    foreach(SessionUI sui in FlowLayoutPanelSessions.Controls) {
                        if(sui.Session.ProcessID == session.ProcessID) {
                            sui.UnsetSession();
                            break;
                        }
                    }
                    break;
                case AudioSessionState.AudioSessionStateExpired:
                    foreach(SessionUI sui in FlowLayoutPanelSessions.Controls) {
                        if(sui.Session.ProcessID == session.ProcessID) {
                            this.Invoke((MethodInvoker)delegate {
                                FlowLayoutPanelSessions.Controls.Remove(sui);
                            });

                            break;
                        }
                    }
                    break;
                default:
                    break;
            }

            ResizeUI();
        }

        private void HandleSessionCreated(object sender, IAudioSessionControl2 newSession) {
            AudioSessionManager2 asm = (AudioSessionManager2)sender;
            newSession.GetProcessId(out uint newSessionId);

            asm.RefreshSessions();
            foreach(var session in asm.Sessions) {
                if(session.ProcessID == newSessionId) {
                    this.Invoke((MethodInvoker)delegate {
                        AddSeesion(session);
                    });
                    break;
                }
            }
        }

        private void UpdateSessionsMasterVolume(AudioVolumeNotificationData data) {
            foreach(SessionUI sui in FlowLayoutPanelSessions.Controls) {
                sui.MasterVolume = data.MasterVolume;
            }
        }

        #region UI Stuff
        private void ResizeUI() {
            this.Invoke((MethodInvoker)delegate {
                int m = 0;
                if(FlowLayoutPanelSessions.VerticalScroll.Visible) {
                    m = SystemInformation.VerticalScrollBarWidth;
                }

                foreach(SessionUI sui in FlowLayoutPanelSessions.Controls) {
                    sui.Width = FlowLayoutPanelSessions.Width -
                                FlowLayoutPanelSessions.Padding.Horizontal -
                                sui.Margin.Horizontal - m;
                }

                FlowLayoutPanelSessions.Refresh(); // This prevents some weird artifacts
            });
        }

        private void ApplyGlobalVUMode(Control parentControl) {
            foreach(Control c in parentControl.Controls) {
                if(c.HasChildren) {
                    ApplyGlobalVUMode(c);
                } else {
                    if(c is VU vu) {
                        ApplyVUMode(vu);
                        break;
                    }
                }
            }
        }

        private void ApplyVUMode(VU vu) {
            vu.Mode = vuMode;
            vu.BackColor = vuMode == VU.Modes.Leds ?
                vuBackColor :
                vuBackColor;
        }
        #endregion
    }
}