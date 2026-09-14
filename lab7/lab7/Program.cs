Console.WriteLine("Введите первое число: ");
string userValue1 = Console.ReadLine();
Console.WriteLine("Введите второе число: ");
string userValue2 = Console.ReadLine();

try
{
    int num1 = Convert.ToInt32(userValue1);
    int num2 = Convert.ToInt32(userValue2);

    int sum = num1 + num2;
    int dif = num1 - num2;
    int mul = num1 * num2;
    int div = num1 / num2;

    Console.WriteLine($"{num1} + {num2} = {sum}");
    Console.WriteLine($"{num1} - {num2} = {dif}");
    Console.WriteLine($"{num1} * {num2} = {mul}");
    Console.WriteLine($"{num1} / {num2} = {div}");
}
catch(FormatException ex)
{
    Console.WriteLine($"Ошибка, {ex.Message}");
    var errorTime = DateTime.Now;
    File.AppendAllText("log.txt", $"[{errorTime:yyyy-MM-dd HH:mm:ss}] {ex.GetType()} {ex.ToString()}");
}
catch(DivideByZeroException ex)
{
    Console.WriteLine($"Ошибка, {ex.Message}");
    var errorTime = DateTime.Now;
    File.AppendAllText("log.txt", $"[{errorTime:yyyy-MM-dd HH:mm:ss}] {ex.GetType()} {ex.ToString()}");
}
catch(OverflowException ex)
{
    Console.WriteLine($"Ошибка, {ex.Message}");
    var errorTime = DateTime.Now;
    File.AppendAllText("log.txt", $"[{errorTime:yyyy-MM-dd HH:mm:ss}] {ex.GetType()} {ex.ToString()}");
}