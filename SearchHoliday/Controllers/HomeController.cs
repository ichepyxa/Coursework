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
        private readonly IAllRecomendedHouses _allRecomendedHouses;
        private readonly IAllHouses _allHouses;

        public HomeController(IAllRecomendedHouses allRecomendedHouses, IAllHouses allHouses)
        {
            _allRecomendedHouses = allRecomendedHouses;
            _allHouses = allHouses;
        }

        public ActionResult Index()
        {
            RecomendedHousesListViewModel obj = new RecomendedHousesListViewModel();
            obj.AllRecomendedHouses = _allRecomendedHouses.Houses;

            return View(obj);
        }

        public ActionResult Houses(int num = 1)
        {
            HousesListViewModel obj = new HousesListViewModel();
            _allHouses.Url = "https://www.holiday.by/by/dom?p=" + num;
            obj.AllHouses = _allHouses.Houses;

            return View(obj);
        }
    }
}
