

using NLog;
using NLog.Targets;

var config = new NLog.Config.LoggingConfiguration();

var fileTarget = new FileTarget("errorFile")
{
    FileName = "C:\\Temp\\ispp-31\\TRPO_Labs\\lab6\\errors.log",
};

config.AddTarget(fileTarget);

LogManager.Configuration = config;

var logger = LogManager.GetCurrentClassLogger();
int Divide()
{
    int res = 0;
    while (true)
    {
        Console.WriteLine("Введите первое число:");
        string num1 = Console.ReadLine();
        Console.WriteLine("Введите второе число:");
        string num2 = Console.ReadLine();

        try
        {
            res = Convert.ToInt32(num1) / Convert.ToInt32(num2);
            break;
        }
        catch (FormatException ex)
        {
            Console.WriteLine("Ошибка:Нельзя вводить текст. Введите число");
            logger.Error(ex, "Ошибка формата данных");
            continue;
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Ошибка: Делить на 0 нельзя");
            logger.Error(ex, "Ошибка деления на 0");
            continue;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.StackTrace}");
            logger.Error(ex, "Ошибка при выполнении Divide()");
            continue;
        }
    }
    return res;
}



Console.WriteLine(Divide());