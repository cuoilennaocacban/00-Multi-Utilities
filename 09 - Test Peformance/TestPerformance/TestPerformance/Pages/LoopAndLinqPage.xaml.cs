using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using TestPerformance.Annotations;

namespace TestPerformance.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoopAndLinqPage : INotifyPropertyChanged
    {
        private string _result;
        private ObservableCollection<DataModel> _dataModels; 

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

        public ObservableCollection<DataModel> DataModels
        {
            get { return _dataModels; }
            set
            {
                if (Equals(value, _dataModels)) return;
                _dataModels = value;
                OnPropertyChanged();
            }
        }

        public LoopAndLinqPage()
        {
            InitializeComponent();
            Loaded += LoopAndLinqPage_Loaded;
        }

        private void LoopAndLinqPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Prepair data
            DataModels = new ObservableCollection<DataModel>();
            Random r = new Random();
            for (int i = 0; i < 1000000; i++)
            {
                DataModel d = new DataModel
                {
                    Id = i,
                    Data = r.Next(0, 10000)
                };
                DataModels.Add(d);
            }
        }

        private void LinQOutside_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Begin test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string marked = "Marked";
            var queryResult = DataModels.Where(dataModel => dataModel.Data < 5000);
            foreach (DataModel dataModel in queryResult)
            {
                dataModel.Note = marked;
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("End test");
        }

        private void LinQInsize_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Begin test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string marked = "Marked";
            foreach (DataModel dataModel in DataModels.Where(dataModel => dataModel.Data < 5000))
            {
                dataModel.Note = marked;
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("End test");
        }

        private void Foreach_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Begin test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string marked = "Marked";
            foreach (DataModel dataModel in DataModels.Where(dataModel => dataModel.Data < 5000))
            {
                dataModel.Note = marked;
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("End test");
        }

        private void For_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Begin test");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string marked = "Marked";
            foreach (DataModel dataModel in DataModels)
            {
                if (dataModel.Data < 5000)
                {
                    dataModel.Note = marked;
                }
            }
            stopwatch.Stop();
            Result = stopwatch.ElapsedMilliseconds.ToString();
            Debug.WriteLine("End test");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class DataModel : INotifyPropertyChanged
    {
        private int _id;
        private int _data;
        private string _note;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public int Data
        {
            get { return _data; }
            set
            {
                if (value == _data) return;
                _data = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get { return _note; }
            set
            {
                if (value == _note) return;
                _note = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
