using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.ViewsModels
{
    public class HousesListViewModel
    {
        public IEnumerable<House> allHouses { get; set; }
    }
}
