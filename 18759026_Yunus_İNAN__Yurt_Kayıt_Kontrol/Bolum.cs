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
    public partial class Bolum : MetroFramework.Forms.MetroForm
    {
        public Bolum()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Bolum_Load(object sender, EventArgs e)
        {
            dgv_bolum.DataSource = vt.Select(@"select b.bolum_id,b.bolum,bT.bolumTur_id,bT.bolumTur
 from tbl_bolum b
 join tbl_bolumTur bT on b.bolumTur_id=bT.bolumTur_id");

            dgv_bolum.Columns[0].Visible = false;

            dgv_bolum.Columns[2].Visible = false;


            cbx_bolumTur.DisplayMember = "bolumTur";
            cbx_bolumTur.ValueMember = "bolumTur_id";
            cbx_bolumTur.DataSource = vt.Select("select * from tbl_bolumTur");
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_bolum.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_bolum where bolum_id="
                + dgv_bolum.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Bolum_Load(null, null);
                //MessageBox.Show("Kayıt Silindi");
                MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_ekle1_Click(object sender, EventArgs e)
        {
            if (tx_bolum.Text == "")
            {
                MessageBox.Show("Bölüm Boş Bırakılamaz");
                return;
            }
            

            if (cbx_bolumTur.SelectedIndex == -1)
            {
                MessageBox.Show("Bölüm Türü seçiniz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_bolum(bolum,bolumTur_id)
				values('" + tx_bolum.Text + "','" + cbx_bolumTur.SelectedValue + "')");



            if ((int)kayitSay > 0)
            {
                Bolum_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void btn_guncelle1_Click(object sender, EventArgs e)
        {
            if (dgv_bolum.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_bolum.Text == "")
            {
                MessageBox.Show("Bölüm Boş Bırakılamaz");
                return;
            }


            if (cbx_bolumTur.SelectedIndex == -1)
            {
                MessageBox.Show("Bölüm Türü seçiniz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_bolum set bolum='" + tx_bolum.Text + "',bolumTur_id='" + cbx_bolumTur.SelectedValue + @"'
                where bolum_id=" + dgv_bolum.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Bolum_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void dgv_bolum_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_bolum.SelectedRows.Count == 0)
            {
                tx_bolum.Text = "";
                cbx_bolumTur.SelectedIndex = -1;

            }
            else
            {
                tx_bolum.Text = dgv_bolum.SelectedRows[0].Cells[1].Value.ToString();
                cbx_bolumTur.SelectedValue = dgv_bolum.SelectedRows[0].Cells[2].Value;
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_bolum.SelectedRows.Count != 0)
                dgv_bolum.SelectedRows[0].Selected = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
