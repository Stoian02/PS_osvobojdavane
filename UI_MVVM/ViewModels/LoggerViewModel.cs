using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using DataLayer.Database;
using DataLayer.Others;
using UI_MVVM.Helpers;

namespace UI.ViewModels
{
    public class LoggerViewModel : ObservableObject
    {
        public ObservableCollection<LogEntry> _logs;
        private string _userIdFilter;

        private string? _level;
        private string? _message;
        private int _userId;

        public string? Level 
        { 
            get => _level;
            set => SetProperty(ref _level, value);
        }
        public string? Message 
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        public int UserId 
        { 
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public ICommand FilterCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand AddLogCommand { get; private set; }

        public ObservableCollection<LogEntry> Logs
        {
            get => _logs;
            set => SetProperty(ref _logs, value);
        }

        public LoggerViewModel()
        {
            _level = "Info";
            _message = String.Empty;
            _userId = 1;

            FilterCommand = new DelegateCommand(ExecuteFilter);
            ClearFilterCommand = new DelegateCommand(() => LoadAllLogs());
            AddLogCommand = new DelegateCommand(AddLogManually);
            LoadAllLogs();
        }
        public string UserIdFilter
        {
            get => _userIdFilter;
            set
            {
                if (_userIdFilter != value)
                {
                    _userIdFilter = value;
                    OnPropertyChanged(nameof(UserIdFilter));
                    FilterLogs();
                }
            }
        }

        public void AddLog(LogEntry log)
        {
            DatabaseService.Add(log);
            Logs.Add(log);
        }

        private void FilterLogs()
        {
            if (int.TryParse(_userIdFilter, out int userId))
            {
                using (var context = new DatabaseContext())
                {
                    Logs = new ObservableCollection<LogEntry>(
                        context.LogEntries.Where(log => log.UserId == userId).ToList());
                }
            }
            else
            {
                LoadAllLogs();
            }
        }

        public void ClearFilter()
        {
            UserIdFilter = string.Empty;
            LoadAllLogs();
        }
        private void ExecuteFilter()
        {
            if (int.TryParse(UserIdFilter, out int userId))
            {
                using (var context = new DatabaseContext())
                {
                    var filteredLogs = context.LogEntries.Where(log => log.UserId == userId).ToList();
                    Logs = new ObservableCollection<LogEntry>(filteredLogs);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid User ID.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LoadAllLogs()
        {
            Logs = new ObservableCollection<LogEntry>(DatabaseService.GetAllLogs());
        }

        public void AddLogManually()
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Level = Level,
                Message = Message,
                UserId = UserId
            };

            using (var context = new DatabaseContext())
            {
                var userExists = context.Users.Any(u => u._id == UserId);
                if (!userExists)
                {
                    // Show a message if the user ID is not found
                    MessageBox.Show("Please enter a valid User ID.", "User Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                context.LogEntries.Add(logEntry);
                context.SaveChanges();
            }

            Logs.Add(logEntry);

            // Clear the input fields
            Level = null;
            Message = null;
            UserId = 0;
        }
    }
}
