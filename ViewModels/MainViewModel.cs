using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; private set; }

        public MainViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "SitarLib - Library Management System";
            
            // Set up navigation service event
            navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;
            
            // Navigate to Dashboard on startup
            navigationService.NavigateTo("Dashboard");
        }

        private void OnCurrentViewModelChanged(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
            OnPropertyChanged(nameof(CurrentViewModel));
            
            // Update title based on current view
            if (CurrentViewModel != null)
            {
                Title = CurrentViewModel.Title;
            }
        }
    }
}