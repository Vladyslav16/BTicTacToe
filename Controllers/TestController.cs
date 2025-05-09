using BTicTacToe.Models;
using Microsoft.AspNetCore.Mvc;

public class TestController : Controller
{
    public IActionResult Test()
    {
        var questions = StaticQuestions.All;
        return View(questions);
    }

    [HttpPost]
    public IActionResult Test(List<UserAnswer> answers)
    {
        int correct = 0;
        var allQuestions = StaticQuestions.All;

        for (int i = 0; i < answers.Count; i++)
        {
            if (answers[i].SelectedIndex == allQuestions[i].CorrectIndex)
                correct++;
        }

        ViewBag.Score = correct;
        return View("Result");
    }
}
