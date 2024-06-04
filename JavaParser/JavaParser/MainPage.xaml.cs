using System.Diagnostics;

namespace JavaParser
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnParseClicked(object sender, EventArgs e)
        {
            
            
            var resultWindow = new Window
            {
                Page = new ResultPage() { }
            };

            Application.Current.OpenWindow(resultWindow);
        }

       
    }

}
