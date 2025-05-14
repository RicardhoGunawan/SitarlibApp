using System.Collections.ObjectModel;
using System.Windows.Input;
using SitarLib.Helpers;
using SitarLib.Models;
using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class MemberViewModel : BaseViewModel
    {
        private ObservableCollection<Member> _members;
        public ObservableCollection<Member> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }

        private Member _selectedMember;
        public Member SelectedMember
        {
            get => _selectedMember;
            set
            {
                if (SetProperty(ref _selectedMember, value) && value != null)
                {
                    // Create a clone for editing
                    CurrentMember = new Member
                    {
                        Id = value.Id,
                        MemberCode = value.MemberCode,
                        FullName = value.FullName,
                        Address = value.Address,
                        PhoneNumber = value.PhoneNumber,
                        Email = value.Email,
                        RegistrationDate = value.RegistrationDate,
                        MembershipExpiry = value.MembershipExpiry,
                        IsActive = value.IsActive
                    };
                    IsEditing = true;
                }
            }
        }

        private Member _currentMember;
        public Member CurrentMember
        {
            get => _currentMember;
            set => SetProperty(ref _currentMember, value);
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterMembers();
                }
            }
        }

        public ICommand AddNewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }

        public MemberViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Manage Members - SitarLib";
            
            AddNewCommand = new RelayCommand(_ => ExecuteAddNew());
            SaveCommand = new RelayCommand(_ => ExecuteSave(), _ => CanSave());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => SelectedMember != null);
            CancelCommand = new RelayCommand(_ => ExecuteCancel());
            NavigateToDashboardCommand = new RelayCommand(_ => NavigationService.NavigateTo("Dashboard"));
            
            LoadMembers();
            ExecuteAddNew(); // Start with a new member form
        }

        private void LoadMembers()
        {
            IsBusy = true;
            Members = new ObservableCollection<Member>(DataService.GetAllMembers());
            IsBusy = false;
        }

        private void FilterMembers()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadMembers();
                return;
            }

            var searchTerm = SearchText.ToLower();
            var filteredMembers = DataService.GetAllMembers().Where(m =>
                m.FullName.ToLower().Contains(searchTerm) ||
                m.MemberCode.ToLower().Contains(searchTerm) ||
                m.Email.ToLower().Contains(searchTerm) ||
                m.PhoneNumber.Contains(searchTerm)
            );
            
            Members = new ObservableCollection<Member>(filteredMembers);
        }

        private void ExecuteAddNew()
        {
            CurrentMember = new Member
            {
                MemberCode = GenerateNewMemberCode(),
                IsActive = true,
                RegistrationDate = DateTime.Now,
                MembershipExpiry = DateTime.Now.AddYears(1)
            };
            IsEditing = false;
            SelectedMember = null;
        }

        private string GenerateNewMemberCode()
        {
            // Generate a new member code (e.g., M001, M002, etc.)
            int nextId = 1;
            if (Members != null && Members.Count > 0)
            {
                nextId = Members.Max(m => int.Parse(m.MemberCode.Substring(1))) + 1;
            }
            return $"M{nextId:000}";
        }

        private bool CanSave()
        {
            return CurrentMember != null &&
                   !string.IsNullOrWhiteSpace(CurrentMember.FullName) &&
                   !string.IsNullOrWhiteSpace(CurrentMember.PhoneNumber);
        }

        private void ExecuteSave()
        {
            if (IsEditing)
            {
                DataService.UpdateMember(CurrentMember);
                DialogService.ShowMessage("Member updated successfully!");
            }
            else
            {
                DataService.AddMember(CurrentMember);
                DialogService.ShowMessage("Member added successfully!");
            }
            
            LoadMembers();
            ExecuteAddNew(); // Reset form for a new entry
        }

        private void ExecuteDelete()
        {
            if (SelectedMember != null)
            {
                bool confirm = DialogService.ShowConfirmation($"Are you sure you want to delete '{SelectedMember.FullName}'?");
                if (confirm)
                {
                    DataService.DeleteMember(SelectedMember.Id);
                    DialogService.ShowMessage("Member deleted successfully!");
                    LoadMembers();
                    ExecuteAddNew();
                }
            }
        }

        private void ExecuteCancel()
        {
            ExecuteAddNew();
        }
    }
}