using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using RestaurantDesktopClient.Services;

namespace RestaurantDesktopClient.Views.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthRepository _authRepository;

        public LoginViewModel(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            initCommands();
        }

        private void initCommands()
        {
            throw new NotImplementedException();
        }

        public RelayCommand LoginCommand;
    }
}
