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
    /// Логика взаимодействия для GameOverDialogWindow.xaml
    /// </summary>
    public partial class GameOverDialogWindow : Window
    {
        public delegate void ButtonClickedEventHandler(object sender, EventArgs e);
        public event ButtonClickedEventHandler RepeatButtonClicked;
        public event ButtonClickedEventHandler ExitButtonClicked;
        public event ButtonClickedEventHandler StayButtonClicked;
        public GameOverDialogWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            switch (button.Tag)
            {
                case "Repeat":
                    RepeatButtonClicked?.Invoke(this, new EventArgs());
                    Close();
                    break;
                case "Exit":
                    ExitButtonClicked?.Invoke(this, new EventArgs());
                    Close();
                    break;
                case "Stay":
                    StayButtonClicked?.Invoke(this, new EventArgs());
                    Close();
                    break;
            }
        }
    }
}
