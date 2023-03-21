namespace WebbLabb2.Shared;

public class ServiceResponse<T>
{
    public T Data { get; set; }
    public bool Error { get; set; } = false;
    public string Message { get; set; } = string.Empty;
}