using System.Text.Json;
using Blazor.Models;
using Microsoft.AspNetCore.Hosting;

namespace Blazor.Service
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetAllQuizzesAsync();
        Task<Quiz?> GetQuizByIdAsync(string id);
        Task<Quiz?> GetQuizByTitleAsync(string title);
    }

    public class QuizService : IQuizService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<QuizService> _logger;
        private QuizData? _quizData;
        private readonly Random _random = new Random();

        public QuizService(IWebHostEnvironment webHostEnvironment, ILogger<QuizService> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            try
            {
                if (_quizData == null)
                {
                    await LoadQuizDataAsync();
                }

                return _quizData?.Quizzes ?? new List<Quiz>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af quizzer");
                return new List<Quiz>();
            }
        }

        public async Task<Quiz?> GetQuizByIdAsync(string id)
        {
            try
            {
                if (_quizData == null)
                {
                    await LoadQuizDataAsync();
                }

                return _quizData?.Quizzes?.FirstOrDefault(q => q.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af quiz med ID: {QuizId}", id);
                return null;
            }
        }

        public async Task<Quiz?> GetQuizByTitleAsync(string title)
        {
            try
            {
                if (_quizData == null)
                {
                    await LoadQuizDataAsync();
                }

                return _quizData?.Quizzes?.FirstOrDefault(q => 
                    q.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af quiz med titel: {QuizTitle}", title);
                return null;
            }
        }

        private async Task LoadQuizDataAsync()
        {
            try
            {
                var quizzesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "data", "quizzes");
                
                if (!Directory.Exists(quizzesDirectory))
                {
                    _logger.LogWarning("Quiz mappe ikke fundet: {Directory}", quizzesDirectory);
                    _quizData = new QuizData();
                    return;
                }

                // Find alle .json filer undtagen index.json og template.json
                var jsonFiles = Directory.GetFiles(quizzesDirectory, "*.json")
                    .Where(f => !Path.GetFileName(f).Equals("index.json", StringComparison.OrdinalIgnoreCase) &&
                               !Path.GetFileName(f).Equals("template.json", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var quizzes = new List<Quiz>();
                
                foreach (var jsonFile in jsonFiles)
                {
                    try
                    {
                        var fileName = Path.GetFileName(jsonFile);
                        var quizContent = await File.ReadAllTextAsync(jsonFile);
                        
                        // Handle category field that might be array or string
                        var quiz = await DeserializeQuizWithCategoryHandling(quizContent);
                        
                        // Handle backward compatibility for categories
                        if (quiz != null)
                        {
                            await HandleCategoryBackwardCompatibility(quiz);
                        }
                        
                        if (quiz != null && !string.IsNullOrEmpty(quiz.Id))
                        {
                            // Shuffle options for all questions in this quiz
                            foreach (var question in quiz.Questions)
                            {
                                ShuffleQuestionOptions(question);
                            }
                            
                            quizzes.Add(quiz);
                            _logger.LogDebug("Indlæst quiz: {Id} fra {File}", quiz.Id, fileName);
                        }
                        else
                        {
                            _logger.LogWarning("Kunne ikke læse quiz fra fil: {File} (manglende ID eller ugyldig JSON)", fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Fejl ved indlæsning af quiz fil: {File}", jsonFile);
                    }
                }

                // Sorter quizzer efter kategori og derefter ID for konsistent rækkefølge
                quizzes = quizzes.OrderBy(q => q.SortOrder).ThenBy(q => q.Id).ToList();

                _quizData = new QuizData { Quizzes = quizzes };
                _logger.LogInformation("Indlæst {Count} quizzer fra {Directory}", quizzes.Count, quizzesDirectory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved indlæsning af quiz data fra separate filer");
                _quizData = new QuizData();
            }
        }

        private Task<Quiz?> DeserializeQuizWithCategoryHandling(string jsonContent)
        {
            try
            {
                using var document = JsonDocument.Parse(jsonContent);
                var root = document.RootElement;
                
                var quiz = new Quiz();
                
                // Deserialize basic properties
                if (root.TryGetProperty("id", out var idElement))
                    quiz.Id = idElement.GetString() ?? "";
                if (root.TryGetProperty("title", out var titleElement))
                    quiz.Title = titleElement.GetString() ?? "";
                if (root.TryGetProperty("description", out var descElement))
                    quiz.Description = descElement.GetString() ?? "";
                if (root.TryGetProperty("difficulty", out var diffElement))
                    quiz.Difficulty = diffElement.GetString() ?? "";
                if (root.TryGetProperty("estimatedTime", out var timeElement))
                    quiz.EstimatedTime = timeElement.GetString() ?? "";
                
                // Handle category field - can be string or array
                if (root.TryGetProperty("category", out var categoryElement))
                {
                    if (categoryElement.ValueKind == JsonValueKind.String)
                    {
                        quiz.Category = categoryElement.GetString() ?? "";
                    }
                    else if (categoryElement.ValueKind == JsonValueKind.Array)
                    {
                        // Convert array to categories list
                        foreach (var item in categoryElement.EnumerateArray())
                        {
                            if (item.ValueKind == JsonValueKind.String)
                            {
                                quiz.Categories.Add(item.GetString() ?? "");
                            }
                        }
                    }
                }
                
                // Handle categories array
                if (root.TryGetProperty("categories", out var categoriesElement) && 
                    categoriesElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in categoriesElement.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.String)
                        {
                            quiz.Categories.Add(item.GetString() ?? "");
                        }
                    }
                }
                
                // Deserialize questions
                if (root.TryGetProperty("questions", out var questionsElement) && 
                    questionsElement.ValueKind == JsonValueKind.Array)
                {
                    var questionsJson = questionsElement.GetRawText();
                    quiz.Questions = JsonSerializer.Deserialize<List<QuizQuestion>>(questionsJson, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<QuizQuestion>();
                }
                
                return Task.FromResult<Quiz?>(quiz);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved deserialisering af quiz med category handling");
                return Task.FromResult<Quiz?>(null);
            }
        }

        private Task HandleCategoryBackwardCompatibility(Quiz quiz)
        {
            // If Categories list is empty but Category field has a value, migrate it
            if (quiz.Categories.Count == 0 && !string.IsNullOrEmpty(quiz.Category))
            {
                quiz.Categories.Add(quiz.Category);
            }
            
            // If both exist, merge them (avoid duplicates)
            if (quiz.Categories.Count > 0 && !string.IsNullOrEmpty(quiz.Category))
            {
                if (!quiz.Categories.Contains(quiz.Category))
                {
                    quiz.Categories.Add(quiz.Category);
                }
            }
            
            // Handle case where Category was an array in JSON (now empty string due to converter)
            // We need to check the original JSON for array format
            if (string.IsNullOrEmpty(quiz.Category) && quiz.Categories.Count == 0)
            {
                // This might be a case where category was an array, try to detect from file
                // For now, we'll leave it as is since the converter should handle it
            }
            
            return Task.CompletedTask;
        }

        private void ShuffleQuestionOptions(QuizQuestion question)
        {
            // Create a list of option pairs to shuffle
            var optionPairs = question.Options.ToList();
            
            // Shuffle the pairs
            for (int i = optionPairs.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                var temp = optionPairs[i];
                optionPairs[i] = optionPairs[j];
                optionPairs[j] = temp;
            }

            // Create new shuffled options with new keys (A, B, C, D...)
            question.ShuffledOptions.Clear();
            question.ShuffledToOriginalMapping.Clear();
            
            var newKeys = new[] { "A", "B", "C", "D", "E", "F" }; // Support for up to 6 options
            
            for (int i = 0; i < optionPairs.Count; i++)
            {
                var newKey = newKeys[i];
                var originalKey = optionPairs[i].Key;
                var optionText = optionPairs[i].Value;
                
                question.ShuffledOptions[newKey] = optionText;
                question.ShuffledToOriginalMapping[newKey] = originalKey;
            }
        }
    }
}
