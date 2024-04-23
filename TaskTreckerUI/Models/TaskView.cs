using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TaskTrackerUI.Models
{
    public class TaskView
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public Models.TaskStatus StatusTask { get; set; }
        public DateTime? ApproximateDateOfCompleted { get; set; }
        public DateTime Date { get; set; }
        public string DatePropertyName { get; set; } = "Дата создания: ";
        public Brush Background { get; set; } = Brushes.White;
    }
}
