using Cobalt.MvM.DB;
using Cobalt.MvM.Items;
using Cobalt.Src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace Cobalt.Forms
{
    /// <summary>
    /// LoadingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {  
            InitializeComponent();
            e_label.Content = Properties.Settings.Default.LOAD_ITEM;
            //디폴트 텍스트로 바꾸기
        }

        int QuerryCountTotal;
        int QuerryCount;
        public async Task initItemsResource(ItemsDB db)
        {
            //난 초기화를 좋아하니까 ㅎ
            QuerryCountTotal = 0;
            QuerryCount = 0;

            if (!Directory.Exists(Properties.Settings.Default.PATH_IMG_ITEMS)) //이미지 디렉토리
                Directory.CreateDirectory(Properties.Settings.Default.PATH_IMG_ITEMS);

            List<String> FileList = new List<String>();
            foreach (TFItem item in db.querryAllItem())
            {
                if(item.ImageURL != null)
                {
                    string[] splited = item.ImageURL.Split('/');
                    string fileName = splited[splited.Length - 1];
                    if (!File.Exists(Properties.Settings.Default.PATH_IMG_ITEMS+fileName) && !FileList.Contains(fileName))
                        FileList.Add(fileName);
                    item.ImageURL = fileName;
                }    
            }

            //파일 리스트가 비어있지 않으면
            if(FileList.Count > 0)
            {
                //설정
                QuerryCountTotal = FileList.Count;
                e_bar.IsIndeterminate = false;

                //다운로더 생성
                Downloader dl = new Downloader();
                dl.BaseURL = "http://media.steampowered.com/apps/440/icons/";
                dl.BaseDirectory = Properties.Settings.Default.PATH_IMG_ITEMS;
                dl.DownloadFileStarted += c_DownloadFileStarted;
                dl.DownloadFileCompleted += c_DownloadFileCompleted;
                dl.HandlerChanged();
                dl.saveFormatFunction = formatName;
                await dl.downloadFiles(FileList);
            }
        }

        /*
         * 파일 다운로드 시작시 이벤트
         */
        void c_DownloadFileStarted(object sender, DlStartedEventArgs e)
        {
            // 이미지 다운로드 중 . . . : filename.png 형식으로 라벨 변경
            e_label.Content = String.Format("{0} : {1}", Properties.Settings.Default.LOAD_ITEM_IMAGE, e.File);
        }

        /*
         * 다운로드 완료시 이벤트
         */
        void c_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            QuerryCount++;
            double percentage = (double)QuerryCount / QuerryCountTotal * 100;
            e_bar.Value = percentage;
        }

        /*포멧
             * 입력 포멧
             * w_bottle.859ddb315a2748f04bcc211aa7a04f2c926e6169.png
             * 출력 포멧
             * w_bottle.png
        */
        //파일이름 포멧
        string formatName(string url)
        {
            return url.Split('.')[0]+".png";
        }

        public async Task initTemplateResource(TemplateDB db)
        {
            
        }
    }
}
