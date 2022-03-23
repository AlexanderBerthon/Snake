namespace Snake {
    public partial class Form1 : Form {
        //global variables :(
        int currentIndex;
        int lastIndex;
        int trajectory;
        int snake;
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

            timer.Interval = 300;
            timer.Start();
            timer.Tick += new EventHandler(TimerEventProcessor);

            snake = 1;
            currentIndex = 18;
            lastIndex = 200;
            trajectory = +16;
            btnArray = new Button[256];
            flowLayoutPanel1.Controls.CopyTo(btnArray, 0);

            spawn();
        }

        private void move() {
            //paint beginning
            //paint end
            //nothing else should change?

            

            if (snake == 1) {
                btnArray[currentIndex].BackColor = Color.LightBlue;
                currentIndex += trajectory;

                if (btnArray[currentIndex].BackColor == Color.Firebrick) {
                    snake++;
                    spawn();
                }

                btnArray[currentIndex].BackColor = Color.Black;
            }
            else {
                if (snake == 2) {
                    lastIndex = currentIndex;
                }
                else {

                    btnArray[lastIndex].BackColor = Color.LightBlue;
                    //this can't be a good way to solve this problem..
                    if (btnArray[lastIndex - 1].BackColor == Color.Black) {
                        lastIndex = (lastIndex - 1)*snake;
                    }
                    else if (btnArray[lastIndex + 1].BackColor == Color.Black) {
                        lastIndex = (lastIndex + 1)*snake;
                    }
                    else if (btnArray[lastIndex - 16].BackColor == Color.Black) {
                        lastIndex = (lastIndex - 16)*snake;
                    }
                    else if (btnArray[lastIndex + 16].BackColor == Color.Black) {
                        lastIndex = (lastIndex + 16)*snake;
                    }
                }

                currentIndex += trajectory;

                if (btnArray[currentIndex].BackColor == Color.Firebrick) {
                    snake++;
                    spawn();
                }

                btnArray[currentIndex].BackColor = Color.Black;

                
            }
            
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