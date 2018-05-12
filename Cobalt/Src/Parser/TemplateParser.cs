using Cobalt.FileIO;
using Cobalt.Properties;
using System.IO;
using System.Threading.Tasks;

namespace Cobalt.Parser
{
    public class TemplateParser
    {

        public async Task parse()
        {
            string[] files = Directory.GetFiles(Properties.Settings.Default.PATH_BASE);
            foreach (string file in files)
            {
                if (!file.EndsWith(".pop"))
                    continue;

                await parseFile(getFilename(file));
            }
        }

        //파일 추가
        public async Task parseFile(string name)
        {
            //TFTemplate.AddTemplate(new TFTemplate("robot_click.pop", false));
            if(true)
                return;

        }

        public async Task Parse(string Content)
        {
            /*
            StringBuilder builder = new StringBuilder();
            int step = 0;
            foreach (string[] e in ParseAsList(Content))
            {
                for (int i = 0; i < e.Length; i++)
                    builder.Append(e[i] + " ");
                builder.AppendLine();
            }
            Console.WriteLine(builder.ToString());
            */
        }

        private string getFilename(string url)
        {
            string[] splited = url.Split('/');
            return splited[splited.Length - 1];
        }
    }
}
