using CoreAudio;
using System;
using System.Diagnostics;

namespace CoreAudioForms.Framework.Sample {
    class Program {
        static void Main(string[] args) {
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator(Guid.NewGuid());
            MMDevice device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            // Note the AudioSession manager did not have a method to enumerate all sessions in windows Vista
            // this will only work on Win7 and newer.

            foreach(var session in device.AudioSessionManager2.Sessions) {
                if(session.State == AudioSessionState.AudioSessionStateActive) {
                    Console.CursorVisible = false;

                    WriteLine("DisplayName", session.DisplayName);
                    WriteLine("State", session.State.ToString());
                    WriteLine("IconPath", session.IconPath);
                    WriteLine("SessionIdentifier", session.SessionIdentifier);
                    WriteLine("SessionInstanceIdentifier", session.SessionInstanceIdentifier);
                    WriteLine("ProcessID", session.ProcessID.ToString());
                    WriteLine("IsSystemIsSystemSoundsSession", session.IsSystemSoundsSession.ToString());

                    Process p = Process.GetProcessById((int)session.ProcessID);
                    WriteLine("ProcessName", p.ProcessName);
                    WriteLine("MainWindowTitle", p.MainWindowTitle);

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\n---[Hotkeys]---");
                    WriteLine("M", "Toggle Mute");
                    WriteLine("↑", "Lower volume");
                    WriteLine("↓", "Raise volume");
                    WriteLine("Q", "Quit\n");

                    AudioMeterInformation mi = session.AudioMeterInformation;
                    SimpleAudioVolume vol = session.SimpleAudioVolume;

                    int start = Console.CursorTop;
                    while(true) {
                        //Draw a VU meter
                        int w = Console.WindowWidth - 1;
                        int len = (int)(mi.MasterPeakValue * w);

                        Console.SetCursorPosition(0, start);
                        Console.ForegroundColor = ConsoleColor.Green;
                        for(int j = 0; j < len; j++) Console.Write("█");
                        for(int j = 0; j < w - len; j++) Console.Write(" ");

                        Console.SetCursorPosition(0, start + 1);
                        WriteLine("Mute   ", $"{vol.Mute,6}");
                        WriteLine("Master ", $"{vol.MasterVolume * 100,6:N2}");

                        if(Console.KeyAvailable) {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            switch(key.Key) {
                                case ConsoleKey.M:
                                    vol.Mute = !vol.Mute;
                                    break;
                                case ConsoleKey.Escape:
                                case ConsoleKey.Q:
                                    ResetConsole();
                                    return;
                                case ConsoleKey.DownArrow:
                                    float curvol = vol.MasterVolume - 0.1f;
                                    if(curvol < 0) curvol = 0;
                                    vol.MasterVolume = curvol;
                                    break;
                                case ConsoleKey.UpArrow:
                                    float curvold = vol.MasterVolume + 0.1f;
                                    if(curvold > 1) curvold = 1;
                                    vol.MasterVolume = curvold;
                                    break;
                            }

                        }
                    }
                }
            }

            // If we end up here there where no open audio sessions to monitor.
            Console.WriteLine("No Audio sessions found");
            Console.ReadKey(true);
            ResetConsole();
        }

        static void WriteLine(string key, string value) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{key}: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{value}");
        }

        static void ResetConsole() {
            Console.CursorVisible = true;
            Console.ResetColor();
        }
    }
}
