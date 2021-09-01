using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace CoreAudioForms.Framework.Sessions {
    public static class NativeMethods {
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_SMALLICON = 1;
        private const uint SHGFI_LARGEICON = 0;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEINFO {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)] public string szTypeName;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        public static Icon GetIconFromFile(string fileName, int index = 0, bool smallIcon = false) {
            if(fileName.Contains(",") && int.TryParse(fileName.Split(',')[1], out index)) fileName = fileName.Split(',')[0];
            fileName = Environment.ExpandEnvironmentVariables(fileName);
            if(fileName.Contains("@")) fileName = fileName.Replace("@", "");

            if(File.Exists(fileName)) {
                SHFILEINFO shInfo = new SHFILEINFO {
                    szDisplayName = new string('\0', 260),
                    szTypeName = new string('\0', 80),
                    iIcon = index
                };

                var r = SHGetFileInfo(fileName, 0, ref shInfo, (uint)Marshal.SizeOf(shInfo), SHGFI_ICON | (smallIcon ? SHGFI_SMALLICON : SHGFI_LARGEICON));

                return shInfo.hIcon.ToInt32() == 0 ? null : Icon.FromHandle(shInfo.hIcon);
            } else {
                Console.WriteLine($"File not found: {fileName}");
            }

            return null;
        }
    }
}