using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreAudioForms.Framework.Sessions {
    public partial class VU : UserControl {
        private int channels;
        private int[] values;
        private CancellationTokenSource cts;
        private Color barFullLevelColor = Color.FromArgb(44, 44, 44);
        private float[] ledsRanges = new float[] { 50.0f, 35.0f, 15.0f };
        private Color[] ledsColorsOff = new Color[] { ControlPaint.Dark(Color.DarkGreen), ControlPaint.Dark(Color.DarkGoldenrod), ControlPaint.Dark(Color.DarkRed) };
        private Color[] ledsFullColorsOn = new Color[] { ControlPaint.Dark(Color.LightGreen), ControlPaint.Dark(Color.Yellow), ControlPaint.Dark(Color.Red) };
        private Color[] ledsAdjColorsOn = new Color[] { Color.LightGreen, Color.Yellow, Color.Red };

        public enum Modes {
            Bar,
            Leds
        }

        public VU() {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);

            this.BorderStyle = BorderStyle.None;
        }

        public float Volume { get; set; }

        [Description("Value Levels for each Channel"), Category("Behavior")]
        public int[] Values {
            get => values;
            set {
                for(int i = 0; i < Channels; i++) {
                    if(value[i] < 0)
                        value[i] = 0;
                    else if(value[i] > 100)
                        value[i] = 100;
                    this.values[i] = value[i];
                }
            }
        }

        [Description("Border Color"), Category("Appearance")]
        public Color BorderColor { get; set; } = Color.FromKnownColor(KnownColor.ActiveBorder);

        [Description("Channels"), Category("Appearance")]
        public int Channels {
            get => channels;
            set {
                cts?.Cancel();
                cts = new CancellationTokenSource();

                channels = value;
                values = new int[channels];

                Task.Run(async () => {
                    while(true) {
                        await Task.Delay(30);
                        this.Invalidate();
                    }
                }, cts.Token);
            }
        }

        [Description("Bar Background Color"), Category("Appearance")]
        public Color BarBackColor { get; set; } = Color.FromKnownColor(KnownColor.Control);

        [Description("Mode"), Category("Appearance")]
        public Modes Mode { get; set; } = Modes.Bar;

        [Description("Leds Colors On (Full Scale)"), Category("Appearance")]
        public Color[] LedsFullColorsOn {
            get => ledsFullColorsOn;
            set {
                if(value.Length != ledsColorsOff.Length) return;
                ledsFullColorsOn = value;
            }
        }

        [Description("Leds Colors On (Volume Adjusted)"), Category("Appearance")]
        public Color[] LedsAdjColorsOn {
            get => ledsAdjColorsOn;
            set {
                if(value.Length != ledsColorsOff.Length) return;
                ledsAdjColorsOn = value;
            }
        }

        [Description("Leds Colors Off"), Category("Appearance")]
        public Color[] LedsColorsOff {
            get => ledsColorsOff;
            set {
                if(value.Length != ledsFullColorsOn.Length) return;
                ledsColorsOff = value;
            }
        }

        [Description("Leds Ranges"), Category("Appearance")]
        public float[] LedsRanges {
            get => ledsRanges;
            set {
                if(value.Length != ledsRanges.Length) return;
                ledsRanges = value;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e) {
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            Size size = new Size(this.Width - 1, this.Height - 1);

            g.Clear(this.BackColor);
            g.DrawRectangle(new Pen(BorderColor), 0, 0, size.Width, size.Height);

            size.Width -= 3;
            size.Height -= 3;
            size.Height /= Channels;

            for(int i = 0; i < Channels; i++) {
                int fullLevel = (int)Math.Floor(size.Width * values[i] / 100.0f);
                int adjLevel = (int)Math.Floor(size.Width * values[i] * Volume / 100.0f);

                switch(Mode) {
                    case Modes.Bar:
                        g.FillRectangle(new SolidBrush(barFullLevelColor), 2, 1 + size.Height * i + (i + 1), fullLevel, size.Height);
                        g.FillRectangle(new SolidBrush(ForeColor), 2, 1 + size.Height * i + (i + 1), adjLevel, size.Height);
                        break;
                    case Modes.Leds:
                        for(int j = 0; j < size.Width; j += 2) {
                            float p = (float)Math.Floor((float)j / size.Width * 100.0f);
                            float range = 0;

                            for(int k = 0; k < ledsRanges.Length; k++) {
                                range += ledsRanges[k];
                                if(p < range) {
                                    if(fullLevel > j) {
                                        g.FillRectangle(new SolidBrush(ledsFullColorsOn[k]),
                                            2 + j,
                                            1 + size.Height * i + (i + 1),
                                            1,
                                            size.Height);
                                    } else {
                                        g.FillRectangle(new SolidBrush(ledsColorsOff[k]),
                                            2 + j,
                                            1 + size.Height * i + (i + 1),
                                            1,
                                            size.Height);
                                    }

                                    if(adjLevel > j) {
                                        g.FillRectangle(new SolidBrush(ledsAdjColorsOn[k]),
                                            2 + j,
                                            1 + size.Height * i + (i + 1),
                                            1,
                                            size.Height);
                                    }

                                    break;
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}