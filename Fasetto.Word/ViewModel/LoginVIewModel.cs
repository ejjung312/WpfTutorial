using CommunityToolkit.Mvvm.Input;
using System.Security;
using System.Windows;

namespace Fasetto.Word
{
    public class LoginViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public bool LoginIsRunning { get; set; }

        public RelayCommand<object> LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<object>(async (parameter) => await Login(parameter));
        }

        public async Task Login(object parameter)
        {
            await RunCommand(() => this.LoginIsRunning, async () =>
            {
                await Task.Delay(5000);

                var email = this.Email;
                var pass = (parameter as IHavePassword).SecurePassword.Unsecure();
            });
        }
    }
}
