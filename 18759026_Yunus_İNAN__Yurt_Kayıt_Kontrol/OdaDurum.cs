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
    public partial class OdaDurum : MetroFramework.Forms.MetroForm
    {
        public OdaDurum()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);

        private void OdaDurum_Load(object sender, EventArgs e)
        {
            dgv_odaDurum.DataSource = vt.Select(@"select * from tbl_odaDurum");

            dgv_odaDurum.Columns[0].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_odaDurum.Text == "")
            {
                MessageBox.Show("Oda Durumu Boş Bırakılamaz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_odaDurum(odaDurum)
				values('" + tx_odaDurum.Text + "')");



            if ((int)kayitSay > 0)
            {
                OdaDurum_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_odaDurum.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_odaDurum where odaDurum_id="
                + dgv_odaDurum.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                OdaDurum_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_odaDurum.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_odaDurum.Text == "")
            {
                MessageBox.Show("Oda Durumu Boş Bırakılamaz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_odaDurum set odaDurum='" + tx_odaDurum.Text + @"'
                where odaDurum_id=" + dgv_odaDurum.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                OdaDurum_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_odaDurum.SelectedRows.Count != 0)
                dgv_odaDurum.SelectedRows[0].Selected = false;
        }

        private void dgv_odaDurum_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_odaDurum.SelectedRows.Count == 0)
            {
                tx_odaDurum.Text = "";


            }
            else
            {
                tx_odaDurum.Text = dgv_odaDurum.SelectedRows[0].Cells[1].Value.ToString();

            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
