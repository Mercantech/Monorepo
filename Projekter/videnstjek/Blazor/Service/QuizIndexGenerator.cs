using System.Text.Json;
using Blazor.Models;
using Microsoft.AspNetCore.Hosting;

namespace Blazor.Service
{
    public interface IQuizIndexGenerator
    {
        Task GenerateIndexAsync();
    }

    public class QuizIndexGenerator : IQuizIndexGenerator
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<QuizIndexGenerator> _logger;

        public QuizIndexGenerator(IWebHostEnvironment webHostEnvironment, ILogger<QuizIndexGenerator> logger)
        {
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task GenerateIndexAsync()
        {
            try
            {
                var quizzesDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "data", "quizzes");
                var indexFilePath = Path.Combine(quizzesDirectory, "index.json");

                if (!Directory.Exists(quizzesDirectory))
                {
                    _logger.LogWarning("Quiz mappe ikke fundet: {Directory}", quizzesDirectory);
                    return;
                }

                // Find alle .json filer undtagen index.json og template.json
                var jsonFiles = Directory.GetFiles(quizzesDirectory, "*.json")
                    .Where(f => !Path.GetFileName(f).Equals("index.json", StringComparison.OrdinalIgnoreCase) &&
                               !Path.GetFileName(f).Equals("template.json", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                var quizReferences = new List<QuizReference>();

                foreach (var jsonFile in jsonFiles)
                {
                    try
                    {
                        var fileName = Path.GetFileName(jsonFile);
                        var content = await File.ReadAllTextAsync(jsonFile);
                        
                        // Parse kun de første linjer for at få quiz-id uden at deserialisere hele filen
                        var quiz = JsonSerializer.Deserialize<Quiz>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        if (quiz != null && !string.IsNullOrEmpty(quiz.Id))
                        {
                            quizReferences.Add(new QuizReference
                            {
                                Id = quiz.Id,
                                File = fileName
                            });
                            
                            _logger.LogInformation("Tilføjet quiz til index: {Id} -> {File}", quiz.Id, fileName);
                        }
                        else
                        {
                            _logger.LogWarning("Kunne ikke læse quiz-id fra fil: {File}", fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Fejl ved læsning af quiz fil: {File}", jsonFile);
                    }
                }

                // Sorter efter quiz-id for konsistent rækkefølge
                quizReferences = quizReferences.OrderBy(q => q.Id).ToList();

                // Opret index data
                var indexData = new QuizIndex
                {
                    Quizzes = quizReferences
                };

                // Skriv index fil
                var indexJson = JsonSerializer.Serialize(indexData, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await File.WriteAllTextAsync(indexFilePath, indexJson);
                
                _logger.LogInformation("Quiz index genereret med {Count} quizzer", quizReferences.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved generering af quiz index");
                throw;
            }
        }
    }
}
