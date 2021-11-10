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
    public class AllHouses : IAllHouses
    {   
        public IEnumerable<House> Houses 
        {
            get
            {
                int i = 0;
                string[] names = new string[10];
                string[] category = new string[10];
                string[] price = new string[10];
                string[] images = new string[10];

                var connection = new Leaf.xNet.HttpRequest();
                string response = connection.Get("https://www.holiday.by/by").ToString();

                HtmlDocument hap = new HtmlDocument();
                hap.LoadHtml(response);

                foreach (var item in hap.DocumentNode.QuerySelectorAll("div.main-section__inner"))
                    if (i <= 0)
                    {
                        for (int k = 0; k < 10; k++)
                            if (item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div") != null)
                            {
                                names[i] = item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>h3").InnerText;
                                category[i] = item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>span").InnerText;
                                price[i] = item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a>div>div>div:nth-child(1)>span").InnerText;
                                images[i] = item.QuerySelector($"section>div.section-grids__wrap-grids>div>div:nth-child({k + 1})>a").GetAttributeValue("style", null);
                                i++;
                            }
                    }

                for (int k = 0; k < i; k++)
                {
                    names[k] = names[k].TrimEnd();
                    if (names[k].IndexOf("&amp;") != -1)
                        names[k] = names[k].Replace("&amp;", "&");
                    category[k] = category[k].TrimEnd();
                    price[k] = price[k].TrimEnd();
                    images[k] = images[k].TrimEnd();
                    Regex regex = new Regex(@"(/[-_a-zA-Z0-9.]*)+");
                    Match match = regex.Match(images[k]);
                    images[k] = match.Value.Insert(0, "https://www.holiday.by");
                }

                List<House> HouseRepository = new List<House>();

                for (int k = 0; k < i; k++)
                    HouseRepository.Add(new House { name = names[k], type = category[k], price = price[k], image = images[k] });

                return HouseRepository;
            }
        
        }

        public House GetHouseById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
