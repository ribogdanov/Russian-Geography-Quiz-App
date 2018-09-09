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
            var button = sender as Button;
            switch (button.Tag)
            {
                case "PauseButton":
                    break;
                case "StartgiveUpButton":
                    break;
                case "ExitButton":
                    break;
            }
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var senderPath = sender as Path;
            var senderPathTag = senderPath.Tag as string;
            foreach (var item in theCanvas.Children)
            {
                var itemAsPath = item as Path;
                if (itemAsPath != null)//Если это действительно path
                {
                    var itemAsPathTag = itemAsPath.Tag as string;
                    if (itemAsPathTag == senderPathTag)
                    {
                        itemAsPath.Fill = Brushes.LightGreen;
                        itemAsPath.Stroke = Brushes.Black;
                    }
                }
                var itemAsTextBlock = item as TextBlock;
                if (itemAsTextBlock != null)
                {
                    var itemAsTextBlockTag = itemAsTextBlock.Tag as string;
                    if (itemAsTextBlockTag == senderPathTag)
                    {
                        itemAsTextBlock.Visibility = Visibility.Visible;
                    }
                }
            }
            
            //TestTextBlock.Text = "penis";
        }
    }
}
