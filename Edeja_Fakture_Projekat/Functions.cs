using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Edeja_Fakture_Projekat
{
    public class Functions
    {
        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public string Check_Status(int status)
        {
            string currentStatus = null;
            switch (status)
            {
                case 0: return currentStatus = "Korisnik";
                case 1: return currentStatus = "Saradnik";
                case 2: return currentStatus = "Vlasnik Firme";
            }
            return currentStatus;
        }
        public int Create_User(string user, string sifra, int type)
        {
            MySQL ebase = new MySQL();
            user.Trim();
            sifra.Trim();
            if (user.Length > 13 || sifra.Length > 13)
            {
                MessageBox.Show("Korisnicko ime / sifra ne moze biti vise od 13 karaktera");
                return 0;
            }
            bool insertQuery = ebase.mSQL_InsertUser(user.ToString(), sifra.ToString(), type);
            if (insertQuery) MessageBox.Show("Korisnik uspesno kreiran!");
            return 1;
        }

        public int Create_Fakture(string BrojDokument, DateTimePicker dPicker)
        {
            MySQL ebase = new MySQL();
            BrojDokument.Trim();
            if (BrojDokument.Length != 10)
            {
                MessageBox.Show("Broj dokumenta mora imati 10 karaktera!");
                return 0;
            }
            bool insertQuery = ebase.mSQL_InsertFakture(BrojDokument.ToString(), dPicker);
            if (insertQuery) MessageBox.Show("Faktura uspesno kreirana");
            return 1;
        }
        public int Create_StFakture(int IDFakt, string RedniBroj, int cena, int  kolicina)
        {
            MySQL ebase = new MySQL();
            RedniBroj.Trim();
            bool insertQuery = ebase.mSQL_InsertStavkeFakt(IDFakt, RedniBroj, cena, kolicina);
            if (insertQuery) MessageBox.Show("Stavka fakture je uspesno kreirana!");
            return 1;
        }
    }
}
