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

namespace _18759026_Yunus_İNAN__Yurt_Kayıt_Kontrol
{
    public partial class Personel : MetroFramework.Forms.MetroForm
    {
        public Personel()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Personel_Load(object sender, EventArgs e)
        {
            dgv_personel.DataSource = vt.Select(@"select p.personel_id,p.ad,p.soyad,p.tcNO,p.sigortaNo,p.adres,p.telefon,p.sifre,pG.personelGorev_id,pG.personelGorevAd,kT.kullaniciTur_id,kT.kullaniciTur
                                                        from tbl_personel p
                                                        join tbl_personelGorev pG on p.personelGorev_id=pG.personelGorev_id
                                                        join tbl_kullaniciTur kT on p.kullaniciTur_id=kT.kullaniciTur_id");

            dgv_personel.Columns[0].Visible = false;
            dgv_personel.Columns[8].Visible = false;
            dgv_personel.Columns[10].Visible = false;

            cbx_gorev.DisplayMember = "personelGorevAd";
            cbx_gorev.ValueMember = "personelGorev_id";
            cbx_gorev.DataSource = vt.Select("select * from tbl_personelGorev");

            cbx_kullaniciTur.DisplayMember = "kullaniciTur";
            cbx_kullaniciTur.ValueMember = "kullaniciTur_id";
            cbx_kullaniciTur.DataSource = vt.Select("select * from tbl_kullaniciTur");


        }

        private void btn_ekle1_Click(object sender, EventArgs e)
        {
            if (tx_tcNo.Text == "")
            {
                MessageBox.Show("TcNo Boş Bırakılamaz");
                return;
            }
            if (tx_tcNo.Text.Length != 11)
            {
                MessageBox.Show("TcNo 11 karakter olmalıdır.");
                return;
            }
            if (tx_ad.Text == "" || tx_soyad.Text == "")
            {
                MessageBox.Show("Ad Soyad Boş Bırakılamaz");
                return;
            }

            if (tx_sigortaNo.Text == "")
            {
                MessageBox.Show("Sigorta No Boş Bırakılamaz");
                return;
            }
            if (tx_adres.Text == "")
            {
                MessageBox.Show("Adres Boş Bırakılamaz");
                return;
            }
            if (tx_telefon.Text == "")
            {
                MessageBox.Show("Telefon Boş Bırakılamaz");
                return;
            }
            if (tx_telefon.Text.Length != 10)
            {
                MessageBox.Show("Telefon 10 karakter olmalıdır.");
                return;
            }
            if (tx_sifre.Text == "")
            {
                MessageBox.Show("Şifre Boş Bırakılamaz");
                return;
            }
            if (cbx_gorev.SelectedIndex == -1)
            {
                MessageBox.Show("Görev kısmı boş bırakılamaz....");
                return;
            }
            if (cbx_kullaniciTur.SelectedIndex == -1)
            {
                MessageBox.Show("Kullanıcı Türü kısmı boş bırakılamaz....");
                return;
            }

            object kayitSay = vt.Insert(@"insert into tbl_personel(ad,soyad,tcNO,sigortaNo,adres,telefon,sifre,personelGorev_id,kullaniciTur_id)
				values('" + tx_ad.Text + "','" + tx_soyad.Text + "','" + tx_tcNo.Text + "','" + tx_sigortaNo.Text + "','" + tx_adres.Text + "','" + tx_telefon.Text + "','" +DigerIslemler.MD5Sifrele(tx_sifre.Text) + "','" + cbx_gorev.SelectedValue + "','"+cbx_kullaniciTur.SelectedValue+"')");



            if ((int)kayitSay > 0)
            {
                Personel_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_personel where personel_id="
                + dgv_personel.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Personel_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle1_Click(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_tcNo.Text == "")
            {
                MessageBox.Show("TcNo Boş Bırakılamaz");
                return;
            }
            if (tx_tcNo.Text.Length != 11)
            {
                MessageBox.Show("TcNo 11 karakter olmalıdır.");
                return;
            }
            if (tx_ad.Text == "" || tx_soyad.Text == "")
            {
                MessageBox.Show("Ad Soyad Boş Bırakılamaz");
                return;
            }

            if (tx_sigortaNo.Text == "")
            {
                MessageBox.Show("Sigorta No Boş Bırakılamaz");
                return;
            }
            if (tx_adres.Text == "")
            {
                MessageBox.Show("Adres Boş Bırakılamaz");
                return;
            }
            if (tx_telefon.Text == "")
            {
                MessageBox.Show("Telefon Boş Bırakılamaz");
                return;
            }
            if (tx_telefon.Text.Length != 10)
            {
                MessageBox.Show("Telefon 10 karakter olmalıdır.");
                return;
            }
            if (tx_sifre.Text == "")
            {
                MessageBox.Show("Şifre Boş Bırakılamaz");
                return;
            }
            if (cbx_gorev.SelectedIndex == -1)
            {
                MessageBox.Show("Görev kısmı boş bırakılamaz....");
                return;
            }
            if (cbx_kullaniciTur.SelectedIndex == -1)
            {
                MessageBox.Show("Kullanıcı Türü kısmı boş bırakılamaz....");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_personel set ad='" + tx_ad.Text + "',soyad='" + tx_soyad.Text + "',tcNO='" + tx_tcNo.Text + "',sigortaNo='"+tx_sigortaNo.Text+"',adres='" + tx_adres.Text + @"',
                telefon='" + tx_telefon.Text + "',sifre='" + DigerIslemler.MD5Sifrele(tx_sifre.Text) + "',personelGorev_id='" + cbx_gorev.SelectedValue +"',kullaniciTur_id='" + cbx_kullaniciTur.SelectedValue+ @"'
                where personel_id=" + dgv_personel.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Personel_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle2_Click(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count != 0)
                dgv_personel.SelectedRows[0].Selected = false;
        }

        private void dgv_personel_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count == 0)
            {
                tx_ad.Text = "";
                tx_soyad.Text = "";
                tx_tcNo.Text = "";
                tx_sigortaNo.Text = "";
                tx_adres.Text = "";
                tx_telefon.Text = "";
                cbx_gorev.SelectedIndex = -1;
                cbx_kullaniciTur.SelectedIndex = -1;
            }
            else
            {
                tx_ad.Text = dgv_personel.SelectedRows[0].Cells[1].Value.ToString();
                tx_soyad.Text = dgv_personel.SelectedRows[0].Cells[2].Value.ToString();
                tx_tcNo.Text = dgv_personel.SelectedRows[0].Cells[3].Value.ToString();
                tx_sigortaNo.Text = dgv_personel.SelectedRows[0].Cells[4].Value.ToString();
                tx_adres.Text = dgv_personel.SelectedRows[0].Cells[5].Value.ToString();
                tx_telefon.Text = dgv_personel.SelectedRows[0].Cells[6].Value.ToString();
                tx_sifre.Text = dgv_personel.SelectedRows[0].Cells[7].Value.ToString();
                cbx_gorev.SelectedValue = dgv_personel.SelectedRows[0].Cells[8].Value;
                cbx_kullaniciTur.SelectedValue = dgv_personel.SelectedRows[0].Cells[10].Value;

            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_sifirla_Click(object sender, EventArgs e)
        {
            if (dgv_personel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Şifresi sıfırlanacak kullanıcıyı seçiniz.");
                return;
            }

            int kayitSay = vt.UpdateDelete("update tbl_kullaniciGiris set sifre='" +
                DigerIslemler.MD5Sifrele(dgv_personel.SelectedRows[0].Cells[3].Value.ToString()) +
                "' where kullaniciGiris_id=" + dgv_personel.SelectedRows[0].Cells[0].Value);
            if (kayitSay > 0)
            {
                MessageBox.Show("Kullanıcın şifresi tckimlik numarasına sıfırlanmıştır.");
                Personel_Load(null, null);
            }
        }
    }
}
