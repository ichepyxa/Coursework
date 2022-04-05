using SearchHoliday.Interfaces;
using SearchHoliday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchHoliday.Implementations
{
    public class HousesQuestions : IHousesQuestions
    {
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public IEnumerable<HouseQuestion> GetHousesQuestions
        {
            get
            {
                try
                {
                    List<HouseQuestion> housesQuestions = new List<HouseQuestion>();

                    /*housesQuestions.Add(new HouseQuestion() { Id = 1, Title = "Вопрос №1", Question = "Выберите подходящий вам пункт:", Answers = new string[] { "Нет", "50/50", "Возможно", "Да" }, RightAnswer = "Да" });
                    housesQuestions.Add(new HouseQuestion() { Id = 2, Title = "Вопрос №2", Question = "Выберите подходящий вам пункт:", Answers = new string[] { "Нет", "50/50", "Возможно", "Да" }, RightAnswer = "Нет" });
                    housesQuestions.Add(new HouseQuestion() { Id = 3, Title = "Вопрос №3", Question = "Выберите подходящий вам пункт:", Answers = new string[] { "Нет", "50/50", "Возможно", "Да" }, RightAnswer = "Да" });
                    housesQuestions.Add(new HouseQuestion() { Id = 4, Title = "Вопрос №4", Question = "Выберите подходящий вам пункт:", Answers = new string[] { "Нет", "50/50", "Возможно", "Да", "Лол" }, RightAnswer = "50/50" });
                    housesQuestions.Add(new HouseQuestion() { Id = 5, Title = "Вопрос №5", Question = "Выберите подходящий вам пункт:", Answers = new string[] { "Нет", "50/50", "Возможно", "Да" }, RightAnswer = "Возможно" });*/
                    return housesQuestions;
                }
                catch
                {
                    return new List<HouseQuestion> { new HouseQuestion { IsError = true } };
                }
            }
        }
    }
}
