﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskTrackerUI.Services;

namespace TaskTrackerUI.Views
{
    /// <summary>
    /// Логика взаимодействия для ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : Window
    {
        public CancellationToken cancelationToken { get; set; }
        public ConnectionWindow()
        {
            cancelationToken = new CancellationToken();
            InitializeComponent();
            Ip.Visibility = Visibility.Hidden;
            Ip_label.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cancelationToken.ThrowIfCancellationRequested();
            Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
            => FindServer();
        private async Task FindServer(string? ip = null)
        {
            ErrorText.Visibility= Visibility.Hidden;
            LoadImage.Visibility = Visibility.Visible;
            RetryButton.Visibility = Visibility.Hidden;

            if (!(await LocalConnectionService.FindServer(ip,cancelationToken)))
            {
                RetryButton.Visibility = Visibility.Visible;
                LoadImage.Visibility = Visibility.Hidden;
                ErrorText.Visibility = Visibility.Visible;
                Ip.Visibility = Visibility.Visible;
                Ip_label.Visibility = Visibility.Visible;


            }
            else
                await Authorize();


        }
        private async Task Authorize()
        {
           InfoText.Text = "Получение учетной записи";
           await AuthService.TryAuthorizeWithJwtTokens();
           Close();
            
        }

        private async void Retry_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Ip.Text.Contains(':'))
                Ip.Text += ":5050";
            await FindServer(Ip.Text.Trim());
        }
    }
}
