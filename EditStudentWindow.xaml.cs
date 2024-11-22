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
    public partial class EditStudentWindow : Window
    {
        private praktika_321Entities context;
        private StudentTableClass student;

        public EditStudentWindow(praktika_321Entities context, StudentTableClass student)
        {
            InitializeComponent();
            this.context = context;
            this.student = student;
            RegNumberTextBox.Text = student.Reg_Number.ToString();
            FullNameTextBox.Text = student.Fullname;
            NumberTextBox.Text = student.Number;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var studentToUpdate = context.Student.Find(student.Reg_Number);
                if (studentToUpdate != null)
                {
                    studentToUpdate.Fullname = FullNameTextBox.Text;
                    studentToUpdate.Number = NumberTextBox.Text;

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Student updated successfully!");
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
