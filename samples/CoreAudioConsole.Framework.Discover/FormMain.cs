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
        private readonly string nl = Environment.NewLine;
        private readonly string dashes = new string('-', 32);

        private System.Threading.Timer timer;
        private int tabLevel;

        private readonly List<Part> lines;
        private readonly List<Part> ctrls;

        private bool isFirstTime;

        public FomMain() {
            InitializeComponent();

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
        }

        string lastResult = "";
        private void SafeEnum() {
            Task.Run(() => {
                string result = EnumDevices(DataFlow.All);
                if(lastResult != result) {
                    lastResult = result;
                    this.Invoke(new MethodInvoker(() => {
                        RichTextBoxOutput.Text = result.ToString();
                    }));
                }
            });
        }

        private string EnumDevices(DataFlow flow) {
            MMDeviceCollection deviceCollection = new MMDeviceEnumerator(Guid.NewGuid()).EnumerateAudioEndPoints(flow, DeviceState.Active);
            StringBuilder sb = new StringBuilder($"{DateTime.Now}{nl}{nl}");
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
                sb.AppendLine($"{dashes}{nl}");
            }

            return sb.ToString();
        }

        private string EnumSessions(MMDevice dev) {
            string tabs = new string('\t', tabLevel + 1);
            StringBuilder sb = new StringBuilder($"{tabs}SESSIONS{nl}");
            SessionCollection sessions = dev.AudioSessionManager2.Sessions;
            for(int i = 0; i < sessions.Count; i++) {
                using(AudioSessionControl2 audioSessionControl2 = sessions[i]) {
                    string name = audioSessionControl2.DisplayName;
                    if(name.Contains(",")) name = name.Split(',')[0];
                    if(name == "") name = "<No Name>";
                    sb.AppendLine($"{tabs}{name} [{audioSessionControl2.ProcessID} | {audioSessionControl2.State}]:");
                    sb.AppendLine($"{tabs}{'\t'}Volume: {audioSessionControl2.SimpleAudioVolume.MasterVolume:F2}dB");
                    sb.AppendLine($"{tabs}{'\t'}Mute: {(!audioSessionControl2.SimpleAudioVolume.Mute ? "Not " : "")}Muted{nl}");
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
            string tabs = new string('\t', tabLevel);
            StringBuilder sb = new StringBuilder($"{tabs}{part.SubTypeName}: {(part.Name == "" ? "(Unnamed)" : part.Name)} ({part.PartType}){nl}");
            // If part.GetSubTypeName = "UNDEFINED" Then Debugger.Break();

            if(part.PartType == PartType.Connector) tabLevel += 1;

            switch(part.SubType) {
                case Guid r when r == KsNodeType.Volume: {
                        AudioVolumeLevel audioVolumeLevel = part.AudioVolumeLevel;
                        if(audioVolumeLevel != null) {
                            for(int i = 0; i < audioVolumeLevel.ChannelCount; i++) {
                                PerChannelDbLevel.LevelRange levelRange = audioVolumeLevel.GetLevelRange(i);
                                sb.AppendLine($"\t {tabs}Volume for channel {i} is {audioVolumeLevel.GetLevel(i):F2}dB (range is {levelRange.MinLevel:F2}dB to {levelRange.MaxLevel:F2}dB in increments of {levelRange.Stepping:F2}dB)");
                            }
                        }

                        break;
                    }

                case Guid r when r == KsNodeType.Speaker: {
                        sb.AppendLine($"{tabs}Incoming Parts:");
                        break;
                    }

                case Guid r when r == KsNodeType.Mute: {
                        AudioMute audioMute = part.AudioMute;
                        if(audioMute != null)
                            sb.AppendLine($"{tabs}\t{(!audioMute.Mute ? "Not " : "")}Muted");
                        break;
                    }

                case Guid r when r == KsNodeType.PeakMeter: {
                        AudioPeakMeter audioPeakMeter = part.AudioPeakMeter;
                        if(audioPeakMeter != null) {
                            int num = checked(audioPeakMeter.ChannelCount - 1);
                            int channel = 0;
                            while(channel <= num) {
                                sb.AppendLine($"{tabs}\tLevel for channel {channel} is {audioPeakMeter.Level(channel)}dB");
                                checked { ++channel; }
                            }
                        }
                        break;
                    }

                case Guid r when r == KsNodeType.Loudness: {
                        var al = part.AudioLoudness;
                        if(al != null)
                            sb.AppendLine($"{tabs}\t{(!al.Enabled ? "Not " : "")} Enabled");
                        break;
                    }

                default: {
                        if(part.PartType == PartType.Connector)
                            sb.AppendLine($"\t{tabs}I/O Jack");
                        else
                            sb.AppendLine($"\t{tabs}Undefined Part: {part.SubTypeName}");
                        break;
                    }
            }

            var plIn = part.EnumPartsIncoming;
            if(plIn == null || plIn.Count == 0)
                sb.AppendLine();
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