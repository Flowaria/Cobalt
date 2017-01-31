using Cobalt.Data;
using Cobalt.Enums;
using Cobalt.Parser;
using Cobalt.TFItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cobalt.Src.Parser
{
    public class InitParser
    {
        public ProgressBar Progress;
        public Label TextBox;

        int QuerryCountTotal;
        int QuerryCount;

        public async Task initSchema(SchemaParser db)
        {
            //난 초기화를 좋아하니까 ㅎ
            QuerryCountTotal = 0;
            QuerryCount = 0;

            if (!Directory.Exists(Properties.Settings.Default.PATH_IMG_ITEMS)) //이미지 디렉토리
                Directory.CreateDirectory(Properties.Settings.Default.PATH_IMG_ITEMS);

            List<String> FileList = new List<String>();
            foreach (TFItem item in ItemsData.Items)
            {
                if (item.ImageURL != null)
                {
                    string fileName = Format.UrlFile(item.ImageURL);
                    if (!File.Exists(Properties.Settings.Default.PATH_IMG_ITEMS + Format.ItemImage(fileName)) && !FileList.Contains(fileName))
                        FileList.Add(fileName);
                    item.ImageURL = Format.ItemImage(fileName);
                }
            }

            //파일 리스트가 비어있지 않으면
            if (FileList.Count > 0)
            {
                //설정
                QuerryCountTotal = FileList.Count;
                Progress.IsIndeterminate = false;

                //다운로더 생성
                Downloader dl = new Downloader();
                dl.BaseURL = "http://media.steampowered.com/apps/440/icons/";
                dl.BaseDirectory = Properties.Settings.Default.PATH_IMG_ITEMS;
                dl.DownloadFileStarted += c_DownloadFileStarted;
                dl.DownloadFileCompleted += c_DownloadFileCompleted;
                dl.Mode = DownloaderOverrideMode.Off;
                dl.saveFormatFunction = Format.ItemImage;
                dl.AutoDispose = true;
                await dl.downloadFiles(FileList);
            }
            return;
        }

        /*
        public bool isImageValid(string local, string web)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    Console.WriteLine("Valid Check!");
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    byte[] byte_web = client.DownloadData(web);
                    byte[] byte_local = File.ReadAllBytes(local);
                    if (byte_web.SequenceEqual(byte_local))
                    {
                        Console.WriteLine("Valid!");
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        */

        /*
         * 파일 다운로드 시작시 이벤트
         */
        void c_DownloadFileStarted(object sender, DownloaderEventArgs e)
        {
            // 이미지 다운로드 중 . . . : filename.png 형식으로 라벨 변경
            TextBox.Content = String.Format("{0} : {1}", Properties.Settings.Default.Load_Item_Image, e.File);
        }

        /*
         * 다운로드 완료시 이벤트
         */
        void c_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            QuerryCount++;
            double percentage = (double)QuerryCount / QuerryCountTotal * 100;
            Progress.Value = percentage;
        }

        /*
        public async Task initTemplate(TemplateParser db)
        {

        }
        */
    }
}
