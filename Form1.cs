using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace Snake {
    //improvements
    //highscore sheet
    //47! 

    public partial class Form1 : Form {
        //global variables :(
        int currentIndex;
        int lastIndex;
        int trajectory;
        List<int> snake;
        Highscore[] highScores;
        Random random = new Random();
        Button[] btnArray;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        private void TimerEventProcessor(Object anObject, EventArgs eventArgs) {
            //reset event clock
            timer.Stop();
            if (move()) {
                timer.Start();
            }
            else {
                ScoreLabel.Text = "Final Score: " + (snake.Count-1).ToString();
                GameOverPanel.Visible = true;
            }
        }

        public Form1() {
            InitializeComponent();

            timer.Interval = 100;
            timer.Start();
            timer.Tick += new EventHandler(TimerEventProcessor);

            snake = new List<int>();

            currentIndex = 18;
            lastIndex = 200;
            trajectory = +16;
            btnArray = new Button[256];
            flowLayoutPanel1.Controls.CopyTo(btnArray, 0);

            highScores = new Highscore[5];

            spawn();
        }

        /// <summary>
        /// This function moves the player snake based on user input gathered by the movement_keypressed action listener
        /// Also checks for collisions with the border / snake and ends the game on collision 
        /// </summary>
        /// <returns></returns>
        private Boolean move() {
            Boolean runGame = true;
            snake.Add(currentIndex);
            lastIndex = snake[0];
            btnArray[lastIndex].BackColor = Color.PeachPuff; //remove last
            // start here
            //check left border
            if (currentIndex % 16 == 0 && trajectory == -1) {
                //Border Collision: game over
                runGame = false;
            }
            //check right border
            else if (currentIndex % 16 == 15 && trajectory == +1) {
                //Border Collision: game over
                runGame = false;
            }
            //check bottom border
            else if (currentIndex >= 239 && trajectory == +16) {
                //Border Collision: game over
                runGame = false;
            }
            //check top border
            else if (currentIndex <= 15 && trajectory == -16) {
                //Border Collision: game over
                runGame = false;
            }
            else {

            currentIndex += trajectory;

            if (btnArray[currentIndex].BackColor == Color.Firebrick) {
                snake.Add(currentIndex);
                ScoreLabel.Text = (snake.Count-1).ToString();
                spawn();
            }
            else if (btnArray[currentIndex].BackColor == Color.Black) {
                runGame = false;
            }

            btnArray[currentIndex].BackColor = Color.Black;

            snake.RemoveAt(0);
            }

            return runGame;
        }

        /// <summary>
        /// This function spawns the orbs for the player to collect.
        /// 
        /// the function is called after the previous orb has been collected. 
        /// 
        /// the player recieves one point for each orb collected.
        /// 
        /// the location of the orb is random, however, if the orb would have spawned on top of the player
        /// the location will be re-randomized until it does not. 
        /// </summary>
        private void spawn() {
            int location = 0;
            Boolean valid = false;
            while (!valid) {
                valid = true;
                location = random.Next(0, btnArray.Length);
                foreach (int i in snake) {
                    if (location == i) {
                        valid = false;
                    }
                }
            }
            btnArray[location].BackColor = Color.Firebrick;
        }

        /// <summary>
        /// This function sets the direction of the snake at the given "game tick"
        /// which is then fed into the move function to move the snake in the right direction.
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param>
        private void movement_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == 'w') {
                trajectory = -16;
            }
            else if (e.KeyChar == 'a') {
                trajectory = -1;
            }
            else if (e.KeyChar == 's') {
                trajectory = 16;
            }
            else if (e.KeyChar == 'd') {
                trajectory = 1;
            }
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void RestartButton_Click(object sender, EventArgs e) {
            GameOverPanel.Visible = false;
            foreach (Button btn in btnArray) {
                btn.BackColor = Color.PeachPuff;
            }
            trajectory = 16;
            snake.Clear();
            ScoreLabel.Text = "0";
            currentIndex = 18;
            lastIndex = 200;
            spawn();
            button1.Focus();

            timer.Start();

        }

        private void displayGameOver() {
            Boolean newHighScore = false;

            //check for new highscore
            for (int i = 0; i < 5; i++) {
                if (snake.Count - 1 >= highScores[i].getScore()) {
                    newHighScore = true;
                }
            }

            if (newHighScore) {
                //display new highscore UI
                newHighScorePanel.Visible = true;
            }
            else {
                //populate highscore board
                highscoreName1.Text = highScores[0].getName();
                highscoreName2.Text = highScores[1].getName();
                highscoreName3.Text = highScores[2].getName();
                highscoreName4.Text = highScores[3].getName();
                highscoreName5.Text = highScores[4].getName();
                highscore1.Text = highScores[0].getScore().ToString();
                highscore2.Text = highScores[1].getScore().ToString();
                highscore3.Text = highScores[2].getScore().ToString();
                highscore4.Text = highScores[3].getScore().ToString();
                highscore5.Text = highScores[4].getScore().ToString();

                //display gameover UI
                highscorePanel.Visible = true;
                playAgainLabel.Visible = true;
                continueButton.Visible = true;
                exitButton.Visible = true;

                //hide poweup UI
                powerUpProgress.Visible = false;
                powerUpProgress.Value = 0;
            }
        }

        private void confirmUserInputButton_Click(object sender, EventArgs e) {
            String userInput = "";
            Regex regex = new Regex("[0-9]");
            if (newHighScoreTextbox.Text != null) {
                userInput = newHighScoreTextbox.Text;

                if (regex.IsMatch(userInput)) {
                    userInputErrorLabel.Text = "Error: no numbers allowed";
                    userInputErrorLabel.Visible = true;
                }
                else if (userInput.Contains(" ")) {
                    userInputErrorLabel.Text = "Error: no spaces allowed";
                    userInputErrorLabel.Visible = true;
                }
                else if (userInput.Length < 1) {
                    userInputErrorLabel.Text = "Error: please enter a name";
                    userInputErrorLabel.Visible = true;
                }
                else {
                    //add new highscore to list
                    highScores[4] = new Highscore(newHighScoreTextbox.Text, score);

                    Array.Sort(highScores, Highscore.SortScoreAcending());

                    //close new highscore menu
                    newHighScorePanel.Visible = false;

                    //populate highscore board
                    highscoreName1.Text = highScores[0].getName();
                    highscoreName2.Text = highScores[1].getName();
                    highscoreName3.Text = highScores[2].getName();
                    highscoreName4.Text = highScores[3].getName();
                    highscoreName5.Text = highScores[4].getName();
                    highscore1.Text = highScores[0].getScore().ToString();
                    highscore2.Text = highScores[1].getScore().ToString();
                    highscore3.Text = highScores[2].getScore().ToString();
                    highscore4.Text = highScores[3].getScore().ToString();
                    highscore5.Text = highScores[4].getScore().ToString();

                    //display highscore board
                    highscorePanel.Visible = true;
                    continueButton.Visible = true;
                    exitButton.Visible = true;
                    playAgainLabel.Visible = true;

                    //hide powerUp UI
                    powerUpProgress.Visible = false;
                    powerUpProgress.Value = 0;

                    String[] temp = new string[5];

                    //write to file
                    for (int i = 0; i < 5; i++) {
                        temp[i] = highScores[i].getName() + " " + highScores[i].getScore().ToString();
                    }

                    File.WriteAllLines("C:\\Users\\" + Environment.UserName + "\\Desktop\\Brickbreaker_Highscores.txt", temp);
                }
            }
        }

        /// <summary>
        /// Helper function that clears error message upon user interaction on text box
        /// Prevents a permanent error message showing , makes it more clear that format is incorrect on multiple user attempts at adding a new highscore
        /// </summary>
        private void NewHighScoreTextBox_TextChanged(object sender, EventArgs e) {
            userInputErrorLabel.Visible = false;
        }

    }
}