using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Leaf.xNet;
using Fizzler;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SearchHoliday.Views;
using SearchHoliday.Models;
using SearchHoliday.Implementations;
using SearchHoliday.Interfaces;
using SearchHoliday.ViewsModels;

namespace SearchHoliday.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllHouses _allHouses;

        public HomeController(IAllHouses allHouses)
        {
            _allHouses = allHouses;
        }

        public ActionResult Index()
        {
            HousesListViewModel obj = new HousesListViewModel();
            obj.allHouses = _allHouses.Houses;

            return View(obj);
        }

        public ActionResult House()
        {
            return View();
        }
    }
}
