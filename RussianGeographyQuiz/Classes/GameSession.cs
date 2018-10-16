using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RussianGeographyQuiz.Classes
{
    class GameSession
    {
        public int TotalNumberOfItemsToFind { get; set; }
        public int CurrentNumberOfItemToFind { get; set; }
        public int CurrentCountOfCorrectAnswers { get; set; }
        public bool GameOnFlag { get; set; }
        public List<TerritorialObject> ItemsToFind { get; set; }
        public DispatcherTimer DispatcherTimer { get; set; }
        public TimeSpan DisplayedTimer { get; set; }
        public TextBlock TimerTextBlock { get; set; }

        private List<TerritorialObject> Randomizer(List<TerritorialObject> itemsToFind)
        {
            //Генерация случайной последовательности вывода территориальных объектов

            //Создаю последовательность чисел типа 0,1,2,3,4... 
            //Самое большое число последовательности на 1 меньше длины списка территориальных объектов itemsToFind
            List<int> listOfNumbers = new List<int>();
            int listLength = itemsToFind.Count();
            for (int i = 0; i < listLength; i++)
            {
                listOfNumbers.Add(i);
            }

            //Заполняю buffer объектами из itemsToFind в случайном порядке.
            //Для этого записываю в number случайное число из ранее созданной последовательности listOfNumbers,
            //удаляю это число из последовательности, добавляю в buffer элемент из itemsToFind, для которого это число является индексом. 
            //Затем повторяю, но при каждом повторении последовательность listONumbers меньше на 1 элемент.
            List<TerritorialObject> buffer = new List<TerritorialObject>();
            Random random = new Random();
            for (int i = 0; i < listLength; i++)
            {
                int number = listOfNumbers[random.Next(listOfNumbers.Count())];
                listOfNumbers.Remove(number);
                buffer.Add(itemsToFind[number]);
            }            
            return buffer;
        }

        public GameSession()
        {
            GameOnFlag = false;
        }

        public GameSession(List<TerritorialObject> itemsToFind, TextBlock timerTextBlock)
        {
            CurrentNumberOfItemToFind = 0;
            CurrentCountOfCorrectAnswers = 0;
            GameOnFlag = false;
            ItemsToFind = Randomizer(itemsToFind);
            TotalNumberOfItemsToFind = ItemsToFind.Count();
            TimerTextBlock = timerTextBlock;

            //Запуск таймера:
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 1);
            DispatcherTimer.Start();
            DisplayedTimer = new TimeSpan(days: 0, hours: 0, minutes: 0, seconds: 0);
            TimerTextBlock.Text = DisplayedTimer.ToString();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DisplayedTimer += DispatcherTimer.Interval;
            TimerTextBlock.Text = DisplayedTimer.ToString();
        }
    }
}
