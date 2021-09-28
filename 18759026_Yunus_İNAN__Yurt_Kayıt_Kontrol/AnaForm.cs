using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18759026_Yunus_İNAN__Yurt_Kayıt_Kontrol
{
    public partial class AnaForm : MetroFramework.Forms.MetroForm
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        public int personel_id = 1;
        public int kullaniciTur_id = 1;

        private void AnaForm_Load(object sender, EventArgs e)
        {
            if (kullaniciTur_id != 1)
            {
                btn_bolum.Visible = false;
                btn_personel.Visible = false;
                btn_bolumTur.Visible = false;
                btn_yatak.Visible = false;
                btn_oda.Visible = false;
                btn_odaTur.Visible = false;
                btn_odaDurum.Visible = false;
                btn_odemeTuru.Visible = false;
                btn_personelGorev.Visible = false;
                btn_kullaniciTur.Visible = false;

                ödemeTürüToolStripMenuItem.Visible = false;
                personelToolStripMenuItem.Visible = false;
                bölümToolStripMenuItem.Visible = false;
                bölümTürüToolStripMenuItem.Visible = false;
                yatakToolStripMenuItem.Visible = false;
                odaToolStripMenuItem.Visible = false;
                odaTürüToolStripMenuItem.Visible = false;
                odaDurumToolStripMenuItem.Visible = false;
                personelGöreviToolStripMenuItem.Visible = false;
                kullanıcıTürüToolStripMenuItem.Visible = false;








                this.Text += " - (Standart Kullanıcı)";
            }
            else
            {
                this.Text += " - (Yönetici)";
            }
            btn_ogrenci_Click(null, null);
        }

        private void btn_ogrenci_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Ogrenci frm_ogrenci = new Ogrenci();
            frm_ogrenci.MdiParent = this;
            frm_ogrenci.TopLevel = false;
            ortapanel.Controls.Add(frm_ogrenci);
            frm_ogrenci.Show();
            frm_ogrenci.Dock = DockStyle.Fill;
            frm_ogrenci.BringToFront();
            
        }

        

        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btn_oda_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Oda frm_oda = new Oda();
            frm_oda.MdiParent = this;
            frm_oda.TopLevel = false;
            ortapanel.Controls.Add(frm_oda);
            frm_oda.Show();
            frm_oda.Dock = DockStyle.Fill;
            frm_oda.BringToFront();
        }

        private void btn_veli_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Veli frm_veli = new Veli();
            frm_veli.MdiParent = this;
            frm_veli.TopLevel = false;
            ortapanel.Controls.Add(frm_veli);
            frm_veli.Show();
            frm_veli.Dock = DockStyle.Fill;
            frm_veli.BringToFront();
        }

        private void btn_personel_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Personel frm_personel = new Personel();
            frm_personel.MdiParent = this;
            frm_personel.TopLevel = false;
            ortapanel.Controls.Add(frm_personel);
            frm_personel.Show();
            frm_personel.Dock = DockStyle.Fill;
            frm_personel.BringToFront();
        }

        private void btn_odeme_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Odeme frm_odeme = new Odeme();
            frm_odeme.MdiParent = this;
            frm_odeme.TopLevel = false;
            ortapanel.Controls.Add(frm_odeme);
            frm_odeme.Show();
            frm_odeme.Dock = DockStyle.Fill;
            frm_odeme.BringToFront();
        }

        private void btn_odemePlani_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            OdemePlan frm_odemePlan = new OdemePlan();
            frm_odemePlan.MdiParent = this;
            frm_odemePlan.TopLevel = false;
            ortapanel.Controls.Add(frm_odemePlan);
            frm_odemePlan.Show();
            frm_odemePlan.Dock = DockStyle.Fill;
            frm_odemePlan.BringToFront();
        }

        private void btn_izin_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            İzin frm_izin = new İzin();
            frm_izin.MdiParent = this;
            frm_izin.TopLevel = false;
            ortapanel.Controls.Add(frm_izin);
            frm_izin.Show();
            frm_izin.Dock = DockStyle.Fill;
            frm_izin.BringToFront();
        }

        private void gösterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void gizleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_odemeTuru_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            OdemeTur frm_odemeTur = new OdemeTur();
            frm_odemeTur.MdiParent = this;
            frm_odemeTur.TopLevel = false;
            ortapanel.Controls.Add(frm_odemeTur);
            frm_odemeTur.Show();
            frm_odemeTur.Dock = DockStyle.Fill;
            frm_odemeTur.BringToFront();
        }

        private void btn_bolum_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Bolum frm_bolum = new Bolum();
            frm_bolum.MdiParent = this;
            frm_bolum.TopLevel = false;
            ortapanel.Controls.Add(frm_bolum);
            frm_bolum.Show();
            frm_bolum.Dock = DockStyle.Fill;
            frm_bolum.BringToFront();
        }

        private void btn_bolumTur_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            BolumTur frm_bolumTur = new BolumTur();
            frm_bolumTur.MdiParent = this;
            frm_bolumTur.TopLevel = false;
            ortapanel.Controls.Add(frm_bolumTur);
            frm_bolumTur.Show();
            frm_bolumTur.Dock = DockStyle.Fill;
            frm_bolumTur.BringToFront();
        }

        private void btn_yatak_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            Yatak frm_yatak = new Yatak();
            frm_yatak.MdiParent = this;
            frm_yatak.TopLevel = false;
            ortapanel.Controls.Add(frm_yatak);
            frm_yatak.Show();
            frm_yatak.Dock = DockStyle.Fill;
            frm_yatak.BringToFront();
        }

        private void btn_odaTur_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            OdaTur frm_odaTur = new OdaTur();
            frm_odaTur.MdiParent = this;
            frm_odaTur.TopLevel = false;
            ortapanel.Controls.Add(frm_odaTur);
            frm_odaTur.Show();
            frm_odaTur.Dock = DockStyle.Fill;
            frm_odaTur.BringToFront();
        }

        private void btn_odaDurum_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            OdaDurum frm_odaDurum = new OdaDurum();
            frm_odaDurum.MdiParent = this;
            frm_odaDurum.TopLevel = false;
            ortapanel.Controls.Add(frm_odaDurum);
            frm_odaDurum.Show();
            frm_odaDurum.Dock = DockStyle.Fill;
            frm_odaDurum.BringToFront();
        }

        private void btn_personelGorev_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            PersonelGorev frm_personelDurum = new PersonelGorev();
            frm_personelDurum.MdiParent = this;
            frm_personelDurum.TopLevel = false;
            ortapanel.Controls.Add(frm_personelDurum);
            frm_personelDurum.Show();
            frm_personelDurum.Dock = DockStyle.Fill;
            frm_personelDurum.BringToFront();
        }

        private void btn_kullaniciTur_Click(object sender, EventArgs e)
        {
            ortapanel.Controls.Clear();
            KullaniciTur frm_kullaniciTur = new KullaniciTur();
            frm_kullaniciTur.MdiParent = this;
            frm_kullaniciTur.TopLevel = false;
            ortapanel.Controls.Add(frm_kullaniciTur);
            frm_kullaniciTur.Show();
            frm_kullaniciTur.Dock = DockStyle.Fill;
            frm_kullaniciTur.BringToFront();
        }

        
    }
}
