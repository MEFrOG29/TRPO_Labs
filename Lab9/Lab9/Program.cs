using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class Project
{
    //Task1
    public async Task ReadFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        using(var reader  = new StreamReader(filePath, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: 8192))
        {
            string? line;
            while((line = await  reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }

    //Task2
    public async Task WriteToFileAsync(string filePath, string data)
    {
        using (var writer = new StreamWriter(filePath, append: true, encoding: Encoding.UTF8, bufferSize: 8192))
        {
            await writer.WriteAsync(data);
        }
    }

    //Task3
    private readonly Dictionary<int, long> _fibonacciCache = new();

    public long Fibonacci(int n)
    {
        if(n <= 1)
            return n;

        if(_fibonacciCache.TryGetValue(n, out long cacheValue))
        {
            return cacheValue;
        }

        long result = Fibonacci(n - 1) + Fibonacci(n - 2);
        _fibonacciCache[n] = result;

        return result;
    }
}