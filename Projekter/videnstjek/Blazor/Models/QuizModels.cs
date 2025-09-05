using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor.Models
{
    public class QuizData
    {
        public List<Quiz> Quizzes { get; set; } = new();
    }

    public class QuizIndex
    {
        public List<QuizReference> Quizzes { get; set; } = new();
    }

    public class QuizReference
    {
        public string Id { get; set; } = "";
        public string File { get; set; } = "";
    }

    public class Quiz
    {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        
        public string Category { get; set; } = ""; // For backward compatibility
        
        public List<string> Categories { get; set; } = new(); // New multi-category support
        public string Difficulty { get; set; } = "";
        public string EstimatedTime { get; set; } = "";
        public List<QuizQuestion> Questions { get; set; } = new();
        
        // Computed properties for better organization
        public string CategoryPrefix => GetCategoryPrefix();
        public string DisplayCategory => GetDisplayCategory();
        public int SortOrder => GetSortOrder();
        
        private string GetCategoryPrefix()
        {
            // Get categories from both old and new format
            var allCategories = new List<string>();
            
            if (!string.IsNullOrEmpty(Category))
                allCategories.Add(Category);
            
            allCategories.AddRange(Categories);
            
            if (allCategories.Count == 0) return "other";
            
            // Check if any category matches our main categories
            var mainCategories = new[] { "gf2", "backend", "frontend" };
            var matchingCategory = allCategories.FirstOrDefault(c => 
                mainCategories.Contains(c.ToLowerInvariant()));
            
            return matchingCategory?.ToLowerInvariant() ?? "other";
        }
        
        private string GetDisplayCategory()
        {
            // Get categories from both old and new format
            var allCategories = new List<string>();
            
            if (!string.IsNullOrEmpty(Category))
                allCategories.Add(Category);
            
            allCategories.AddRange(Categories);
            
            if (allCategories.Count == 0) return "Andet";
            
            // Join multiple categories with comma
            return string.Join(", ", allCategories.Distinct());
        }
        
        private int GetSortOrder()
        {
            return CategoryPrefix switch
            {
                "gf2" => 1,
                "backend" => 2,
                "frontend" => 3,
                "other" => 999,
                _ => 999
            };
        }
    }

    public class QuizQuestion
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = "";
        public Dictionary<string, string> Options { get; set; } = new();
        public List<string> CorrectAnswers { get; set; } = new();
        public Dictionary<string, string> Explanations { get; set; } = new();
        public int ExpectedAnswerCount => CorrectAnswers.Count;
        
        // Shuffled options for display
        public Dictionary<string, string> ShuffledOptions { get; set; } = new();
        
        // Mapping from shuffled keys to original keys for validation
        public Dictionary<string, string> ShuffledToOriginalMapping { get; set; } = new();
        
        // Optional learning resource
        public LearnMore? LearnMore { get; set; }
    }

    public class LearnMore
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Url { get; set; } = "";
        public string Type { get; set; } = ""; // "article", "video", "document", etc.
        public string? Icon { get; set; } = ""; // FontAwesome icon class
    }

}
