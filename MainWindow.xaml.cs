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

        }

        /// <summary>
        /// Initiate download
        /// </summary>
        private void btn_download_Click(object sender, RoutedEventArgs e)
        {
            Download();
        }

        /// <summary>
        /// Get the download output file format/type/extension
        /// </summary>
        /// <returns>File extension/format - like .mp3</returns>
        private string GetOutputFileFormat()
        {
            // == true cuz nullable bool
            if (rbtn_downloadAsAudio.IsPressed == true)
            {
                return ".mp3";
            }
            else if (rbtn_downloadAsVideo.IsPressed == true)
            {
                return ".mp4";
            }
            // mp4 file format by default? kinda here if more file formats are added later...
            else
            {
                return ".mp4"; //mp4 by default?
                //throw new Exception("No file format chosen");
            }
        }
        
        /// <summary>
        /// For getting a selection - ie 480p, 720p, best, etc
        /// </summary>
        public string GetQualitySelection()
        {
            throw new NotImplementedException();
        }
        

        /// <summary>
        /// Trim every textbox from excess whitespace. Maybe remove bad characters too.
        /// </summary>
        public void SanitizeTextboxes()
        {
            txt_outputFileName.Text = txt_outputFileName.Text.Trim();
            txt_downloadURL.Text = txt_downloadURL.Text.Trim();
            txt_customArgs.Text = txt_customArgs.Text.Trim();
        }


        /// <summary>
        /// Invoke the youtube-dl.exe and pass it arguments from the GUI to download a video/audio file
        /// </summary>
        public void Download()
        {
            //24 hour time like 6-27-2016_09-33
            string downloadDateTime = DateTime.Now.ToString("M-d-yyyy_HH-mm");

            //download the target url as either best audio or best video
            string downloadedFileExt = GetOutputFileFormat();
            
            SanitizeTextboxes();
            
            //default for a download file name if no name is given
            if (String.IsNullOrEmpty(txt_outputFileName.Text))
            {
                txt_outputFileName.Text = "youtube-dl-download_" + downloadDateTime + downloadedFileExt;
            }

            //if custom args are given
            if (!String.IsNullOrEmpty(txt_customArgs.Text) && chkbx_customArgs.IsChecked == true)
            {
                string customArgs = " " + txt_downloadURL.Text + " " + txt_customArgs.Text;
                Process.Start(YouTubeDLFilePath, customArgs);
                txt_log.AppendText(downloadDateTime + ": " + txt_downloadURL.Text + " downloaded with custom args: " + Environment.NewLine + "\t" + txt_customArgs.Text);
                return;
            }

            string args = " " + txt_downloadURL.Text + " -o " + txt_outputPath.Text + "\\" + txt_outputFileName.Text + downloadedFileExt;

            //force audio download from youtube? check if soundcloud works fine
            //i think this is 128kbps quality too - investigate for higher quality (best)
            if (downloadedFileExt == ".mp3")// && txt_downloadURL.Text.Contains("*youtube.com*")
            {
                args += " --extract-audio --audio-format mp3 -l";
            }

            //shorter filename?
            //In some cases, you don't want special characters such as 中, spaces, or &, 
            //such as when transferring the downloaded filename to a Windows system
            args += " --restrict-filenames";

            Process.Start(YouTubeDLFilePath, args);

            txt_log.AppendText(downloadDateTime + ": " + txt_downloadURL.Text + " downloaded as: " + Environment.NewLine + "\t" + txt_outputFileName.Text);
            txt_log.AppendText(Environment.NewLine);

        }


        /// <summary>
        /// Toggle CustomArgs enabled if custom args is checked
        /// </summary>
        private void chkbx_customArgs_Click(object sender, RoutedEventArgs e)
        {
            txt_customArgs.IsEnabled = (chkbx_customArgs.IsChecked == true) ? true : false;
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
