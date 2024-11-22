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
    public partial class EditSpecializationWindow : Window
    {
        private praktika_321Entities context;
        private SpecializationTableClass specialization;

        public EditSpecializationWindow(praktika_321Entities context, SpecializationTableClass specialization)
        {
            InitializeComponent();
            this.context = context;
            this.specialization = specialization;
            NumberTextBox.Text = specialization.Number;
            NameSpecializationTextBox.Text = specialization.Name_Specialization;
            CodeTextBox.Text = specialization.Code;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var specializationToUpdate = context.Specialization.Find(specialization.Number);
                if (specializationToUpdate != null)
                {
                    specializationToUpdate.Name_Specialization = NameSpecializationTextBox.Text;
                    specializationToUpdate.Code = CodeTextBox.Text;

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Specialization updated successfully!");
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

            if (string.IsNullOrWhiteSpace(NameSpecializationTextBox.Text))
            {
                NameSpecializationTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                NameSpecializationTextBox.Background = Brushes.White;
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

            return isValid;
        }
    }
}
