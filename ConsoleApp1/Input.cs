namespace ConsoleApp1;

public class Input
{
    public Random Random;
    
    public Input()
    {
        Random = new Random();
    }
    
    public int GetPlayerCommandNumber()
    {
        Console.Write("Введите номер: ");
        int commandNumber;
        string input = Console.ReadLine();
        while (!int.TryParse(input, out commandNumber))
        {
            Console.WriteLine("Команда не распознана. Попробуйте еще раз. \n");
            input = Console.ReadLine();
        }
        
        return commandNumber; 
    }

    public int GetAICommandNumber(int maxNumber)
    {
        int randomCommand = Random.Next(1, maxNumber + 1);
        return randomCommand;
    }
}