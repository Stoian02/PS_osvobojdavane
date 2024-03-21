using DataLayer.Database;
using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVVM.Helpers;

namespace UI_MVVM.ViewModels
{
    public class StudentsViewModel : ObservableObject
    {
        private ObservableCollection<DatabaseUser> _students;

        public ObservableCollection<DatabaseUser> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }

        public StudentsViewModel()
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            // Use the DataLayer to populate the _students collection
            using (var context = new DatabaseContext())
            {
                Students = new ObservableCollection<DatabaseUser>(context.Users.ToList());
            }
        }

    }
}
