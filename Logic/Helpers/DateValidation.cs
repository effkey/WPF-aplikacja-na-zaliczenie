using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekt_WPF_TODO_app.Logic.Helpers
{
    public class DateValidation
    {
        public static DateTime Yesterday => DateTime.Today.AddDays(-1);
    }
}
