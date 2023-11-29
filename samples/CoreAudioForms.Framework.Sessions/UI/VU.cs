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
        private float[] ledsRanges = new float[] { 50.0f, 35.0f, 15.0f };
        private Color[] ledsColorsOff = new Color[] { Color.DarkGreen, Color.DarkGoldenrod, Color.DarkRed };
        private Color[] ledsColorsOn = new Color[] { Color.LightGreen, Color.Yellow, Color.Red };

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

        [Description("Leds Colors On"), Category("Appearance")]
        public Color[] LedsColorsOn {
            get => ledsColorsOn;
            set {
                if(value.Length != ledsColorsOff.Length) return;
                ledsColorsOn = value;
            }
        }

        [Description("Leds Colors Off"), Category("Appearance")]
        public Color[] LedsColorsOff {
            get => ledsColorsOff;
            set {
                if(value.Length != ledsColorsOn.Length) return;
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
                int b = (int)Math.Floor(size.Width * (float)values[i] / 100.0f);

                switch(Mode) {
                    case Modes.Bar:
                        g.FillRectangle(new SolidBrush(ForeColor), 2, 1 + size.Height * i + (i + 1), b, size.Height);
                        break;
                    case Modes.Leds:
                        for(int j = 0; j < size.Width; j += 2) {
                            float p = (float)Math.Floor((float)j / size.Width * 100.0f);
                            float range = 0;

                            for(int k = 0; k < ledsRanges.Length; k++) {
                                range += ledsRanges[k];
                                if(p < range) {
                                    g.FillRectangle(new SolidBrush(b > j ? ledsColorsOn[k] : ledsColorsOff[k]),
                                        2 + j,
                                        1 + size.Height * i + (i + 1),
                                        1,
                                        size.Height);
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