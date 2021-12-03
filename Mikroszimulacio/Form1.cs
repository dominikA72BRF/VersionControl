using Mikroszimulacio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroszimulacio
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbability = new List<BirthProbability>();
        List<DeathProbability> DeathProbability = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();
        }
    }
}
