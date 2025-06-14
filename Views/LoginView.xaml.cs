﻿using System.Windows;
using System.Windows.Controls;
using SitarLib.ViewModels;

namespace SitarLib.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            
            // Handle binding the password which can't be directly bound
            Loaded += (s, e) =>
            {
                if (DataContext is LoginViewModel viewModel)
                {
                    PasswordBox.PasswordChanged += (sender, args) =>
                    {
                        viewModel.Password = PasswordBox.Password;
                    };
                }
            };
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

    }
}