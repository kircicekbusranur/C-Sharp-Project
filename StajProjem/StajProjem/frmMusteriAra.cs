using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StajProjem
{
    public partial class frmMusteriAra : Form
    {
        public frmMusteriAra()
        {
            InitializeComponent();
        }
        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ÇIKMAK İSTEDİĞİNİZDEN EMİN MİSİNİZ?", "UYARI !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void frmMusteriAra_Load(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.musterileriGetir(lvMusteriler);
        }


        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {

            MusteriEkleme frm = new MusteriEkleme();
            cGenel._musteriEkleme = 1;
            frm.btnMusteriGuncelle.Visible = false;
            frm.btnEkle.Visible = true;
            frm.label8.Visible = false;
            this.Close();
            frm.Show();

        }
   

        private void btnMusteriGuncelle_Click(object sender, EventArgs e)
        {
            if (lvMusteriler.SelectedItems.Count > 0)
            {
                MusteriEkleme frm = new MusteriEkleme();
                cGenel._musteriEkleme = 1;
                cGenel._musteriId = Convert.ToInt32(lvMusteriler.SelectedItems[0].SubItems[0].Text);
                frm.btnEkle.Visible = false;
                frm.btnMusteriGuncelle.Visible = true;
                frm.label6.Visible = false;
                this.Close();
                frm.Show();

            }
        }

        private void btnAdisyonBul_Click(object sender, EventArgs e)
        {
            if (txtAdisyonID.Text != "")
            {
                cGenel._AdisyonId = txtAdisyonID.Text;
                cPaketler c = new cPaketler();
                bool sonuc = c.getCheckOpenAdditionID(Convert.ToInt32(txtAdisyonID.Text));
                if (sonuc)
                {

                    frmBill frm = new frmBill();
                    cGenel._ServisTurNo = 2;
                    this.Close();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show(txtAdisyonID.Text + " " + "nolu adisyon bulunamadı");
                }

            }
            else
            {
                MessageBox.Show("Aramak istediğiniz adisyonu yazınız!");


            }
        }

       

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            cMusteriler c = new cMusteriler();
            c.musterigetirAd(lvMusteriler, txtAd.Text);
        }

       

       

        private void frmGeriDon_Click(object sender, EventArgs e)
        {
            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

    }
}
