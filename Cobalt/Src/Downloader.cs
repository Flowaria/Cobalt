using Cobalt.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;

namespace Cobalt.Src
{
    public class Downloader
    {
        private WebClient client;
        private MD5CryptoServiceProvider md5;

        public DownloaderOverrideMode Mode = DownloaderOverrideMode.Off;
        public string BaseURL; //다운로드시 중점 url
        public string BaseDirectory; //다운로드시 중점 저장 디렉토리

        public Func<string, string> saveFormatFunction = null;
        public Func<string, string, bool> validateFunction = null;

        public bool AutoDispose = false;

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
            md5 = new MD5CryptoServiceProvider();
        }

        //URL 리스트로 다운로드
        public async Task downloadFiles(List<String> items)
        {
            foreach (String item in items)
                await downloadFile(item);

            if(AutoDispose)
                client.Dispose();
        }

        //URL로 단일 파일 다운로드
        private async Task downloadFile(String item)
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
            OnDownloadStarted(new DownloaderEventArgs(BaseURL, BaseDirectory, item));

            //다운로드 시작
            try
            {
                await client.DownloadFileTaskAsync(new Uri(BaseURL + oItem), BaseDirectory + item);
            }
            catch
            {
                OnDownloadError(new DownloaderEventArgs(BaseURL, BaseDirectory, item));
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

        public DownloaderEventArgs(String Url, String Directory, String File)
        {
            this.Url = Url;
            this.Directory = Directory;
            this.File = File;
        }
    }
}
