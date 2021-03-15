using BIAwC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BIAwC.Data
{
    public class RssFeed
    {
        public async Task<List<NewsModel>> GetFeedsAsyc(string url)
        {
            CultureInfo culture = new CultureInfo("en-US");

            try
            {
                XDocument doc = XDocument.Load(url); //https://www.c-sharpcorner.com/rss/latestcontentall.aspx
                var entries = from item in doc.Root.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                              select new NewsModel
                              {
                                  Content = item.Elements().First(i => i.Name.LocalName == "description").Value,
                                  Link = item.Elements().First(i => i.Name.LocalName == "link").Value,
                                  PublishDate = Convert.ToDateTime(item.Elements().First(i => i.Name.LocalName == "pubDate").Value, culture),
                                  Title = item.Elements().First(i => i.Name.LocalName == "title").Value
                              };

                return await Task.FromResult(entries.OrderByDescending(o => o.PublishDate).ToList());
            }
            catch
            {
                List<NewsModel> feeds = new List<NewsModel>();
                NewsModel feed = new NewsModel();
                feeds.Add(feed);
                return await Task.FromResult(feeds);
            }
        }
    }
}
