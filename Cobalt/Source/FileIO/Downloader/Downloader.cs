using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Cobalt.FileIO.DL
{
    //DOWNLOADER
    public enum DownloaderOverrideMode
    {
        All, WhenNotEqual, Off
    };

    /*
     * 파일 리스트의 파일을 비동기로 받아오는 클래스
     */
    public class Downloader : IDisposable
    {
        private WebClient client;

        public DownloaderOverrideMode Mode = DownloaderOverrideMode.Off;
        public string BaseURL; //다운로드시 중점 url
        public string BaseDirectory; //다운로드시 중점 저장 디렉토리

        public Func<string, string> saveFormatFunction = null;
        public Func<string, string, bool> validateFunction = null;

        public event EventHandler<DownloaderEventArgs> DownloadFileStarted = null;
        public event EventHandler<DownloaderEventArgs> DownloadFileError = null;
        private event AsyncCompletedEventHandler m_DownloadFileCompleted = null;
        public AsyncCompletedEventHandler DownloadFileCompleted
        {
            get{return m_DownloadFileCompleted;}
            set
            {
                if(value != null)
                {
                    client.DownloadFileCompleted += value;
                }
            }
        }

        //생성자 : 클라이언트 생성 / 설정
        public Downloader()
        {
            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
        }

        //URL 리스트로 다운로드
        public async Task downloadFiles(List<String> items)
        {
            int Max = items.Count;
            for (int i=0; i<Max; i++)
                await downloadFile(items[i], Max, i);
        }

        //URL로 단일 파일 다운로드
        private async Task downloadFile(String item, int Max, int Index)
        {
            //파일 존재하면 걍 안받
            if (File.Exists(BaseDirectory + item))
            {
                if(Mode == DownloaderOverrideMode.Off)
                    return;
                else if (Mode == DownloaderOverrideMode.WhenNotEqual)
                {
                    if(validateFunction(BaseDirectory + item, BaseURL + item))
                        return;
                }
            }

            //이름 수정
            String oItem = item;
            if (saveFormatFunction != null)
                item = saveFormatFunction(item);

            //다운로드 시작 핸들 전달
            OnDownloadStarted(new DownloaderEventArgs(Max, Index, BaseURL, BaseDirectory, item));

            //다운로드 시작
            try
            {
                await client.DownloadFileTaskAsync(new Uri(BaseURL + oItem), BaseDirectory + item);
            }
            catch
            {
                OnDownloadError(new DownloaderEventArgs(Max, Index, BaseURL, BaseDirectory, item));
                return;
            }
        }

        //다운로드 시작시 핸들러
        protected virtual void OnDownloadStarted(DownloaderEventArgs e)
        {
            EventHandler<DownloaderEventArgs> handler = DownloadFileStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        //다운로드 에러시 핸들러
        protected virtual void OnDownloadError(DownloaderEventArgs e)
        {
            EventHandler<DownloaderEventArgs> handler = DownloadFileError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        //접근 가능한 URL 인지 확인
        public static bool CheckValidURL(string url)
        {
            try
            {
                HttpWebRequest querry = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)querry.GetResponse();
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }

    //이벤트 Args
    public class DownloaderEventArgs : EventArgs
    {
        public String File { get; set; }
        public String Directory { get; set; }
        public String Url { get; set; }
        public int Total { get; set; }
        public int Current { get; set; }

        public DownloaderEventArgs(int Total, int Current, String Url, String Directory, String File)
        {
            this.Url = Url;
            this.Directory = Directory;
            this.File = File;
            this.Total = Total;
            this.Current = Current;
        }
    }
}
