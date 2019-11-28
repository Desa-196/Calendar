using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static class SenderSMS
    {
        static WebRequest request;
        static WebResponse response;
        static SenderSMS() { 
            
        }
        public static int CheckLoginPassword(string login, string password) 
        {

            string line;

            request = WebRequest.Create("https://sms.ru/auth/check?login="+login+"&password="+password+"&json=0");

            try
            {
                response = request.GetResponse();


                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        line = reader.ReadLine();

                    }

                }
                response.Close();
            }
            catch
            {
                return 0;
            }


            if (line == "100")
            {
                return 1;
            }
            else if (line == "301")
            {
                return 2;
            }
            else 
            {
                return 0;
            }
 

            
        }

           
    }
}
