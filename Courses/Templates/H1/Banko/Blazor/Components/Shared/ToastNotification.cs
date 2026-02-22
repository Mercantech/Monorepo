namespace Blazor.Components.Shared;

public class ToastNotification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool Show { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsError { get; set; }
}

