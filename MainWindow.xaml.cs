using System;
using System.IO;
using System.Text;
using System.Windows;

namespace Base64Encoder
{
    public partial class MainWindow
    {
        private bool _textEncoded;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _textEncoded = !_textEncoded;
            if (_textEncoded)
            {
                TbText.Text = ConvertToBase64(TbText.Text);
                BtEncode.Content = "Base64";
            }
            else
            {
                var text = TbText.Text;
                TbText.Text = ConvertFromBase64(text);
                BtEncode.Content = "ASCII";
            }
        }

        static string ConvertToBase64(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        static string ConvertFromBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return string.Empty;
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, TbText.Text);
            }
        }

        private void ButtonCopy_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbText.Text);
        }
    }
}