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
    public partial class Form3 : Form
    {
        OleDbConnection baglanti = new OleDbConnection(
   "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=StokVeri.accdb");
        public Form3()
        {
            InitializeComponent();
        }

        private void btnSoruGetir_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "SELECT GuvenlikSorusu FROM Kullanicilar WHERE KullaniciAdi=@kadi", baglanti);
            komut.Parameters.AddWithValue("@kadi", txtKullaniciAdi.Text);
            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                lblSoru.Text = dr["GuvenlikSorusu"].ToString();
            }
            else
            {
                MessageBox.Show("Kullanıcı bulunamadı.");
            }
            baglanti.Close();

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "SELECT * FROM Kullanicilar WHERE KullaniciAdi=@kadi AND GuvenlikCevabi=@cevap", baglanti);
            komut.Parameters.AddWithValue("@kadi", txtKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@cevap", txtCevap.Text);
            OleDbDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                Form2 stok = new Form2();
                stok.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Cevap yanlış!");
            }
            baglanti.Close();

        }
    }
}
