using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchHoliday.Models;

namespace SearchHoliday.Interfaces
{
    public interface IAllHouses
    {
        IEnumerable<House> Houses { get; }
        House GetHouseById(int id);
    }
}
