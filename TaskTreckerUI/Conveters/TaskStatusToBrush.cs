using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TaskTrackerUI.Conveters
{
    public class TaskStatusToBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (Models.TaskStatus)(int)value;
            switch (status)
            {
                case Models.TaskStatus.Free:
                    return Brushes.Orange;
                case Models.TaskStatus.Work:
                   return new SolidColorBrush(Color.FromRgb(1, 102, 204));
                case Models.TaskStatus.Pause:
                    return Brushes.Gray;
                case Models.TaskStatus.Blocked:
                   return Brushes.Red; 
                case Models.TaskStatus.Completed:
                    return Brushes.Green;
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
