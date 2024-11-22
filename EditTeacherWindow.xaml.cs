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
    public partial class EditTeacherWindow : Window
    {
        private praktika_321Entities context;
        private TeacherTableClass teacher;

        public EditTeacherWindow(praktika_321Entities context, TeacherTableClass teacher)
        {
            InitializeComponent();
            this.context = context;
            this.teacher = teacher;
            TabNumberTextBox.Text = teacher.Tab_Number.ToString();
            RankTextBox.Text = teacher.Rank;
            DegreeTextBox.Text = teacher.Degree;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var teacherToUpdate = context.Teacher.Find(teacher.Tab_Number);
                if (teacherToUpdate != null)
                {
                    teacherToUpdate.Rank = RankTextBox.Text;
                    teacherToUpdate.Degree = DegreeTextBox.Text;

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Teacher updated successfully!");
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
