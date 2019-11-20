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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        
        private void FrmGiris_Load(object sender, EventArgs e)
        {

            cPersoneller p = new cPersoneller();
            p.personelGetbyInformation(cbKullanici);
        }
     
        private void btnGiris_Click(object sender, EventArgs e)
        {
             
            cGenel gnl = new cGenel();
            cPersoneller p = new cPersoneller();
            bool result = p.personelEntryControl(txtSifre.Text,cGenel._personelId);
            if(result)
            {
                cPersonelHareketleri ph = new cPersonelHareketleri();
                ph.PersonelId = cGenel._personelId;
                ph.Islem = "Giriş Yaptı";
                ph.Tarih=DateTime.Now;
                ph.PersonelActiveSave(ph);

                this.Hide();
                frmMenu menu = new frmMenu();
                menu.Show();

            }
            else
            {
                MessageBox.Show("Şifreniz yanlış","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Stop);

            }

        }
        private void cbKullanici_SelectedIndexChanged(object sender, EventArgs e)
        {
            cPersoneller p = (cPersoneller)cbKullanici.SelectedItem;
            cGenel._personelId = p.PersonelId;
            cGenel._gorevId = p.PersonelGorevId;

        }
        private void btnCikis_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Çıkmak istediğinizden emin misiniz?","UYARI",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                Application.Exit();

            }

        }

    
    }
}
