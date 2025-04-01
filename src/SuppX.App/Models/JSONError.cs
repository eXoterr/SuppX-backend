namespace SuppX.App.Models;

public class JSONError
{
    public JSONError(string ErrorText)
    {
        Errors = [ErrorText];
    }
    public List<string> Errors { get; set; }
}
