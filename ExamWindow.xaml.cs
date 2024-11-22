using System;
using System.Collections.Generic;
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
    public partial class ExamWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<ExamTableClass> allExam;

        public ExamWindow(List<ExamTableClass> exams, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = exams;
            allExam = exams;
            context = new praktika_321Entities();
            this.userRole = userRole;
            ApplyRolePermissions();
        }

        private void ApplyRolePermissions()
        {
            if (userRole == "Student")
            {

            }
            else if (userRole == "Teacher")
            {
                menu.Visibility = Visibility.Collapsed;
                add.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "HeadOfDepartment")
            {
                menu.Visibility = Visibility.Collapsed;
                add.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Admin")
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addExamWindow = new AddExamWindow(context);
            addExamWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.Exam.Select(ex => new ExamTableClass
            {
                ID_Exam = ex.ID_Exam,
                Date = ex.Date,
                Disciple = ex.Disciple,
                Reg_Number = ex.Reg_Number,
                Teacher = ex.Teacher,
                Room = ex.Room,
                Grade = ex.Grade
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as ExamTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedExam = DataGrid.SelectedItem as ExamTableClass;
            if (selectedExam != null)
            {
                var editExamWindow = new EditExamWindow(context, selectedExam);
                editExamWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedExam = DataGrid.SelectedItem as ExamTableClass;
            if (selectedExam != null)
            {
                var examToDelete = context.Exam.Find(selectedExam.ID_Exam);
                if (examToDelete != null)
                {
                    context.Exam.Remove(examToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allExam.Where(s =>
                s.Date.ToString().ToLower().Contains(searchText) ||
                s.Disciple.ToString().ToLower().Contains(searchText) ||
                s.Reg_Number.ToString().ToLower().Contains(searchText) ||
                s.Teacher.ToString().ToLower().Contains(searchText) ||
                s.Room.ToLower().Contains(searchText) ||
                s.Grade.ToString().ToLower().Contains(searchText) ||
                s.ID_Exam.ToString().ToLower().Contains(searchText) 
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allExam;
        }
    }
}
