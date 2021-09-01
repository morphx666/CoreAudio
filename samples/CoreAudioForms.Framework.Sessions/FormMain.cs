using CoreAudio;
using System.Windows.Forms;

namespace CoreAudioForms.Framework.Sessions {
    public partial class FormMain : Form {
        private class RenderDevice {
            public readonly string Name;
            public readonly MMDevice Device;

            public RenderDevice(MMDevice device) {
                Device = device;
                Name = $"{device.Properties[PKEY.PKEY_Device_DeviceDesc].Value} ({device.FriendlyName})";
            }

            public override string ToString() {
                return Name;
            }
        }

        public FormMain() {
            InitializeComponent();

            ComboBoxDevices.SelectedIndexChanged += (_, __) => EnumerateSessions();

            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            var devCol = deviceEnumerator.EnumerateAudioEndPoints(EDataFlow.eRender, DEVICE_STATE.DEVICE_STATE_ACTIVE);
            for(int i = 0; i < devCol.Count; i++) {
                ComboBoxDevices.Items.Add(new RenderDevice(devCol[i]));
            }

            if(ComboBoxDevices.Items.Count > 0) ComboBoxDevices.SelectedIndex = 0;
        }

        // TODO: Handle Sessions' changes (creation and expiration) and adjust UI accordingly
        private void EnumerateSessions() {
            MMDevice selDevice = ((RenderDevice)ComboBoxDevices.SelectedItem).Device;
            SessionCollection sessions = selDevice.AudioSessionManager2.Sessions;

            while(TableLayoutPanelSessions.Controls.Count > 0) {
                TableLayoutPanelSessions.Controls[0].Dispose();
            }
            TableLayoutPanelSessions.RowCount = 0;
            TableLayoutPanelSessions.RowStyles[0].SizeType = SizeType.AutoSize;

            foreach(AudioSessionControl2 session in sessions) {
                if(session.State != AudioSessionState.AudioSessionStateExpired) {
                    SessionUI sui = new SessionUI();
                    sui.Width = TableLayoutPanelSessions.Width - TableLayoutPanelSessions.Padding.Horizontal - sui.Margin.Horizontal;
                    sui.SetSession(session);
                    sui.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    TableLayoutPanelSessions.Controls.Add(sui);
                }
            }
        }
    }
}