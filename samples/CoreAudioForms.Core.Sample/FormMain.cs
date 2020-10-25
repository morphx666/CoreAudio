using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreAudio;

// Original code provided by bferdinandus
// https://github.com/bferdinandus

namespace CoreAudioForms.Core.Sample {
    public partial class FormMain : Form {
        private readonly MMDevice device;

        public FormMain() {
            InitializeComponent();

            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            device = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            device.AudioEndpointVolume.OnVolumeNotification += (AudioVolumeNotificationData data) =>
                this.Invoke((MethodInvoker)delegate { TrackBarMaster.Value = (int)(data.MasterVolume * 100); });

            TrackBarMaster.Value = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);

            TrackBarMaster.Scroll += (object ss, EventArgs e) =>
                device.AudioEndpointVolume.MasterVolumeLevelScalar = (TrackBarMaster.Value / 100.0f);

            Task.Run(() => {
                var UpdateUI = new MethodInvoker(() => {
                    int v;
                    v = (int)(device.AudioMeterInformation.MasterPeakValue * 100);
                    if(v != ProgressBarMaster.Value) ProgressBarMaster.Value = v;

                    v = (int)(device.AudioMeterInformation.PeakValues[0] * 100);
                    if(v != ProgressBarLeft.Value) ProgressBarLeft.Value = v;

                    v = (int)(device.AudioMeterInformation.PeakValues[1] * 100);
                    if(v != ProgressBarRight.Value) ProgressBarRight.Value = v;
                });

                while(true) {
                    Thread.Sleep(33);
                    this.BeginInvoke(UpdateUI);
                }
            });
        }
    }
}