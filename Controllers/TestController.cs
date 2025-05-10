using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BTicTacToe.Models;
using BTicTacToe.Services;

namespace BTicTacToe.Controllers
{
    public class TestController : Controller
    {
        private readonly EmailService _emailService;

        public TestController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserInfo user)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetString("UserInfo", JsonSerializer.Serialize(user));
                return RedirectToAction("Test");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Test()
        {
            var questions = StaticQuestions.All;
            return View(questions);
        }

        [HttpPost]
        public IActionResult Test(List<UserAnswer> answers)
        {
            int correct = 0;
            var questions = StaticQuestions.All;

            for (int i = 0; i < answers.Count; i++)
            {
                if (answers[i].SelectedIndex == questions[i].CorrectIndex)
                    correct++;
            }

            ViewBag.Score = correct;

            // Дістаємо дані користувача із сесії
            var json = HttpContext.Session.GetString("UserInfo");
            if (json != null)
            {
                var user = JsonSerializer.Deserialize<UserInfo>(json);

                string subject = "Результат тесту ASP.NET";
                string body = $"Ім’я: {user.FirstName}\nEmail: {user.Email}\nПравильних відповідей: {correct} з {questions.Count}";

                _emailService.SendResultEmail(user.Email, subject, body);

                ViewBag.User = user;
            }

            return View("Result");
        }
    }
}
