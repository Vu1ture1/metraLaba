using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace JavaParser
{
    public partial class ResultPage : ContentPage
    {
        public ResultPage()
        {
            string outputStr = "";
            string filePath = @"C:\Parser\JavaCode.txt";
            Dictionary<string, int> operatorCounts = new Dictionary<string, int>();

            int maxDepth = 0;
            int currentDepth = 0;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    MatchCollection matches = Regex.Matches(line, @"\b(class|\*|\+|-|<|==|!=|>=|&&|\|\||\[\]|\(\)|;|:|{}|\+\+|for|switch|if|else|while|case|--|\.\w+)\b(?=(?:[^""\\]*(\\.|""([^""\\]*\\.)*[^""\\]*""))*[^""]*$)\b");
                    MatchCollection matches2 = Regex.Matches(line, @"(?:\+{1,2}|\-{1,2}|==|!=|\/|\*|>|<|<=|>=|;|:)");

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
                outputStr += $"\nOperator '{kvp.Key}' found {kvp.Value} times.";
            }

            fuloper += ysloper;
            InitializeComponent();
            ResOutput.Text += outputStr;
            ResOutput.Text += $"\nКоличество условных операторов {ysloper}";
            ResOutput.Text += $"\nКоличество операторов {fuloper}";
            ResOutput.Text += $"\nНасыщенность условными операторами {(ysloper / (double)fuloper)}";
            ResOutput.Text += $"\nМаксимальная глубина ветвления {maxDepth - 1}";
        }
    }
}
