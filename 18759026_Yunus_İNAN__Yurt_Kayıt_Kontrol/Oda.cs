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
    public partial class Oda : MetroFramework.Forms.MetroForm
    {
        public Oda()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Oda_Load(object sender, EventArgs e)
        {
            dgv_oda.DataSource = vt.Select(@"select o.oda_id,o.odaNo,o.yatakSayisi,o.kat,oT.odaTur_id,oT.odaTur,oD.odaDurum_id,oD.odaDurum
                                             from tbl_oda o
                                             inner join tbl_odaTur oT on o.odaTur_id=oT.odaTur_id
                                             inner join tbl_odaDurum oD on o.odaDurum_id=oD.odaDurum_id");

            dgv_oda.Columns[0].Visible = false;

            dgv_oda.Columns[4].Visible = false;
            dgv_oda.Columns[6].Visible = false;

            cbx_odaTur.DisplayMember = "odaTur";
            cbx_odaTur.ValueMember = "odaTur_id";
            cbx_odaTur.DataSource = vt.Select("select * from tbl_odaTur");

            cbx_odaDurum.DisplayMember = "odaDurum";
            cbx_odaDurum.ValueMember = "odaDurum_id";
            cbx_odaDurum.DataSource = vt.Select("Select * from tbl_odaDurum");

            
        }

        private void btn_ekle1_Click(object sender, EventArgs e)
        {
            if (tx_odaNo.Text == "")
            {
                MessageBox.Show("Oda No Boş Bırakılamaz");
                return;
            }
           
            if (tx_yatak.Text == "")
            {
                MessageBox.Show("Yatak Sayısı Boş Bırakılamaz");
                return;
            }

            if (tx_kat.Text == "")
            {
                MessageBox.Show("Kat Boş Bırakılamaz");
                return;
            }
           

            if (cbx_odaTur.SelectedIndex == -1)
            {
                MessageBox.Show("Oda Türünü Seçiniz");
                return;
            }

            if (cbx_odaDurum.SelectedIndex == -1)
            {
                MessageBox.Show("Oda Durumunu Seçiniz");
                return;
            }

            object kayitSay = vt.Insert(@"insert into tbl_oda(odaNo,yatakSayisi,kat,odaTur_id,odaDurum_id)
				values('" + tx_odaNo.Text + "','" + tx_yatak.Text + "','" + tx_kat.Text + "','" + cbx_odaTur.SelectedValue + "','" + cbx_odaDurum.SelectedValue + "')");



            if ((int)kayitSay > 0)
            {
                Oda_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_oda.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_oda where oda_id="
                + dgv_oda.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Oda_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle1_Click(object sender, EventArgs e)
        {
            if (tx_odaNo.Text == "")
            {
                MessageBox.Show("Oda No Boş Bırakılamaz");
                return;
            }

            if (tx_yatak.Text == "")
            {
                MessageBox.Show("Yatak Sayısı Boş Bırakılamaz");
                return;
            }

            if (tx_kat.Text == "")
            {
                MessageBox.Show("Kat Boş Bırakılamaz");
                return;
            }


            if (cbx_odaTur.SelectedIndex == -1)
            {
                MessageBox.Show("Oda Türünü Seçiniz");
                return;
            }

            if (cbx_odaDurum.SelectedIndex == -1)
            {
                MessageBox.Show("Oda Durumunu Seçiniz");
                return;
            }

            if (dgv_oda.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_oda set odaNo='" + tx_odaNo.Text + "',yatakSayisi='" + tx_yatak.Text + "',kat='" + tx_kat.Text + "',odaTur_id='" + cbx_odaTur.SelectedValue + "',odaDurum_id='" + cbx_odaDurum.SelectedValue + @"'
                where oda_id=" + dgv_oda.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Oda_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void dgv_oda_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_oda.SelectedRows.Count == 0)
            {
                tx_odaNo.Text = "";
                tx_yatak.Text = "";
                tx_kat.Text = "";
             
                cbx_odaTur.SelectedIndex = -1;
                cbx_odaDurum.SelectedIndex = -1;
            }
            else
            {
                tx_odaNo.Text = dgv_oda.SelectedRows[0].Cells[1].Value.ToString();
                tx_yatak.Text = dgv_oda.SelectedRows[0].Cells[2].Value.ToString();
                tx_kat.Text = dgv_oda.SelectedRows[0].Cells[3].Value.ToString();
                
                cbx_odaTur.SelectedValue = dgv_oda.SelectedRows[0].Cells[4].Value;
                cbx_odaDurum.SelectedValue = dgv_oda.SelectedRows[0].Cells[6].Value;
            }
        }

        private void btn_temizle2_Click(object sender, EventArgs e)
        {
            if (dgv_oda.SelectedRows.Count != 0)
                dgv_oda.SelectedRows[0].Selected = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
