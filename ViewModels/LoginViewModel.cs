using System;
using System.Threading.Tasks;
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
            set
            {
                SetProperty(ref _username, value);
                // Update status command
                ((AsyncRelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ((AsyncRelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand LoginCommand { get; }

        public event EventHandler LoginSuccessful;

        public LoginViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Login - SitarLib";
            LoginCommand = new AsyncRelayCommand(ExecuteLoginAsync, CanExecuteLogin);
        }

        private bool CanExecuteLogin(object obj)
        {
            return !IsBusy && !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password);
        }

        private async Task ExecuteLoginAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                // Simulasi delay atau proses login berat bisa ganti dengan DataService.AuthenticateUserAsync jika ada
                await Task.Delay(1000);

                var user = DataService.AuthenticateUser(Username, Password);

                if (user != null)
                {
                    LoginSuccessful?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    ErrorMessage = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                DialogService.ShowError("Login Error", $"An error occurred during login: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
                ((AsyncRelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }
    }

    // Implementasi AsyncRelayCommand untuk async command
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Predicate<object> _canExecute;
        private bool _isExecuting;

        public AsyncRelayCommand(Func<Task> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async void Execute(object parameter)
        {
            _isExecuting = true;
            RaiseCanExecuteChanged();
            try
            {
                await _execute();
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
