using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Models
{
    public class HouseAnswer
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionsId { get; set; }
    }
}
