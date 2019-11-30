using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static class Encription
    {
        private static string Key = "key";

        public static string EncryptOrDecrypt(string text)
        {
            string result = "";

            if (text == "") return "";

            for (int i=0,j=0; i < text.Length; i++,j++)
            {
                if( j >= Key.Length ) j = 0;

                result += (char)(text[i] ^ Key[j]);
            }
            //System.Windows.MessageBox.Show("Входная строка: "+text+"\n"+result);
            return result;
        }
    }
}
