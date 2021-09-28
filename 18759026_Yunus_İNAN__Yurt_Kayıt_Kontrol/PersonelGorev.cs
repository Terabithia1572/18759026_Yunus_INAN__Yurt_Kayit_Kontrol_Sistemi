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
    public partial class PersonelGorev : MetroFramework.Forms.MetroForm
    {
        public PersonelGorev()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void PersonelGorev_Load(object sender, EventArgs e)
        {
            dgv_personelGorev.DataSource = vt.Select(@"select * from tbl_personelGorev");

            dgv_personelGorev.Columns[0].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_personelGorev.Text == "")
            {
                MessageBox.Show("Personel Gorevi Boş Bırakılamaz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_personelGorev(personelGorevAd)
				values('" + tx_personelGorev.Text + "')");



            if ((int)kayitSay > 0)
            {
                PersonelGorev_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_personelGorev.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_personelGorev where personelGorev_id="
                + dgv_personelGorev.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                PersonelGorev_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_personelGorev.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_personelGorev.Text == "")
            {
                MessageBox.Show("Personel Görev Boş Bırakılamaz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_personelGorev set personelGorevAd='" + tx_personelGorev.Text + @"'
                where personelGorev_id=" + dgv_personelGorev.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                PersonelGorev_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_personelGorev.SelectedRows.Count != 0)
                dgv_personelGorev.SelectedRows[0].Selected = false;
        }

        private void dgv_personelGorev_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_personelGorev.SelectedRows.Count == 0)
            {
                tx_personelGorev.Text = "";


            }
            else
            {
                tx_personelGorev.Text = dgv_personelGorev.SelectedRows[0].Cells[1].Value.ToString();

            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
