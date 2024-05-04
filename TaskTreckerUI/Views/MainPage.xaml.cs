using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        const string Info = """
            Task Trecker

            Описание:
            Программа для организации и распределении задач в команде.
            Данная программа поможет руководителям лучше организовывать работу сотрудников
            и следить за ходом исполнением работы. Рабочие же могут создавать собственные задачи и заметки.
            А так же остовлять коментарии и вложения к различным задачам.

            Возможности
            Программа позволяет:
               - Хранить историю заметок
               - Организовывать пользователей в команды (проекты)
               - Создавать и назначать задачи
               - Следить за ходом выполения задач

            Технологии
               - Asp Net Core
               - Entity Framework Core
               - Windows Presentation Foundation
            """;
        public MainPage()
        {
            InitializeComponent();
            Text_Info.Text = Info;
        }
        private void Open_vk(object sender, EventArgs e)
            =>Process.Start(new ProcessStartInfo("https://vk.com/mikov2003") { UseShellExecute = true });

        private void Open_git(object sender, EventArgs e)
            =>Process.Start(new ProcessStartInfo("https://github.com/artem6115/TaskTracker") { UseShellExecute = true });
        private void Open_tg(object sender, EventArgs e) 
            => Process.Start(new ProcessStartInfo("https://web.telegram.org/k/#@Artem6115") { UseShellExecute = true });

    }
}
