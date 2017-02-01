using Cobalt.Enums;
using Cobalt.MvM.Element;
using System.Collections.Generic;

namespace Cobalt.Data
{
    public static class TemplatesData
    {
        //템플릿 보관
        public static List<TFBot> Templates;

        //쿼리시 필터링
        public static TFClass filter_Class;
        public static string filter_ContainName;
        public static string filter_TemplateFile;
        public static bool filter_Gatebot;

        public static List<TFBot> querry()
        {
            return new List<TFBot>();
        }
    }
}
