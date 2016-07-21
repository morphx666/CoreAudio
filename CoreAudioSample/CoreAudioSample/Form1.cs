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
    public partial class Form1 : Form
    {
        private MMDevice device;

        public Form1()
        {
            InitializeComponent();
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            device = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            tbMaster.Value = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            device.AudioEndpointVolume.OnVolumeNotification += new AudioEndpointVolumeNotificationDelegate(AudioEndpointVolume_OnVolumeNotification);
            timer1.Enabled = true;
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
                tbMaster.Value = (int)(data.MasterVolume * 100);
            }
        }

        private void tbMaster_Scroll(object sender, EventArgs e)
        {
            device.AudioEndpointVolume.MasterVolumeLevelScalar = ((float)tbMaster.Value / 100.0f);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pkMaster.Value = (int)(device.AudioMeterInformation.MasterPeakValue * 100);
            pkLeft.Value = (int)(device.AudioMeterInformation.PeakValues[0]*100);
            pkRight.Value = (int)(device.AudioMeterInformation.PeakValues[1]* 100);
        }

        
    }
}