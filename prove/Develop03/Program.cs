using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Fraction f1 = new Fraction();
        Console.WriteLine(f1.GetFractionString());
        Console.WriteLine(f1.GetDecimalValue());

        Fraction f2 = new Fraction(5);
        Console.WriteLine(f2.GetFractionString());
        Console.WriteLine(f2.GetDecimalValue());

        Fraction f3 = new Fraction(3, 4);
        Console.WriteLine(f3.GetFractionString());
        Console.WriteLine(f3.GetDecimalValue());

        Fraction f4 = new Fraction(1, 3);
        Console.WriteLine(f4.GetFractionString());
        Console.WriteLine(f4.GetDecimalValue());


        




    
        //  new instance of the Scripture class
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world...");

        // Clear the console and display the complete scripture
        Console.Clear();
        Console.WriteLine(scripture.ToString());

        // hiding words until all words are hidden
        while (!scripture.AllWordsHidden)
        {
            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWord();
            Console.Clear();
            Console.WriteLine(scripture.ToString());
        }
    }
}

public class Scripture
{
    private string reference;
    private string text;
    private List<Word> words;
    private Random random;

    public bool AllWordsHidden => words.All(w => w.IsHidden);

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        this.words = CreateWordList();
        this.random = new Random();
    }

    public void HideRandomWord()
    {
        List<Word> visibleWords = words.Where(w => !w.IsHidden).ToList();
        int index = random.Next(visibleWords.Count);
        visibleWords[index].IsHidden = true;
    }

    private List<Word> CreateWordList()
    {
        string[] wordArray = text.Split(' ');
        List<Word> wordList = new List<Word>();

        foreach (string wordText in wordArray)
        {
            Word word = new Word(wordText);
            wordList.Add(word);
        }

        return wordList;
    }

    public override string ToString()
    {
        string result = $"{reference}\n";

        foreach (Word word in words)
        {
            result += word.ToString() + " ";
        }

        return result;
    }
}

public class Reference
{
    public string VerseReference { get; private set; }

    public Reference(string verseReference)
    {
        VerseReference = verseReference;
    }

    //additional constructors or methods if needed
}

public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public override string ToString()
    {
        return IsHidden ? "___" : Text;
    }
}
