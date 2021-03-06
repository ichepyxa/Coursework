using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Leaf.xNet;
using SearchHoliday.Interfaces;
using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchHoliday.Implementations
{
    public class AllHouses : IAllHouses
    {
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public IEnumerable<House> GetHouses
        { 
            get
            {
                var connection = new HttpRequest();
                try
                {
                    string response = connection.Get(Url).ToString();

                    HtmlDocument hap = new HtmlDocument();
                    hap.LoadHtml(response);
                    List<House> Houses = new List<House>();

                    foreach (var item in hap.DocumentNode.QuerySelectorAll("div.cottages-list-cols"))
                    {
                        for (int k = 0; k < 40; k++)
                        {
                            if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)") != null)
                            {
                                if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})").GetAttributeValue("class", null) == "tours-list-col__wrap-line-card")
                                {
                                    string name = "";
                                    string image = "";
                                    string type = "";
                                    string price = "";
                                    string url = "";

                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>div:nth-child(1)>div>h3>a") != null)
                                    {
                                        name = (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>div:nth-child(1)>div>h3>a").InnerText).TrimEnd();
                                        if (name.IndexOf("&amp;") != -1)
                                            name = name.Replace("&amp;", "&");
                                        if (name.IndexOf("&quot;") != -1)
                                            name = name.Replace("&quot;", "\"");
                                    }

                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(1)>div>div:nth-child(1)>a") != null)
                                        image = (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(1)>div>div:nth-child(1)>a").GetAttributeValue("style", null)).TrimEnd();
                                    else if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(1)>div>span") != null)
                                        image = (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(1)>div>span").GetAttributeValue("style", null)).TrimEnd();

                                    Regex regex = new Regex(@"(/[-_a-zA-Z0-9.]*)+");
                                    Match match = regex.Match(image);
                                    image = match.Value.Insert(0, "https://www.holiday.by");

                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>div:nth-child(1)>div>span") != null)
                                        type = (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>div:nth-child(1)>div>span").InnerText).TrimEnd();

                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(1)") != null)
                                        price += item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(1)").InnerText;
                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(2)") != null)
                                        price += item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(2)").InnerText;
                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(3)") != null)
                                        price += item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(3)").InnerText;
                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(4)") != null)
                                        price += " " + item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)>div:nth-child(2)>div:nth-child(2)>div:nth-child(1)>div:nth-child(1)>span>span:nth-child(4)").InnerText;
                                    price.Trim();

                                    if (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})") != null)
                                        url = (item.QuerySelector($"div:nth-child(2)>div:nth-child(2)>div:nth-child({k + 1})>div:nth-child(1)").GetAttributeValue("data-href", null)).TrimEnd();
                                    url = url.Insert(0, "https://www.holiday.by");

                                    Houses.Add(new House
                                    {
                                        Id = k,
                                        Name = name,
                                        Type = type,
                                        Price = price,
                                        Image = image,
                                        UrlHouse = url,
                                        UrlHousePhotos = url.Insert(url.Length, "/photo#content")
                                    });
                                }
                            }
                        }
                    }

                    return Houses;
                }
                catch
                {
                    return new List<House> { new House { IsError = true } };
                }
            }
        }
    }
}
