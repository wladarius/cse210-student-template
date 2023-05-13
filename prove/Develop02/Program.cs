using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    
    abstract class JournalEntry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public DateTime Date { get; set; }

        public abstract void Display();
    }

    class ConcreteJournalEntry : JournalEntry
    {
        public override void Display()
        {
            Console.WriteLine("{0} - {1}: {2}", Date, Prompt, Response);
        }
    }

   
    class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();

   
        public void AddEntry(JournalEntry entry)
        {
            entries.Add(entry);
        }

      
        public void DisplayEntries()
        {
            foreach (JournalEntry entry in entries)
            {
                entry.Display();
            }
        }

       
        public void SaveToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (JournalEntry entry in entries)
                {
                    writer.WriteLine("{0}\t{1}\t{2}", entry.Date, entry.Prompt, entry.Response);
                }
            }
        }

       
        public void LoadFromFile(string filename)
        {
            entries.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    JournalEntry entry = new ConcreteJournalEntry();
                    entry.Date = DateTime.Parse(fields[0]);
                    entry.Prompt = fields[1];
                    entry.Response = fields[2];
                    entries.Add(entry);
                }
            }
        }
    }

  
    class Program
    {
      
        static List<string> prompts = new List<string>()
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        static void Main(string[] args)
        {
            Journal journal = new Journal();

            while (true)
            {
        
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display the journal");
                Console.WriteLine("3. Save the journal to a file");
                Console.WriteLine("4. Load the journal from a file");
                Console.WriteLine("5. Exit");

              
                Console.Write("\nEnter choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                     
                        Random random = new Random();
                        string prompt = prompts[random.Next(prompts.Count)];

                       
                        Console.Write("\n{0}\n", prompt);
                        string response = Console.ReadLine();

                    
                        JournalEntry entry = new ConcreteJournalEntry();
                        entry.Prompt = prompt;
                        entry.Response = response;
                        entry.Date = DateTime.Now;
                        journal.AddEntry(entry);
                        break;

                    case 2:
                      
                        journal.DisplayEntries();
                        break;

                    case 3:
                   
                        Console.Write("\nEnter filename to save to: ");
                        string filename = Console.ReadLine();
                        journal.SaveToFile(filename);
                        break;

                    case 4:
        
                        Console.Write("\nEnter filename to load from: ");
                        filename = Console.ReadLine();
                        journal.LoadFromFile(filename);
                        break;

                    case 5:
        
                        Environment.Exit(0);
                        break;

                    default:
         
                        Console.WriteLine("\nInvalid choice, please try again.");
                        break;
                }
            }
        }
    }
}

