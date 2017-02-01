using System;

namespace Cobalt.MvM.Function
{
    public static class TFFunction
    {
        public static string RelayOutputFormat(string relay, string action)
        {
            if (relay != null && action != null)
            {
                return String.Format("%s:%s", relay, action);
            }
            return null;
        }
    }
}