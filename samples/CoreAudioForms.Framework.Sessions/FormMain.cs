using System;
using CoreAudio;
using CoreAudio.Interfaces;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace CoreAudioForms.Framework.Sessions {
    public partial class FormMain : Form {
        MMDevice selDevice;
        private VU.Modes vuMode = VU.Modes.Bar;

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

            this.BackColor = Color.FromArgb(55, 55, 55);
            this.ForeColor = Color.Gainsboro;
        }

        private void EnumerateSessions() {
            selDevice = ((RenderDevice)ComboBoxDevices.SelectedItem).Device;
            selDevice.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(UpdateMasterVolume);
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
                sui.BackColor = Color.FromArgb(46, 46, 46);
                sui.VUDisplay.BorderColor = Color.FromArgb(66, 66, 66);
                sui.SetSession(session, selDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
                sui.VUDisplay.Mode = vuMode;
                sui.Click += (_, __) => {
                    if(vuMode == VU.Modes.Bar) {
                        vuMode = VU.Modes.Leds;
                    } else {
                        vuMode = VU.Modes.Bar;
                    }

                    ApplyGlobalVUMode(this);
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

        private void UpdateMasterVolume(AudioVolumeNotificationData data) {
            foreach(SessionUI s in FlowLayoutPanelSessions.Controls) {
                s.MasterVolume = data.MasterVolume;
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
                Color.FromArgb(33, 33, 33) :
                Color.FromArgb(33, 33, 33);
        }
        #endregion
    }
}