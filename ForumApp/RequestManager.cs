using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ForumApp
{
    public class RequestManager
    {

        public int GetForumComments(Uri source)
        {

            var req = (HttpWebRequest)WebRequest.Create(source);
            req.Host = "catalog.api.onliner.by";
            req.KeepAlive = true;
            req.Accept = "application/json, text/javascript, */*; q=0.01";
            req.Headers.Add("Origin: https://catalog.onliner.by");


            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using (StreamReader stream = new StreamReader(
                resp.GetResponseStream(), Encoding.UTF8))
            {
                var json = stream.ReadToEnd();
                var data = (JObject)JsonConvert.DeserializeObject(json);

                var products = data["products"].Value<JArray>();
                foreach (var product in products)
                {
                    var forumNode = product["forum"].Value<JObject>();
                    var forumRepliesCount = forumNode["replies_count"].Value<int>();
                }
            }
            return 0;
        }
    }

    public class Comments
    {
        [JsonProperty(PropertyName = "FooBar")]
        public double Count { get; set; }
    }
}