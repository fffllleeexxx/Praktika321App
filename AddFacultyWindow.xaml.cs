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
    public partial class AddFacultyWindow : Window
    {
        private praktika_321Entities context;

        public AddFacultyWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var faculty = new faculty
                {
                    abbreviation = AbbreviationTextBox.Text,
                    Name = NameTextBox.Text
                };

                context.faculty.Add(faculty);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Faculty added successfully!");
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
                    context.Entry(faculty).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(faculty).State = System.Data.Entity.EntityState.Detached;
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

            if (string.IsNullOrWhiteSpace(AbbreviationTextBox.Text))
            {
                AbbreviationTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                AbbreviationTextBox.Background = Brushes.White;
            }

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
