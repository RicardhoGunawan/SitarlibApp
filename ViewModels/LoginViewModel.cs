using System.Windows.Input;
using SitarLib.Helpers;
using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Login - SitarLib";
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object obj)
        {
            return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object obj)
        {
            var user = DataService.AuthenticateUser(Username, Password);
            if (user != null)
            {
                NavigationService.NavigateTo("Dashboard");
            }
            else
            {
                ErrorMessage = "Invalid username or password";
            }
        }
    }
}