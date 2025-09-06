namespace Blazor.Data.Models;

public enum QuestionType
{
    Text,           // Fritekst svar
    MultipleChoice, // Flervalgs spørgsmål
    SingleChoice,   // Enkeltvalgs spørgsmål
    Rating,         // Bedømmelse (1-5, 1-10, etc.)
    YesNo,          // Ja/Nej spørgsmål
    Date,           // Dato spørgsmål
    Number,         // Tal spørgsmål
    Email,          // Email spørgsmål
    Scale           // Skala spørgsmål (1-10, 1-5, etc.)
}
