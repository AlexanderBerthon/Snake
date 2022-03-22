namespace Snake {
    public partial class Form1 : Form {
        //global variables :(
        int currentIndex;
        int trajectory;
        Button[] btnArray;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        private void TimerEventProcessor(Object anObject, EventArgs eventArgs) {
            //reset event clock
            timer.Stop();
            move('x');
            timer.Start();
        }

        public Form1() {
            InitializeComponent();

            timer.Interval = 100;
            timer.Start();
            timer.Tick += new EventHandler(TimerEventProcessor);

            currentIndex = 18;
            trajectory = +16;
            btnArray = new Button[256];
            flowLayoutPanel1.Controls.CopyTo(btnArray, 0);
        }

        private void move(Char key) {
            btnArray[currentIndex].BackColor = Color.AliceBlue;
            currentIndex += trajectory;
            btnArray[currentIndex].BackColor = Color.Firebrick;
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