using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using DLLIslemleri_;
using System.IO;

namespace _18759026_Yunus_İNAN__Yurt_Kayıt_Kontrol
{
    static class LisansKontrol
    {
        public static bool KontrolEt()
        {
            if (File.Exists("lisans.l"))
            {
                StreamReader sr = new StreamReader("lisans.l", Encoding.Default);
                string okunanLisans = sr.ReadLine();
                sr.Close();
                if (okunanLisans == lisansKoduUret())
                {
                    return true;
                }
            }
            return false;
        }
        public static string BuBilgisayarYazilimKodu()
        {
            string mac = Mac();
            string ram = RAMcek();

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

            string yazilimKodu = DigerIslemler.MD5Sifrele(kod).ToUpper();
            return yazilimKodu;

            
           

        }

        private static string lisansKoduUret()
        {
            string kod = BuBilgisayarYazilimKodu() + "YurtKayıtKontrol";

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
        
    }
}
