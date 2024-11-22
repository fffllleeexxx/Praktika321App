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
    public partial class AddExamWindow : Window
    {
        private praktika_321Entities context;

        public AddExamWindow(praktika_321Entities context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var exam = new Exam
                {
                    ID_Exam = int.Parse(IDExamTextBox.Text),
                    Date = DatePicker.SelectedDate ?? DateTime.Now,
                    Disciple = int.Parse(DiscipleTextBox.Text),
                    Reg_Number = int.Parse(RegNumberTextBox.Text),
                    Teacher = int.Parse(TeacherTextBox.Text),
                    Room = RoomTextBox.Text,
                    Grade = int.Parse(GradeTextBox.Text)
                };

                context.Exam.Add(exam);

                try
                {
                    context.SaveChanges();
                    MessageBox.Show("Exam added successfully!");
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
                    context.Entry(exam).State = System.Data.Entity.EntityState.Detached;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                    context.Entry(exam).State = System.Data.Entity.EntityState.Detached;
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

            if (!int.TryParse(IDExamTextBox.Text, out _))
            {
                IDExamTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                IDExamTextBox.Background = Brushes.White;
            }

            if (DatePicker.SelectedDate == null)
            {
                DatePicker.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                DatePicker.Background = Brushes.White;
            }

            if (!int.TryParse(DiscipleTextBox.Text, out _))
            {
                DiscipleTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                DiscipleTextBox.Background = Brushes.White;
            }

            if (!int.TryParse(RegNumberTextBox.Text, out _))
            {
                RegNumberTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                RegNumberTextBox.Background = Brushes.White;
            }

            if (!int.TryParse(TeacherTextBox.Text, out _))
            {
                TeacherTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                TeacherTextBox.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(RoomTextBox.Text))
            {
                RoomTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                RoomTextBox.Background = Brushes.White;
            }

            if (!int.TryParse(GradeTextBox.Text, out _))
            {
                GradeTextBox.Background = Brushes.Red;
                isValid = false;
            }
            else
            {
                GradeTextBox.Background = Brushes.White;
            }

            return isValid;
        }
    }
}
