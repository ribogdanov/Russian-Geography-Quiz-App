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

namespace RussianGeographyQuiz.Windows.Subjects
{
    /// <summary>
    /// Логика взаимодействия для MainSubjectsWindow.xaml
    /// </summary>
    public partial class MainSubjectsWindow : Window
    {
        AllRussianSubjectsTestWindow allRussianSubjectsTestWindow;
        EuropeanSubjectsTestWindow europeanSubjectsTestWindow;
        AsianSubjectsTestWindow asianSubjectsTestWindow;
        SubjectsByFederalDistrictWindow subjectsByFederalDistrictWindow;
        public MainSubjectsWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag;
            switch (tag)
            {
                case "show tests on all of Russian subjects":
                    allRussianSubjectsTestWindow = new AllRussianSubjectsTestWindow();
                    allRussianSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in the european part of Russia":
                    europeanSubjectsTestWindow = new EuropeanSubjectsTestWindow();
                    europeanSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in the asian part of Russia":
                    asianSubjectsTestWindow = new AsianSubjectsTestWindow();
                    asianSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on russian subjects by federal district":
                    subjectsByFederalDistrictWindow = new SubjectsByFederalDistrictWindow();
                    subjectsByFederalDistrictWindow.ShowDialog();
                    break;
                case "close":
                    Close();
                    break;
            }
        }
    }
}
