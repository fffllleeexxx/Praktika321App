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
    public partial class CathedraWindow : Window
    {
        private praktika_321Entities context;
        private string userRole;
        private List<CathedraTableClass> allcath;

        public CathedraWindow(List<CathedraTableClass> cathedras, string userRole)
        {
            InitializeComponent();
            DataGrid.ItemsSource = cathedras;
            allcath = cathedras;
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

            }
            else if (userRole == "Admin")
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addCathedraWindow = new AddCathedraWindow(context);
            addCathedraWindow.ShowDialog();
            RefreshData();
        }

        private void RefreshData()
        {
            DataGrid.ItemsSource = context.cathedra.Select(c => new CathedraTableClass
            {
                Code = c.Code,
                Name = c.Name,
                Faculty = c.Faculty
            }).ToList();
        }

        private void DataGrid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedRow = dataGrid.SelectedItem as CathedraTableClass;
            if (selectedRow != null)
            {
                dataGrid.ContextMenu.DataContext = selectedRow;
                dataGrid.ContextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedCathedra = DataGrid.SelectedItem as CathedraTableClass;
            if (selectedCathedra != null)
            {
                var editCathedraWindow = new EditCathedraWindow(context, selectedCathedra);
                editCathedraWindow.ShowDialog();
                RefreshData();
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var selectedCathedra = DataGrid.SelectedItem as CathedraTableClass;
            if (selectedCathedra != null)
            {
                var cathedraToDelete = context.cathedra.Find(selectedCathedra.Code);
                if (cathedraToDelete != null)
                {
                    context.cathedra.Remove(cathedraToDelete);
                    context.SaveChanges();
                    RefreshData();
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredStudents = allcath.Where(s =>
                s.Code.ToString().ToLower().Contains(searchText) ||
                s.Name.ToLower().Contains(searchText) ||
                s.Faculty.ToLower().Contains(searchText)
            ).ToList();
            DataGrid.ItemsSource = filteredStudents;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            DataGrid.ItemsSource = allcath;
        }
    }
}
