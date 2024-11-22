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
    public partial class StudentWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<StudentTableClass> allStudents;

        public StudentWindow(List<StudentTableClass> students, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = students;
            allStudents=students;
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
                add_button.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "HeadOfDepartment")
            {
                menu.Visibility = Visibility.Collapsed;
                add_button.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Admin")
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addStudentWindow = new AddStudentWindow(context);
            addStudentWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.Student.Select(s => new StudentTableClass
            {
                Reg_Number = s.Reg_Number,
                Fullname = s.Fullname,
                Number = s.Number
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as StudentTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = DataGrid.SelectedItem as StudentTableClass;
            if (selectedStudent != null)
            {
                var editStudentWindow = new EditStudentWindow(context, selectedStudent);
                editStudentWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = DataGrid.SelectedItem as StudentTableClass;
            if (selectedStudent != null)
            {
                var studentToDelete = context.Student.Find(selectedStudent.Reg_Number);
                if (studentToDelete != null)
                {
                    context.Student.Remove(studentToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allStudents.Where(s =>
                s.Reg_Number.ToString().ToLower().Contains(searchText) ||
                s.Fullname.ToLower().Contains(searchText) ||
                s.Number.ToLower().Contains(searchText)
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allStudents;
        }
    }
}
