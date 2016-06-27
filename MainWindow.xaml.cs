using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace YouTubeDLGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string YouTubeDLFilePath = @"C:\youtube-dl\youtube-dl.exe";
        //public string YouTubeDLFilePath = Environment.CurrentDirectory + @"\youtube-dl.exe";
        
        //Might never be used... youtube-dl will look for ffmpeg in its current directory by default
        //public string FfmpegFilePath = Environment.CurrentDirectory + @"\ffmpeg.exe";
        
        public MainWindow()
        {
            txt_outputPath.Text = Environment.CurrentDirectory;

            InitializeComponent();
        }



        private void btn_download_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrEmpty(txt_outputFileName.Text))
            {
                txt_outputFileName.Text = "youtube-dl-download_" + DateTime.Today.ToFileTime() + ".mp3";
            }

            string args = " " + txt_downloadURL.Text.Trim() + " -o " + txt_outputPath.Text + "\\" + txt_outputFileName.Text + ".mp3";

            Process.Start(YouTubeDLFilePath, args);

            txt_log.AppendText(txt_downloadURL.Text + " downloaded as: " + Environment.NewLine + "\t" + txt_outputFileName.Text);
            txt_log.AppendText(Environment.NewLine);
        }




        /// <summary>
        /// Dunno if ever needed
        /// </summary>
        //public void TerminateYouTubeDLProcesses()
        //{
        //    var youtubeDLProcesses = Process.GetProcessesByName("youtube-dl");

        //    foreach (var process in youtubeDLProcesses)
        //    {
        //        //process.Kill();
        //    }
        //}

    }
}
