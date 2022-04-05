using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string UrlHouse { get; set; }
        public string UrlHousePhotos { get; set; }
        public bool IsError { get; set; }
    }
}
