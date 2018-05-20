namespace WpfCalculatorApp.Interfaces
{
    public interface ICalculatorViewModel
    {
        string CurrentDisplayValue { get; set; }

        void DoPressNumber(string number);
    }
}