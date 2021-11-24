using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Models
{
    public class HouseDescription
    {
        public string Name { get; set; }
        public string[] Images { get; set; }
        public string Type { get; set; }
        public string PriceForPerson { get; set; }
        public string[] PricesForHouse { get; set; }
        public string[] Features { get; set; }
        public HtmlString Description { get; set; }
        public string UrlHouse { get; set; }
        public string UrlHousePhotos { get; set; }
    }
}
