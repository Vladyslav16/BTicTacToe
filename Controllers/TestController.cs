using Microsoft.AspNetCore.Mvc;
using BTicTacToe.Models;

namespace BTicTacToe.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var questions = new List<Question>
            {
                new Question { Text = "Що таке Razor в ASP.NET?", Options = new[] {"Мова програмування", "СУБД", "Синтаксис для шаблонів HTML і C#", "Фреймворк для API"}, CorrectIndex = 2 },
                new Question { Text = "Який шаблон використовується для створення веб-додатків у ASP.NET Core?", Options = new[] {"MVC", "MVVM", "MVP", "SPA"}, CorrectIndex = 0 },
                new Question { Text = "Що таке View у MVC?", Options = new[] {"База даних", "Контролер", "Представлення", "Модель"}, CorrectIndex = 2 },
                new Question { Text = "Який файл містить конфігурацію маршрутизації?", Options = new[] {"Program.cs", "Startup.cs", "appsettings.json", "launchSettings.json"}, CorrectIndex = 1 },
                new Question { Text = "Що таке Tag Helper?", Options = new[] {"Клас для доступу до БД", "HTML-допоміжник у Razor", "Файл стилів", "Інтерфейс для API"}, CorrectIndex = 1 },
                new Question { Text = "Який метод відповідає за обробку GET-запиту у контролері?", Options = new[] {"GetData()", "Index()", "Post()", "Execute()"}, CorrectIndex = 1 },
                new Question { Text = "Що таке Partial View?", Options = new[] {"Повна сторінка", "Частина шаблону для повторного використання", "JS-бібліотека", "Тестовий інтерфейс"}, CorrectIndex = 1 },
                new Question { Text = "Що таке Middleware в ASP.NET Core?", Options = new[] {"Шаблон View", "Елемент обробки запиту", "Роутинг система", "Бібліотека бази даних"}, CorrectIndex = 1 },
                new Question { Text = "Який файл використовується для зберігання конфігурації?", Options = new[] {"web.config", "routes.json", "appsettings.json", "settings.cs"}, CorrectIndex = 2 },
                new Question { Text = "Який результат повертає View() у контролері?", Options = new[] {"HTML-сторінку", "JSON", "404 помилку", "Файл"}, CorrectIndex = 0 }
            };

            return View(questions);
        }

        [HttpPost]
        public IActionResult Index(List<UserAnswer> answers)
        {
            int correct = 0;

            var allQuestions = StaticQuestions.All; // Статичне джерело, щоб перевірити правильність

            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i].SelectedIndex == allQuestions[i].CorrectIndex)
                    correct++;
            }

            ViewBag.Score = correct;
            return View("Result");
        }
    }
}