using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoreAudio;
using static CoreAudio.AudioSessionControl2;
using System.Diagnostics;
using System.IO;
using System.Collections.Concurrent;
using System;
using System.Threading;
using xFXJumpStart;
using XComponent.SliderBar;

namespace CoreAudioForms.Framework.Sessions {
    public partial class SessionUI : UserControl {
        private AudioSessionControl2 session;
        private ConcurrentQueue<float>[] volPeakHistory;
        private readonly int historySize = 8;
        private readonly bool allowUpdateUI = true;
        private float masterVolume = 1.0f;
        private CancellationTokenSource cts;

        public delegate void UpdateDeviceMasterVolumeDelegate(float newVolume);
        public event UpdateDeviceMasterVolumeDelegate OnSessionVolumeChanged;

        private int channels;
        private int[] newValues;
        private int[] lastValues;

        public SessionUI() {
            InitializeComponent();


            this.SetStyle(ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            this.FontChanged += (_, __) => SetSessionNameFont();

            SetSessionNameFont();

            foreach(Control c in this.Controls) {
                if(!(c is MACTrackBar)) c.Click += (_, __) => this.OnClick(__);
            }

            this.Paint += Render;
        }

        public float MasterVolume {
            set {
                masterVolume = value;
                UpdateUI(null, session.SimpleAudioVolume.MasterVolume, session.SimpleAudioVolume.Mute);
            }
        }

        public AudioSessionControl2 Session {
            get => session;
        }

        public void SetSession(AudioSessionControl2 session, float masterVolume) {
            CancelMonitors();

            this.session = session;
            this.masterVolume = masterVolume;
            this.channels = session.AudioMeterInformation.PeakValues.Count;
            SafeSetSession();
            StartRenderer();
        }

        private void SafeSetSession() {
            if(this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate {
                    UpdateSessionUI();
                });
            else {
                UpdateSessionUI();
            }
        }

        public void UnsetSession() {
            CancelMonitors();

            this.channels = 0;
            SafeSetSession();
        }

        private void CancelMonitors() {
            cts?.Cancel();
            cts = new CancellationTokenSource();

            Application.DoEvents(); // Ugly hack to allow some time for the cts.Cancel() request to be processed
        }

        private void InitChannels() {
            newValues = new int[channels];
            lastValues = new int[channels];

            volPeakHistory = new ConcurrentQueue<float>[channels];
            for(int i = 0; i < channels; i++) {
                volPeakHistory[i] = new ConcurrentQueue<float>();

                newValues[i] = 0;
                lastValues[i] = -1;
            }

            VUDisplay.Channels = channels == 0 ? 2 : channels;
        }

        private Bitmap GetSessionIcon(Process p, AudioSessionControl2 session) {
            Icon icon = NativeMethods.GetIconFromFile(session.IconPath);
            if(icon == null) {
                try {
                    string tmpFileName = Path.Combine((new FileInfo(p.MainModule.FileName)).DirectoryName, @"Assets\AppList.scale-200.png");
                    if(File.Exists(tmpFileName))
                        return (Bitmap)Image.FromFile(tmpFileName);
                    else
                        return NativeMethods.GetIconFromFile(p.MainModule.FileName).ToBitmap();
                } catch {
                    return null;
                }
            } else {
                return icon.ToBitmap();
            }
        }

        private void UpdateVolume() {
            float v = (TrackBarVol.Value / masterVolume) / 100.0f;
            if(v > 1.0f) {
                OnSessionVolumeChanged?.Invoke(masterVolume + (v - 1.0f));
            } else {
                session.SimpleAudioVolume.MasterVolume = v;
            }
        }

        #region UI Stuff
        private void StartRenderer() {
            Task.Run(async () => {
                int m = 5;
                int r = 30;
                int t = 0;

                while(!this.IsDisposed) {
                    await Task.Delay(m);

                    for(int i = 0; i < channels; i++) {
                        volPeakHistory[i].Enqueue(session.AudioMeterInformation.PeakValues[i]);
                        if(volPeakHistory[i].Count > historySize) volPeakHistory[i].TryDequeue(out float _);
                    }

                    t += m;
                    if(t >= r) {
                        for(int i = 0; i < channels; i++) {
                            newValues[i] = Math.Min((int)(volPeakHistory[i].Average() * 100), 100);
                            if(newValues[i] != lastValues[i]) {
                                VUDisplay.Values[i] = newValues[i];
                                lastValues[i] = newValues[i];
                            }
                        }
                    }
                }
            }, cts.Token);
        }

        private void SetSessionNameFont() {
            LabelName.Font = new Font(this.Font, FontStyle.Bold);
            LabelSessionInfo.Font = new Font(this.Font.FontFamily, this.Font.Size - 2, FontStyle.Regular);

            VUDisplay.Top = LabelName.Height + LabelSessionInfo.Height + 4;
            TrackBarVol.Top = VUDisplay.Bottom + 4;

            this.Height = Math.Max(PictureBoxIcon.Bottom, VUDisplay.Bottom) + 8;
        }

        private void UpdateSessionUI() {
            Process p = Process.GetProcessById((int)session.ProcessID);
            LabelName.Text = session.IsSystemSoundsSession ? "System Sounds" : session.DisplayName;
            if(LabelName.Text == "") LabelName.Text = p.ProcessName;
            LabelSessionInfo.Text = channels == 0 ? "" : p.MainWindowTitle;

            Bitmap bmp = GetSessionIcon(p, session);
            if(bmp != null) PictureBoxIcon.Image = bmp;

            session.OnSimpleVolumeChanged += new SimpleVolumeChangedDelegate(UpdateUI);
            TrackBarVol.Scroll += (_, __) => UpdateVolume();

            InitChannels();
            UpdateUI(null, session.SimpleAudioVolume.MasterVolume, session.SimpleAudioVolume.Mute);
        }

        private void UpdateUI(object sender, float newVolume, bool newMute) {
            if(!allowUpdateUI) return;
            if(InvokeRequired)
                Invoke(new SimpleVolumeChangedDelegate(UpdateUI), new object[] { sender, newVolume, newMute });
            else
                TrackBarVol.Value = (int)(newVolume * masterVolume * 100.0f);
            VUDisplay.Volume = newVolume * masterVolume;
        }

        private void Render(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Rectangle b = this.ClientRectangle;
            b.Width -= 1;
            b.Height -= 1;

            g.Clear(this.ParentForm.BackColor);
            g.FillRoundedRectangle(new SolidBrush(BackColor), b, 12);
        }
        #endregion
    }
}