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
    public partial class Veli : MetroFramework.Forms.MetroForm
    {
        public Veli()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Veli_Load(object sender, EventArgs e)
        {
            dgv_veli.DataSource = vt.Select(@"select v.veli_id,v.ad,v.soyad,v.tcNO,v.telefon,v.adres,o.ogr_id,o.ad+' '+o.soyad ogrAdSoyad
 from tbl_veli v
 join tbl_ogrenci o on v.ogr_id=o.ogr_id");

            dgv_veli.Columns[0].Visible = false;

            dgv_veli.Columns[6].Visible = false;
            

            cbx_ogrenci.DisplayMember = "adSoyad";
            cbx_ogrenci.ValueMember = "ogr_id";
            cbx_ogrenci.DataSource = vt.Select("select ogr_id,ad+' '+soyad adSoyad from tbl_ogrenci");
        }

        private void btn_ekle_Click(object sender, EventArgs e)
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
            if (tx_telefon.Text.Length != 9)
            {
                MessageBox.Show("Telefon 10 karakter olmalıdır.");
                return;
            }

            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci seçiniz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_veli(ad,soyad,tcNO,telefon,adres,ogr_id)
				values('" + tx_ad.Text + "','" + tx_soyad.Text + "','" + tx_tcNo.Text + "','" + tx_telefon.Text + "','" + tx_adres.Text + "','" + cbx_ogrenci.SelectedValue + "')");



            if ((int)kayitSay > 0)
            {
                Veli_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_veli.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_veli where veli_id="
                + dgv_veli.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Veli_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_veli.SelectedRows.Count == 0)
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
            if (tx_telefon.Text.Length != 9)
            {
                MessageBox.Show("Telefon 10 karakter olmalıdır.");
                return;
            }

            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci seçiniz");
                return;
            }

            int a = vt.UpdateDelete(@"update tbl_veli set ad='" + tx_ad.Text + "',soyad='" + tx_soyad.Text + "',tcNO='" + tx_tcNo.Text + "',telefon='" + tx_telefon.Text + @"',
                adres='" + tx_adres.Text + "',ogr_id='" + cbx_ogrenci.SelectedValue + @"'
                where veli_id=" + dgv_veli.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Veli_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void dgv_veli_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_veli.SelectedRows.Count == 0)
            {
                tx_ad.Text = "";
                tx_soyad.Text = "";
                tx_tcNo.Text = "";
                tx_telefon.Text = "";
                tx_adres.Text = "";
                cbx_ogrenci.SelectedIndex = -1;
                
            }
            else
            {
                tx_ad.Text = dgv_veli.SelectedRows[0].Cells[1].Value.ToString();
                tx_soyad.Text = dgv_veli.SelectedRows[0].Cells[2].Value.ToString();
                tx_tcNo.Text = dgv_veli.SelectedRows[0].Cells[3].Value.ToString();
                tx_telefon.Text = dgv_veli.SelectedRows[0].Cells[4].Value.ToString();
                tx_adres.Text = dgv_veli.SelectedRows[0].Cells[5].Value.ToString();
                cbx_ogrenci.SelectedValue = dgv_veli.SelectedRows[0].Cells[6].Value;
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_veli.SelectedRows.Count != 0)
                dgv_veli.SelectedRows[0].Selected = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
