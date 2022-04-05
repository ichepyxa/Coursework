using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Interfaces
{
    public interface IHousesQuestions
    {
        string Url { get; set; }
        IEnumerable<HouseQuestion> GetHousesQuestions { get; }
    }
}
