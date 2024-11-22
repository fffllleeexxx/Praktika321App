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
    public partial class SpecializationWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<SpecializationTableClass> allSpec;

        public SpecializationWindow(List<SpecializationTableClass> specializations, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = specializations;
            allSpec = specializations;
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

            }
            else if (userRole == "Admin")
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addSpecializationWindow = new AddSpecializationWindow(context);
            addSpecializationWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.Specialization.Select(sp => new SpecializationTableClass
            {
                Number = sp.Number,
                Name_Specialization = sp.Name_Specialization,
                Code = sp.Code
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as SpecializationTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpecialization = DataGrid.SelectedItem as SpecializationTableClass;
            if (selectedSpecialization != null)
            {
                var editSpecializationWindow = new EditSpecializationWindow(context, selectedSpecialization);
                editSpecializationWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedSpecialization = DataGrid.SelectedItem as SpecializationTableClass;
            if (selectedSpecialization != null)
            {
                var specializationToDelete = context.Specialization.Find(selectedSpecialization.Number);
                if (specializationToDelete != null)
                {
                    context.Specialization.Remove(specializationToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allSpec.Where(s =>
                s.Number.ToString().ToLower().Contains(searchText) ||
                s.Name_Specialization.ToLower().Contains(searchText) ||
                s.Code.ToLower().Contains(searchText)
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allSpec;
        }
    }
}
