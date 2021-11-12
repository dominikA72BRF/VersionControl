using SantaFActory.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SantaFActory
{
    public partial class Form1 : Form
    {

        List<Ball> _balls = new List<Ball>();

        private BallFactory _ballFactory;

        public BallFactory BallFactory
        {
            get { return _ballFactory; }
            set { _ballFactory = value; }
        }


        public Form1()
        {
            InitializeComponent();
            BallFactory = new BallFactory();
            
         

        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = BallFactory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var ball in _balls)
            {
                ball.MoveBall();
                if (ball.Left > maxPosition)
                    maxPosition = ball.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestBall = _balls[0];
                mainPanel.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }
    }
}
