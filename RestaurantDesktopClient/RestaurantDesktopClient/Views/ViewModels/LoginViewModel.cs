using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RestaurantDesktopClient.Services;

namespace RestaurantDesktopClient.Views.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthRepository _authRepository;

        public RelayCommand<PasswordBox> LoginCommand { get; set; }

        public string Username { get; set; }
        public LoginViewModel(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            InitCommands();
        }

        private void InitCommands()
        {
            LoginCommand = new RelayCommand<PasswordBox>(LoginPressed);
        }



        private void LoginPressed(PasswordBox passwordBox)
        {
            var success = _authRepository.Authenticate(Username, passwordBox.Password);
            passwordBox.Clear();
            if (success)
            {
                MainWindow.ChangeFrame(new MainMenu());
                
                return;
            };
            MessageBox.Show("Invalid Login");
        }
    }
}
