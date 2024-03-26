using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI_MVVM.ViewModels
{
    public class ViewModelLocator
    {
        private LoggerViewModel _loggerViewModel;
        private StudentsViewModel _studentsViewModel;


        public LoggerViewModel LoggerViewModel
        {
            get
            {
                return _loggerViewModel;
            }
        }
        // Will be used for the shared ViewModel as well
        public StudentsViewModel StudentsViewModel
        {
            get
            {
                return _studentsViewModel;
            }
        }

        public ViewModelLocator()
        {
            _loggerViewModel = new LoggerViewModel();
            _studentsViewModel = new StudentsViewModel();
        }
    }
}
