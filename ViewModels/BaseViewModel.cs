using SitarLib.Helpers;
using SitarLib.Services;


namespace SitarLib.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected readonly DataService DataService;
        protected readonly DialogService DialogService;
        protected readonly NavigationService NavigationService;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public BaseViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
        {
            DataService = dataService;
            DialogService = dialogService;
            NavigationService = navigationService;
        }
    }
}