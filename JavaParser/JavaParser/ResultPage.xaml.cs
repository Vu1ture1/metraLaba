using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JavaParser
{
    public partial class ResultPage : ContentPage
    {

        private static bool IsKeyword(string word)
        {
            string[] keywords = {"abstract", "continue", "for", "new", "switch", "assert", "default", "goto",
                "package", "synchronized", "boolean", "do", "if", "private", "this", "break", "double",
                "implements", "protected", "throw", "byte", "else", "import", "public", "throws", "case",
                "enum", "instanceof", "return", "transient", "catch", "extends", "try",
                "char", "final", "interface", "static", "void", "class", "finally", "long", "strictfp",
                "volatile", "const", "float", "native", "super", "while"};

            return Array.IndexOf(keywords, word) != -1;
        }

        private static bool IsType(string word)
        {
            string[] keywords = {"int", "byte", "short", "long", "float", "double", "boolean", "char", "String", "string"};

            return Array.IndexOf(keywords, word) != -1;
        }

        private static bool IsTypeMany(string word)
        {
            string[] keywords = { "int", "byte", "short", "long", "float", "double", "boolean", "char", "String", "string" };

            foreach(var key in keywords) 
            {
                if (word.Contains(key))
                {
                    return true;
                }
            }

            

            return Array.IndexOf(keywords, word) != -1;
        }
        public ResultPage()
        {
            string outputStr = "";
            string filePath = @"D:\JavaCode.txt";
            Dictionary<string, int> operatorCounts = new Dictionary<string, int>();

            Dictionary<string, int> operandCounts = new Dictionary<string, int>();

            int maxDepth = 0;
            int currentDepth = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    MatchCollection matches = Regex.Matches(line, @"\b(-=|\*=|\/=|%=|<<=|>>=|>>>=|&=|\|=|\^=|&&|\|\||\+\+|--|&|\||\^|~|>>|<<|>>>|!|\*|\+|\-|>|<|=>|=<|==|!=|\|\||\[\]|\(\)|;|:|{}|\+\+|for|switch|if|else|while|case|break|continue|default|do|goto|\.|try|catch|return\w+)\b(?=(?:[^""\\]*(\\.|""([^""\\]*\\.)*[^""\\]*""))*[^""]*$)\b");
                    MatchCollection matches2 = Regex.Matches(line, @"(?:\+{1,2}|\-{1,2}|==|!=|\/|\*|>|<|<=|>=|;|:|=)");
                    MatchCollection matches3 = Regex.Matches(line, @"((?<!([\+\-\*\/\%\=\!\>\<\&\|\^\~\[]))[\+\-\*\/\%\=\!\>\<\&\|\^\~\[](?![\+\-\*\/\%\=\!\>\<\&\|\^\~\]])){1}\ *([A-Za-z0-9_\.]+)"); // тут проверка на ключи
                    MatchCollection matches4 = Regex.Matches(line, @"((\!\=)|(\-\=)|(\*\=)|(\/\=)|(\%\=)|(\>\>\=)|(\<\<\=)|(\>\>\>\=)|(\&\=)|(\|\=)|(\^\=)|(\&\&)|(\|\|)|(\=\=)|(\>\=)|(\<\=)|(\+\+)|(\-\-)|(\>\>)|(\<\<)|(\>\>\>)){1}\ *([A-Za-z0-9_\.]+)"); // тут проверка на ключи
                    MatchCollection matches5 = Regex.Matches(line, @"(\(){1}\ *(\""|\')?([A-Za-z0-9_\.\:\ ]+)(""|\')?");
                    MatchCollection matches6 = Regex.Matches(line, @"(\(){1}\ *([A-Za-z0-9_\.\,\ \[\]\+\-\*\/\%\=\!\>\<\&\|\^\~]+)(\)){1}"); //(\,|\;){1}\ *([A-Za-z0-9_\.]+)

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
                    if (line.Contains("if") || line.Contains("else") || line.Contains("for") || line.Contains("while") || line.Contains("case"))
                    {
                        currentDepth++;
                        if (currentDepth > maxDepth)
                        {
                            maxDepth = currentDepth;
                        }
                    }
                    if (line.Contains("}"))
                    {
                        currentDepth--;
                    }
                    if (line.Contains("default"))
                    {
                        if (currentDepth > maxDepth)
                        {
                            maxDepth = currentDepth;
                        }
                        currentDepth = 0;
                    }
                    lineNumber++;



                    foreach (Match match in matches3)
                    {
                        string operandValue = match.Groups[3].Value;

                        if (IsKeyword(operandValue) == false) 
                        {
                            if (!operandCounts.ContainsKey(operandValue) )
                            {
                                operandCounts[operandValue] = 0;
                            }

                            operandCounts[operandValue]++;

                        }
                    }

                    foreach (Match match in matches4)
                    {
                        string operandValue = match.Groups[3].Value;

                        if (IsKeyword(operandValue) == false)
                        {
                            if (!operandCounts.ContainsKey(operandValue))
                            {
                                operandCounts[operandValue] = 0;
                            }

                            operandCounts[operandValue]++;
                        }
                    }

                    foreach (Match match in matches4)
                    {
                        string operandValue = match.Groups[3].Value;

                        if (IsKeyword(operandValue) == false)
                        {
                            if (!operandCounts.ContainsKey(operandValue))
                            {
                                operandCounts[operandValue] = 0;
                            }

                            operandCounts[operandValue]++;
                        }
                    }

                    foreach (Match match in matches5)
                    {
                        string operandValue = match.Groups[3].Value;

                        if (IsType(operandValue) == false)
                        {
                            if (!operandCounts.ContainsKey(operandValue))
                            {
                                operandCounts[operandValue] = 0;
                            }

                            operandCounts[operandValue]++;
                        }
                    }

                    foreach (Match match in matches6)
                    {
                        string word = match.Groups[2].Value;

                        if (!IsTypeMany(word))
                        {
                            MatchCollection matches7 = Regex.Matches(line, @"(\,|\;){1}\ *([A-Za-z0-9_\.]+)"); //(\,|\;){1}\ *([A-Za-z0-9_\.]+)

                            foreach (Match match2 in matches7)
                            {
                                string operandValue = match2.Groups[2].Value;

                                if (!operandCounts.ContainsKey(operandValue))
                                {
                                    operandCounts[operandValue] = 0;
                                }

                                operandCounts[operandValue]++;
                            }
                        }
                    }
                }
            }
            
            int ysloper = 0;
            int fuloper = 0;

            foreach (var kvp in operatorCounts)
            {
                if (kvp.Key == "if" || kvp.Key == "else" || kvp.Key == "switch" || kvp.Key == "case" || kvp.Key == "for"|| kvp.Key == "while")
                {
                    ysloper += kvp.Value;
                }
                else
                {
                    fuloper += kvp.Value;
                }
                outputStr += $"\nОператор '{kvp.Key}' нашелся {kvp.Value} раз.";
            }

            fuloper += ysloper;


            int fuloperd = 0;

            foreach (var kvp in operandCounts)
            {
                outputStr += $"\nОперанд {kvp.Key} нашелся {kvp.Value} раз.";
                fuloperd += kvp.Value;
            }




            InitializeComponent();
            ResOutput.Text += outputStr;
            ResOutput.Text += $"\n\nКоличество уникальных операторов {operatorCounts.Count}";
            ResOutput.Text += $"\nОбщее количество операторов {fuloper}";

            ResOutput.Text += $"\n\nКоличество уникальных операндов {operandCounts.Count}";
            ResOutput.Text += $"\nОбщее количество операндов {fuloperd}";

            ResOutput.Text += $"\n\nОбъём программы {(fuloper + fuloperd) * System.Math.Log2( operatorCounts.Count + operandCounts.Count )}";
            ResOutput.Text += $"\nСловарь программы {operatorCounts.Count + operandCounts.Count}";
            ResOutput.Text += $"\nРазмер программы {fuloper + fuloperd}";

            ResOutput.Text += $"\n\nКоличество условных операторов {ysloper}";
            
            ResOutput.Text += $"\nНасыщенность условными операторами {(ysloper / (double)fuloper)}";
            ResOutput.Text += $"\nМаксимальная глубина ветвления {maxDepth - 1}";


        }
    }

    
}