using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC_Verwaltung.Dashboard.UserControls
{
    /// <summary>
    /// Interaktionslogik für MonitoringUC.xaml
    /// </summary>
    public partial class MonitoringUC : UserControl
    {

        public SeriesCollection SeriesCollection { get; set; }
        public MonitoringUC()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Verfügbar",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true,
                    Fill = Brushes.Green
                },
                new PieSeries
                {
                    Title = "Bald verfügbar",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(6) },
                    DataLabels = true,
                    Fill = Brushes.Orange
                },
                new PieSeries
                {
                    Title = "Nicht verfügbar",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(10) },
                    DataLabels = true,
                    Fill = Brushes.Red
                }
                
            };

            DataContext = this;

        }
    }
}
