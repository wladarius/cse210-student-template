public class MathAssignment : Assignment
{
        

    private string _textbookSection;
    private string _problems;


    public MathAssignment(string studentName, string topic, string text_book_section, string problems)
    : base(studentName, topic)
    {
        _textbookSection = text_book_section;
        _problems = problems;
    }

    public string GetHomeworkList()
    {
        return  $"Section {_textbookSection} Problems {_problems}";
    }

}  



  