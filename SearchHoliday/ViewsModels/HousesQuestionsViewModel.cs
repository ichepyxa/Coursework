using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.ViewsModels
{
    public class HousesQuestionsViewModel
    {
        public IEnumerable<HouseQuestion> HousesQuestions { get; set; }
        public IEnumerable<HouseAnswer> HousesAnswers { get; set; }
    }
}
