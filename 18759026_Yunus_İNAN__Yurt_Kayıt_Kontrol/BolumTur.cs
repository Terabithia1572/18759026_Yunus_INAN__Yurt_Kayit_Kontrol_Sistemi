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
    public partial class BolumTur : MetroFramework.Forms.MetroForm
    {
        public BolumTur()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void BolumTur_Load(object sender, EventArgs e)
        {
            dgv_bolumTur.DataSource = vt.Select(@"select * from tbl_bolumTur");

            dgv_bolumTur.Columns[0].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_bolumTur.Text == "")
            {
                MessageBox.Show("Bölüm Türü Boş Bırakılamaz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_bolumTur(bolumTur)
				values('" + tx_bolumTur.Text + "')");



            if ((int)kayitSay > 0)
            {
                BolumTur_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_bolumTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_bolumTur where bolumTur_id="
                + dgv_bolumTur.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                BolumTur_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_bolumTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_bolumTur.Text == "")
            {
                MessageBox.Show("Bölüm Boş Bırakılamaz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_bolumTur set bolumTur='" + tx_bolumTur.Text + @"'
                where bolumTur_id=" + dgv_bolumTur.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                BolumTur_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void dgv_bolumTur_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_bolumTur.SelectedRows.Count == 0)
            {
                tx_bolumTur.Text = "";
                

            }
            else
            {
                tx_bolumTur.Text = dgv_bolumTur.SelectedRows[0].Cells[1].Value.ToString();
                
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_bolumTur.SelectedRows.Count != 0)
                dgv_bolumTur.SelectedRows[0].Selected = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
