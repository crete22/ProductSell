using System.Windows;

namespace Product_sell
{
    public partial class RegisterWindow : Window
    {
        private CustomerFacade customerFacade = new CustomerFacade();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            string surname = SurnameBox.Text.Trim();
            string name = NameBox.Text.Trim();
            string patronymic = PatronymicBox.Text.Trim();
            string phone = PhoneBox.Text.Trim();
            string email = EmailBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");
                return;
            }

            bool result = customerFacade.Register(login, password, surname, name, patronymic, phone, email);

            if (result)
            {
                MessageBox.Show("Регистрация успешна! Теперь вы можете войти.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка регистрации. Возможно, такой логин уже существует.");
            }
        }
    }
}