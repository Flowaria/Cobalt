using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Cobalt.Src
{
    public class Downloader
    {
        private WebClient client;

        public bool Override = false;
        public string BaseURL; //다운로드시 중점 url
        public string BaseDirectory; //다운로드시 중점 저장 디렉토리

        public event EventHandler<DlStartedEventArgs> DownloadFileStarted = null;
        public event AsyncCompletedEventHandler DownloadFileCompleted = null;

        public Func<string, string> saveFormatFunction = null;

        //생성자 : 클라이언트 생성 / 설정
        public Downloader()
        {
            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
        }

        //Completed 핸들 변경시 불러올것
        public void HandlerChanged()
        {
            if (DownloadFileCompleted != null)
                client.DownloadFileCompleted += DownloadFileCompleted;
        }

        //URL 리스트로 다운로드
        public async Task downloadFiles(List<String> items)
        {
            foreach (String item in items)
            {
                await downloadFile(item);           
            }
        }

        //URL로 단일 파일 다운로드
        public async Task downloadFile(String item)
        {
            //파일 존재하면 걍 안받
            if (File.Exists(BaseDirectory + item) && !Override)
                return;

            //이름 수정
            String oItem = item;
            if (saveFormatFunction != null)
                item = saveFormatFunction(item);

            //다운로드 시작 핸들 전달
            OnDownloadStarted(new DlStartedEventArgs(BaseURL, BaseDirectory, item));

            //다운로드 시작
            await client.DownloadFileTaskAsync(new Uri(BaseURL + oItem), BaseDirectory + item);
        }

        //다운로드 시작시 핸들러
        protected virtual void OnDownloadStarted(DlStartedEventArgs e)
        {
            EventHandler<DlStartedEventArgs> handler = DownloadFileStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }

    //이벤트 Args
    public class DlStartedEventArgs : EventArgs
    {
        public String File { get; set; }
        public String Directory { get; set; }
        public String Url { get; set; }

        public DlStartedEventArgs(String Url, String Directory, String File)
        {
            this.Url = Url;
            this.Directory = Directory;
            this.File = File;
        }
    }
}
