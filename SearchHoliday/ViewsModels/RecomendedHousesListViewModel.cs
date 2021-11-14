using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.ViewsModels
{
    public class RecomendedHousesListViewModel
    {
        public IEnumerable<House> AllRecomendedHouses { get; set; }
    }
}
