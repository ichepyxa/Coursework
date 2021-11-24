using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Leaf.xNet;
using Microsoft.AspNetCore.Html;
using SearchHoliday.Interfaces;
using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SearchHoliday.Implementations
{
    public class HousesDescription : IHouseDescription
    {
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public HouseDescription GetHousesDescription
        {
            get
            {
                var connection = new HttpRequest();
                try
                {
                    string response = connection.Get(Url).ToString();
                    string name = "";
                    string type = "";
                    string[] images = new string[3] { "", "", "" };
                    string[] priceForHouse = new string[2] { "", "" };
                    string[] features = { "" };
                    HtmlString description = new HtmlString("");
                    string priceForPerson = "";
                    string url = Url;

                    HtmlDocument hap = new HtmlDocument();
                    hap.LoadHtml(response);
                    HouseDescription HouseDescription = new HouseDescription();

                    foreach (var item in hap.DocumentNode.QuerySelectorAll("div.section-header__title-col"))
                        if (item.QuerySelector($"div:nth-child(1)") != null)
                        {
                            name = (hap.DocumentNode.QuerySelector($"div:nth-child(1)>h1").InnerText).TrimEnd();
                            if (name.IndexOf("&amp;") != -1)
                                name = name.Replace("&amp;", "&");
                            if (name.IndexOf("&quot;") != -1)
                                name = name.Replace("&quot;", "\"");

                            if (item.QuerySelector($"div:nth-child(1)>a") != null)
                                type = (item.QuerySelector($"div:nth-child(1)>a").InnerText).TrimEnd();
                        }

                    foreach (var item in hap.DocumentNode.QuerySelectorAll("section.top-apartment-card"))
                    {
                        if (item.QuerySelector($"div>div.top-apartment-card__photo") != null)
                        {
                            if (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__wrap-photo-big") != null)
                                if (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__wrap-photo-big>a>div>img") != null)
                                    images[0] = (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__wrap-photo-big>a>div>img").GetAttributeValue("src", null)).TrimEnd();

                            if (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__photo-mini") != null)
                                if (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__photo-mini>div.top-apartment-card__photo-mini-left>ul>li>a>img") != null)
                                {
                                    images[1] = (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__photo-mini>div.top-apartment-card__photo-mini-left>ul>li:nth-child(1)>a>img").GetAttributeValue("src", null)).TrimEnd();
                                    images[2] = (item.QuerySelector($"div>div.top-apartment-card__photo>div.top-apartment-card__photo-mini>div.top-apartment-card__photo-mini-left>ul>li:nth-child(2)>a>img").GetAttributeValue("src", null)).TrimEnd();
                                }
                        }

                        if ((images[0] == "") && (images[1] == "") && (images[2] == ""))
                            if (item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free") != null)
                            {
                                if (item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free>ul>li:nth-child(1)>a") != null)
                                    images[0] = item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free>ul>li:nth-child(1)>a").GetAttributeValue("href", null);

                                if (item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free>ul>li:nth-child(1)>a") != null)
                                    images[1] = item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free>ul>li:nth-child(2)>a").GetAttributeValue("href", null);

                                if (item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free>ul>li:nth-child(1)>a") != null)
                                    images[2] = item.QuerySelector($"div>div.inner-card-photos.js-fotorama-custom-style.photos-three-free>ul>li:nth-child(3)>a").GetAttributeValue("href", null);
                            }

                        for (int i = 0; i < 3; i++)
                        {
                            Regex regex = new Regex(@"(/[-_a-zA-Z0-9.]*)+");
                            Match match = regex.Match(images[i]);
                            images[i] = match.Value.Insert(0, "https://www.holiday.by");
                        }

                        if (item.QuerySelector($"div>div.top-apartment-card__info") != null)
                            if (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price") != null)
                            {
                                if (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__person") != null)
                                    priceForPerson = (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__person>span.card-info-price-top__price-p").InnerText).Trim();

                                if (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__house") != null)
                                {
                                    if (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__house>div>div:nth-child(1)>span.card-info-price-top__price-h") != null)
                                        priceForHouse[0] = (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__house>div>div:nth-child(1)>span.card-info-price-top__price-h").InnerText).Trim();
                                    if (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__house>div>div:nth-child(2)>span.card-info-price-top__price-h") != null)
                                        priceForHouse[1] = (item.QuerySelector($"div>div.top-apartment-card__info>div.card-info-price-top.card-info-top__price>div.card-info-price-top__house>div>div:nth-child(2)>span.card-info-price-top__price-h").InnerText).Trim();
                                }
                            }
                    }

                    if (hap.DocumentNode.QuerySelector($"div#content>div:nth-child(2)>div:nth-child(2)>div:nth-child(2)") != null)
                        description = new HtmlString((hap.DocumentNode.QuerySelector($"div#content>div:nth-child(2)>div:nth-child(2)>div:nth-child(2)>article>div").InnerHtml).Trim());

                    if (description.Value == "")
                        if (hap.DocumentNode.QuerySelector($"div.content-main-card>div:nth-child(2)>div:nth-child(2)") != null)
                            description = new HtmlString((hap.DocumentNode.QuerySelector($"div.content-main-card>div:nth-child(2)>div:nth-child(2)>article>div").InnerHtml).Trim());

                    HouseDescription.Name = name;
                    HouseDescription.Type = type;
                    HouseDescription.PricesForHouse = priceForHouse;
                    HouseDescription.PriceForPerson = priceForPerson;
                    HouseDescription.Description = description;
                    HouseDescription.Images = images;
                    HouseDescription.UrlHouse = url;
                    HouseDescription.UrlHouse = url.Insert(url.Length, "/photo#content");
                    HouseDescription.UrlHousePhotos = url;

                    return HouseDescription;
                }
                catch
                {
                    HouseDescription Error = new HouseDescription { Name = "Что-то пошло не так..." };
                    return Error;
                }
            }
        }
    }
}
