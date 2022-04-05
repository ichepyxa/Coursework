using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Models
{
    public class HouseQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answers { get; set; }
        public string RightAnswer { get; set; }
        public bool IsError { get; set; }
    }
}
