using DataLayer.Database;
using DataLayer.Others;
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

namespace UI.Components
{
    /// <summary>
    /// Interaction logic for LoggerList.xaml
    /// </summary>
    public partial class LoggerList : UserControl
    {
        public LoggerList()
        {
            InitializeComponent();

            using (var context = new DatabaseContext())
            {
                var records = context.LogEntries.ToList();
                logs.DataContext = records;
            }
        }
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem is LogEntry logEntry)
            {
                MessageBox.Show($"Timestamp: {logEntry.Timestamp}\nLevel: {logEntry.Level}\nMessage: {logEntry.Message}",
                                "Log Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
