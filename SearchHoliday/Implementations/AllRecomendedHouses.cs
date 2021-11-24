using System;
using System.Collections.Generic;
using System.Linq;
using Leaf.xNet;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Threading.Tasks;
using SearchHoliday.Interfaces;
using SearchHoliday.Models;
using System.Text.RegularExpressions;

namespace SearchHoliday.Implementations
{
    public class AllRecomendedHouses : IAllRecomendedHouses
    {   
        public IEnumerable<House> GetHouses
        {
            get
            {
                var connection = new HttpRequest();
                try
                {
                    int i = 0;
                    string response = connection.Get("https://www.holiday.by/by").ToString();

                    HtmlDocument hap = new HtmlDocument();
                    hap.LoadHtml(response);
                    List<House> RecomendedHouses = new List<House>();

                    foreach (var item in hap.DocumentNode.QuerySelectorAll("div.main-section__inner"))
                        if (i <= 0)
                            for (int k = 0; k < 10; k++)
                                if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div") != null)
                                {
                                    string name = "";
                                    string image = "";
                                    string type = "";
                                    string price = "";
                                    string url = "";

                                    if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>h3") != null)
                                    {
                                        name = (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>h3").InnerText).TrimEnd();
                                        if (name.IndexOf("&amp;") != -1)
                                            name = name.Replace("&amp;", "&");
                                        if (name.IndexOf("&quot;") != -1)
                                            name = name.Replace("&quot;", "\"");
                                    }

                                    if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a") != null)
                                        image = (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a").GetAttributeValue("style", null)).TrimEnd();

                                    Regex regex = new Regex(@"(/[-_a-zA-Z0-9.]*)+");
                                    Match match = regex.Match(image);
                                    image = match.Value.Insert(0, "https://www.holiday.by");

                                    if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>span") != null)
                                        type = (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>span").InnerText).TrimEnd();

                                    if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>div>div:nth-child(1)>span") != null)
                                        price = (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>div>div:nth-child(1)>span").InnerText).TrimEnd();

                                    if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a") != null)
                                        url = (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a").GetAttributeValue("href", null)).TrimEnd();
                                    url = url.Insert(0, "https://www.holiday.by");

                                    RecomendedHouses.Add(new House
                                    {
                                        Id = k,
                                        Name = name,
                                        Type = type,
                                        Price = price,
                                        Image = image,
                                        UrlHouse = url,
                                        UrlHousePhotos = url.Insert(url.Length, "/photo#content")
                                    });
                                    i++;
                                }

                    return RecomendedHouses;
                }
                catch
                {
                    List<House> Error = new List<House> { new House { Name = "Что-то пошло не так..." } };
                    return Error;
                }
            }
        
        }
    }
}
