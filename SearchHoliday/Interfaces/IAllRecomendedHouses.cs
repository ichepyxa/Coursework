using SearchHoliday.Models;
using System.Collections.Generic;

namespace SearchHoliday.Interfaces
{
    public interface IAllRecomendedHouses
    {
        IEnumerable<House> GetHouses { get; }
    }
}
