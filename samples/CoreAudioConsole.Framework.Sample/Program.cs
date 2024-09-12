using CoreAudio;
using System;
using System.Diagnostics;

namespace CoreAudioForms.Framework.Sample {
    class Program {
        static void Main(string[] args) {
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator(Guid.NewGuid());
            MMDevice device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            foreach(var session in device.AudioSessionManager2.Sessions) {
                if(session.State == AudioSessionState.AudioSessionStateActive) {
                    Console.CursorVisible = false;
                    PrintSessionInfo(session);

                    AudioMeterInformation mi = session.AudioMeterInformation;
                    SimpleAudioVolume vol = session.SimpleAudioVolume;

                    (int cw, int ch) = (Console.WindowWidth, Console.WindowHeight);
                    int start = Console.CursorTop;
                    while(true) {
                        if(cw != Console.WindowWidth || ch != Console.WindowHeight) {
                            Console.Clear();
                            PrintSessionInfo(session);

                            start = Console.CursorTop;
                            Console.SetCursorPosition(0, start);
                            cw = Console.WindowWidth;
                            ch = Console.WindowHeight;
                        }

                        //Draw a VU meter
                        int w = Console.WindowWidth - 1;
                        int len = (int)(mi.MasterPeakValue * w);

                        Console.SetCursorPosition(0, start);
                        Console.ForegroundColor = ConsoleColor.Green;
                        for(int j = 0; j < len; j++) Console.Write("█");
                        for(int j = 0; j < w - len + 1; j++) Console.Write(" ");

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

        private static void PrintSessionInfo(AudioSessionControl2 session) {
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
