using System;

class Program
{
      static void Main(string[] args)
    {
        string response = "yes";

        while (response == "yes")
        {

            Random randomGenerator = new Random();
            int m_number = randomGenerator.Next(1, 101);

            int guess = -1;


            while (m_number != guess)
            {

                

                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());

                    

                if (m_number > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (m_number < guess)
                {
                    Console.WriteLine("Lower");
                }
                else 
                {
                    Console.WriteLine("Got it!");
                }


                }
                    
            Console.Write("Do you want to continue? ");
            response = Console.ReadLine();
        }

    }   


}