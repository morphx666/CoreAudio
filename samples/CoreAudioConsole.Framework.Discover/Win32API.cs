using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreAudioConsole.Framework.Discover {
    internal static class Win32API {
        [DllImport("user32.dll")] private static extern int SendMessage(IntPtr hwndLock, Int32 wMsg, Int32 wParam, ref Point pt);
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private const int WM_USER = 0x400;
        private const int EM_GETSCROLLPOS = WM_USER + 0xDD;
        private const int EM_SETSCROLLPOS = WM_USER + 0xDE;
        private const int WM_SETREDRAW = 0x0b;

        public static Point GetScrollPos(this RichTextBox txtbox) {
            Point pt = new Point();
            SendMessage(txtbox.Handle, EM_GETSCROLLPOS, 0, ref pt);
            return pt;
        }

        public static void SetScrollPos(this RichTextBox txtbox, Point pt) {
            SendMessage(txtbox.Handle, EM_SETSCROLLPOS, 0, ref pt);
        }

        public static void SuspendDrawing(this RichTextBox richTextBox) {
            SendMessage(richTextBox.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero);
        }

        public static void ResumeDrawing(this RichTextBox richTextBox) {
            SendMessage(richTextBox.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            richTextBox.Invalidate();
        }
    }
}
