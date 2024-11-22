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
    public partial class FacultyWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<FacultyTableClass> allfaculty;

        public FacultyWindow(List<FacultyTableClass> faculties, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = faculties;
            allfaculty= faculties;
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
            var addFacultyWindow = new AddFacultyWindow(context);
            addFacultyWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.faculty.Select(f => new FacultyTableClass
            {
                abbreviation = f.abbreviation,
                Name = f.Name
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as FacultyTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedFaculty = DataGrid.SelectedItem as FacultyTableClass;
            if (selectedFaculty != null)
            {
                var editFacultyWindow = new EditFacultyWindow(context, selectedFaculty);
                editFacultyWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedFaculty = DataGrid.SelectedItem as FacultyTableClass;
            if (selectedFaculty != null)
            {
                var facultyToDelete = context.faculty.Find(selectedFaculty.abbreviation);
                if (facultyToDelete != null)
                {
                    context.faculty.Remove(facultyToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allfaculty.Where(s =>
                s.abbreviation.ToLower().Contains(searchText) ||
                s.Name.ToLower().Contains(searchText)
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allfaculty;
        }
    }
}
