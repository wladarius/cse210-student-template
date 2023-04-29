using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();
        

        int user_Number = -1;
        while (user_Number != 0)
        {
            Console.Write("Enter a number (0 to quit): ");
            
            string userResponse = Console.ReadLine();
            user_Number = int.Parse(userResponse);
            

            if (user_Number != 0)
            {
                numbers.Add(user_Number);
            }
        }

        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        Console.WriteLine($"The sum is: {sum}");

        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");


        int max = numbers[0];

        foreach (int number in numbers)
        {
            if (number > max)
            {
                // if this number is greater than the max, we have found the new max!
                max = number;
            }
        }

        Console.WriteLine($"The max is: {max}");
    }
}