using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JavaParser.Parse
{
    public class CodeElement
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
    }

    public class ClassParse
    {
        private List<CodeElement> res = new List<CodeElement>();

        public List<CodeElement> Parse(List<string> javaCode)
        {
            string[] operators = {
            "+", "-", "*", "/", "%", "<", ">", "=", "==", "!=", ">=", "<=", "&&", "||", "|", "&", "<<", ">>", "^",
            "+=", "-=", "*=", "/=", "&=", "%=", "|=", "^=", ">>=", "<<=", "++", "--", "{}", "[]", "()", ";", "while",
            "for", "if", "else", "elseif", "switch", ":"
        };

            foreach (string str in javaCode)
            {
                foreach (string op in operators)
                {
                    if (op == "++")
                    {
                        if (Regex.IsMatch(str, @"\w+\+\+") || Regex.IsMatch(str, @"\+\+\w+"))
                        {
                            IncrementElementQuantity(op);
                        }
                    }
                    else if (op == "--")
                    {
                        if (Regex.IsMatch(str, @"\w+\-\-") || Regex.IsMatch(str, @"\-\-\w+"))
                        {
                            IncrementElementQuantity(op);
                        }
                    }
                    else if (op == "for" || op == "while" || op == ";" || op == "else" || op == "if" || op == "elseif" || op == "switch")
                    {
                        if (str.Contains(op))
                        {
                            IncrementElementQuantity(op);
                        }
                    }
                    else if (op == "{}")
                    {
                        if (str.Contains(op[0]) || str.Contains(op[1]))
                        {
                            IncrementElementQuantity(op);
                        }
                    }
                    else if (op == "[]" || op == "()")
                    {
                        Regex reg = new Regex("\\" + op[0]);
                        MatchCollection matches = reg.Matches(str);
                        IncrementElementQuantity(op, matches.Count);
                    }
                    else if (op == "||")
                    {
                        Regex reg = new Regex("(\\w+)?\\s(\\|\\|)\\s(\\w+)?");
                        MatchCollection matches = reg.Matches(str);
                        IncrementElementQuantity(op, matches.Count);
                    }
                    else
                    {
                        Regex reg = new Regex($"(\\w+)?\\s({Regex.Escape(op)})\\s(\\w+)?");
                        MatchCollection matches = reg.Matches(str);
                        IncrementElementQuantity(op, matches.Count);
                    }
                }
            }

            AddOperands(javaCode);
            FindOperands(javaCode);

            foreach (var element in res)
            {
                if (element.Name == "{}")
                {
                    element.Quantity /= 2;
                }
            }

            return res;
        }

        public List<String> SplitString(String str)
        {
            List<string> substrings = new List<string>();

            string[] parts = str.Split('\n');

            foreach (var part in parts)
            {
                substrings.Add(part);
            }

            return substrings;
        }
        private void IncrementElementQuantity(string name, int increment = 1)
        {
            foreach (var item in res)
            {
                if (item.Name == name)
                {
                    item.Quantity += increment;
                    return;
                }
            }

            res.Add(new CodeElement { Name = name, Quantity = increment, Type = "operator" });
        }

        private void AddOperands(List<string> javaCode)
        {
            Regex reg = new Regex(@"\b(?:int|short|byte|long|float|double|boolean|char|String)(?:\[)?(?:\])?\s+(\w+)\s*=");
            foreach (var str in javaCode)
            {
                Match match = reg.Match(str);
                if (match.Success)
                {
                    string operandName = match.Groups[1].Value;
                    if (!res.Contains(new CodeElement { Name = operandName, Quantity = 0, Type = "operand" }))
                        res.Add(new CodeElement { Name = operandName, Quantity = 0, Type = "operand" });
                }
            }
        }

        private void FindOperands(List<string> javaCode)
        {
            foreach (var str in javaCode)
            {
                foreach (var item in res)
                {
                    Regex reg = new Regex($"\\b{Regex.Escape(item.Name)}\\b", RegexOptions.IgnoreCase);
                    MatchCollection matches = reg.Matches(str);
                    item.Quantity += matches.Count;
                }
            }
        }
    }
}

