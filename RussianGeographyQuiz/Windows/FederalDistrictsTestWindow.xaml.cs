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
using System.Windows.Threading;
using RussianGeographyQuiz.Classes;

namespace RussianGeographyQuiz.Windows
{
    /// <summary>
    /// Логика взаимодействия для FederalDistrictsWindow.xaml
    /// </summary>
    public partial class FederalDistrictsTestWindow : Window
    {
        List<TerritorialObject> itemsToFindSource;
        GameSession gameSession;
        GameOverDialogWindow gameOverDialogWindow;
        public FederalDistrictsTestWindow()
        {
            InitializeComponent();

            var sourceReader = new SourceReader();
            itemsToFindSource = sourceReader.Read("FederalDistrictsSource.txt");
            gameSession = new GameSession();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Tag)
            {
                case "PauseButton":
                    gameSession.DispatcherTimer.Stop();
                    MessageBox.Show("Пауза");
                    gameSession.DispatcherTimer.Start();
                    //Приостановка таймера здесь
                    break;
                case "StartGiveUpButton":
                    if (gameSession.GameOnFlag == false)
                    {
                        StartGameSession();
                    }
                    else
                    {
                        GameOver();
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
                    {
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
                                RegionsLeftTextBlock.Text = $"Осталось: {gameSession.TotalNumberOfItemsToFind - gameSession.CurrentNumberOfItemToFind}";
                                ScoreTextBlock.Text = $"Счет: {gameSession.CurrentCountOfCorrectAnswers}/{gameSession.TotalNumberOfItemsToFind}";
                            }
                            else
                            {
                                //Элементы для поиска закончились
                                RegionsLeftTextBlock.Text = $"Осталось: 0";
                                ScoreTextBlock.Text = $"Счет: {gameSession.CurrentCountOfCorrectAnswers}/{gameSession.TotalNumberOfItemsToFind}";
                                GameOver();
                            }
                        }
                        //break;
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
            PauseButton.IsEnabled = false;
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

        private void GameOver()
        {
            StartGiveUpButton.Content = "Начать";
            gameSession.DispatcherTimer.Stop();
            //Вызов диалога завершения игры:
            gameOverDialogWindow = new GameOverDialogWindow();
            gameOverDialogWindow.RepeatButtonClicked += GameOverDialogWindow_RepeatButtonClicked;
            gameOverDialogWindow.ExitButtonClicked += GameOverDialogWindow_ExitButtonClicked;
            gameOverDialogWindow.StayButtonClicked += GameOverDialogWindow_StayButtonClicked;
            gameOverDialogWindow.ShowDialog();
        }

        private void StartGameSession()
        {
            StartGiveUpButton.Content = "Сдаться";
            foreach (var item in itemsToFindSource)
            {
                item.IfClicked = false;
            }
            gameSession = new GameSession(itemsToFindSource, TimerTextBlock);
            gameSession.GameOnFlag = true;
            SubheaderTextBox.Text = "Найти: " + gameSession.ItemsToFind[gameSession.CurrentNumberOfItemToFind].RussianName;
            Cleanup();
            PauseButton.IsEnabled = true;
            RegionsLeftTextBlock.Text = $"Осталось: {gameSession.TotalNumberOfItemsToFind}";
            ScoreTextBlock.Text = $"Счет: {gameSession.CurrentCountOfCorrectAnswers}/{gameSession.TotalNumberOfItemsToFind}";
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
            gameSession.CurrentCountOfCorrectAnswers++;
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
                        itemAsPath.Fill = Brushes.Tomato;
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

        private void Path_MouseEnter(object sender, MouseEventArgs e)
        {
            if (gameSession.GameOnFlag == true)
            {
                var senderPath = sender as Path;
                var senderPathTag = senderPath.Tag as string;
                foreach (var item in gameSession.ItemsToFind)
                {
                    //Ищем соответствующий TerritorialObject в ItemsToFind
                    if (item.EnglishName == senderPathTag)
                    //Для найденного объекта проверяем, было ли с ним взаимодействие:
                    {
                        if (item.IfClicked == false)
                        {
                            //Если взаимодействия не было, при наведении мыши все path, относящиеся к объекту, выделяются:
                            foreach (var innerItem in theCanvas.Children)
                            {
                                var itemAsPath = innerItem as Path;
                                if (itemAsPath != null)//Если это действительно path
                                {
                                    var itemAsPathTag = itemAsPath.Tag as string;
                                    if (itemAsPathTag == senderPathTag)
                                    {
                                        itemAsPath.Stroke = Brushes.Black;
                                        itemAsPath.Fill = Brushes.AliceBlue;
                                    }
                                }
                            }
                        }
                        //break;
                    }
                }
            }
        }

        private void Path_MouseLeave(object sender, MouseEventArgs e)
        {
            if (gameSession.GameOnFlag == true)
            {
                var senderPath = sender as Path;
                var senderPathTag = senderPath.Tag as string;
                foreach (var item in gameSession.ItemsToFind)
                {
                    //Ищем соответствующий TerritorialObject в ItemsToFind
                    if (item.EnglishName == senderPathTag)
                    //Для найденного объекта проверяем, было ли с ним взаимодействие:
                    {
                        if (item.IfClicked == false)
                        {
                            //Если взаимодействия не было, при выводе мыши за границы объекта со всех path, относящихся к объекту, снимается выделение:
                            foreach (var innerItem in theCanvas.Children)
                            {
                                var itemAsPath = innerItem as Path;
                                if (itemAsPath != null)//Если это действительно path
                                {
                                    var itemAsPathTag = itemAsPath.Tag as string;
                                    if (itemAsPathTag == senderPathTag)
                                    {
                                        itemAsPath.Stroke = Brushes.Transparent;
                                        itemAsPath.Fill = Brushes.Transparent;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}

