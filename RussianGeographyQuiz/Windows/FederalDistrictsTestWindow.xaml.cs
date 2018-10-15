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
        List<TerritorialObject> federalDistrictsList;
        GameSession gameSession;
        GameOverDialogWindow gameOverDialogWindow;
        public FederalDistrictsTestWindow()
        {
            InitializeComponent();
            federalDistrictsList = new List<TerritorialObject> {
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
                        StartGameSession();
                    }
                    else
                    {
                        ShowGameOverDialog();
                    }
                    break;
                case "ExitButton":
                    Close();
                    break;
            }
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Если игра не начата, ничего не происходит
            if (gameSession.GameOnFlag == true)
            {
                var senderPath = sender as Path;
                var senderPathTag = senderPath.Tag as string;
                foreach (var item in gameSession.ItemsToFind)
                {
                    //Пользователь нажал на некоторый объект Path, ищем соответствующий ему объект Territorial Object в gameSession.ItemsToFind:
                    if (item.EnglishName == senderPathTag)
                        //Проверяем, происходило ли до этого взаимодействие с этим объектом TerritorialObject:
                        if (item.IfClicked == false)
                        {
                            //Если взаимодействие не происходило (все относящиеся к этому объекту Path в сером цвете), то
                            //происходит взаимодействие с текущим искомым объектом Territorial Object и окрашивание всех его Path в соотвествующий ситуации цвет
                            if (senderPathTag == gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].EnglishName)
                                MarkPathAsSelectedCorrectly();
                            else
                                MarkPathAsSelectedIncorrectly();
                            //А также помечаем, что взаимодействие с текущим искомым объектом TerritorialObject произошло:
                            gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].IfClicked = true;

                            //Переход к поиску следующего объекта TerritorialObject
                            if (gameSession.CurrentNumberOfItemToFind + 1 < gameSession.TotalNumberOfItemsToFind)
                            {
                                gameSession.CurrentNumberOfItemToFind++;
                                SubheaderTextBox.Text = "Найти: " + gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].RussianName;
                            }
                            else
                            {
                                //Элементы для поиска закончились
                                ShowGameOverDialog();
                            }
                        }
                }

                
            }

        }

        private void GameOverDialogWindow_RepeatButtonClicked(object sender, EventArgs e)
        {
            StartGameSession();
        }
        private void GameOverDialogWindow_ExitButtonClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void GameOverDialogWindow_StayButtonClicked(object sender, EventArgs e)
        {
            SubheaderTextBox.Text = "Наблюдение результатов последней игры";
            gameSession.GameOnFlag = false;
        }

        private void Cleanup()
        {
            //Возвращает игровое поле к состоянию до начала игры
            foreach (var item in theCanvas.Children)
            {
                var itemAsPath = item as Path;
                if (itemAsPath != null)//Если это действительно path
                {
                    itemAsPath.Fill = Brushes.Transparent;
                    itemAsPath.Stroke = Brushes.Transparent;
                }
                var itemAsTextBlock = item as TextBlock;
                if (itemAsTextBlock != null)
                {
                    itemAsTextBlock.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ShowGameOverDialog()
        {
            StartGiveUpButton.Content = "Начать";
            gameOverDialogWindow = new GameOverDialogWindow();
            gameOverDialogWindow.RepeatButtonClicked += GameOverDialogWindow_RepeatButtonClicked;
            gameOverDialogWindow.ExitButtonClicked += GameOverDialogWindow_ExitButtonClicked;
            gameOverDialogWindow.StayButtonClicked += GameOverDialogWindow_StayButtonClicked;
            gameOverDialogWindow.ShowDialog();
        }

        private void StartGameSession()
        {
            StartGiveUpButton.Content = "Сдаться";
            gameSession = new GameSession(federalDistrictsList);
            gameSession.GameOnFlag = true;
            SubheaderTextBox.Text = "Найти: " + gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].RussianName;
            Cleanup();
        }

        private void MarkPathAsSelectedCorrectly()
        {
            foreach (var item in theCanvas.Children)
            {
                var itemAsPath = item as Path;
                if (itemAsPath != null)//Если это действительно path
                {
                    var itemAsPathTag = itemAsPath.Tag as string;
                    if (itemAsPathTag == gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].EnglishName)
                    {
                        itemAsPath.Fill = Brushes.LightGreen;
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
        }

        private void MarkPathAsSelectedIncorrectly()
        {
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
        }
    }
}

