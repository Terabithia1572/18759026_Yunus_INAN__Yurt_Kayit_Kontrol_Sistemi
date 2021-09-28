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
    public partial class Yatak : MetroFramework.Forms.MetroForm
    {
        public Yatak()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Yatak_Load(object sender, EventArgs e)
        {
            dgv_yatak.DataSource = vt.Select(@" select  y.yatak_id,y.yatakNo,o.oda_id,o.odaNo
 from tbl_yatak y
 join tbl_oda o on y.oda_id=o.oda_id");

            dgv_yatak.Columns[0].Visible = false;

            dgv_yatak.Columns[2].Visible = false;
            

            cbx_oda.DisplayMember = "odaNo";
            cbx_oda.ValueMember = "oda_id";
            cbx_oda.DataSource = vt.Select("select oda_id,odaNo from tbl_oda");

            
        }

        private void btn_ekle1_Click(object sender, EventArgs e)
        {
            if (tx_yatakNo.Text == "")
            {
                MessageBox.Show("Yatak No Boş Bırakılamaz");
                return;
            }

            if (cbx_oda.SelectedIndex == -1)
            {
                MessageBox.Show("Oda No Seçiniz");
                return;
            }


            object kayitSay = vt.Insert(@"insert into tbl_yatak(yatakNo,oda_id)
				values('" + tx_yatakNo.Text + "','" + cbx_oda.SelectedValue + "')");



            if ((int)kayitSay > 0)
            {
                Yatak_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_yatak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_yatak where yatak_id="
                + dgv_yatak.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Yatak_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle1_Click(object sender, EventArgs e)
        {
            if (tx_yatakNo.Text == "")
            {
                MessageBox.Show("Yatak No Boş Bırakılamaz");
                return;
            }

            if (cbx_oda.SelectedIndex == -1)
            {
                MessageBox.Show("Oda no Seçiniz");
                return;
            }

            
            if (dgv_yatak.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_yatak set yatakNo='" + tx_yatakNo.Text + "',oda_id='" + cbx_oda.SelectedValue + @"'
                where yatak_id=" + dgv_yatak.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Yatak_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle2_Click(object sender, EventArgs e)
        {
            if (dgv_yatak.SelectedRows.Count != 0)
                dgv_yatak.SelectedRows[0].Selected = false;
        }

        private void dgv_yatak_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_yatak.SelectedRows.Count == 0)
            {
                tx_yatakNo.Text = "";
               
                cbx_oda.SelectedIndex = -1;
                
            }
            else
            {
                tx_yatakNo.Text = dgv_yatak.SelectedRows[0].Cells[1].Value.ToString();
                cbx_oda.SelectedValue = dgv_yatak.SelectedRows[0].Cells[2].Value;
                
            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
