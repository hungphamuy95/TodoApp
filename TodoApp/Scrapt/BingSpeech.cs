using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;

namespace TodoApp.Scrapt
{
    public class BingSpeech
    {
        public void CreateRequeset(string requestUri)
        {
            HttpWebRequest request = null;
            request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
            request.Headers["Ocp-Apim-Subscription-Key"] = "";
            // Send audio file by 1024 byte chunks
            using (FileStream fs = new FileStream("", FileMode.Open, FileAccess.Read))
            {
                /*
                 * Open a request stream and write 1024 byte chunks in the stream one at a time.
                 */

            }
        }
    }
}
