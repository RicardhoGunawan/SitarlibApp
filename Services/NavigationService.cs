using System;
using System.Collections.Generic;
using SitarLib.ViewModels;

namespace SitarLib.Services
{
    public class NavigationService
    {
        private readonly Dictionary<string, Func<BaseViewModel>> _viewModels;
        public event Action<BaseViewModel> OnViewModelChanged;

        public NavigationService()
        {
            _viewModels = new Dictionary<string, Func<BaseViewModel>>();
        }

        public void Register(string viewModelName, Func<BaseViewModel> createViewModel)
        {
            _viewModels[viewModelName] = createViewModel;
        }

        public void NavigateTo(string viewModelName)
        {
            if (_viewModels.ContainsKey(viewModelName))
            {
                var viewModel = _viewModels[viewModelName]();
                OnViewModelChanged?.Invoke(viewModel);
            }
        }
    }
}