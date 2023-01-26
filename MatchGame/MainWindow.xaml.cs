using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
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
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock lastTextBlockClicked;
        bool findingMatch = false;

        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        // shows each 0.1 sec that have passed since launching the game.
        int matchesFound;
        // in-code timer that counts how many matches have been found. I will try to implement this into my Rec Room Game so that a full wave has to
        // be beat instead of the mini-boss.
        float lowestTime;
        float elapsedTime;


        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            timeTextBlockBest.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                //When the timer stops it will display "- Play again?" next to the time it took the player to match all 8 pairs.
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
                timeTextBlockBest.Text = timeTextBlockBest.Text + " - Best Time";

            }
        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
            {
            "🐱‍", "🐱‍",
            "🐺", "🐺",
            "🐗", "🐗",
            "🐮", "🐮",
            "🐨", "🐨",
            "🦒", "🦒",
            "🦊", "🦊",
            "🐵", "🐵",
            };
            //TODO: Replace the Fox or the Wolf. They are too visually similar.
            //List of Characters or Emojis that the game chooses from, if you are going to replace one, make sure to replace both in the line with the same-
            //-emoji or character with the same formatting.
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if ((textBlock.Name != "timeTextBlock") && (textBlock.Name != "timeTextBlockBest"))
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
                timer.Start();
                tenthsOfSecondsElapsed = 0;
                matchesFound = 0;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //When a TextBlock is clicked it runs a check to see if the second textblock matches and acts accordingly
            TextBlock textBlock = sender as TextBlock; if (findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;
                findingMatch = false;

            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();

            }
        }

        private void TimeTextBlockBest_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            { 
                SetUpGame();           
            }
        }
    }
    }