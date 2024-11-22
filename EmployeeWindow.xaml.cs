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
    public partial class EmployeeWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<EmployeeTableClass> allemp;

        public EmployeeWindow(List<EmployeeTableClass> employees, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = employees;
            allemp = employees;
            context = new praktika_321Entities();
            this.userRole = userRole;
            ApplyRolePermissions();
        }

        private void ApplyRolePermissions()
        {
            if (userRole == "Student")
            {
                menu.Visibility = Visibility.Collapsed;
                add.Visibility = Visibility.Collapsed;
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
            var addEmployeeWindow = new AddEmployeeWindow(context);
            addEmployeeWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.Employee.Select(emp => new EmployeeTableClass
            {
                Tab_Number = emp.Tab_Number,
                Code = emp.Code,
                FullName = emp.FullName,
                Position = emp.Position,
                Salary = emp.Salary,
                Chief = emp.Chief
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as EmployeeTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = DataGrid.SelectedItem as EmployeeTableClass;
            if (selectedEmployee != null)
            {
                var editEmployeeWindow = new EditEmployeeWindow(context, selectedEmployee);
                editEmployeeWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = DataGrid.SelectedItem as EmployeeTableClass;
            if (selectedEmployee != null)
            {
                var employeeToDelete = context.Employee.Find(selectedEmployee.Tab_Number);
                if (employeeToDelete != null)
                {
                    context.Employee.Remove(employeeToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allemp.Where(s =>
                s.Tab_Number.ToString().ToLower().Contains(searchText) ||
                s.Code.ToLower().Contains(searchText) ||
                s.FullName.ToLower().Contains(searchText) ||
                s.Position.ToLower().Contains(searchText) ||
                s.Salary.ToString().ToLower().Contains(searchText) ||
                s.Chief.ToString().ToLower().Contains(searchText) 
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allemp;
        }
    }
}
