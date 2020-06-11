using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Trio
{
    class HTTPRequest  // HTTP连接工具类
    {
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }

        public static void SetHeaderValues(HttpWebRequest request,string token)
        {
            SetHeaderValue(request.Headers, "Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            SetHeaderValue(request.Headers, "User-Agent", "doSingle/11 CFNetwork/893.14.2 Darwin/17.3.0");
            SetHeaderValue(request.Headers, "Connection", "Keep-Alive");
            SetHeaderValue(request.Headers, "token", token);
        }

        public static JObject HttpGetRequest(string url, string token, int timeout = 10000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            SetHeaderValues(request, token);
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            request.Timeout = timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));

            return JObject.Parse(streamReader.ReadToEnd());
        }

        public static JObject HttpPostRequest(string url, string token, byte[] data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            SetHeaderValues(request, token);
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
            request.ContentLength = data.Length;
            request.GetRequestStream().Write(data, 0, data.Length);
            request.Timeout = 5000;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8"));

            return JObject.Parse(streamReader.ReadToEnd());
        }

        public static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }
    }
}
