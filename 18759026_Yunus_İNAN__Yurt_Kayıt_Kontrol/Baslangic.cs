using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using DLLIslemleri_;
using System.IO;

namespace _18759026_Yunus_İNAN__Yurt_Kayıt_Kontrol
{
    public partial class Baslangic : MetroFramework.Forms.MetroForm
    {
        public Baslangic()
        {
            InitializeComponent();
        }
        string yazilimKodu = "";
        private void Baslangic_Load(object sender, EventArgs e)
        {
            string mac = Mac();
            string ram = RAMcek();


            if (String.IsNullOrEmpty(mac))
            {
                MessageBox.Show("Biglisayarınızda bir ağ bağdaştırıcısı bulunamadı.");
            }
            

            string ramMacAdresi = ram + mac;

            string kod = "";
            foreach (char item in ramMacAdresi)
            {
                if (item == '0')
                    kod += 'B';
                else if (item == 'A')
                    kod += 'D';
                else if (item == 'C')
                    kod += '2';
                else if (item == '1')
                    kod += '7';
                else if (item == '8')
                    kod += '2';
                else if (item == '2')
                    kod += '8';
                else if (item == '9')
                    kod += '3';
                else if (item == '3')
                    kod += '6';
                else if (item == '7')
                    kod += '1';
                else if (item == '4')
                    kod += '9';
                else if (item == '5')
                    kod += '7';
                else if (item == 'B')
                    kod += 'K';
                else if (item == 'D')
                    kod += 'Y';
                else
                    kod += item;
            }

            yazilimKodu = DigerIslemler.MD5Sifrele(kod).ToUpper();

            tx_yazilimKodu.Text = yazilimKodu;
            if (File.Exists("lisans.l"))
            {
                StreamReader sr = new StreamReader("lisans.l", Encoding.Default);
                string okunanLisans = sr.ReadLine();
                sr.Close();
                if (okunanLisans == lisansKoduUret())
                {
                    this.Hide();
                    KullaniciGirisi kullaniciGiris_frm = new KullaniciGirisi();
                    kullaniciGiris_frm.baslangic_frm = this;
                    kullaniciGiris_frm.Show();
                }
            }

        }
        static string Mac()
        {
            ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj in manager.GetInstances())
            {
                if ((bool)obj["IPEnabled"])
                {
                    return obj["MacAddress"].ToString();
                }
            }

            return String.Empty;
        }
        public static String RAMcek()
        {
            String ramBilgi = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_PhysicalMemory");
            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                ramBilgi = obj["SerialNumber"].ToString();
            }

            return ramBilgi;
        }
        


        private void btn_lisansla_Click(object sender, EventArgs e)
        {

            string lisansKodu = lisansKoduUret();

            if (tx_lisansKodu.Text == lisansKodu)
            {
                MessageBox.Show("Lisans Geçerli");
                StreamWriter sw = new StreamWriter("lisans.l", false, Encoding.Default);
                sw.WriteLine(lisansKodu);
                sw.Close();
                this.Hide();
                KullaniciGirisi kullaniciGiris_frm = new KullaniciGirisi();

                kullaniciGiris_frm.Show();

            }
            else
                MessageBox.Show("Lisans Kodu Yanlış");
        }
        private string lisansKoduUret()
        {
            string kod = yazilimKodu + "YurtKayıtKontrol";

            string sifreliKod = DigerIslemler.MD5Sifrele(kod).ToUpper();

            string lisansKodu = sifreliKod.Substring(1, 3) +
                                sifreliKod.Substring(8, 5) +
                                sifreliKod.Substring(17, 2) +
                                sifreliKod.Substring(25, 2) +
                                sifreliKod.Substring(29, 2) +
                                sifreliKod.Substring(22, 1) +
                                sifreliKod.Substring(12, 1);

            lisansKodu = lisansKodu.Insert(12, "-");
            lisansKodu = lisansKodu.Insert(8, "-");
            lisansKodu = lisansKodu.Insert(4, "-");
            return lisansKodu;
        }

        private void Baslangic_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
