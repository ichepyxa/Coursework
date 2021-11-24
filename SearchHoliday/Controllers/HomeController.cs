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
        private readonly IHouseDescription _houseDescription;

        public HomeController(IAllRecomendedHouses allRecomendedHouses, IAllHouses allHouses, IHouseDescription houseDescription)
        {
            _allRecomendedHouses = allRecomendedHouses;
            _allHouses = allHouses;
            _houseDescription = houseDescription;
        }

        public ActionResult Index()
        {
            RecomendedHousesListViewModel obj = new RecomendedHousesListViewModel();
            obj.AllRecomendedHouses = _allRecomendedHouses.GetHouses;
            ViewData["Title"] = "SearchHoliday - сайт для подбора мест отдыха";
            ViewData["MainTitle"] = "Рекомендованные места отдыха";
            ViewData["IsError"] = "";

            return View(obj);
        }

        public ActionResult Houses(int num = 1)
        {
            HousesListViewModel obj = new HousesListViewModel();
            _allHouses.Url = "https://www.holiday.by/by/dom?p=" + num;
            obj.AllHouses = _allHouses.GetHouses;
            ViewData["Title"] = "SearchHoliday - сайт для подбора мест отдыха";
            ViewData["MainTitle"] = "Места для отдыха";
            if (obj.AllHouses.First().Name == "Что-то пошло не так...") ViewData["IsError"] = "Да";
            else ViewData["IsError"] = "Нет";

            return View(obj);
        }

        public ActionResult Description(string url)
        {
            HouseDescriptionViewModel obj = new HouseDescriptionViewModel();
            _houseDescription.Url = url;
            obj.HouseDescription = _houseDescription.GetHousesDescription;
            ViewData["Title"] = obj.HouseDescription.Name;
            ViewData["MainTitle"] = obj.HouseDescription.Name;
            ViewData["IsError"] = "";

            return View(obj);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
