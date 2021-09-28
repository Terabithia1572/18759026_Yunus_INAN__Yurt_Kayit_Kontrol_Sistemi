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
    public partial class Ogrenci : MetroFramework.Forms.MetroForm
    {
        public Ogrenci()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Ogrenci_Load(object sender, EventArgs e)
        {
            AnaForm afrm = new AnaForm();
            if (afrm.kullaniciTur_id != 1)
            {
                btn_guncelle1.Enabled = false;
                brn_sil.Enabled = false;

            }
            dgv_ogrenci.DataSource = vt.Select(@"select o.ogr_id,o.ad,o.soyad,o.tcNO,o.ogrNo,o.adres,o.telefon,b.bolum_id,b.bolum,oD.oda_id,oD.odaNo
                                                from tbl_ogrenci o
                                                join tbl_bolum b on o.bolum_id=b.bolum_id
                                                join tbl_oda oD on o.oda_id=oD.oda_id
                                                ");

            dgv_ogrenci.Columns[0].Visible = false;

            dgv_ogrenci.Columns[7].Visible = false;
            dgv_ogrenci.Columns[9].Visible = false;

            cbx_bolum.DisplayMember = "bolum";
            cbx_bolum.ValueMember = "bolum_id";
            cbx_bolum.DataSource = vt.Select("select bolum_id,bolum from tbl_bolum");

            cbx_oda.DisplayMember = "odaNo";
            cbx_oda.ValueMember = "oda_id";
            cbx_oda.DataSource = vt.Select("Select oda_id,odaNo from tbl_oda");
        }



        private void dgv_ogrenci_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_ogrenci.SelectedRows.Count == 0)
            {
                tx_ad.Text = "";
                tx_soyad.Text = "";
                tx_tcNo.Text = "";
                tx_ogrNo.Text = "";
                tx_adres.Text = "";
                tx_telefon.Text = "";
                cbx_bolum.SelectedIndex = -1;
                cbx_oda.SelectedIndex = -1;
            }
            else
            {
                tx_ad.Text = dgv_ogrenci.SelectedRows[0].Cells[1].Value.ToString();
                tx_soyad.Text = dgv_ogrenci.SelectedRows[0].Cells[2].Value.ToString();
                tx_tcNo.Text = dgv_ogrenci.SelectedRows[0].Cells[3].Value.ToString();
                tx_ogrNo.Text = dgv_ogrenci.SelectedRows[0].Cells[4].Value.ToString();
                tx_adres.Text = dgv_ogrenci.SelectedRows[0].Cells[5].Value.ToString();
                tx_telefon.Text = dgv_ogrenci.SelectedRows[0].Cells[6].Value.ToString();
                cbx_bolum.SelectedValue = dgv_ogrenci.SelectedRows[0].Cells[7].Value;
                cbx_oda.SelectedValue = dgv_ogrenci.SelectedRows[0].Cells[9].Value;
            }
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

            if (tx_ogrNo.Text == "")
            {
                MessageBox.Show("Öğrenci No Boş Bırakılamaz");
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

            if (cbx_bolum.SelectedIndex == -1)
            {
                MessageBox.Show("Bölüm kısmı boş bırakılamaz....");
                return;
            }

            if (cbx_oda.SelectedIndex == -1)
            {
                MessageBox.Show("Oda kısmı boş bırakılamaz....");
                return;
            }

            object kayitSay = vt.Insert(@"insert into tbl_ogrenci(ad,soyad,tcNO,ogrNo,adres,telefon,bolum_id,oda_id)
				values('" + tx_ad.Text + "','" + tx_soyad.Text + "','" + tx_tcNo.Text + "','" + tx_ogrNo.Text + "','" + tx_adres.Text + "','" + tx_telefon.Text + "','" + cbx_bolum.SelectedValue + "','" + cbx_oda.SelectedValue + "')");



            if ((int)kayitSay > 0)
            {
                Ogrenci_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_ogrenci.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_ogrenci where ogr_id="
                + dgv_ogrenci.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Ogrenci_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle1_Click(object sender, EventArgs e)
        {
            if (dgv_ogrenci.SelectedRows.Count == 0)
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

            if (tx_ogrNo.Text == "")
            {
                MessageBox.Show("Öğrenci No Boş Bırakılamaz");
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
            if (tx_telefon.Text.Length < 10)
            {
                MessageBox.Show("Telefon 9 karakter olmalıdır.");
                return;
            }

            if (cbx_bolum.SelectedIndex == -1)
            {
                MessageBox.Show("Bölüm kısmı boş bırakılamaz....");
                return;
            }

            if (cbx_oda.SelectedIndex == -1)
            {
                MessageBox.Show("Oda kısmı boş bırakılamaz....");
                return;
            }

            int a = vt.UpdateDelete(@"update tbl_ogrenci set ad='" + tx_ad.Text + "',soyad='" + tx_soyad.Text + "',tcNO='" + tx_tcNo.Text + "',ogrNo='"+tx_ogrNo.Text+"',adres='" + tx_adres.Text + @"',
                telefon='" + tx_telefon.Text + "',bolum_id='" + cbx_bolum.SelectedValue + "',oda_id='" + cbx_oda.SelectedValue + @"'
                where ogr_id=" + dgv_ogrenci.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Ogrenci_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle2_Click(object sender, EventArgs e)
        {
            if (dgv_ogrenci.SelectedRows.Count != 0)
                dgv_ogrenci.SelectedRows[0].Selected = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
