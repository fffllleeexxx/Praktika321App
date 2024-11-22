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
    public partial class EditExamWindow : Window
    {
        private praktika_321Entities context;
        private ExamTableClass exam;

        public EditExamWindow(praktika_321Entities context, ExamTableClass exam)
        {
            InitializeComponent();
            this.context = context;
            this.exam = exam;
            IDExamTextBox.Text = exam.ID_Exam.ToString();
            DatePicker.SelectedDate = exam.Date;
            DiscipleTextBox.Text = exam.Disciple.ToString();
            RegNumberTextBox.Text = exam.Reg_Number.ToString();
            TeacherTextBox.Text = exam.Teacher.ToString();
            RoomTextBox.Text = exam.Room;
            GradeTextBox.Text = exam.Grade.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                var examToUpdate = context.Exam.Find(exam.ID_Exam);
                if (examToUpdate != null)
                {
                    examToUpdate.Date = DatePicker.SelectedDate ?? DateTime.Now;
                    examToUpdate.Disciple = int.Parse(DiscipleTextBox.Text);
                    examToUpdate.Reg_Number = int.Parse(RegNumberTextBox.Text);
                    examToUpdate.Teacher = int.Parse(TeacherTextBox.Text);
                    examToUpdate.Room = RoomTextBox.Text;
                    examToUpdate.Grade = int.Parse(GradeTextBox.Text);

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Exam updated successfully!");
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
