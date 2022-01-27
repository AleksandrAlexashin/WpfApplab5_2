using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inkCanvans.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            inkCanvans.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "  Изображения (*.jpg) | *.jpg |Изображения (*.bmp)|*.bmp| Изображения(*.png) | *.png | Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
                image.EndInit();
                imgI.Source = image;

                
               
                
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Изображения (*.bmp)|*.bmp| Изображения(*.jpg) | *.JPG | Изображения(*.png) | *.png | Все файлы (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                
                string filename = dlg.FileName;
                
                int margin = (int)this.inkCanvans.Margin.Left;
                int width = (int)this.inkCanvans.ActualWidth - margin;
                int height = (int)this.inkCanvans.ActualHeight - margin;
              
                RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(inkCanvans);

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));
                    encoder.Save(fs);
                }

            }
        }
            

        

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            Application.Current.Shutdown();

        }

       
    }
}
