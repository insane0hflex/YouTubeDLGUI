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
        //public string YouTubeDLFilePath = Environment.CurrentDirectory + @"\youtube-dl.exe";
        public string YouTubeDLFilePath = @"C:\youtube-dl\youtube-dl.exe";
        
        //Might never be used... youtube-dl will look for ffmpeg in its current directory by default
        //public string FfmpegFilePath = Environment.CurrentDirectory + @"\ffmpeg.exe";
        
        public MainWindow()
        {
            InitializeComponent();

            txt_outputPath.Text = Environment.CurrentDirectory;

            //set defaults for download file output
            chk_downloadAsAudio.IsChecked = true;
            chk_downloadAsVideo.IsChecked = false;

        }

        /// <summary>
        /// 
        /// </summary>
        private void btn_download_Click(object sender, RoutedEventArgs e)
        {
            Download();
        }

        public void Download()
        {
            string downloadedFileExt = "";

            //24 hour time like 6-27-2016_09-33
            string downloadDate = DateTime.Now.ToString("M-d-yyyy_HH-mm");

            if (chk_downloadAsAudio.IsChecked == true)
            {
                downloadedFileExt = ".mp3";
            }
            else
            {
                downloadedFileExt = ".mp4";
            }

            if (String.IsNullOrEmpty(txt_outputFileName.Text))
            {
                txt_outputFileName.Text = "youtube-dl-download_" + downloadDate + downloadedFileExt;
            }

            if (!String.IsNullOrEmpty(txt_customArgs.Text))
            {
                Process.Start(YouTubeDLFilePath, txt_customArgs.Text);
                txt_log.AppendText(txt_downloadURL.Text + " downloaded with custom args: " + Environment.NewLine + "\t" + txt_customArgs.Text);
                return;
            }

            string args = " " + txt_downloadURL.Text.Trim() + " -o " + txt_outputPath.Text + "\\" + txt_outputFileName.Text + ".mp3";

            //shorter filename?
            //In some cases, you don't want special characters such as 中, spaces, or &, 
            //such as when transferring the downloaded filename to a Windows system
            args += " --restrict-filenames";

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
