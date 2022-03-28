namespace Snake {


    /* BUGS
     * orb spawns on top of the snake, gets deleted
     */


    public partial class Form1 : Form {
        //global variables :(
        int currentIndex;
        int lastIndex;
        int trajectory;
        List<int> snake;
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
                label2.Visible = true;
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

            spawn();
        }

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
                label1.Text = snake.Count.ToString();
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

        private void spawn() {
            int location = random.Next(0, btnArray.Length);
            btnArray[location].BackColor = Color.Firebrick;
        }

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
    }
}