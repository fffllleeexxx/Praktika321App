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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika321App
{
    public partial class MainWindow : Window
    {
        private string userRole;

        public MainWindow(string role)
        {
            InitializeComponent();
            userRole = role;
            ApplyRolePermissions();
        }

        private void ApplyRolePermissions()
        {
            if (userRole == "Student")
            {
                ShowStudents_Button.Visibility = Visibility.Collapsed;
                ShowExams_Button.Visibility = Visibility.Collapsed;
                ShowTeachers_Button.Visibility = Visibility.Collapsed;
                ShowFaculties_Button.Visibility = Visibility.Collapsed;
                ShowCathedras_Button.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Teacher")
            {

            }
            else if (userRole == "HeadOfDepartment")
            {

            }
            else if (userRole == "Admin")
            {

            }
        }

        private void ShowStudents_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var students = context.Student.Select(s => new StudentTableClass
                {
                    Reg_Number = s.Reg_Number,
                    Fullname = s.Fullname,
                    Number = s.Number
                }).ToList();

                var studentWindow = new StudentWindow(students, userRole);
                studentWindow.Show();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ShowExams_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var exams = context.Exam.Select(ex => new ExamTableClass
                {
                    ID_Exam = ex.ID_Exam,
                    Date = ex.Date,
                    Disciple = ex.Disciple,
                    Reg_Number = ex.Reg_Number,
                    Teacher = ex.Teacher,
                    Room = ex.Room,
                    Grade = ex.Grade
                }).ToList();

                var examWindow = new ExamWindow(exams,userRole);
                examWindow.Show();
            }
        }

        private void ShowSpecializations_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var specializations = context.Specialization.Select(sp => new SpecializationTableClass
                {
                    Number = sp.Number,
                    Name_Specialization = sp.Name_Specialization,
                    Code = sp.Code
                }).ToList();

                var specializationWindow = new SpecializationWindow(specializations,userRole);
                specializationWindow.Show();
            }
        }

        private void ShowTeachers_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var teachers = context.Teacher.Select(t => new TeacherTableClass
                {
                    Tab_Number = t.Tab_Number,
                    Rank = t.Rank,
                    Degree = t.Degree
                }).ToList();

                var teacherWindow = new TeacherWindow(teachers,userRole);
                teacherWindow.Show();
            }
        }

        private void ShowFaculties_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var faculties = context.faculty.Select(f => new FacultyTableClass
                {
                    abbreviation = f.abbreviation,
                    Name = f.Name
                }).ToList();

                var facultyWindow = new FacultyWindow(faculties,userRole);
                facultyWindow.Show();
            }
        }

        private void ShowCathedras_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var cathedras = context.cathedra.Select(c => new CathedraTableClass
                {
                    Code = c.Code,
                    Name = c.Name,
                    Faculty = c.Faculty
                }).ToList();

                var cathedraWindow = new CathedraWindow(cathedras,userRole);
                cathedraWindow.Show();
            }
        }

        private void ShowEmployees_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new praktika_321Entities())
            {
                var employees = context.Employee.Select(emp => new EmployeeTableClass
                {
                    Tab_Number = emp.Tab_Number,
                    Code = emp.Code,
                    FullName = emp.FullName,
                    Position = emp.Position,
                    Salary = emp.Salary,
                    Chief = emp.Chief
                }).ToList();

                var employeeWindow = new EmployeeWindow(employees,userRole);
                employeeWindow.Show();
            }
        }
    }
}
