using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Interfaces
{
    public interface IHouseDescription
    {
        string Url { get; set; }
        HouseDescription GetHousesDescription { get; }
    }
}
