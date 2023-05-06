public class Job
{
    string _company;
    public string _jobTitle;
    int _startYear;
    int _endYear;

    public void Display()
    {
        string workTime = "(" + _startYear + "-" + _endYear + ")";
        if (_startYear == _endYear)
        {
            workTime = "during " + _startYear.ToString();
        }
        Console.WriteLine("Worked at: " + _company + ", as a " + _jobTitle + " " + workTime);
    }
}