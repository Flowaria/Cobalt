namespace Cobalt.Src
{
    public class Format
    {
        /*포멧
             * 입력 포멧
             * media.steampowered.com/apps/440/icons/w_bottle.859ddb315a2748f04bcc211aa7a04f2c926e6169.png
             * 출력 포멧
             * w_bottle.859ddb315a2748f04bcc211aa7a04f2c926e6169.png
        */
        //URL 처리 포멧
        public static string UrlFile(string str)
        {
            string[] splited = str.Split('/');
            return splited[splited.Length - 1];
        }

        /*포멧
             * 입력 포멧
             * w_bottle.859ddb315a2748f04bcc211aa7a04f2c926e6169.png
             * 출력 포멧
             * w_bottle.png
        */
        //파일이름 포멧
        public static string ItemImage(string str)
        {
            return str.Split('.')[0] + ".png";
        }
    }
}
