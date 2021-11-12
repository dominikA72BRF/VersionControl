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

        List<Ball> balls = new List<Ball>();

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
    }
}
