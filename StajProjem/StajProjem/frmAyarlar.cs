using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StajProjem
{
    public partial class frmAyarlar : Form
    {
        public frmAyarlar()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {

            frmMenu frm = new frmMenu();
            this.Close();
            frm.Show();
        }

        private void frmAyarlar_Load(object sender, EventArgs e)
        {
            cPersoneller cp = new cPersoneller();
            cPersonelGorev cpg = new cPersonelGorev();
            string gorev = cpg.PersonelGorevTanim(cGenel._gorevId);
            if (gorev == "Şef")
            {
                cpg.PersonelGorevGetir(cbGorevi);
                cp.personelBilgileriniGetirLv(lvPersoneller);

                btnSil.Enabled = false;
                btnBilgiDegistir.Enabled = true;
                lblBilgi.Text = "Mevki : ŞEF / Yetki Sınırsız / Kullanıcı :" + cp.personelBilgiGetirIsim(cGenel._personelId);
                panel1.Visible = false;
            }
            else
            {
                lblBilgi.Text = "Yetki Sınırlı / Kullanıcı :" + cp.personelBilgiGetirIsim(cGenel._personelId);
                panel2.Visible = false;
                panel3.Visible = false;
            }

        }

      

        private void cbGorevi_SelectedIndexChanged(object sender, EventArgs e)
        {

            cPersonelGorev c = (cPersonelGorev)cbGorevi.SelectedItem;
            txtGorevId2.Text = Convert.ToString(c.PersonelGorevId);
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (lvPersoneller.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Silmek istediğinizden emin misiniz?", "UYARI !", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    cPersoneller c = new cPersoneller();
                    bool sonuc = c.personelSil(Convert.ToInt32(lvPersoneller.SelectedItems[0].Text));
                    if (sonuc)
                    {
                        MessageBox.Show("Kayıt Başarıyla Silinmiştir !");
                        c.personelBilgileriniGetirLv(lvPersoneller);

                    }
                    else
                    {

                        MessageBox.Show("Kayıt Silinirken Bir Hata Oluşmuştur !");
                    }

                }
                else
                {
                    MessageBox.Show("Kayıt Seçiniz !");

                }
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text.Trim() != "" || txtSoyad.Text.Trim() != "" || txtSifre.Text.Trim() != "" || txtSifreTekrar.Text.Trim() != "" || txtGorevId2.Text.Trim() != "")
            {
                if (txtSifreTekrar.Text.Trim() == txtSifre.Text.Trim())
                {
                    cPersoneller c = new cPersoneller();
                    c.PersonelAd = txtAd.Text.Trim();
                    c.PersonelSoyad = txtSoyad.Text.Trim();
                    c.PersonelParola = txtSifreTekrar.Text;
                    c.PersonelGorevId = Convert.ToInt32(txtGorevId2.Text);
                    bool sonuc = c.personelEkle(c);
                    if (sonuc)
                    {
                        MessageBox.Show("Kayıt Başarıyla Eklenmiştir !");
                        c.personelBilgileriniGetirLv(lvPersoneller);
                    }
                    else
                    {
                        MessageBox.Show("Kayıt Eklenirken Bir Hata Oluştu !");
                    }
                }
                else
                {
                    MessageBox.Show("Şifreler Aynı Değil");
                }

            }
            else
            {
                MessageBox.Show("Boş Alan Bırakmayınız !");
            }
        }

        private void btnBilgiDegistir_Click(object sender, EventArgs e)
        {
            if (lvPersoneller.SelectedItems.Count > 0)
            {

                if (txtAd.Text != "" || txtSoyad.Text != "" || txtSifre.Text != "" || txtSifreTekrar.Text != "" || txtGorevId2.Text != "")
                {
                    if (txtSifreTekrar.Text.Trim() == txtSifre.Text.Trim())
                    {
                        cPersoneller c = new cPersoneller();
                        c.PersonelAd = txtAd.Text.Trim();
                        c.PersonelSoyad = txtSoyad.Text.Trim();
                        c.PersonelParola = txtSifreTekrar.Text;
                        c.PersonelGorevId = Convert.ToInt32(txtPersonelId2.Text);
                        bool sonuc = c.personelGuncelle(c, Convert.ToInt32(txtPersonelId2.Text));
                        if (sonuc)
                        {
                            MessageBox.Show("Kayıt Başarıyla Değiştirildi !");
                        }
                        else
                        {
                            MessageBox.Show("Kayıt Değiştirilirken bir hata oluştu!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Şifreler Aynı Değil");
                    }

                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakmayınız !");
                }

            }
            else
            {
                MessageBox.Show("Kayıt Seçiniz !");
            }
        }

        

        private void lvPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPersoneller.SelectedItems.Count > 0)
            {

                btnSil.Enabled = true;
                txtPersonelId2.Text = lvPersoneller.SelectedItems[0].SubItems[0].Text;
                cbGorevi.SelectedIndex = Convert.ToInt32(lvPersoneller.SelectedItems[0].SubItems[1].Text) - 1;

                txtAd.Text = lvPersoneller.SelectedItems[0].SubItems[3].Text;
                txtSoyad.Text = lvPersoneller.SelectedItems[0].SubItems[4].Text;
            }
            else
            {
                btnSil.Enabled = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtYeniSifreTekrar.Text.Trim() != "" || txtYeniSifre.Text.Trim() != "")
            {
                if (txtYeniSifre.Text == txtYeniSifreTekrar.Text)
                {
                    if (cGenel._personelId.ToString() != "")
                    {
                        cPersoneller c = new cPersoneller();
                        bool sonuc = c.personelSifreDegistir(Convert.ToInt32(cGenel._personelId), txtYeniSifreTekrar.Text);
                        if (sonuc)
                        {
                            MessageBox.Show("Şifre Değiştirme işlemi başarıyla tamamlandı !");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Personel Seçiniz!");
                    }

                }
                else
                {
                    MessageBox.Show("Şifreler Aynı Değil!");
                }

            }
            else
            {
                MessageBox.Show("Şifre Alanını Boş Bırakmayınız !");
            }
        }
    }
}
