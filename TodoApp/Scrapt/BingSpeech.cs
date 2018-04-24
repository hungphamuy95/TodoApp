using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TodoApp.Scrapt
{
    public class BingSpeech
    {
        public IConfiguration Configuration;
        public BingSpeech(IConfiguration config)
        {
            Configuration = config;
        }
        public string CreateRequeset(string requestUri)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
            request.Headers["Ocp-Apim-Subscription-Key"] = Configuration["BingSpeechKey"];
            // Send audio file by 1024 byte chunks
            using (FileStream fs = new FileStream("", FileMode.Open, FileAccess.Read))
            {
                /*
                 * Open a request stream and write 1024 byte chunks in the stream one at a time.
                 */
                byte[] buffer = null;
                int bytesRead = 0;
                using (Stream requestStream = request.GetRequestStream())
                {
                    /*
                     * Read 1024 raw bytes from the input audio file.
                     */
                    buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
                    while ((bytesRead=fs.Read(buffer,0,buffer.Length))!=0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                    // Flush
                    requestStream.Flush();
                }
            }
            using(WebResponse response = request.GetResponse())
            {
                using(StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string responString = sr.ReadToEnd();
                    return responString;
                }
            }
        }
    }
}
