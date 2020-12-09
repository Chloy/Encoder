using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace Encrypter {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public long fileSize;
        public MainWindow() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if ((string)Mode.Content == "Encrypt")
                Mode.Content = "Dencrypt";
            else Mode.Content = "Encrypt";
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true) {
                Input.Text = fileDialog.FileName;
                Output.Text = fileDialog.FileName;
            }
        }

        public byte[] ReadBytes(string path) {
            byte[] bytes;
            using (FileStream file = new FileStream(path, FileMode.Open)) {
                fileSize = file.Length;
                bytes = new byte[fileSize];
                for (long i = 0; i < fileSize; i++) {
                    bytes[i] = (byte)file.ReadByte();
                }
            }
            return bytes;
        }

        private void WriteBytes(ref byte[] res) {
            using (FileStream file = new FileStream(Output.Text, FileMode.Create)) {
                for (long i = 0; i < fileSize; i++) {
                    file.WriteByte(res[i]);
                }
            }
        }

        

        private void Button_Click_2(object sender, RoutedEventArgs e) {
           
            string path = Input.Text;
            byte[] bytes = ReadBytes(path);

            byte[] res;
            switch (Mode.Content) {
                case "Encrypt":
                    res = new MyBlowfish.MyBlowfish(Password.Text).Encrypt(bytes);
                    break;
                case "Dencrypt":
                    res = new MyBlowfish.MyBlowfish(Password.Text).Dencrypt(bytes);
                    break;
                default:
                    res = new byte[0];
                    break;
            }

            WriteBytes(ref res);
          
        }
    }
}
