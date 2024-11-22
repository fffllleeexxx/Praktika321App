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
    public partial class EditEmployeeWindow : Window
    {
        private praktika_321Entities context;
        private EmployeeTableClass employee;

        public EditEmployeeWindow(praktika_321Entities context, EmployeeTableClass employee)
        {
            InitializeComponent();
            this.context = context;
            this.employee = employee;
            TabNumberTextBox.Text = employee.Tab_Number.ToString();
            CodeTextBox.Text = employee.Code;
            FullNameTextBox.Text = employee.FullName;
            PositionTextBox.Text = employee.Position;
            SalaryTextBox.Text = employee.Salary.ToString();
            ChiefTextBox.Text = employee.Chief.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var employeeToUpdate = context.Employee.Find(employee.Tab_Number);
                if (employeeToUpdate != null)
                {
                    employeeToUpdate.Code = CodeTextBox.Text;
                    employeeToUpdate.FullName = FullNameTextBox.Text;
                    employeeToUpdate.Position = PositionTextBox.Text;
                    employeeToUpdate.Salary = decimal.Parse(SalaryTextBox.Text);
                    employeeToUpdate.Chief = int.Parse(ChiefTextBox.Text);

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Employee updated successfully!");
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
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
