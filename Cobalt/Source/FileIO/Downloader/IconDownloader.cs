using Cobalt.Data;
using Cobalt.TFItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cobalt.FileIO.DL
{
    /*
     * 아이템을 파싱해 읽어온 데이터를 이용해
     * 아이콘 파일을 다운로드 해오는 클래스
     */
    public class IconDownloader
    {
        public ProgressBar Progress;
        public Label TextBox;

        private int QuerryCountTotal;
        private int QuerryCount;

        public async Task download()
        {
            //난 초기화를 좋아하니까 ㅎ
            QuerryCountTotal = 0;
            QuerryCount = 0;

            if (!Directory.Exists(Properties.Settings.Default.PATH_IMG_ITEMS)) //이미지 디렉토리
                Directory.CreateDirectory(Properties.Settings.Default.PATH_IMG_ITEMS);

            List<String> FileList = new List<String>();
            foreach (TFItem item in TFItem.ItemList())
            {
                if (item.ImageURL != null)
                {
                    string fileName = Format.UrlFile(item.ImageURL);
                    if (fileName != null && !File.Exists(Properties.Settings.Default.PATH_IMG_ITEMS + Format.ItemImage(fileName)) && !FileList.Contains(fileName))
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
                using (Downloader dl = new Downloader())
                {
                    dl.BaseURL = "http://media.steampowered.com/apps/440/icons/";
                    dl.BaseDirectory = Properties.Settings.Default.PATH_IMG_ITEMS;
                    dl.DownloadFileStarted += c_DownloadFileStarted;
                    dl.DownloadFileCompleted += c_DownloadFileCompleted;
                    dl.Mode = DownloaderOverrideMode.Off;
                    dl.saveFormatFunction = Format.ItemImage;
                    await dl.downloadFiles(FileList);
                }
            }
            return;
        }

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
    }
}
