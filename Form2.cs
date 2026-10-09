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
    public partial class Form2 : Form
    {
        OleDbConnection baglanti = new OleDbConnection(
   "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=StokVeri.accdb");
        public Form2()
        {
            InitializeComponent();
        }

        private void lblStok_Click(object sender, EventArgs e)
        {

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM Urunler ORDER BY UrunAdi ASC", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();


        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArama.Text))
            {
                MessageBox.Show("Arama kutusu boş olamaz.");
                return;
            }

            baglanti.Open();

            OleDbDataAdapter da = new OleDbDataAdapter(
                "SELECT * FROM Urunler WHERE UrunAdi LIKE '%' + @adi + '%'", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@adi", txtArama.Text);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            baglanti.Close();

            if (dt.Rows.Count > 0)
            {
                lblStok.Text = "Toplam " + dt.Rows.Count + " ürün bulundu.";
            }
            else
            {
                lblStok.Text = "Ürün bulunamadı.";
            }


        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "INSERT INTO Urunler (UrunAdi, Stok) VALUES (@adi, @stok)", baglanti);
            komut.Parameters.AddWithValue("@adi", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@stok", Convert.ToInt32(txtStok.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün eklendi.");

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "DELETE FROM Urunler WHERE UrunAdi=@adi", baglanti);
            komut.Parameters.AddWithValue("@adi", txtUrunAdi.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silindi.");

        }

        private void btnStokArttir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArama.Text) || string.IsNullOrWhiteSpace(txtMiktar.Text))
            {
                MessageBox.Show("Ürün adı ve miktar boş olamaz.");
                return;
            }

            int miktar;
            if (!int.TryParse(txtMiktar.Text, out miktar) || miktar <= 0)
            {
                MessageBox.Show("Geçerli bir miktar girin.");
                return;
            }

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "UPDATE Urunler SET Stok = Stok + @miktar WHERE UrunAdi LIKE '%' + @adi + '%'", baglanti);
            komut.Parameters.AddWithValue("@miktar", miktar);
            komut.Parameters.AddWithValue("@adi", txtArama.Text);

            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();

            if (sonuc > 0)
                MessageBox.Show("Stok " + miktar + " artırıldı.");
            else
                MessageBox.Show("Ürün bulunamadı.");


        }

        private void btnStokAzalt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArama.Text) || string.IsNullOrWhiteSpace(txtMiktar.Text))
            {
                MessageBox.Show("Ürün adı ve miktar boş olamaz.");
                return;
            }

            int miktar;
            if (!int.TryParse(txtMiktar.Text, out miktar) || miktar <= 0)
            {
                MessageBox.Show("Geçerli bir miktar girin.");
                return;
            }

            baglanti.Open();
            OleDbCommand komut = new OleDbCommand(
                "UPDATE Urunler SET Stok = Stok - @miktar WHERE UrunAdi LIKE '%' + @adi + '%'", baglanti);
            komut.Parameters.AddWithValue("@miktar", miktar);
            komut.Parameters.AddWithValue("@adi", txtArama.Text);

            int sonuc = komut.ExecuteNonQuery();
            baglanti.Close();

            if (sonuc > 0)
                MessageBox.Show("Stok " + miktar + " azaltıldı.");
            else
                MessageBox.Show("Ürün bulunamadı.");


        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            Form4 rapor = new Form4();
            rapor.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Form1 giris = new Form1();
            giris.Show();
            this.Hide();

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void txtStok_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
