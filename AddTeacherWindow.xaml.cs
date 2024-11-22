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
    public partial class AddTeacherWindow : Window
    {
        private praktika_321Entities context;

        public AddTeacherWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var teacher = new Teacher
                {
                    Tab_Number = int.Parse(TabNumberTextBox.Text),
                    Rank = RankTextBox.Text,
                    Degree = DegreeTextBox.Text
                };

                context.Teacher.Add(teacher);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Teacher added successfully!");
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
                    context.Entry(teacher).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(teacher).State = System.Data.Entity.EntityState.Detached;
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

            if (string.IsNullOrWhiteSpace(RankTextBox.Text))
            {
                RankTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                RankTextBox.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(DegreeTextBox.Text))
            {
                DegreeTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                DegreeTextBox.Background = Brushes.White;
            }

            return isValid;
        }
    }
}
