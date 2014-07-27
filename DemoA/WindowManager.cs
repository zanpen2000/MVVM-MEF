using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.ComponentModel.Composition;

namespace DemoA
{
   
    public interface IWindowManager
    {


        void ShowMessage(string msg);



        Size Size { set; get; }

        bool MaxSize { set; get; }
    }

    
     /// <summary>
     /// WPF窗口管理器实例
     /// </summary>
    [Export(typeof(IWindowManager))]
    public class WindowManager : IWindowManager
    {
        #region Window Style
        
        private const int GWL_STYLE = -16;

        [Flags]
        public enum WindowStyle : int
        {
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
        }

        // Don't use this version for dealing with pointers
        [DllImport("user32", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // Don't use this version for dealing with pointers
        [DllImport("user32", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public static int AlterWindowStyle(Window win,
            WindowStyle orFlags, WindowStyle andNotFlags)
        {
            var interop = new WindowInteropHelper(win);

            int prevStyle = GetWindowLong(interop.Handle, GWL_STYLE);
            if (prevStyle == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(),
                    "Failed to get window style");
            }

            int newStyle = (prevStyle | (int)orFlags) & ~((int)andNotFlags);
            if (SetWindowLong(interop.Handle, GWL_STYLE, newStyle) == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(),
                    "Failed to set window style");
            }
            return prevStyle;
        }

        public static int DisableMaximizeButton(Window win)
        {
            return AlterWindowStyle(win, 0, WindowStyle.WS_MAXIMIZEBOX);
        }
        public static int DisableMinimizeButton(Window win)
        {
            return AlterWindowStyle(win, 0, WindowStyle.WS_MINIMIZEBOX);
        }
        #endregion

        public WindowManager()
        {
            Size = new Size(0, 0);
        }

        public Size Size { set; get; }
        public bool MaxSize{set;get;}

       

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }




    }
}
