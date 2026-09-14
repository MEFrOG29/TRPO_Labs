using System.Diagnostics;

var time = DateTime.Now;
Stopwatch sw = new();
sw.Start();
var fileData = File.ReadAllLines("data.txt");
Console.WriteLine($"Прочитано строк: {fileData.Length}");
sw.Stop();
TimeSpan ts = sw.Elapsed;
Console.WriteLine($"[{time:yyyy-MM-dd HH:mm:ss}] Operation=ReadTxtFile, Elapsed={ts} ms");
