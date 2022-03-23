namespace Snake {
    public partial class Form1 : Form {
        //global variables :(
        int currentIndex;
        int lastIndex;
        int trajectory;
        //int snake;
        List<int> snake;
        Random random = new Random();
        Button[] btnArray;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        private void TimerEventProcessor(Object anObject, EventArgs eventArgs) {
            //reset event clock
            timer.Stop();
            move();
            timer.Start();
        }

        public Form1() {
            InitializeComponent();

            timer.Interval = 100;
            timer.Start();
            timer.Tick += new EventHandler(TimerEventProcessor);

            snake = new List<int>();

            //snake = 1;
            currentIndex = 18;
            lastIndex = 200;
            trajectory = +16;
            btnArray = new Button[256];
            flowLayoutPanel1.Controls.CopyTo(btnArray, 0);

            spawn();
        }

        private void move() {
            snake.Add(currentIndex);
            lastIndex = snake[0];
            btnArray[lastIndex].BackColor = Color.LightBlue; //remove last
            currentIndex += trajectory;

            if (btnArray[currentIndex].BackColor == Color.Firebrick) {
                snake.Add(currentIndex);
                spawn();
            }

            btnArray[currentIndex].BackColor = Color.Black; 
                
            snake.RemoveAt(0);
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