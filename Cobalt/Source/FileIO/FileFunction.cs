using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;

namespace Cobalt.FileIO
{
    /*
     * 파일 관련 각종 기능
     */
    public static class FileFunction
    {
        public static string RelativeURL(string baseurl)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(AppDomain.CurrentDomain.BaseDirectory);
            builder.Append(baseurl.Replace('/','\\'));
            return builder.ToString();
        }

        public enum Status
        {
            Success, Exist
        }

        //미리 다운로드 설정
        public static Status ExportString(string content, string dir, string file)
        {
            StringBuilder builder = new StringBuilder(128);
            builder.AppendFormat("{0}{1}", dir, file);
            string filepath = builder.ToString();

            if (!File.Exists(filepath))
            {
                //디렉토리 생성
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                //파일 추출
                File.WriteAllText(filepath, content);
                return Status.Success; //성공
            }
            return Status.Exist; //이미 파일이 존재함
        }
    }
}
