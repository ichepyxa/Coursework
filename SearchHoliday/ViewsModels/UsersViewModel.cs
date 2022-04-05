using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.ViewsModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> AllUsers { get; set; }
    }
}
