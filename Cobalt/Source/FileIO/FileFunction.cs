﻿using System.IO;

namespace Cobalt.FileIO
{
    /*
     * 파일 관련 각종 기능
     */
    public static class FileFunction
    {
        public enum Status
        {
            Success, Exist
        }

        //미리 다운로드 설정
        public static Status ExportString(string content, string dir, string file)
        {
            if (!File.Exists(dir+file))
            {
                //디렉토리 생성
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                //파일 추출
                File.WriteAllText(dir+file, content);
                return Status.Success; //성공
            }
            return Status.Exist; //이미 파일이 존재함
        }
    }
}
