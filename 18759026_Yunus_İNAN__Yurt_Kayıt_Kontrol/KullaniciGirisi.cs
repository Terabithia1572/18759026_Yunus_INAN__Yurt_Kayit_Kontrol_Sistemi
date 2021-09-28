using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DLLIslemleri_;
using Microsoft.Win32;

namespace _18759026_Yunus_İNAN__Yurt_Kayıt_Kontrol
{
    public partial class KullaniciGirisi : MetroFramework.Forms.MetroForm
    {
        public KullaniciGirisi()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        public Baslangic baslangic_frm;
        private void KullaniciGirisi_Load(object sender, EventArgs e)
        {
            if (Ayarlar.Default.beniHatirla == true)
            {
                chbx_beniHatirla.Checked = true;
                tx_kullaniciAdi.Text = Ayarlar.Default.tcNo;
                tx_sifre.Text = Ayarlar.Default.sifre;
            }
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (key.GetValue("KullaniciGirisi").ToString() == "\"" + Application.ExecutablePath + "\"")
                {
                    chbx_windowsIleBaslama.Checked = true;
                }
            }
            catch
            { }

            tx_sifre.PasswordChar = '●';
        }

        private void btn_girisYap_Click(object sender, EventArgs e)
        {
            if (tx_kullaniciAdi.Text == "" || tx_sifre.Text == "")
            {
                MessageBox.Show("Kullanıcı adı veya şifre boş bırakılamaz !");
                return;
            }

            DataTable dt = vt.Select("select * from tbl_personel where tcNO='" + tx_kullaniciAdi.Text + "' and sifre='" + DigerIslemler.MD5Sifrele(tx_sifre.Text) + "'");
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı");
                return;
            }
            else
            {
                AnaForm afrm = new AnaForm();

                this.Hide();
                afrm.lbl_kullanici.Text = ""+ dt.Rows[0][1] + " " + dt.Rows[0][2] + "";
                afrm.personel_id = Convert.ToInt32(dt.Rows[0][0]);
                afrm.kullaniciTur_id = Convert.ToInt32(dt.Rows[0][3]);
                afrm.Show();
            }

            if (chbx_beniHatirla.Checked == true)
            {
                Ayarlar.Default.beniHatirla = true;
                Ayarlar.Default.tcNo = tx_kullaniciAdi.Text;
                Ayarlar.Default.sifre = tx_sifre.Text;
                Ayarlar.Default.Save();
            }
            else
            {
                Ayarlar.Default.Reset();
            }
        }

        private void chbx_sifreyiGoster_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_sifreyiGoster.Checked)
            {
                tx_sifre.PasswordChar = '\0';
            }
            else
            {
                tx_sifre.PasswordChar = '●';
            }
        }

        private void KullaniciGirisi_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void chbx_windowsIleBaslama_CheckedChanged(object sender, EventArgs e)
        {
            if (chbx_windowsIleBaslama.Checked)
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.SetValue("KullaniciGirisi", "\"" + Application.ExecutablePath + "\"");
            }
            else
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                key.DeleteValue("KullaniciGirisi");
            }
        }
    }
}
