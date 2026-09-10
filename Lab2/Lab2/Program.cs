double Degree(double a, int n)
{
    double number = Math.Pow(a, n);
    return Math.Round(number, 3);
}

//Console.WriteLine(Degree(2, 2));
//Console.WriteLine(Degree(-2, 3));
//Console.WriteLine(Degree(2.5, 3));
//Console.WriteLine(Degree(2.5, 0));
//Console.WriteLine(Degree(0, 5));
//Console.WriteLine(Degree(2, -3));
//Console.WriteLine(Degree(-3, -2));


bool IsPasswordValidate(string pass)
{
    var specialChars = new[] { '+', '-', '=', '*', '/', ',', '.', '!', '?', ':', ';' };
    bool hasDigit = pass.Any(char.IsDigit);
    bool hasLower = pass.Any(char.IsLower);
    bool hasUpper = pass.Any(char.IsUpper);
    bool hasSpecials = pass.Any(c => specialChars.Contains(c));
    bool hasCorrectLength = pass.Length >= 8 && pass.Length <= 30;


    if (hasDigit && hasLower && hasUpper && hasSpecials && hasCorrectLength)
    {
        return true;
    }
    return false;
}

Console.WriteLine(IsPasswordValidate("q,w=Erty123"));
Console.WriteLine(IsPasswordValidate("q,wErtyabc"));
Console.WriteLine(IsPasswordValidate("Q,W=ERTY123"));
Console.WriteLine(IsPasswordValidate("q,w=erty123"));
Console.WriteLine(IsPasswordValidate("qwErty123"));
Console.WriteLine(IsPasswordValidate("q,wEy3"));
Console.WriteLine(IsPasswordValidate("q,w=Erty123dfghjkl768=9463=cjrl"));