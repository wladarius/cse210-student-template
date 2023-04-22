using System;

class Program
{
    private const int V = 60;

    static void Main(string[] args)
    {
        Console.Write("What is your grade? ");
        string grade = Console.ReadLine();

        int x = int.Parse(grade);
        int A = 90;
        int B = 80;
        int C = 70;
        int D = 60;
        

        if (x >= A)
        {
            Console.WriteLine("Your grade is 'A'!");
        }
        else if (x >= B)
        {
            Console.WriteLine("Your grade is 'B'!");
        }

        else if (x >= C)
        {

            Console.WriteLine("Your grade is 'C'!");
        }

        else if (x >= D)
        {   
            Console.WriteLine("Your grade is 'D'!");
        }
        else
        {
            Console.WriteLine("Your grade is 'F'!");
        }


    }
}
