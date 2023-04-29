using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();

        string userName = PromptUserName();
        int userNumber = PromptUserNumber();

        int squaredNumber = SquareNumber(userNumber);

        DisplayResult(userName, squaredNumber);
    }

    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }
    static string PromptUserName()
    {
        Console.Write("Please enter your name ");
        string user_name = Console.ReadLine();
        return user_name;
    }
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int user_number = int.Parse(Console.ReadLine());

        return user_number;
    }

    static int SquareNumber(int user_number)
    {

        int square_number = user_number * user_number;
        return square_number;
    }

    static void DisplayResult(string user_name, int square_number)
    {

        Console.Write($"{user_name}, the square of your number is {square_number}");
    }

        
}
