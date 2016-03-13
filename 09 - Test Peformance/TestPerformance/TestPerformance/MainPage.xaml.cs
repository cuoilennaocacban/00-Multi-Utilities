using System;
using System.Diagnostics;

namespace TestPerformance
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Debug.WriteLine("Start the test");
            //Test the string concat
            for (int i = 0; i < 100000; i++)
            {
                //string s = "Tuan Tran Van";
                //s += " welcome to my test";
                //s += " and hello world";
                //s += i;
            }
            Debug.WriteLine("Test end");
        }
    }
}
