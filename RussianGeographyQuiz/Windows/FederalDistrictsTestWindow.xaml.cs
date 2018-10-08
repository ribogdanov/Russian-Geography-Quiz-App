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
using RussianGeographyQuiz.Classes;

namespace RussianGeographyQuiz.Windows
{
    /// <summary>
    /// Логика взаимодействия для FederalDistrictsWindow.xaml
    /// </summary>
    public partial class FederalDistrictsTestWindow : Window
    {
        GameSession gameSession;
        public FederalDistrictsTestWindow()
        {
            InitializeComponent();
            List<TerritorialObject> federalDistrictsList = new List<TerritorialObject> {
                new TerritorialObject("Центральный","Central"),
                new TerritorialObject("Южный","South"),
                new TerritorialObject("Северо-Западный","Northwest"),
                new TerritorialObject("Дальневосточный","Far_East"),
                new TerritorialObject("Сибирский","Siberia"),
                new TerritorialObject("Уральский","Ural"),
                new TerritorialObject("Приволжский","Volga"),
                new TerritorialObject("Северо-Кавказский","North_Caucasus")
                };
            gameSession = new GameSession(federalDistrictsList);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Tag)
            {
                case "PauseButton":
                    if (gameSession.GameOnFlag == true)
                        MessageBox.Show("Пауза");
                    //Приостановка таймера здесь
                    else
                        MessageBox.Show("Пауза возможна только во время игры");
                    //Вместо вывода сообщения кнопка паузы должна появляться только во время игры
                    break;
                case "StartGiveUpButton":
                    if (gameSession.GameOnFlag == false)
                    {
                        gameSession.GameOnFlag = true;
                        SubheaderTextBox.Text = "Найти: " + gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].RussianName;
                    }
                        break;
                case "ExitButton":
                    Close();
                    break;
            }
        }
        
        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (gameSession.GameOnFlag == true)
            {
                var senderPath = sender as Path;
                var senderPathTag = senderPath.Tag as string;
                if (senderPathTag == gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].EnglishName)
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
                else
                    foreach (var item in theCanvas.Children)
                    {
                        var itemAsPath = item as Path;
                        if (itemAsPath != null)//Если это действительно path
                        {
                            var itemAsPathTag = itemAsPath.Tag as string;
                            if (itemAsPathTag == gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].EnglishName)
                            {
                                itemAsPath.Fill = Brushes.Red;
                                itemAsPath.Stroke = Brushes.Black;
                            }
                        }
                        var itemAsTextBlock = item as TextBlock;
                        if (itemAsTextBlock != null)
                        {
                            var itemAsTextBlockTag = itemAsTextBlock.Tag as string;
                            if (itemAsTextBlockTag == gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].EnglishName)
                            {
                                itemAsTextBlock.Visibility = Visibility.Visible;
                            }
                        }
                    }
                gameSession.CurrentNumberOfItemToFind++;
                SubheaderTextBox.Text = "Найти: " + gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].RussianName;
            }

        }
    }
}

