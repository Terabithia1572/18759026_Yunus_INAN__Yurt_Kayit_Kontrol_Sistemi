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
    public partial class Odeme : MetroFramework.Forms.MetroForm
    {
        public Odeme()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void Odeme_Load(object sender, EventArgs e)
        {
            dgv_odeme.DataSource = vt.Select(@"select o.odeme_id,o.odemeTutari,oP.odemePlan_id,oP.odemeTarihi,o.odeyenAdSoyad,oG.ogr_id,oG.ad+' '+oG.soyad AdSoyad,oT.odemeTur_id,oT.odemeTurAd,p.personel_id,p.ad+' '+p.soyad personelAdSoyad
                                    from tbl_odeme o
                                    join tbl_odemePlan oP on o.odemePlan_id=oP.odemePlan_id
                                    join tbl_ogrenci oG on o.ogr_id=oG.ogr_id
                                    join tbl_odemeTur oT on o.odemeTur_id=oT.odemeTur_id
                                    join tbl_personel p on o.personel_id=p.personel_id");

            dgv_odeme.Columns[0].Visible = false;

            dgv_odeme.Columns[2].Visible = false;
            dgv_odeme.Columns[5].Visible = false;
            dgv_odeme.Columns[7].Visible = false;
            dgv_odeme.Columns[9].Visible = false;

            cbx_odemePlani.DisplayMember = "odemeTarihi";
            cbx_odemePlani.ValueMember = "odemePlan_id";
            cbx_odemePlani.DataSource = vt.Select("select odemePlan_id,odemeTarihi from tbl_odemePlan");

            cbx_ogrenci.DisplayMember = "adSoyad";
            cbx_ogrenci.ValueMember = "ogr_id";
            cbx_ogrenci.DataSource = vt.Select("select ogr_id,ad+' '+soyad adSoyad from tbl_ogrenci");

            cbx_odemeTur.DisplayMember = "odemeTurAd";
            cbx_odemeTur.ValueMember = "odemeTur_id";
            cbx_odemeTur.DataSource = vt.Select("select * from tbl_odemeTur");

            cbx_personel.DisplayMember = "PadSoyad";
            cbx_personel.ValueMember = "personel_id";
            cbx_personel.DataSource = vt.Select("select personel_id,ad+' '+soyad PadSoyad from tbl_personel");
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_odemeTutari.Text == "")
            {
                MessageBox.Show("Ödeme Tutaru Boş Bırakılamaz");
                return;
            }
            if (cbx_odemePlani.SelectedIndex == -1)
            {
                MessageBox.Show("Ödeme Planı kısmı boş bırakılamaz....");
                return;
            }
            if (tx_odeyenAdSoyad.Text == "")
            {
                MessageBox.Show("Ödeyen Ad Soyad Boş Bırakılamaz");
                return;
            }
            if (cbx_odemePlani.SelectedIndex == -1)
            {
                MessageBox.Show("Ödeme Planı kısmı boş bırakılamaz....");
                return;
            }

            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci kısmı boş bırakılamaz....");
                return;
            }
            if (cbx_odemeTur.SelectedIndex == -1)
            {
                MessageBox.Show("Ödeme Türü kısmı boş bırakılamaz....");
                return;
            }
            if (cbx_personel.SelectedIndex == -1)
            {
                MessageBox.Show("Personel kısmı boş bırakılamaz....");
                return;
            }

            object kayitSay = vt.Insert(@"insert into tbl_odeme(odemeTutari,odemePlan_id,odeyenAdSoyad,ogr_id,odemeTur_id,personel_id)
				values('"+tx_odemeTutari.Text+"','"+cbx_odemePlani.SelectedValue+"','"+tx_odeyenAdSoyad.Text+"','"+cbx_ogrenci.SelectedValue+"','"+cbx_odemeTur.SelectedValue+"','"+cbx_personel.SelectedValue+"')");



            if ((int)kayitSay > 0)
            {
                Odeme_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void btn_sil_Click(object sender, EventArgs e)
        {

            if (dgv_odeme.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_odeme where odeme_id="
                + dgv_odeme.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                Odeme_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_odeme.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_odemeTutari.Text == "")
            {
                MessageBox.Show("Ödeme Tutaru Boş Bırakılamaz");
                return;
            }
            if (cbx_odemePlani.SelectedIndex == -1)
            {
                MessageBox.Show("Ödeme Planı kısmı boş bırakılamaz....");
                return;
            }
            if (tx_odeyenAdSoyad.Text == "")
            {
                MessageBox.Show("Ödeyen Ad Soyad Boş Bırakılamaz");
                return;
            }
            if (cbx_odemePlani.SelectedIndex == -1)
            {
                MessageBox.Show("Ödeme Planı kısmı boş bırakılamaz....");
                return;
            }

            if (cbx_ogrenci.SelectedIndex == -1)
            {
                MessageBox.Show("Öğrenci kısmı boş bırakılamaz....");
                return;
            }
            if (cbx_odemeTur.SelectedIndex == -1)
            {
                MessageBox.Show("Ödeme Türü kısmı boş bırakılamaz....");
                return;
            }
            if (cbx_personel.SelectedIndex == -1)
            {
                MessageBox.Show("Personel kısmı boş bırakılamaz....");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_odeme set odemeTutari='"+tx_odemeTutari.Text+"',odemePlan_id='"+cbx_odemePlani.SelectedValue+@"'
            ,odeyenAdSoyad='"+tx_odeyenAdSoyad.Text+"',ogr_id='"+cbx_ogrenci.SelectedValue+"',odemeTur_id='"+cbx_odemeTur.SelectedValue+"',personel_id='"+cbx_personel.SelectedValue+@"'
            where odeme_id=" + dgv_odeme.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                Odeme_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void dgv_odeme_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_odeme.SelectedRows.Count == 0)
            {
                tx_odemeTutari.Text = "";
                cbx_odemePlani.SelectedIndex = -1;
                tx_odeyenAdSoyad.Text = "";
                cbx_ogrenci.SelectedIndex = -1;
                cbx_odemeTur.SelectedIndex = -1;
                cbx_personel.SelectedIndex = -1;
            }
            else
            {
                tx_odemeTutari.Text = dgv_odeme.SelectedRows[0].Cells[1].Value.ToString();
                cbx_odemePlani.SelectedValue = dgv_odeme.SelectedRows[0].Cells[2].Value;
                tx_odeyenAdSoyad.Text = dgv_odeme.SelectedRows[0].Cells[4].Value.ToString();
                cbx_ogrenci.SelectedValue = dgv_odeme.SelectedRows[0].Cells[5].Value;
                cbx_odemeTur.SelectedValue = dgv_odeme.SelectedRows[0].Cells[7].Value;
                cbx_personel.SelectedValue = dgv_odeme.SelectedRows[0].Cells[9].Value;
            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_odeme.SelectedRows.Count != 0)
                dgv_odeme.SelectedRows[0].Selected = false;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
