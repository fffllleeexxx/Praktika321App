using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Praktika321App
{
    public partial class LoginWindow : Window
    {
        private praktika_321Entities context;

        public LoginWindow()
        {
            InitializeComponent();
            context = new praktika_321Entities();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            if (CheckCredentials(login, password, out string role))
            {
                MainWindow mainWindow = new MainWindow(role);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid login or password");
            }
        }

        private void QRButton_Click(object sender, RoutedEventArgs e)
        {
            QRWindow qRWindow = new QRWindow();
            qRWindow.Show();
        }

        private bool CheckCredentials(string login, string password, out string role)
        {
            role = null;


            var student = context.Ученики.FirstOrDefault(s => s.ID.ToString() == login && s.Password == password);
            if (student != null)
            {
                role = "Student";
                return true;
            }


            var academic = context.Academics.FirstOrDefault(a => a.ID.ToString() == login && a.Password == password);
            if (academic != null)
            {
                role = "Teacher";
                return true;
            }


            var manager = context.Cathedra_Manager.FirstOrDefault(m => m.Tab_Number.ToString() == login && m.Password == password);
            if (manager != null)
            {
                role = "HeadOfDepartment";
                return true;
            }


            var engineer = context.Engeneer.FirstOrDefault(en => en.Tab_Number.ToString() == login && en.Password == password);
            if (engineer != null)
            {
                role = "Admin";
                return true;
            }

            return false;
        }
    }
}
