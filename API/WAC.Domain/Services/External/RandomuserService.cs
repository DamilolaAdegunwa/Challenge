using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WAC.Domain.Interfaces.Services;
using WAC.Domain.Model;

namespace WAC.Domain.Services
{
    public class RandomUserService : IRandomUserService
    {
        public RootObject Get()
        {
            using (System.Net.WebClient wb = new System.Net.WebClient())
            {
                // TODO sacar de ala config.
                // TODO : se podria usar wttpclient
                // TODO : HttpClient
                string url = "https://randomuser.me/api/?results=50";
                
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "GET";
                string responseStringJson = null ;
                using (HttpWebResponse response =  (HttpWebResponse)request.GetResponse())
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    responseStringJson = reader.ReadToEnd();
                    reader.Close();
                    dataStream.Close();
                    


                }  
                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(responseStringJson); 

                return results;




            }
        }



    }
}
