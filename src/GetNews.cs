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
        
        public List<(string, string)> whuNewsTitle = new List<(string, string)>();
        public Dictionary<string, string> whuNewsUrl = new Dictionary<string, string>();
        public List<(string, string)> bkNewsTitle = new List<(string, string)>();
        public Dictionary<string, string> bkNewsUrl = new Dictionary<string, string>();
        public List<(string, string)> csNewsTitle = new List<(string, string)>();
        public Dictionary<string, string> csNewsUrl = new Dictionary<string, string>();

        public GetNews()
        {
            client.Encoding = Encoding.UTF8;
            getWhuTitle();
            getBkTitle();
            getCsTitle();
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
            for (int i = 0; i != 8; ++i)  //新闻类
            {
                var node = nodes[i];
                string title = node.InnerText;
                whuNewsTitle.Add((title, "新闻"));
                string href=node.GetAttributeValue("href", "");  //下一级的链接
                whuNewsUrl.Add(title, href);
            }
            for (int i = 8; i != 16; ++i)  //学术类
            {
                var node = nodes[i];
                string title = node.InnerText;
                whuNewsTitle.Add((title, "学术"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                whuNewsUrl.Add(title, href);
            }
            for (int i = 16; i != 24; ++i)   //通知类
            {
                var node = nodes[i];
                string title = node.InnerText;
                whuNewsTitle.Add((title, "通知"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                whuNewsUrl.Add(title, href);
            }
        }

        public void getBkTitle()  //武汉大学本科生院官网
        {
            string url = @"http://ugs.whu.edu.cn/";
            HtmlDocument pagedoc = getHtml(url);
            string newsxpath = @"//div[@class = 'news']//li/a";
            var nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)  //新闻类
            {
                string title = node.InnerText;
                bkNewsTitle.Add((title, "新闻"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                bkNewsUrl.Add(title, href);
            }

            newsxpath = @"//div[@class = 'notice']//li/a";
            nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)   //通知类
            {
                string title = node.InnerText;
                bkNewsTitle.Add((title, "通知"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                bkNewsUrl.Add(title, href);
            }

            //没有学术类的
        }

        public void getCsTitle()  //武汉大学计算机学院官网
        {
            string url = @"http://cs.whu.edu.cn";
            HtmlDocument pagedoc = getHtml(url);
            string newsxpath = @"//ul[@class = 'txt-list']//li/a";
            var nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)  //新闻类
            {
                string title = node.InnerText;
                csNewsTitle.Add((title, "新闻"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                csNewsUrl.Add(title, href);
            }

            newsxpath = @"//div[@class = 'talks-list-wrap clearfix']//a";
            nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)  //学术类
            {
                string title = node.GetAttributeValue("title", "");
                csNewsTitle.Add((title, "学术"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                csNewsUrl.Add(title, href);
            }

            newsxpath = @"//ul[@class = 'list-wrap']//a";
            nodes = pagedoc.DocumentNode.SelectNodes(newsxpath);
            foreach (var node in nodes)  //通知类
            {
                string title = node.GetAttributeValue("title", "");
                csNewsTitle.Add((title, "通知"));
                string href = node.GetAttributeValue("href", "");  //下一级的链接
                href = url + href;
                csNewsUrl.Add(title, href);
            }
        }

    }
}
