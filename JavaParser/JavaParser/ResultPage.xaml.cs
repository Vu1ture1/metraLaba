using JavaParser.Parse;
using System.Diagnostics;
namespace JavaParser;

public partial class ResultPage : ContentPage
{
	public ResultPage() 
	{
        string filePath = @"C:\Users\Àנע¸ל\Desktop\JavaCode.txt";
        string str = File.ReadAllText(filePath);
        string outputStr = "";
        ClassParse parser = new ClassParse();
        List<string> javaCode = parser.SplitString(str);
        List<CodeElement> result = parser.Parse(javaCode);
        int count = 0;
        int countt = 0;
        int countoperand = 0;
        int countoperator = 0;
        int operatorQuantity = 0;
        int operandQuantity = 0;
        outputStr += "Name\t\tQuantity\tType\n";
        Debug.WriteLine("Name\t\tQuantity\tType");
        foreach (var item in result)
        {
           
            if (item.Quantity != 0)
            {
                if (!outputStr.Contains($"{item.Name}\t\t{item.Quantity}\t\t{item.Type}\n"))
                {
                    if (item.Type == "operator") { countoperator++; operatorQuantity += item.Quantity; }
                    if (item.Type == "operand") { countoperand++; operandQuantity += item.Quantity; }
                    outputStr += $"{item.Name}\t\t{item.Quantity}\t\t{item.Type}\n";
                }
                Debug.WriteLine($"{item.Name}\t\t{item.Quantity}\t\t{item.Type}");
            }
        }
        int Diction = countoperand + countoperator;
        int Length = operandQuantity + operatorQuantity;
        int Volume = (int)(Length * Math.Log2(Diction));
        outputStr += $"Ñכמגאנü ןנמדנאללû: {Diction}\nÄכטםא ןנמדנאללû: {Length}\nÎבתול ןנמדנאללû: {Volume}";
        InitializeComponent();
        ResOutput.Text = outputStr;
	}
}