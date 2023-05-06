using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Learning02 World!");
        Job job1 = new Job();
        job1._jobTitle = "SE";
        job1.Display();
        Job job2 = new Job();
        job2._jobTitle = "Apple";
        job2.Display();
    }
}