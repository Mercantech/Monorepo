using Blazor.Data;
using Blazor.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Blazor.Services;

public class SurveyService
{
    private readonly ApplicationDbContext _context;

    public SurveyService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Survey CRUD operations
    public async Task<List<Survey>> GetAllSurveysAsync()
    {
        return await _context.Surveys
            .Include(s => s.Questions)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
    }

    public async Task<Survey?> GetSurveyByIdAsync(int id)
    {
        return await _context.Surveys
            .Include(s => s.Questions.OrderBy(q => q.Order))
                .ThenInclude(q => q.Conditions)
            .Include(s => s.Questions)
                .ThenInclude(q => q.ParentConditions)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Survey?> GetSurveyByAccessCodeAsync(string accessCode)
    {
        return await _context.Surveys
            .Include(s => s.Questions.OrderBy(q => q.Order))
                .ThenInclude(q => q.Conditions)
            .Include(s => s.Questions)
                .ThenInclude(q => q.ParentConditions)
            .FirstOrDefaultAsync(s => s.AccessCode == accessCode && s.IsActive);
    }

    public async Task<Survey> CreateSurveyAsync(Survey survey)
    {
        // Generer unik 4-cifret adgangskode
        survey.AccessCode = await GenerateUniqueAccessCodeAsync();
        
        // Sæt ID til 0 for at sikre auto-generering
        survey.Id = 0;
        
        _context.Surveys.Add(survey);
        await _context.SaveChangesAsync();
        return survey;
    }

    public async Task<Survey> UpdateSurveyAsync(Survey survey)
    {
        _context.Surveys.Update(survey);
        await _context.SaveChangesAsync();
        return survey;
    }

    public async Task DeleteSurveyAsync(int id)
    {
        var survey = await _context.Surveys.FindAsync(id);
        if (survey != null)
        {
            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Survey> DuplicateSurveyAsync(int id)
    {
        var originalSurvey = await GetSurveyByIdAsync(id);
        if (originalSurvey == null)
        {
            throw new ArgumentException("Survey ikke fundet");
        }

        // Opret ny survey med kopieret data
        var duplicatedSurvey = new Survey
        {
            Id = 0, // Sikrer auto-generering af ID
            Title = $"{originalSurvey.Title} (Kopi)",
            Description = originalSurvey.Description,
            AccessCode = await GenerateUniqueAccessCodeAsync(),
            IsActive = false, // Start som inaktiv
            ExpiresAt = null, // Fjern udløbsdato
            Questions = new List<Question>()
        };

        // Kopier spørgsmål
        foreach (var question in originalSurvey.Questions.OrderBy(q => q.Order))
        {
            var duplicatedQuestion = new Question
            {
                Id = 0, // Sikrer auto-generering af ID
                SurveyId = 0, // Vil blive sat når survey gemmes
                Text = question.Text,
                Description = question.Description,
                Type = question.Type,
                IsRequired = question.IsRequired,
                Order = question.Order,
                MinValue = question.MinValue,
                MaxValue = question.MaxValue,
                Options = question.Options,
                ResponseData = new List<ResponseData>() // Tom liste for svar
            };

            duplicatedSurvey.Questions.Add(duplicatedQuestion);
        }

        // Gem den duplikerede survey
        _context.Surveys.Add(duplicatedSurvey);
        await _context.SaveChangesAsync();

        return duplicatedSurvey;
    }

    // Question operations
    public async Task<Question> CreateQuestionAsync(Question question)
    {
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task<Question> UpdateQuestionAsync(Question question)
    {
        _context.Questions.Update(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task DeleteQuestionAsync(int id)
    {
        var question = await _context.Questions.FindAsync(id);
        if (question != null)
        {
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }

    public async Task ReorderQuestionsAsync(int surveyId, List<int> questionIds)
    {
        var questions = await _context.Questions
            .Where(q => q.SurveyId == surveyId)
            .ToListAsync();

        for (int i = 0; i < questionIds.Count; i++)
        {
            var question = questions.FirstOrDefault(q => q.Id == questionIds[i]);
            if (question != null)
            {
                question.Order = i + 1;
            }
        }

        await _context.SaveChangesAsync();
    }

    // QuestionCondition operations
    public async Task<QuestionCondition> CreateConditionAsync(QuestionCondition condition)
    {
        _context.QuestionConditions.Add(condition);
        await _context.SaveChangesAsync();
        return condition;
    }

    public async Task<QuestionCondition> UpdateConditionAsync(QuestionCondition condition)
    {
        _context.QuestionConditions.Update(condition);
        await _context.SaveChangesAsync();
        return condition;
    }

    public async Task DeleteConditionAsync(int id)
    {
        var condition = await _context.QuestionConditions.FindAsync(id);
        if (condition != null)
        {
            _context.QuestionConditions.Remove(condition);
            await _context.SaveChangesAsync();
        }
    }

    // Helper methods
    public async Task<string> GenerateUniqueAccessCodeAsync()
    {
        string accessCode;
        bool isUnique;
        
        do
        {
            accessCode = new Random().Next(1000, 9999).ToString();
            isUnique = !await _context.Surveys.AnyAsync(s => s.AccessCode == accessCode);
        } while (!isUnique);
        
        return accessCode;
    }

    // JSON helper methods
    public static List<QuestionOption> ParseOptions(JsonDocument? optionsJson)
    {
        if (optionsJson == null)
            return new List<QuestionOption>();
            
        try
        {
            return JsonSerializer.Deserialize<List<QuestionOption>>(optionsJson) ?? new List<QuestionOption>();
        }
        catch
        {
            return new List<QuestionOption>();
        }
    }

    public static JsonDocument? SerializeOptions(List<QuestionOption> options)
    {
        if (options == null || !options.Any())
            return null;
            
        var jsonString = JsonSerializer.Serialize(options);
        return JsonDocument.Parse(jsonString);
    }

    // Response operations
    public async Task<List<Response>> GetSurveyResponsesAsync(int surveyId)
    {
        return await _context.Responses
            .Include(r => r.ResponseData)
                .ThenInclude(rd => rd.Question)
            .Where(r => r.SurveyId == surveyId)
            .OrderByDescending(r => r.SubmittedAt)
            .ToListAsync();
    }

    public async Task<Response> CreateResponseAsync(Response response)
    {
        _context.Responses.Add(response);
        await _context.SaveChangesAsync();
        return response;
    }

    public async Task<ResponseData> CreateResponseDataAsync(ResponseData responseData)
    {
        _context.ResponseData.Add(responseData);
        await _context.SaveChangesAsync();
        return responseData;
    }
}
