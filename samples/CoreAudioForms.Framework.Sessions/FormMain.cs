using CoreAudio;
using CoreAudio.Interfaces;
using System.Diagnostics;
using System.Windows.Forms;

namespace CoreAudioForms.Framework.Sessions {
    public partial class FormMain : Form {
        MMDevice selDevice;

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

            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();

            ComboBoxDevices.SelectedIndexChanged += (_, __) => EnumerateSessions();

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            int selectedIndex = 0;
            for(int i = 0; i < devCol.Count; i++) {
                RenderDevice rdev = new RenderDevice(devCol[i]);
                rdev.Device.AudioSessionManager2.OnSessionCreated += HandleSessionCreated;
                if(rdev.Device.Selected) selectedIndex = i;
                ComboBoxDevices.Items.Add(rdev);
            }

            if(ComboBoxDevices.Items.Count > 0) ComboBoxDevices.SelectedIndex = selectedIndex;
        }

        private void UpdateMasterVolume(AudioVolumeNotificationData data) {
            foreach(SessionUI s in TableLayoutPanelSessions.Controls) {
                s.MasterVolume = data.MasterVolume;
            }
        }

        private void EnumerateSessions() {
            selDevice = ((RenderDevice)ComboBoxDevices.SelectedItem).Device;
            selDevice.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(UpdateMasterVolume);
            SessionCollection sessions = selDevice.AudioSessionManager2.Sessions;

            while(TableLayoutPanelSessions.Controls.Count > 0) {
                TableLayoutPanelSessions.Controls[0].Dispose();
            }
            TableLayoutPanelSessions.RowCount = 0;
            TableLayoutPanelSessions.RowStyles[0].SizeType = SizeType.AutoSize;

            foreach(AudioSessionControl2 session in sessions) {
                AddSeesion(session);
            }
        }

        private void AddSeesion(AudioSessionControl2 session) {
            if(session.State != AudioSessionState.AudioSessionStateExpired) {
                session.OnStateChanged += HandleSessionStateChanged;

                SessionUI sui = new SessionUI();
                sui.Width = TableLayoutPanelSessions.Width - TableLayoutPanelSessions.Padding.Horizontal - sui.Margin.Horizontal;
                sui.SetSession(session, selDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
                sui.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                TableLayoutPanelSessions.Controls.Add(sui);
            }
        }

        private void HandleSessionStateChanged(object sender, AudioSessionState newState) {
            if(newState == AudioSessionState.AudioSessionStateExpired) {
                AudioSessionControl2 session = (AudioSessionControl2)sender;
                foreach(SessionUI sui in TableLayoutPanelSessions.Controls) {
                    if(sui.Session.ProcessID == session.ProcessID) {
                        this.Invoke((MethodInvoker)delegate {
                            TableLayoutPanelSessions.Controls.Remove(sui);
                        });
                        break;
                    }
                }
            }
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
    }
}