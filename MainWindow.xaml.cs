using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace Base64Encoder
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
            Controls.OnCloseClick += (a, args) => Close();
            Controls.OnMaximizeClick += (a, args) => WindowState = (WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            Controls.OnMinimizeClick += (a, args) => WindowState = WindowState.Minimized;
        }

        private static string ConvertToBase64(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string ConvertFromBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return string.Empty;
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            }
            catch
            {
                return string.Empty;
            }
        }

        private void ButtonCopyAscii_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbTextAscii.Text);
        }

        private void ButtonCopyBase64_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbTextBase64.Text);
        }

        private void TbText_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbTextAscii.IsFocused)
            {
                TbTextBase64.Text = ConvertToBase64(TbTextAscii.Text);
            }
            else if (TbTextBase64.IsFocused)
            {
                TbTextAscii.Text = ConvertFromBase64(TbTextBase64.Text);
            }
        }

        private void CaptionBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            RemoveWindowRoundedCorners();
            RenderOptions.ProcessRenderMode = RenderMode.Default;
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            TbTextAscii.Clear();
            TbTextBase64.Clear();
        }
    }
}