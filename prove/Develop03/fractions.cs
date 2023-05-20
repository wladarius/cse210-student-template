using System;

public class Fraction
{
    private int _top_;
    private int _bottom_;

        public Fraction()
    {
        _top_ = 1;
        _bottom_ = 1;
    }

        public Fraction(int wholeNumber)
    {
        _top_ = wholeNumber;
        _bottom_ = 1;
    }


    public Fraction(int top, int bottom)
    {
        _top_ = top;
        _bottom_ = bottom;
    }

        public string GetFractionString()
    {
        string text = $"{_top_}/{_bottom_}";
        return text;
    }

    public double GetDecimalValue()
    {
        return (double)_top_ / (double)_bottom_;
    }

}