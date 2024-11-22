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
    public partial class EditCathedraWindow : Window
    {
        private praktika_321Entities context;
        private CathedraTableClass cathedra;

        public EditCathedraWindow(praktika_321Entities context, CathedraTableClass cathedra)
        {
            InitializeComponent();
            this.context = context;
            this.cathedra = cathedra;
            CodeTextBox.Text = cathedra.Code;
            NameTextBox.Text = cathedra.Name;
            FacultyTextBox.Text = cathedra.Faculty;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var cathedraToUpdate = context.cathedra.Find(cathedra.Code);
                if (cathedraToUpdate != null)
                {
                    cathedraToUpdate.Name = NameTextBox.Text;
                    cathedraToUpdate.Faculty = FacultyTextBox.Text;

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Cathedra updated successfully!");
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
