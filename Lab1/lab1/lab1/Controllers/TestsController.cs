using lab1.DTOs;
using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

[Route("api/[controller]")]
[ApiController]
public class TestsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTest([FromBody] CreateTestDto dto)
    {
        var test = new Test
        {
            Title = dto.Title,
            TopicId = dto.TopicId
        };

        _context.Tests.Add(test);
        await _context.SaveChangesAsync();

        foreach (var q in dto.Questions)
        {
            var question = new TestQuestion
            {
                TestId = test.Id,
                QuestionText = q.QuestionText,
                QuestionType = q.QuestionType,
                AnswersJson = q.AnswersJson,
                Points = q.Points
            };
            _context.TestQuestions.Add(question);
        }

        await _context.SaveChangesAsync();
        return Ok(new { Message = "Тест успешно создан", TestId = test.Id });
    }

    [HttpGet("{id}/take")]
    public async Task<IActionResult> GetTestForPassing(int id)
    {
        var test = await _context.Tests.FindAsync(id);
        if (test == null) return NotFound("Тест не найден");

        var questions = await _context.TestQuestions
            .Where(q => q.TestId == id)
            .Select(q => new
            {
                q.Id,
                q.QuestionText,
                q.QuestionType,
                q.AnswersJson
            })
            .ToListAsync();

        return Ok(new
        {
            test.Id,
            test.Title,
            Questions = questions
        });
    }

    [HttpPost("{id}/submit")]
    public async Task<IActionResult> SubmitTest(int id, [FromBody] SubmitTestDto dto)
    {
        var questions = await _context.TestQuestions
            .Where(q => q.TestId == id)
            .ToDictionaryAsync(q => q.Id);

        int correctCount = 0;
        int totalQuestions = questions.Count;

        foreach (var userAnswer in dto.Answers)
        {
            if (questions.TryGetValue(userAnswer.QuestionId, out var officialQuestion))
            {
                try
                {
                    using var doc = JsonDocument.Parse(officialQuestion.AnswersJson);
                    if (doc.RootElement.TryGetProperty("correct", out var correctProp))
                    {
                        string correctAnswer = correctProp.GetString() ?? "";

                        if (correctAnswer.Trim().ToLower() == userAnswer.AnswerText.Trim().ToLower())
                        {
                            correctCount++;
                        }
                    }
                }
                catch (JsonException)
                {
                    if (officialQuestion.AnswersJson.Trim().ToLower() == userAnswer.AnswerText.Trim().ToLower())
                    {
                        correctCount++;
                    }
                }
            }
        }

        double scorePercentage = totalQuestions > 0 ? ((double)correctCount / totalQuestions) * 100 : 0;

        return Ok(new
        {
            Message = "Тест проверен",
            CorrectAnswers = correctCount,
            TotalQuestions = totalQuestions,
            ScorePercentage = Math.Round(scorePercentage, 2)
        });
    }
}
