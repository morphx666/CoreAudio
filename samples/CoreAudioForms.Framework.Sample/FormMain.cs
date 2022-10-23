using CoreAudio;
using System;
using System.Windows.Forms;

namespace CoreAudioForms.Framework.Sample {
    public partial class FormMain : Form {
        private readonly MMDevice _device;

        public FormMain() {
            InitializeComponent();

            var devEnum = new MMDeviceEnumerator();
            _device = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            tbMaster.Value = (int)(_device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            _device.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
        }

        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data) {
            if(InvokeRequired) {
                object[] Params = new object[1];
                Params[0] = data;
                Invoke(new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification), Params);
            } else {
                tbMaster.Value = (int)(data.MasterVolume * 100);
            }
        }

        private void Update_Timer_Tick(object sender, EventArgs e) {
            pbMaster.Value = (int)(_device.AudioMeterInformation.MasterPeakValue * 100);
            pbLeft.Value = (int)(_device.AudioMeterInformation.PeakValues[0] * 100);
            pbRight.Value = (int)(_device.AudioMeterInformation.PeakValues[1] * 100);
        }

        private void Master_Scroll(object sender, EventArgs e) {
            _device.AudioEndpointVolume.MasterVolumeLevelScalar = (tbMaster.Value / 100.0f);
        }
    }
}
