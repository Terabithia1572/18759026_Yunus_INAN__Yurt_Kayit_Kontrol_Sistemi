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
    public partial class KullaniciTur : MetroFramework.Forms.MetroForm
    {
        public KullaniciTur()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vt = new VeritabaniIslemleri(Ayarlar.Default.veritabaniAdi);
        private void KullaniciTur_Load(object sender, EventArgs e)
        {
            dgv_kullaniciTur.DataSource = vt.Select(@"select * from tbl_kullaniciTur");

            dgv_kullaniciTur.Columns[0].Visible = false;
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            if (tx_kullaniciTur.Text == "")
            {
                MessageBox.Show("Kullanıcı Türü Boş Bırakılamaz");
                return;
            }



            object kayitSay = vt.Insert(@"insert into tbl_kullaniciTur(kullaniciTur)
				values('" + tx_kullaniciTur.Text + "')");



            if ((int)kayitSay > 0)
            {
                KullaniciTur_Load(null, null);

                MessageBox.Show("Kayıt Eklendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Eklendi");
            }
        }

        private void brn_sil_Click(object sender, EventArgs e)
        {
            if (dgv_kullaniciTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek satırı seçiniz.");
                return;
            }
            int kayitSay = vt.UpdateDelete("delete from tbl_kullaniciTur where kullaniciTur_id="
                + dgv_kullaniciTur.SelectedRows[0].Cells[0].Value);

            if (kayitSay > 0)
            {
                KullaniciTur_Load(null, null);
                MessageBox.Show("Kayıt Silindi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Silindi");
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            if (dgv_kullaniciTur.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek satırı seçiniz.");
                return;
            }
            if (tx_kullaniciTur.Text == "")
            {
                MessageBox.Show("Kullanıcı Türü Boş Bırakılamaz");
                return;
            }
            int a = vt.UpdateDelete(@"update tbl_kullaniciTur set kullaniciTur='" + tx_kullaniciTur.Text + @"'
                where kullaniciTur_id=" + dgv_kullaniciTur.SelectedRows[0].Cells[0].Value.ToString());


            if (a > 0)
            {
                KullaniciTur_Load(null, null);
                MessageBox.Show("Kayıt güncellendi");
                //MetroFramework.MetroMessageBox.Show(this, "Kayıt Güncellendi");

            }
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            if (dgv_kullaniciTur.SelectedRows.Count != 0)
                dgv_kullaniciTur.SelectedRows[0].Selected = false;


            
        }

        private void dgv_kullaniciTur_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_kullaniciTur.SelectedRows.Count == 0)
            {
                tx_kullaniciTur.Text = "";
            }
            else
            {
                tx_kullaniciTur.Text = dgv_kullaniciTur.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        private void dgv_kullaniciTur_MouseClick(object sender, MouseEventArgs e)
        {
            //Uzerine gelinen satırın numarasını alıyoruz
            int currentMouseOverRow = dgv_kullaniciTur.HitTest(e.X, e.Y).RowIndex;
            //Click Eventi sağ tıklama ise
            if (e.Button == MouseButtons.Right)
            {
                // Bir contextmenu oluşturuyoruz
                ContextMenu m = new ContextMenu();
                //eğer sağ tıklama boşluğa değilse
                if (currentMouseOverRow >= 0)
                {
                    //menuleri ekliyoruz
                    m.MenuItems.Add(new MenuItem("Sil"));
                    m.MenuItems.Add(new MenuItem("Düzenle"));
                }
                //boşluğada tıklansa hepsini sil menüsü gösterilsin
                //m.MenuItems.Add(new MenuItem("Hepsini Sil", dataGridView1_hepsiniSil));
                //m.Show(dataGridView1, new Point(e.X, e.Y));//menuyu goster
            }
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
