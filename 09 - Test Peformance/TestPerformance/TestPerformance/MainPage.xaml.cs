using Windows.UI.Xaml;
using TestPerformance.Pages;

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
        }

        private void StringTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (StringPage));
        }

        private void LoopAndLinqButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoopAndLinqPage));
        }


        private void TryCatButon_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TryCatchPage));
        }
    }
}
