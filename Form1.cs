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
    public partial class Form1 : Form
    {
        OleDbConnection baglanti = new OleDbConnection(
    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=StokVeri.accdb");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "SELECT * FROM Kullanicilar WHERE KullaniciAdi=@kadi AND Sifre=@sifre", baglanti);
            komut.Parameters.AddWithValue("@kadi", txtKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@sifre", txtSifre.Text);

            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                Form2 stok = new Form2();
                stok.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı giriş!");
            }

            baglanti.Close();

        }

        private void btnSifremiUnuttum_Click(object sender, EventArgs e)
        {
            Form3 sifreForm = new Form3();
            sifreForm.Show();
            this.Hide();

        }
    }
}
