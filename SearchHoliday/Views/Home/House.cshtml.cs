using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Views
{
    public class HouseModel : PageModel
    {
        private readonly ILogger<HouseModel> _logger;

        public HouseModel(ILogger<HouseModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
