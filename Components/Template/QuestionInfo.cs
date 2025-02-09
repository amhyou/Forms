using forms.Models;

public class QuestionInfo
{
    public int Order;
    public string? Text;
    public QuestionType Type;
    public List<MultipleChoiceOption> Options = new();
}


public class MultipleChoiceOption
{
    public string Text { get; set; } = "";
}