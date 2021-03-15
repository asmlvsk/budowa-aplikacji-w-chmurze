using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BIAwC.Models
{
    public class NewsModel
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }

        public NewsModel()
        {
            Link = string.Empty;
            Title = string.Empty;
            Content = string.Empty;
            PublishDate = DateTime.Now;
        }
    }
}
