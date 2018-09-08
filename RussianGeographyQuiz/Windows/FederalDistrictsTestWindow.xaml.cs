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

namespace RussianGeographyQuiz.Windows
{
    /// <summary>
    /// Логика взаимодействия для FederalDistrictsWindow.xaml
    /// </summary>
    public partial class FederalDistrictsTestWindow : Window
    {
        double gridWidth;
        public FederalDistrictsTestWindow()
        {
            InitializeComponent();
            gridWidth = TestingGrid.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var path = sender as Path;
            path.Fill = Brushes.LightGreen;
            path.Stroke = Brushes.Black;
            //TestTextBlock.Text = "penis";
        }
    }
}
