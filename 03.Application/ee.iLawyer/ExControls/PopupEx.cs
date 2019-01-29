using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;

namespace ee.iLawyer.ExControls
{
    public class PopupEx : Popup
    {

        public DependencyObject PopupPlacementTarget
        {
            get { return (DependencyObject)GetValue(PopupPlacementTargetProperty); }
            set { SetValue(PopupPlacementTargetProperty, value); }
        }

        public static readonly DependencyProperty PopupPlacementTargetProperty =
            DependencyProperty.Register("PopupPlacementTarget", typeof(DependencyObject), typeof(PopupEx), new PropertyMetadata(null, OnPopupPlacementTargetChanged));


        private static void OnPopupPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DependencyObject popupPopupPlacementTarget = e.NewValue as DependencyObject;
                Popup pop = d as Popup;

                Window w = Window.GetWindow(popupPopupPlacementTarget);
                if (null != w)
                {
                    //让Popup随着窗体的移动而移动
                    w.LocationChanged += delegate
                    {
                        var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        mi.Invoke(pop, null);
                    };
                    //让Popup随着窗体的Size改变而移动位置
                    w.SizeChanged += delegate
                    {
                        var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                        mi.Invoke(pop, null);
                    };
                }
            }
        }
















        /// <summary>  
        /// 是否窗口随动，默认为随动（true）  
        /// </summary>  
        public bool IsPositionUpdate
        {
            get { return (bool)GetValue(IsPositionUpdateProperty); }
            set { SetValue(IsPositionUpdateProperty, value); }
        }

        public static readonly DependencyProperty IsPositionUpdateProperty =
            DependencyProperty.Register("IsPositionUpdate", typeof(bool), typeof(PopupEx), new PropertyMetadata(true, new PropertyChangedCallback(IsPositionUpdateChanged)));

        private static void IsPositionUpdateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PopupEx).pup_Loaded(d as PopupEx, null);
        }

        /// <summary>  
        /// 加载窗口随动事件  
        /// </summary>  
        public PopupEx()
        {
            this.Loaded += pup_Loaded;
        }

        /// <summary>  
        /// 加载窗口随动事件  
        /// </summary>  
        private void pup_Loaded(object sender, RoutedEventArgs e)
        {
            Popup pup = sender as Popup;
            var win = VisualTreeHelper.GetParent(pup);
            while (win != null && (win as Window) == null)
            {
                win = VisualTreeHelper.GetParent(win);
            }
            if ((win as Window) != null)
            {
                (win as Window).LocationChanged -= PositionChanged;
                (win as Window).SizeChanged -= PositionChanged;
                if (IsPositionUpdate)
                {
                    (win as Window).LocationChanged += PositionChanged;
                    (win as Window).SizeChanged += PositionChanged;
                }
            }
        }

        /// <summary>  
        /// 刷新位置  
        /// </summary>  
        private void PositionChanged(object sender, EventArgs e)
        {
            try
            {
                var method = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                if (this.IsOpen)
                {
                    method.Invoke(this, null);
                }
            }
            catch
            {
                return;
            }
        }

        //是否最前默认为非最前（false）  
        public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(PopupEx), new FrameworkPropertyMetadata(false, OnTopmostChanged));
        public bool Topmost
        {
            get { return (bool)GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }
        private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as PopupEx).UpdateWindow();
        }

        /// <summary>  
        /// 重写拉开方法，置于非最前  
        /// </summary>  
        /// <param name="e"></param>  
        protected override void OnOpened(EventArgs e)
        {
            UpdateWindow();
        }

        /// <summary>  
        /// 刷新Popup层级  
        /// </summary>  
        public void UpdateWindow()
        {
            if (this.Child == null)
            {
                return;
            }

            var hwnd = ((HwndSource)PresentationSource.FromVisual(this.Child)).Handle;
            RECT rect;
            if (NativeMethods.GetWindowRect(hwnd, out rect))
            {
                NativeMethods.SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)this.Width, (int)this.Height, 0);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #region P/Invoke imports & definitions  
        public static class NativeMethods
        {


            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
            [DllImport("user32", EntryPoint = "SetWindowPos")]
            internal static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        }
        #endregion
    }

}
