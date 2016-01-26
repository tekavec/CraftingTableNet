using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace WebApiSelfHost.Tests
{
    public static class JsonExtensions
    {

        public static Task<dynamic> ReadAsJsonAsync(this HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            return content.ReadAsStringAsync().ContinueWith(t => JsonConvert.DeserializeObject(t.Result));
        }

        public static Task<XDocument> ReadAsXmlAsync(this HttpContent content)
        {
            if (content == null)
            {
                throw new ArgumentNullException();
            }
            return content.ReadAsStringAsync().ContinueWith(t => XDocument.Parse(t.Result));
        }
        public static dynamic ToJObject<T>(this T obj)
        {
            dynamic serialized = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject(serialized);
        }

    }
}