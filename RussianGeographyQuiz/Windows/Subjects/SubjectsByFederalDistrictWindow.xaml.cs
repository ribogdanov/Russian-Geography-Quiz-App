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
using RussianGeographyQuiz.Windows.Subjects.SubjectsByFederalDistrictTestWindows;

namespace RussianGeographyQuiz.Windows.Subjects
{
    /// <summary>
    /// Логика взаимодействия для SubjectsByFederalDistrictWindow.xaml
    /// </summary>
    public partial class SubjectsByFederalDistrictWindow : Window
    {
        CentralFederalDistrictSubjectsTestWindow centralFederalDistrictSubjectsTestWindow;
        NorthwestFederalDistrictSubjectsTestWindow northwestFederalDistrictSubjectsTestWindow;
        VolgaFederalDistrictSubjectsTestWindow volgaFederalDistrictSubjectsTestWindow;
        SouthFederalDistrictSubjectsTestWindow southFederalDistrictSubjectsTestWindow;
        NorthCaucasusFederalDistrictSubjectsTestWindow northCaucasusFederalDistrictSubjectsTestWindow;
        UralFederalDistrictSubjectsTestWindow uralFederalDistrictSubjectsTestWindow;
        SiberiaFederalDistrictSubjectsTestWindow siberiaFederalDistrictSubjectsTestWindow;
        FarEastFederalDistrictSubjectsTestWindow farEastFederalDistrictSubjectsTestWindow;

        public SubjectsByFederalDistrictWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var tag = button.Tag;
            switch (tag)
            {
                case "show tests on subjects in Central federal district":
                    centralFederalDistrictSubjectsTestWindow = new CentralFederalDistrictSubjectsTestWindow();
                    centralFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in Northwest federal district":
                    northwestFederalDistrictSubjectsTestWindow = new NorthwestFederalDistrictSubjectsTestWindow();
                    northwestFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in Volga federal district":
                    volgaFederalDistrictSubjectsTestWindow = new VolgaFederalDistrictSubjectsTestWindow();
                    volgaFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in South federal district":
                    southFederalDistrictSubjectsTestWindow = new SouthFederalDistrictSubjectsTestWindow();
                    southFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in North Caucasus federal district":
                    northCaucasusFederalDistrictSubjectsTestWindow = new NorthCaucasusFederalDistrictSubjectsTestWindow();
                    northCaucasusFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in Ural federal district":
                    uralFederalDistrictSubjectsTestWindow = new UralFederalDistrictSubjectsTestWindow();
                    uralFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in Siberia federal district":
                    siberiaFederalDistrictSubjectsTestWindow = new SiberiaFederalDistrictSubjectsTestWindow();
                    siberiaFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "show tests on subjects in Far East federal district":
                    farEastFederalDistrictSubjectsTestWindow = new FarEastFederalDistrictSubjectsTestWindow();
                    farEastFederalDistrictSubjectsTestWindow.ShowDialog();
                    break;
                case "close":
                    Close();
                    break;
            }
        }
    }
}
