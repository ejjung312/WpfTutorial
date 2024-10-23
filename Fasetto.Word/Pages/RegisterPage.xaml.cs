using System.Security;

namespace Fasetto.Word
{
    public partial class RegisterPage : BasePage<LoginViewModel>, IHavePassword
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
