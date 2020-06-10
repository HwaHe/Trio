using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;


namespace Trio
{
    class GetNews
    {
        private WebClient client = new WebClient();
        public List<string> newsTitle = new List<string>();
        public Dictionary<string, string> newsUrl = new Dictionary<string, string>();

        public GetNews()
        {
            client.Encoding = Encoding.UTF8;
        }
        private HtmlDocument getHtml(string url)   //下载html
        {
            HtmlDocument pagedoc = new HtmlDocument();
            try
            {
                string pagehtml = client.DownloadString(url);
                pagedoc.LoadHtml(pagehtml);
            }
            catch (Exception e)
            {
                pagedoc = null;
            }
            return pagedoc;
        }
        public void getWhuTitle()  //武汉大学官网
        {
            string url = @"http://www.whu.edu.cn/";
            HtmlDocument pagedoc = getHtml(url);
            string newsxpath = @"//div[@class = 'col-sm-4']//li/a";
            var nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            for (int i = 0; i != 8; ++i)  //只有前8个是新闻
            {
                var node = nodes[i];
                string title = node.InnerText;
                newsTitle.Add(title);
                string href=node.GetAttributeValue("href", "");  //下一级的链接
                newsUrl.Add(title, href);
            }
        }
        public void getBkTitle()  //武汉大学本科生院官网
        {
            string url = @"http://ugs.whu.edu.cn/";
            HtmlDocument pagedoc = getHtml(url);
            string newsxpath = @"//div[@class = 'news']//li/a";
            var nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)
            {
                string title = node.InnerText;
                newsTitle.Add(title);
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                newsUrl.Add(title, href);
            }
        }
        public void getCsTitle()  //武汉大学计算机学院官网
        {
            string url = @"http://cs.whu.edu.cn";
            HtmlDocument pagedoc = getHtml(url);
            string newsxpath = @"//ul[@class = 'txt-list']//li/a";
            var nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)
            {

                string title = node.InnerText;
                newsTitle.Add(title);
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                newsUrl.Add(title, href);
            }
        }

    }
}
