using CoreAudio;
using System;
using System.Windows.Forms;

namespace CoreAudioForms.Framework.Sample {
    public partial class FormMain : Form {
        private readonly MMDevice _device;

        public FormMain() {
            InitializeComponent();

            var devEnum = new MMDeviceEnumerator();
            _device = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            
            TrackBarMaster.Value = (int)(_device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            CheckBoxMute.Checked = _device.AudioEndpointVolume.Mute;

            _device.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
        }

        private void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data) {
            if(InvokeRequired) {
                object[] args = new object[1];
                args[0] = data;
                Invoke(new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification), args);
            } else {
                TrackBarMaster.Value = (int)(data.MasterVolume * 100);
                CheckBoxMute.Checked = data.Muted;
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) {
            ProgressBarMaster.Value = (int)(_device.AudioMeterInformation.MasterPeakValue * 100.0f);
            ProgressBarLeft.Value = (int)(_device.AudioMeterInformation.PeakValues[0] * 100.0f);
            ProgressBarRight.Value = (int)(_device.AudioMeterInformation.PeakValues[1] * 100.0f);
        }

        private void CheckBoxMute_CheckedChanged(object sender, EventArgs e) {
            _device.AudioEndpointVolume.Mute = CheckBoxMute.Checked;
        }

        private void TrackBarMaster_Scroll(object sender, EventArgs e) {
            _device.AudioEndpointVolume.MasterVolumeLevelScalar = (TrackBarMaster.Value / 100.0f);
        }
    }
}
