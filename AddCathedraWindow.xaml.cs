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
    public partial class AddCathedraWindow : Window
    {
        private praktika_321Entities context;

        public AddCathedraWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var cathedra = new cathedra
                {
                    Code = CodeTextBox.Text,
                    Name = NameTextBox.Text,
                    Faculty = FacultyTextBox.Text
                };

                context.cathedra.Add(cathedra);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Cathedra added successfully!");
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
                    context.Entry(cathedra).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(cathedra).State = System.Data.Entity.EntityState.Detached;
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

            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                NameTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                NameTextBox.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(FacultyTextBox.Text))
            {
                FacultyTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                FacultyTextBox.Background = Brushes.White;
            }

            return isValid;
        }
    }
}
