using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ExcelExport
{
    public partial class Form1 : Form
    {

        RealEstateEntities1 context = new RealEstateEntities1();
        List<Flat> lakasok;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            dataGridView1.DataSource = lakasok;
        }

        private void LoadData()
        {
            lakasok = context.Flats.ToList();
        }



    }
}
