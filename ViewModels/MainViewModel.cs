using System.Collections.Generic;
using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public MainViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "SitarLib - Library Management System";
            
            NavigationService.OnViewModelChanged += viewModel => CurrentViewModel = viewModel;
            
            // Navigate to login by default
            NavigationService.NavigateTo("Login");
        }
    }
}