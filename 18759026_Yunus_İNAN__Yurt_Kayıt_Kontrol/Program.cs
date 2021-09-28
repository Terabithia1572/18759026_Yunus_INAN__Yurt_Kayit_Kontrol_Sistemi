using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18759026_Yunus_İNAN__Yurt_Kayıt_Kontrol
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (LisansKontrol.KontrolEt())
            {
                Application.Run(new KullaniciGirisi());
            }
            else
                Application.Run(new Baslangic());
        }
    }
}
