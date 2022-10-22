using CoreAudio;
using CoreAudio.Interfaces;
using System.Windows.Forms;

namespace CoreAudioForms.Framework.Sessions {
    public partial class FormMain : Form {
        private class RenderDevice {
            public readonly string Name;
            public readonly MMDevice Device;

            public RenderDevice(MMDevice device) {
                Device = device;
                Name = $"{device.Properties[PKEY.PKEY_Device_DeviceDesc].Value} ({device.DeviceInterfaceFriendlyName})";
            }

            public override string ToString() {
                return Name;
            }
        }

        public FormMain() {
            InitializeComponent();

            ComboBoxDevices.SelectedIndexChanged += (_, __) => EnumerateSessions();

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDeviceCollection devCol = deviceEnumerator.EnumerateAudioEndPoints(EDataFlow.eRender, DeviceState.Active);
            int selectedIndex = 0;
            for(int i = 0; i < devCol.Count; i++) {
                RenderDevice rdev = new RenderDevice(devCol[i]);
                rdev.Device.AudioSessionManager2.OnSessionCreated += HandleSessionCreated;
                if(rdev.Device.Selected) selectedIndex = i;
                ComboBoxDevices.Items.Add(rdev);
            }

            if(ComboBoxDevices.Items.Count > 0) ComboBoxDevices.SelectedIndex = selectedIndex;
        }

        private void EnumerateSessions() {
            MMDevice selDevice = ((RenderDevice)ComboBoxDevices.SelectedItem).Device;
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
                sui.SetSession(session);
                sui.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                TableLayoutPanelSessions.Controls.Add(sui);
            }
        }

        private void HandleSessionStateChanged(object sender, AudioSessionState newState) {
            if(newState == AudioSessionState.AudioSessionStateExpired) {
                AudioSessionControl2 session = (AudioSessionControl2)sender;
                foreach(SessionUI sui in TableLayoutPanelSessions.Controls) {
                    if(sui.Session.GetProcessID == session.GetProcessID) {
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
                if(session.GetProcessID == newSessionId) {
                    this.Invoke((MethodInvoker)delegate {
                        AddSeesion(session);
                    });
                    break;
                }
            }
        }
    }
}