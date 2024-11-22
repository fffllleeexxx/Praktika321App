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
    public partial class TeacherWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<TeacherTableClass> allTeachers;

        public TeacherWindow(List<TeacherTableClass> teachers, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = teachers;
            allTeachers = teachers;
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
            var addTeacherWindow = new AddTeacherWindow(context);
            addTeacherWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.Teacher.Select(t => new TeacherTableClass
            {
                Tab_Number = t.Tab_Number,
                Rank = t.Rank,
                Degree = t.Degree
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as TeacherTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedTeacher = DataGrid.SelectedItem as TeacherTableClass;
            if (selectedTeacher != null)
            {
                var editTeacherWindow = new EditTeacherWindow(context, selectedTeacher);
                editTeacherWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedTeacher = DataGrid.SelectedItem as TeacherTableClass;
            if (selectedTeacher != null)
            {
                var teacherToDelete = context.Teacher.Find(selectedTeacher.Tab_Number);
                if (teacherToDelete != null)
                {
                    context.Teacher.Remove(teacherToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allTeachers.Where(s =>
                s.Tab_Number.ToString().ToLower().Contains(searchText) ||
                s.Rank.ToLower().Contains(searchText) ||
                s.Degree.ToLower().Contains(searchText)
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allTeachers;
        }
    }
}
