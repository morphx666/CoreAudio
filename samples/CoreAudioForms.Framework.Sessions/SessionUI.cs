using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreAudio;
using System.Threading;
using static CoreAudio.AudioSessionControl2;
using System.Diagnostics;
using System.IO;
using System.Collections.Concurrent;
using System;

namespace CoreAudioForms.Framework.Sessions {
    public partial class SessionUI : UserControl {
        private AudioSessionControl2 session;
        private readonly ConcurrentQueue<float> volPeakHistory = new ConcurrentQueue<float>();
        private int historySize = 4;
        private bool allowUpdateUI = true;
        private float masterVolume = 1.0f;

        public SessionUI() {
            InitializeComponent();

            this.FontChanged += (_, __) => SetSessionNameFont();

            SetSessionNameFont();
        }

        public float MasterVolume {
            set {
                masterVolume = value;
                UpdateUI(null, session.SimpleAudioVolume.MasterVolume, session.SimpleAudioVolume.Mute);
            }
        }

        private void SetSessionNameFont() {
            LabelName.Font = new Font(this.Font, FontStyle.Bold);
        }

        public AudioSessionControl2 Session {
            get => session;
        }

        public void SetSession(AudioSessionControl2 session, float masterVolume) {
            this.session = session;
            this.masterVolume = masterVolume;

            Process p = Process.GetProcessById((int)session.ProcessID);

            LabelName.Text = session.IsSystemSoundsSession ? "System Sounds" : session.DisplayName;
            if(LabelName.Text == "") LabelName.Text = p.ProcessName;

            Bitmap bmp = null;
            Icon icon = NativeMethods.GetIconFromFile(session.IconPath);
            if(icon == null) {
                string tmpFileName = "";
                try {
                    tmpFileName = Path.Combine((new FileInfo(p.MainModule.FileName)).DirectoryName, @"Assets\AppList.scale-200.png");
                    if(File.Exists(tmpFileName))
                        bmp = (Bitmap)Image.FromFile(tmpFileName);
                    else
                        bmp = NativeMethods.GetIconFromFile(p.MainModule.FileName).ToBitmap();
                } catch { }
            } else
                bmp = icon.ToBitmap();
            if(bmp != null) PictureBoxIcon.Image = bmp;

            session.OnSimpleVolumeChanged += new SimpleVolumeChangedDelegate(UpdateUI);
            TrackBarVol.Scroll += (_, __) => UpdateVolume();
            HandleCreated += (_, __) => {
                int newValue = 0;
                int lastValue = -1;

                Task.Run(() => {
                    while(!this.IsDisposed) {
                        Thread.Sleep(5);

                        volPeakHistory.Enqueue(session.AudioMeterInformation.MasterPeakValue);
                        if(volPeakHistory.Count > historySize) volPeakHistory.TryDequeue(out float _);
                    }
                });

                Task.Run(() => {
                    try {
                        while(!this.IsDisposed) {
                            Thread.Sleep(30);

                            newValue = Math.Min((int)(volPeakHistory.Average() * 100), 100);

                            if(newValue != lastValue) {
                                this.Invoke((MethodInvoker)delegate {
                                    ProgressBarVU.Value = newValue;
                                    lastValue = newValue;
                                });
                            }
                        };
                    } catch { }
                });
            };

            UpdateUI(null, session.SimpleAudioVolume.MasterVolume, session.SimpleAudioVolume.Mute);
        }

        private void UpdateVolume() {
            session.SimpleAudioVolume.MasterVolume = TrackBarVol.Value / 100.0f;
        }

        private void UpdateUI(object sender, float newVolume, bool newMute) {
            if(!allowUpdateUI) return;
            if(InvokeRequired)
                Invoke(new SimpleVolumeChangedDelegate(UpdateUI), new object[] { sender, newVolume, newMute });
            else
                TrackBarVol.Value = (int)(newVolume * masterVolume * 100);
        }
    }
}