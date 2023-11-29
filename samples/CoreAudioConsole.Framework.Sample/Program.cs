using CoreAudio;
using System;
using System.Diagnostics;

namespace CoreAudioConsole.Framework.Sample {
    class Program {
        static void Main(string[] args) {
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator(Guid.NewGuid());
            MMDevice device = DevEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            // Note the AudioSession manager did not have a method to enumerate all sessions in windows Vista
            // this will only work on Win7 and newer.

            foreach(var session in device.AudioSessionManager2.Sessions) {
                if(session.State == AudioSessionState.AudioSessionStateActive) {
                    Console.WriteLine("DisplayName: {0}", session.DisplayName);
                    Console.WriteLine("State: {0}", session.State);
                    Console.WriteLine("IconPath: {0}", session.IconPath);
                    Console.WriteLine("SessionIdentifier: {0}", session.SessionIdentifier);
                    Console.WriteLine("SessionInstanceIdentifier: {0}", session.SessionInstanceIdentifier);
                    Console.WriteLine("ProcessID: {0}", session.ProcessID);
                    Console.WriteLine("IsSystemIsSystemSoundsSession: {0}", session.IsSystemSoundsSession);
                    Process p = Process.GetProcessById((int)session.ProcessID);
                    Console.WriteLine("ProcessName: {0}", p.ProcessName);
                    Console.WriteLine("MainWindowTitle: {0}", p.MainWindowTitle);
                    AudioMeterInformation mi = session.AudioMeterInformation;
                    SimpleAudioVolume vol = session.SimpleAudioVolume;
                    Console.WriteLine("---[Hotkeys]---");
                    Console.WriteLine("M  Toggle Mute");
                    Console.WriteLine(",  Lower volume");
                    Console.WriteLine(".  Raise volume");
                    Console.WriteLine("Q  Quit");
                    Console.CursorVisible = false;
                    int start = Console.CursorTop;
                    while(true) {
                        //Draw a VU meter
                        int w = Console.WindowWidth - 1;
                        int len = (int)(mi.MasterPeakValue * w);
                        Console.SetCursorPosition(0, start);
                        for(int j = 0; j < len; j++)
                            Console.Write("*");
                        for(int j = 0; j < w - len; j++)
                            Console.Write(" ");
                        Console.SetCursorPosition(0, start + 1);
                        Console.WriteLine("Mute   : {0}    ", vol.Mute);
                        Console.WriteLine("Master : {0:0.00}    ", vol.MasterVolume * 100);
                        if(Console.KeyAvailable) {
                            ConsoleKeyInfo key = Console.ReadKey(true);
                            switch(key.Key) {
                                case ConsoleKey.M:
                                    vol.Mute = !vol.Mute;
                                    break;
                                case ConsoleKey.Q:
                                    Console.CursorVisible = true;
                                    return;
                                case ConsoleKey.OemComma:
                                    float curvol = vol.MasterVolume - 0.1f;
                                    if(curvol < 0) curvol = 0;
                                    vol.MasterVolume = curvol;
                                    break;
                                case ConsoleKey.OemPeriod:
                                    float curvold = vol.MasterVolume + 0.1f;
                                    if(curvold > 1) curvold = 1;
                                    vol.MasterVolume = curvold;
                                    break;
                            }

                        }
                    }
                }
            }
            //If we end up here there where no open audio sessions to monitor.
            Console.WriteLine("No Audio sessions found");
        }
    }
}
