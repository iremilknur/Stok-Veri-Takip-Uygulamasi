using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokVeri
{
    public partial class Form4 : Form
    {
        OleDbConnection baglanti = new OleDbConnection(
   "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=StokVeri.accdb");
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Urunler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
    }
}
