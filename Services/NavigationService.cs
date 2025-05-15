using System;
using System.Collections.Generic;
using SitarLib.ViewModels;

namespace SitarLib.Services
{
    public class NavigationService
    {
        private readonly Dictionary<string, Func<BaseViewModel>> _viewModels;
        
        public event Action<BaseViewModel> CurrentViewModelChanged;

        public NavigationService()
        {
            _viewModels = new Dictionary<string, Func<BaseViewModel>>();
        }

        public void Register(string viewName, Func<BaseViewModel> createViewModel)
        {
            _viewModels[viewName] = createViewModel;
        }

        public void NavigateTo(string viewName)
        {
            if (_viewModels.ContainsKey(viewName))
            {
                var viewModel = _viewModels[viewName]();
                CurrentViewModelChanged?.Invoke(viewModel);
            }
            else
            {
                throw new ArgumentException($"View model {viewName} is not registered.", nameof(viewName));
            }
        }
    }
}