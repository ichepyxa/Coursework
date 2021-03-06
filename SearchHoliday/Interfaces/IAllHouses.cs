using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Interfaces
{
    public interface IAllHouses
    {
        string Url { get; set; }
        IEnumerable<House> GetHouses { get; }
    }
}
