using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    public partial class AddEmployeeWindow : Window
    {
        private praktika_321Entities context;

        public AddEmployeeWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var employee = new Employee
                {
                    Tab_Number = int.Parse(TabNumberTextBox.Text),
                    Code = CodeTextBox.Text,
                    FullName = FullNameTextBox.Text,
                    Position = PositionTextBox.Text,
                    Salary = decimal.Parse(SalaryTextBox.Text),
                    Chief = int.Parse(ChiefTextBox.Text)
                };

                context.Employee.Add(employee);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Employee added successfully!");
                    this.Close();
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    while (innerException.InnerException != null)
                    {
                        innerException = innerException.InnerException;
                    }
                    MessageBox.Show($"Error: {innerException.Message}");
                    context.Entry(employee).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(employee).State = System.Data.Entity.EntityState.Detached;
                }
            }
            else
            {
                MessageBox.Show("Please correct the highlighted fields.");
            }
        }

        private bool ValidateInput()
        {
            bool isValid = true;

            if (!int.TryParse(TabNumberTextBox.Text, out _))
            {
                TabNumberTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                TabNumberTextBox.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(CodeTextBox.Text))
            {
                CodeTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                CodeTextBox.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(FullNameTextBox.Text))
            {
                FullNameTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                FullNameTextBox.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(PositionTextBox.Text))
            {
                PositionTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                PositionTextBox.Background = Brushes.White;
            }

            if (!decimal.TryParse(SalaryTextBox.Text, out _))
            {
                SalaryTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                SalaryTextBox.Background = Brushes.White;
            }

            if (!int.TryParse(ChiefTextBox.Text, out _))
            {
                ChiefTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                ChiefTextBox.Background = Brushes.White;
            }

            return isValid;
        }
    }
}
