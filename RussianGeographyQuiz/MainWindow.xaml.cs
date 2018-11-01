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
using RussianGeographyQuiz.Windows;
using RussianGeographyQuiz.Windows.Subjects;

namespace RussianGeographyQuiz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FederalDistrictsTestWindow federalDistrictsTestWindow { get; set; }
        MainSubjectsWindow mainSubjectsWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag;
            switch (tag)
            {
                case "show tests on federal districts of Russia":
                    federalDistrictsTestWindow = new FederalDistrictsTestWindow();
                    Visibility = Visibility.Hidden;
                    federalDistrictsTestWindow.ShowDialog();
                    Visibility = Visibility.Visible;
                    break;
                case "show tests on subjects of Russia":
                    mainSubjectsWindow = new MainSubjectsWindow();
                    Visibility = Visibility.Hidden;
                    mainSubjectsWindow.ShowDialog();
                    Visibility = Visibility.Visible;
                    break;
                case "exit":
                    Close();
                    break;
            }
        }
    }
}
