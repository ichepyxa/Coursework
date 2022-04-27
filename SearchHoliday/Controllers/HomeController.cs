using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SearchHoliday.Models;
using SearchHoliday.Interfaces;
using SearchHoliday.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using System;

namespace SearchHoliday.Controllers
{
    //[Route("home")]
    public class HomeController : Controller
    {
        private readonly IAllRecomendedHouses _allRecomendedHouses;
        private readonly IAllHouses _allHouses;
        private readonly IHouseDescription _houseDescription;
        private readonly IHousesQuestions _housesQuestions;
        private ApplicationContext _db;

        public HomeController(IAllRecomendedHouses allRecomendedHouses, IAllHouses allHouses, IHouseDescription houseDescription, IHousesQuestions housesQuestions, ApplicationContext db)
        {
            _allRecomendedHouses = allRecomendedHouses;
            _allHouses = allHouses;
            _houseDescription = houseDescription;
            _housesQuestions = housesQuestions;
            _db = db;
        }

        /*[Route("home")]
        [Route("")]
        [Route("~/")]*/
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

        [Authorize]
        public List<HouseAnswer> GetAnswers()
        {
            List<HouseAnswer> obj = new List<HouseAnswer>();
            obj =
            (
                from answer in _db.HousesAnswers
                select new HouseAnswer
                {
                    Id = answer.Id,
                    AnswerText = answer.AnswerText,
                    QuestionsId = answer.QuestionsId
                }
            ).ToList();

            return obj;
        }

        [Authorize]
        public ActionResult Test()
        {
            Console.WriteLine(User.Identity.Name);
            Console.WriteLine(User.Identity.AuthenticationType);
            HousesQuestionsViewModel obj = new HousesQuestionsViewModel();
            obj.HousesQuestions = 
            (
                from question in _db.HousesQuestions
                select new HouseQuestion
                {
                    Id = question.Id,
                    Title = question.Title,
                    Question = question.Question,
                    RightAnswer = question.RightAnswer,
                    TypeQuestion = question.TypeQuestion,
                    IsError = question.IsError
                }
            ).ToList();

            obj.HousesAnswers =
            (
                from answer in _db.HousesAnswers
                select new HouseAnswer
                {
                    Id = answer.Id,
                    AnswerText = answer.AnswerText,
                    QuestionsId = answer.QuestionsId
                }
            ).ToList();

            ViewData["Title"] = "SearchHoliday | сайт для подбора мест отдыха";
            ViewData["MainTitle"] = "Тест по поиску мест для отдыха";

            //if (obj.HousesQuestions.First().IsError == true) ViewData["IsError"] = "Да";
            //else ViewData["IsError"] = "";
            ViewData["IsError"] = "";

            return View(obj);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}