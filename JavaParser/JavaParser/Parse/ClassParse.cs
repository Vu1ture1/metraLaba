/*using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class Parese
{
    static void Main(string[] args)
    {
        // Путь к файлу для парсинга
        string filePath = @"C:\AAAA_Parser\parser\JavaCode.txt";

        // Создание словаря для хранения количества вхождений операторов
        Dictionary<string, int> operatorCounts = new Dictionary<string, int>();

        // Открытие файла для чтения
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            int lineNumber = 1;

            // Чтение файла построчно
            while ((line = reader.ReadLine()) != null)
            {
                // Поиск операторов в текущей строке
                MatchCollection matches = Regex.Matches(line, @"\b(class|\*|\+|-|<|==|!=|>=|&&|\|\||\[\]|\(\)|;|:|{}|\+\+|for|switch|if|else|while|--|\.\w+)\b");
                MatchCollection matches2 = Regex.Matches(line, @"(?:\+{1,2}|\-{1,2}|==|!=|\/|\*|>|<|<=|>=|;|:)");                // Увеличение счетчика для каждого найденного оператора
                foreach (Match match in matches)
                {
                    string operatorValue = match.Value;
                    if (!operatorCounts.ContainsKey(operatorValue))
                    {
                        operatorCounts[operatorValue] = 0;
                    }
                    operatorCounts[operatorValue]++;
                }
                foreach (Match match in matches2)
                {
                    string operatorValue = match.Value;
                    if (!operatorCounts.ContainsKey(operatorValue))
                    {
                        operatorCounts[operatorValue] = 0;
                    }
                    operatorCounts[operatorValue]++;
                }
                lineNumber++;
            }
        }
        int ysloper = 0;
        int fuloper = 0;
        // Вывод количества каждого оператора
        foreach (var kvp in operatorCounts)
        {
            if (kvp.Key == "if"  kvp.Key == "else"  kvp.Key == "switch")
            {
                ysloper += kvp.Value;
            }
            else
            {
                fuloper += kvp.Value;
            }
            Console.WriteLine($"Operator '{kvp.Key}' found {kvp.Value} times.");
        }
        fuloper += ysloper;
        Console.WriteLine($"Количество условных операторов '{ysloper}'");
        Console.WriteLine($"Количество операторов операторов '{fuloper}'");
        Console.WriteLine($"Насыщенность условными операторами'{(ysloper / (1.0 * fuloper))}'");
    }
}*/