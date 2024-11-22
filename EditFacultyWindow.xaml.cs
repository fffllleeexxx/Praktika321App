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
    public partial class EditFacultyWindow : Window
    {
        private praktika_321Entities context;
        private FacultyTableClass faculty;

        public EditFacultyWindow(praktika_321Entities context, FacultyTableClass faculty)
        {
            InitializeComponent();
            this.context = context;
            this.faculty = faculty;
            AbbreviationTextBox.Text = faculty.abbreviation;
            NameTextBox.Text = faculty.Name;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var facultyToUpdate = context.faculty.Find(faculty.abbreviation);
                if (facultyToUpdate != null)
                {
                    facultyToUpdate.Name = NameTextBox.Text;

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Faculty updated successfully!");
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

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                NameTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                NameTextBox.Background = Brushes.White;
            }

            return isValid;
        }
    }
}
