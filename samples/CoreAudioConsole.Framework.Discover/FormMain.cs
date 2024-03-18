using CoreAudio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreAudioConsole.Framework.Discover.Tester {
    public partial class FomMain : Form {
        private readonly string nl = @"\par ";// + Environment.NewLine;

        private System.Threading.Timer timer;
        private int tabLevel;

        private readonly List<Part> lines;
        private readonly List<Part> ctrls;

        private bool isFirstTime;

        private Point rtbScrollPos = new Point();
        private const string rtf = @"{\rtf1\ansi {\colortbl;" +
                                    @"\red245\green245\blue245;" + // cf1 = WhiteSmoke
                                    @"\red000\green192\blue000;" + // cf2 = DarkGreen
                                    @"\red192\green192\blue000;" + // cf3 = DarkYellow
                                    @"\red192\green000\blue192;" + // cf4 = DarkMagenta
                                    @"\red255\green000\blue000;" + // cf5 = Red
                                    @"\red180\green100\blue180;" + // cf6 = LightMagenta
                                    @"\red080\green080\blue255;" + // cf7 = Violet
                                    @"\red080\green080\blue080;" + // cf8 = DarkGray
                                    @"}%\par}";

        public FomMain() {
            InitializeComponent();

            //RichTextBoxOutput.Text = $"A\tB";
            //return;

            Load += (object s, EventArgs e) => {
                timer = new System.Threading.Timer((_) => SafeEnum(), null, 10, Timeout.Infinite);
            };
            FormClosing += (object s, FormClosingEventArgs e) => {
                timer.Dispose();
            };

            lines = new List<Part>();
            ctrls = new List<Part>();

            ButtonClose.Click += (object s, EventArgs e) => Close();
            ButtonRefresh.Click += (object s, EventArgs e) => timer.Change(10, Timeout.Infinite);
            ButtonSave.Click += (object s, EventArgs e) => SaveOutput();
            CheckBoxAutoRefresh.CheckedChanged += (object s, EventArgs e) => {
                if(CheckBoxAutoRefresh.Checked) {
                    timer.Change(10, 850);
                    CheckBoxAutoRefresh.BackColor = Color.FromArgb(192, 0, 0);
                } else {
                    timer.Change(0, Timeout.Infinite);
                    CheckBoxAutoRefresh.BackColor = this.BackColor;

                }
            };
            RichTextBoxOutput.Click += (object s, EventArgs e) => {
                RichTextBoxOutput.SetScrollPos(rtbScrollPos);
            };
        }

        string lastResult = "";
        private void SafeEnum() {
            Task.Run(() => {
                string result = EnumDevices(DataFlow.All);
                if(lastResult != result) {
                    lastResult = result;
                    this.Invoke(new MethodInvoker(() => {
                        rtbScrollPos = RichTextBoxOutput.GetScrollPos();
                        RichTextBoxOutput.SuspendDrawing();
                        RichTextBoxOutput.Rtf = rtf.Replace("%", result.ToString());
                        RichTextBoxOutput.SetScrollPos(rtbScrollPos);
                        RichTextBoxOutput.ResumeDrawing();
                    }));
                }
            });
        }

        private string EnumDevices(DataFlow flow) {
            MMDeviceCollection deviceCollection = new MMDeviceEnumerator(Guid.NewGuid()).EnumerateAudioEndPoints(flow, DeviceState.Active);
            StringBuilder sb = new StringBuilder($@"\cf2 {DateTime.Now}{nl}{nl}");
            for(int i = 0; i < deviceCollection.Count; i++) {
                MMDevice dev = deviceCollection[i];

#if DEBUG
                if(!isFirstTime) {
                    isFirstTime = true;
                    for(int j = 0; j < dev.Properties.Count; j++) {
                        PropertyStoreProperty property = dev.Properties[j];
                        string str2 = property.Value.GetType().ToString();
                        if(str2.Contains(".")) str2 = str2.Split('.').Last();
                        Debug.Write(string.Format("{0}, {1} ({2}): ", property.Key.fmtId, property.Key.PId, str2));
                        if(property.Value.GetType() == typeof(byte[])) {
                            byte[] values = (byte[])property.Value;
                            for(int k = 0; k < values.Length; k++) {
                                Debug.Write(string.Format("{0} ", values[k].ToString("X2")));
                            }
                        } else
                            Debug.Write(property.Value.ToString());
                        Debug.WriteLine("");
                    }
                }
#endif

                Part getPart = dev.DeviceTopology.GetConnector(0).ConnectedTo.Part;
                tabLevel = 0;
                sb.Append(WalkTreeBackwardsFromPart(getPart));
                sb.Append(EnumSessions(dev));

                sb.Append($@"\cf8 ");
                sb.Append('—', RichTextBoxOutput.Width / 12);
                sb.Append($"{nl}{nl}");
            }

            return sb.ToString();
        }

        private string EnumSessions(MMDevice dev) {
            string tabs = "";
            for(int i = 0; i < tabLevel + 1; i++) tabs += @"\tab ";

            StringBuilder sb = new StringBuilder($@"\cf4 {tabs}SESSIONS{nl}");
            SessionCollection sessions = dev.AudioSessionManager2.Sessions;
            for(int i = 0; i < sessions.Count; i++) {
                using(AudioSessionControl2 audioSessionControl2 = sessions[i]) {
                    string name = audioSessionControl2.DisplayName;
                    if(name.Contains(',')) name = name.Split(',')[0];
                    if(name == "") name = "<No Name>";
                    sb.Append($@"\cf3 {tabs}{name} [{audioSessionControl2.ProcessID} | {audioSessionControl2.State}]:{nl}");
                    sb.Append($@"\cf6 {tabs}\tab Volume: {audioSessionControl2.SimpleAudioVolume.MasterVolume:F2}dB{nl}");
                    sb.Append($@"\cf6 {tabs}\tab Mute: {(!audioSessionControl2.SimpleAudioVolume.Mute ? "Not " : "")}Muted{nl}{nl}");
                }
            }
            return sb.ToString();
        }

        private void GetLines(Part part) {
            if(part.PartType == PartType.Connector) lines.Add(part);
            PartsList enumPartsIncoming = part.EnumPartsIncoming;
            if(enumPartsIncoming == null) return;
            for(int i = 0; i < enumPartsIncoming.Count; i++) {
                GetLines(enumPartsIncoming.Part(i));
            }
        }

        private string WalkTreeBackwardsFromPart(Part part) {
            string tabs = "";
            for(int i = 0; i < tabLevel; i++) tabs += @"\tab ";

            StringBuilder sb = new StringBuilder($@"\cf1 {tabs}{part.SubTypeName}: \cf3 {(part.Name == "" ? "(Unnamed)" : part.Name)} ({part.PartType}){nl}");
            // If part.GetSubTypeName = "UNDEFINED" Then Debugger.Break();

            if(part.PartType == PartType.Connector) tabLevel += 1;

            switch(part.SubType) {
                case Guid r when r == KsNodeType.Volume: {
                    AudioVolumeLevel audioVolumeLevel = part.AudioVolumeLevel;
                    if(audioVolumeLevel != null) {
                        for(int i = 0; i < audioVolumeLevel.ChannelCount; i++) {
                            PerChannelDbLevel.LevelRange levelRange = audioVolumeLevel.GetLevelRange(i);
                            sb.Append($@"\cf6\tab {tabs}Volume for channel {i} is {audioVolumeLevel.GetLevel(i):F2}dB (range is {levelRange.MinLevel:F2}dB to {levelRange.MaxLevel:F2}dB in increments of {levelRange.Stepping:F2}dB){nl}");
                        }
                    }

                    break;
                }

                case Guid r when r == KsNodeType.Speaker: {
                    //sb.Append($@"\cf6 {tabs}Incoming Parts:{nl}");
                    break;
                }

                case Guid r when r == KsNodeType.Mute: {
                    AudioMute audioMute = part.AudioMute;
                    if(audioMute != null)
                        sb.Append($@"\cf6 {tabs}\tab {(!audioMute.Mute ? "Not " : "")}Muted{nl}");
                    break;
                }

                case Guid r when r == KsNodeType.PeakMeter: {
                    AudioPeakMeter audioPeakMeter = part.AudioPeakMeter;
                    if(audioPeakMeter != null) {
                        int num = checked(audioPeakMeter.ChannelCount - 1);
                        int channel = 0;
                        while(channel <= num) {
                            sb.Append($@"\cf6 {tabs}\tab Level for channel {channel} is {audioPeakMeter.Level(channel)}dB{nl}");
                            checked { ++channel; }
                        }
                    }
                    break;
                }

                case Guid r when r == KsNodeType.Loudness: {
                    var al = part.AudioLoudness;
                    if(al != null)
                        sb.Append($@"\cf6 {tabs}\tab {(!al.Enabled ? "Not " : "")} Enabled{nl}");
                    break;
                }

                default: {
                    if(part.PartType == PartType.Connector)
                        sb.Append($@"\cf6 {tabs}\tab I/O Jack{nl}");
                    else
                        sb.Append($@"\cf6 {tabs}\tab Undefined Part: {part.SubTypeName}{nl}");
                    break;
                }
            }

            var plIn = part.EnumPartsIncoming;
            if(plIn == null || plIn.Count == 0)
                sb.Append(nl);
            else
                for(int i = 0; i < plIn.Count; i++) {
                    var iPart = plIn.Part(i);
                    sb.Append(WalkTreeBackwardsFromPart(iPart));
                }

            if(part.PartType == PartType.Connector) tabLevel -= 1;
            part.Dispose();
            return sb.ToString();
        }

        private void SaveOutput() {
            using(SaveFileDialog sfDlg = new SaveFileDialog()) {
                sfDlg.FileName = "CoreAudioNET_Report.txt";
                sfDlg.Filter = "Text Files|*.txt";
                sfDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                sfDlg.Title = "Save CoreAudioNET Report";
                if(sfDlg.ShowDialog() == DialogResult.OK) {
                    File.WriteAllText(sfDlg.FileName, RichTextBoxOutput.Text);
                }
            }
        }
    }
}