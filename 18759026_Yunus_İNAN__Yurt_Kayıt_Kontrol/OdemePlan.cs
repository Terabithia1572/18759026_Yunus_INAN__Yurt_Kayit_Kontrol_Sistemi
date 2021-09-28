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
    public partial class OdemePlan : MetroFramework.Forms.MetroForm
    {
        public OdemePlan()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void OdemePlan_Load(object sender, EventArgs e)
        {
            dgv_odemePlan.DataSource = vt.Select(@"select o.odemePlan_id,o.odenecekTutar,o.odemeTarihi,oG.ogr_id,oG.ad+' '+oG.soyad OgrenciAdSoyad
                                        from tbl_odemePlan o
                                        join tbl_ogrenci oG on o.ogr_id=oG.ogr_id");

            dgv_odemePlan.Columns[0].Visible = false;
            dgv_odemePlan.Columns[3].Visible = false;

            cbx_ogrenci.DisplayMember = "OgrenciAdSoyad";
            cbx_ogrenci.ValueMember = "ogr_id";
            cbx_ogrenci.DataSource = vt.Select("select ogr_id,ad+' '+soyad OgrenciAdSoyad from tbl_ogrenci");
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_odemeTutari.Text == "")
            {
                MessageBox.Show("Ödeme Tutarı Boş Bırakılamaz");
                return;
            }
            
            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci Seçiniz");
                return;
            }

            //Value.ToShortDateString()
            object kayitSay = vt.Insert(@"insert into tbl_odemePlan(odenecekTutar,odemeTarihi,ogr_id)
				values('" + tx_odemeTutari.Text + "','" + dtp_odemeTarihi.Value.ToString("MM/dd/yyyy") + "','" + cbx_ogrenci.SelectedValue + "')");



            if ((int)kayitSay > 0)
            {
                OdemePlan_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_odemePlan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_odemePlan where odemePlan_id="
                + dgv_odemePlan.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                OdemePlan_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_odemePlan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_odemeTutari.Text == "")
            {
                MessageBox.Show("Ödeme Tutarı Boş Bırakılamaz");
                return;
            }

            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci Seçiniz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_odemePlan set odenecekTutar='" + tx_odemeTutari.Text + "',odemeTarihi='" + dtp_odemeTarihi.Value.ToString("MM/dd/yyyy") + "',ogr_id='" + cbx_ogrenci.SelectedValue + @"'
                where odemePlan_id=" + dgv_odemePlan.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                OdemePlan_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void dgv_odemePlan_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_odemePlan.SelectedRows.Count == 0)
            {
                tx_odemeTutari.Text = "";
                cbx_ogrenci.SelectedIndex = -1;
            }
            else
            {
                tx_odemeTutari.Text = dgv_odemePlan.SelectedRows[0].Cells[1].Value.ToString();
                dtp_odemeTarihi.Text = dgv_odemePlan.SelectedRows[0].Cells[2].Value.ToString();
                cbx_ogrenci.SelectedValue = dgv_odemePlan.SelectedRows[0].Cells[3].Value;
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_odemePlan.SelectedRows.Count != 0)
                dgv_odemePlan.SelectedRows[0].Selected = false;
                dtp_odemeTarihi.Text = DateTime.Now.ToString();
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
