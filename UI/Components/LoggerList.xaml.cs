using DataLayer.Database;
using DataLayer.Others;
using Microsoft.Extensions.Logging.Abstractions;
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
            LoadAllLogs();
        }

        private void LoadAllLogs()
        {
            using (var context = new DatabaseContext())
            {
                logs.ItemsSource = context.LogEntries.ToList();
            }
        }

        private void OnFilterClicked(object sender, RoutedEventArgs e)
        {
            int userId;
            if (int.TryParse(userIdInput.Text, out userId))
            {
                using (var context = new DatabaseContext())
                {
                    var filteredLogs = context.LogEntries.Where(log => log.UserId == userId).ToList();
                    logs.ItemsSource = filteredLogs;
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid User ID.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnClearFilterClicked(object sender, RoutedEventArgs e)
        {
            userIdInput.Clear();
            LoadAllLogs(); // Reload all logs to clear the filter
        }
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid grid && grid.SelectedItem is LogEntry logEntry)
            {
                var userId = logEntry.UserId;
                var userName = logEntry.User?.Name;

                // Building the message with User ID and Name
                var messageBuilder = new StringBuilder();
                messageBuilder.AppendLine($"Timestamp: {logEntry.Timestamp}");
                messageBuilder.AppendLine($"Level: {logEntry.Level}");
                messageBuilder.AppendLine($"Message: {logEntry.Message}");
                messageBuilder.AppendLine($"User ID: {userId}");
                messageBuilder.AppendLine($"User Name: {userName ?? "Unknown"}"); // Using "Unknown" if userName is null.

                MessageBox.Show(messageBuilder.ToString(), "Log Details", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
