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
    public partial class AddSpecializationWindow : Window
    {
        private praktika_321Entities context;

        public AddSpecializationWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var specialization = new Specialization
                {
                    Number = NumberTextBox.Text,
                    Name_Specialization = NameSpecializationTextBox.Text,
                    Code = CodeTextBox.Text
                };

                context.Specialization.Add(specialization);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Specialization added successfully!");
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
                    context.Entry(specialization).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(specialization).State = System.Data.Entity.EntityState.Detached;
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

            if (string.IsNullOrWhiteSpace(NumberTextBox.Text))
            {
                NumberTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                NumberTextBox.Background = Brushes.White;
            }

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
