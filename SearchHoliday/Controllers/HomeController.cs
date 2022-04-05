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
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Primitives;

namespace SearchHoliday.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllRecomendedHouses _allRecomendedHouses;
        private readonly IAllHouses _allHouses;
        private readonly IHouseDescription _houseDescription;
        private readonly IHousesQuestions _housesQuestions;
        private ApplicationContext _db = new ApplicationContext();

        public HomeController(IAllRecomendedHouses allRecomendedHouses, IAllHouses allHouses, IHouseDescription houseDescription, IHousesQuestions housesQuestions)
        {
            _allRecomendedHouses = allRecomendedHouses;
            _allHouses = allHouses;
            _houseDescription = houseDescription;
            _housesQuestions = housesQuestions;
        }

        public ActionResult Index()
        {
            RecomendedHousesListViewModel obj = new RecomendedHousesListViewModel();
            obj.AllRecomendedHouses = _allRecomendedHouses.GetHouses;

            ViewData["Title"] = "SearchHoliday | сайт для подбора мест отдыха";
            ViewData["MainTitle"] = "Рекомендованные места отдыха";
            ViewData["IsError"] = "";

            return View(obj);
        }

        public ActionResult Houses(int num = 1)
        {
            HousesListViewModel obj = new HousesListViewModel();
            _allHouses.Url = "https://www.holiday.by/by/dom?p=" + num;
            obj.AllHouses = _allHouses.GetHouses;

            ViewData["Title"] = "SearchHoliday | сайт для подбора мест отдыха";
            ViewData["MainTitle"] = "Места для отдыха";

            if (obj.AllHouses.First().IsError == true) ViewData["IsError"] = "Да";
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

        public ActionResult Test()
        {
            HousesQuestionsViewModel obj = new HousesQuestionsViewModel();
            obj.HousesQuestion = _housesQuestions.GetHousesQuestions;

            ViewData["Title"] = "SearchHoliday | сайт для подбора мест отдыха";
            ViewData["MainTitle"] = "Тест по поиску мест для отдыха";

            if (obj.HousesQuestion.First().IsError == true) ViewData["IsError"] = "Да";
            else ViewData["IsError"] = "";

            return View(obj);
        }

        [HttpPost]
        public ActionResult Login(IFormCollection collection)
        {
            try
            {
                StringValues email, password;
                if (collection.TryGetValue("email", out email) && collection.TryGetValue("password", out password))
                {
                    var user = _db.Users.Where(u => u.Login == email.ToString() && u.Password == password.ToString());
                    if (user.Count() != 0)
                    {
                        return RedirectToAction("Houses");
                    }
                    else
                    {
                        
                        return View();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            /*UsersViewModel obj = new UsersViewModel();
            obj.AllUsers = _db.Users.ToList();*/

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
