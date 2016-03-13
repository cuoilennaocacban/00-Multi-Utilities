using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI.Xaml;
using TestPerformance.Annotations;

namespace TestPerformance.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StringPage : INotifyPropertyChanged
    {
        private string _result;

        public string Result
        {
            get { return _result; }
            set
            {
                if (value == _result) return;
                _result = value;
                OnPropertyChanged();
            }
        }

        public StringPage()
        {
            InitializeComponent();
        }

        private void PlusOperationButton_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start the test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Test the string concat
            for (int i = 0; i < 1000000; i++)
            {
                string s = "Tuan Tran Van";
                s += " welcome to my test";
                s += " and hello world";
                s += i;
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("Test end");
        }

        private void StringBuilder_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start the test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Test the string concat
            for (int i = 0; i < 1000000; i++)
            {
                StringBuilder s = new StringBuilder();
                s.Append("Tuan Tran Van");
                s.Append(" welcome to my test");
                s.Append(" and hello world");
                s.Append(i);
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("Test end");
        }

        private void stringConcat_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start the test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Test the string concat
            for (int i = 0; i < 1000000; i++)
            {
                string s = string.Concat("Tuan Tran Van", " welcome to my test", " and hello world", i);
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("Test end");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
