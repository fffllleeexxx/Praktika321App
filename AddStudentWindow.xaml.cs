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
    public partial class AddStudentWindow : Window
    {
        private praktika_321Entities context;

        public AddStudentWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var student = new Student
                {
                    Reg_Number = int.Parse(RegNumberTextBox.Text),
                    Fullname = FullNameTextBox.Text,
                    Number = NumberTextBox.Text
                };

                context.Student.Add(student);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Student added successfully!");
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
                    context.Entry(student).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(student).State = System.Data.Entity.EntityState.Detached;
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

            if (!int.TryParse(RegNumberTextBox.Text, out _))
            {
                RegNumberTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                RegNumberTextBox.Background = Brushes.White;
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

            if (string.IsNullOrWhiteSpace(NumberTextBox.Text))
            {
                NumberTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                NumberTextBox.Background = Brushes.White;
            }

            return isValid;
        }
    }
}
