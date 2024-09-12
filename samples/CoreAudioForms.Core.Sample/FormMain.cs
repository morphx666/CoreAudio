using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

            MMDeviceEnumerator devEnum = new MMDeviceEnumerator(Guid.NewGuid());
            device = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device.AudioEndpointVolume.OnVolumeNotification += (AudioVolumeNotificationData data) =>
                this.Invoke((MethodInvoker)delegate {
                    TrackBarMaster.Value = (int)(data.MasterVolume * 100);
                    CheckBoxMute.Checked = data.Muted;
                });

            TrackBarMaster.Value = (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);

            TrackBarMaster.Scroll += (object ss, EventArgs e) =>
                device.AudioEndpointVolume.MasterVolumeLevelScalar = (TrackBarMaster.Value / 100.0f);

            CheckBoxMute.CheckedChanged += (object ss, EventArgs e) =>
                device.AudioEndpointVolume.Mute = CheckBoxMute.Checked;

            Task.Run(() => {
                int history = 8;
                FixedSizedStack mpv = new FixedSizedStack(history);
                FixedSizedStack pvl = new FixedSizedStack(history);
                FixedSizedStack pvr = new FixedSizedStack(history);

                var UpdatePeakValues = new MethodInvoker(() => {
                    mpv.Push(device.AudioMeterInformation.MasterPeakValue);
                    pvl.Push(device.AudioMeterInformation.PeakValues[0]);
                    pvr.Push(device.AudioMeterInformation.PeakValues[1]);
                });

                var UpdateUI = new MethodInvoker(() => {
                    int v;

                    v = (int)(mpv.Average() * 100);
                    if(v != ProgressBarMaster.Value) ProgressBarMaster.Value = v;

                    v = (int)(pvl.Average() * 100);
                    if(v != ProgressBarLeft.Value) ProgressBarLeft.Value = v;

                    v = (int)(pvr.Average() * 100);
                    if(v != ProgressBarRight.Value) ProgressBarRight.Value = v;
                });

                int delay = 5;
                int uiDelay = 15;
                int uiCounter = 0;
                while(true) {
                    Thread.Sleep(delay);

                    if(!this.Disposing && !this.IsDisposed) {
                        this.BeginInvoke(UpdatePeakValues);

                        uiCounter += delay;
                        if(uiCounter >= uiDelay) {
                            uiCounter = 0;
                            this.BeginInvoke(UpdateUI);
                        }
                    }
                }
            });
        }
    }

    internal class FixedSizedStack {
        readonly ConcurrentQueue<float> q = new ConcurrentQueue<float>();
        private object lockObject = new object();

        public int Limit { get; set; }

        public FixedSizedStack(int limit) {
            Limit = limit;
        }

        public void Push(float value) {
            q.Enqueue(value);
            lock(lockObject) {
                while(q.Count > Limit && q.TryDequeue(out _)) ;
            }
        }

        public float Average() {
            lock(lockObject) {
                return q.Average();
            }
        }
    }
}