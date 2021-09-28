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
    public partial class OdemeTur : MetroFramework.Forms.MetroForm
    {
        public OdemeTur()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void OdemeTur_Load(object sender, EventArgs e)
        {
            dgv_odemeTur.DataSource = vt.Select(@"select * from tbl_odemeTur");

            dgv_odemeTur.Columns[0].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_odemeTur.Text == "")
            {
                MessageBox.Show("Ödeme Tür Boş Bırakılamaz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_odemeTur(odemeTurAd)
				values('" + tx_odemeTur.Text + "')");



            if ((int)kayitSay > 0)
            {
                OdemeTur_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_odemeTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_odemeTur where odemeTur_id="
                + dgv_odemeTur.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                OdemeTur_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_odemeTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_odemeTur.Text == "")
            {
                MessageBox.Show("Ödeme Türü Boş Bırakılamaz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_odemeTur set odemeTurAd='" + tx_odemeTur.Text + @"'
                where odemeTur_id=" + dgv_odemeTur.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                OdemeTur_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_odemeTur.SelectedRows.Count != 0)
                dgv_odemeTur.SelectedRows[0].Selected = false;

            
        }

        private void dgv_odemeTur_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_odemeTur.SelectedRows.Count == 0)
            {
                tx_odemeTur.Text = "";
            }
            else
            {
                tx_odemeTur.Text = dgv_odemeTur.SelectedRows[0].Cells[1].Value.ToString();

            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
