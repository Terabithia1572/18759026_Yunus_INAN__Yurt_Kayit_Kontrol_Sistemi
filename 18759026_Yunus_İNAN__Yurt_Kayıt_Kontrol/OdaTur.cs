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
    public partial class OdaTur : MetroFramework.Forms.MetroForm
    {
        public OdaTur()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void OdaTur_Load(object sender, EventArgs e)
        {
            dgv_odaTur.DataSource = vt.Select(@"select * from tbl_odaTur");

            dgv_odaTur.Columns[0].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_odaTur.Text == "")
            {
                MessageBox.Show("Oda Türü Boş Bırakılamaz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_odaTur(odaTur)
				values('" + tx_odaTur.Text + "')");



            if ((int)kayitSay > 0)
            {
                OdaTur_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_odaTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_odaTur where odaTur_id="
                + dgv_odaTur.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                OdaTur_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_odaTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_odaTur.Text == "")
            {
                MessageBox.Show("Oda Boş Bırakılamaz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_odaTur set odaTur='" + tx_odaTur.Text + @"'
                where odaTur_id=" + dgv_odaTur.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                OdaTur_Load(null, null);
                //MessageBox.Show("Kayıt güncellendi");
                MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_odaTur.SelectedRows.Count != 0)
                dgv_odaTur.SelectedRows[0].Selected = false;
        }

        private void dgv_odaTur_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_odaTur.SelectedRows.Count == 0)
            {
                tx_odaTur.Text = "";


            }
            else
            {
                tx_odaTur.Text = dgv_odaTur.SelectedRows[0].Cells[1].Value.ToString();

            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
