using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreAudio;

namespace CoreAudioSample
{
    public partial class FormMain : Form
    {
        private MMDevice device;

        public FormMain()
        {
            InitializeComponent();
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            TrackbarMaster.Value = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            device.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
            TimerUpdate.Interval = 30;
            TimerUpdate.Enabled = true;
        }

        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            if (this.InvokeRequired)
            {
                object[] Params = new object[1];
                Params[0] = data;
                this.Invoke(new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification), Params);
            }
            else
            {
                TrackbarMaster.Value = (int)(data.MasterVolume * 100);
            }
        }

        private void TrackbarMaster_Scroll(object sender, EventArgs e)
        {
            device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)TrackbarMaster.Value / 100.0f);
        }

        private void TimerUpdate_Tick(object sender, EventArgs e)
        {
            ProgressBarMaster.Value = (int)(device.AudioMeterInformation.MasterPeakValue * 100);
            ProgressBarLeft.Value = (int)(device.AudioMeterInformation.PeakValues[0]*100);
            ProgressBarRight.Value = (int)(device.AudioMeterInformation.PeakValues[1]* 100);
        }

        
    }
}