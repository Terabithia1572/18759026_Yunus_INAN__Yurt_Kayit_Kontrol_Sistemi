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
    public partial class İzin : MetroFramework.Forms.MetroForm
    {
        public İzin()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void İzin_Load(object sender, EventArgs e)
        {
            dgv_izin.DataSource = vt.Select(@"select i.izin_id,i.izinGirisTarihi,i.izinBitisTarihi,o.ogr_id,o.ad+' '+o.soyad adSoyad
                                                from tbl_izin i
                                                join tbl_ogrenci o on i.ogr_id=o.ogr_id");

            dgv_izin.Columns[0].Visible = false;
            dgv_izin.Columns[3].Visible = false;

            cbx_ogrenci.DisplayMember = "adSoyad";
            cbx_ogrenci.ValueMember = "ogr_id";
            cbx_ogrenci.DataSource = vt.Select("select ogr_id,ad+' '+soyad adSoyad from tbl_ogrenci");
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci kısmı boş bırakılamaz....");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_izin(izinGirisTarihi,izinBitisTarihi,o.ogr_id)
				values('" + dtp_izinGiris.Value.ToString("MM/dd/yyyy") + "','"+dtp_izinBitis.Value.ToString("MM/dd/yyyy")+ "','"+cbx_ogrenci.SelectedValue+"')");



            if ((int)kayitSay > 0)
            {
                İzin_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_izin.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_izin where izin_id="
                + dgv_izin.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                İzin_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_izin.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci kısmı boş bırakılamaz....");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_izin set izinGirisTarihi='"+dtp_izinGiris.Value.ToString("MM/dd/yyyy")+ "',izinBitisTarihi='"+dtp_izinBitis.Value.ToString("MM/dd/yyyy") + "',ogr_id='"+cbx_ogrenci.SelectedValue+@"'
                where izin_id=" + dgv_izin.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                İzin_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_izin.SelectedRows.Count != 0)
                dgv_izin.SelectedRows[0].Selected = false;
                dtp_izinGiris.Text = DateTime.Now.ToString();
                dtp_izinBitis.Text = DateTime.Now.ToString();
        }

        private void dgv_izin_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_izin.SelectedRows.Count == 0)
            {
                cbx_ogrenci.SelectedIndex = -1;
            }
            else
            {
                dtp_izinGiris.Text = dgv_izin.SelectedRows[0].Cells[1].Value.ToString();
                dtp_izinBitis.Text = dgv_izin.SelectedRows[0].Cells[2].Value.ToString();
                cbx_ogrenci.SelectedValue = dgv_izin.SelectedRows[0].Cells[3].Value;
            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
