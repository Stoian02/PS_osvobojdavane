﻿using System.Collections.ObjectModel;
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

        public string? Level { get; set; }
        public string? Message { get; set; }
        public int UserId { get; set; }

        public ICommand FilterCommand { get; }
        public ICommand ClearFilterCommand { get; }
        public ICommand AddLogCommand { get; private set; }

        public ObservableCollection<LogEntry> Logs
        {
            get => _logs;
            set => SetProperty(ref _logs, value);
        }

        public void AddLog(LogEntry log)
        {
            using (var context = new DatabaseContext())
            {
                context.LogEntries.Add(log);
                context.SaveChanges();
            }
            Logs.Add(log);
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

        public LoggerViewModel()
        {
            FilterCommand = new DelegateCommand(ExecuteFilter);
            ClearFilterCommand = new DelegateCommand(() => LoadAllLogs());
            AddLogCommand = new DelegateCommand(AddLogManually);
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
            using (var context = new DatabaseContext())
            {
                Logs = new ObservableCollection<LogEntry>(context.LogEntries.ToList());
            }
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
                context.LogEntries.Add(logEntry);
                context.SaveChanges();
            }

            _logs.Add(logEntry);
        }
    }
}
