using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Edeja_Fakture_Projekat
{
    public partial class CreateStavkeFaktura : Form
    {
        public int UpdateList;
        public int IdStavke;
        public CreateStavkeFaktura()
        {
            InitializeComponent();
        }

        private void btnKreirajFakt_Click(object sender, EventArgs e)
        {
            MySQL eBase = new MySQL();
            int cena, kolicina, ID;
            Functions function = new Functions();
            if(txtID.Text == "" || rBrojTxt.Text == "" || txtCena.Text == "" || txtKolicina.Text == "")
            {
                MessageBox.Show("Sva polja su obavezna da se popune");
                return;
            }
            if (!int.TryParse(txtCena.Text, out cena))
            {
                MessageBox.Show("Cena mora biti broj!");
                return;
            }
            if (!int.TryParse(txtKolicina.Text, out kolicina))
            {
                MessageBox.Show("Kolicina mora biti broj!");
                return;
            }
            if(!int.TryParse(txtID.Text , out ID))
            {
                MessageBox.Show("Id Fakture mora biti BROJ!");
                return;
            }
            if (UpdateList == 1)
            {
                eBase.mSQL_SFakturaIzmeni(IdStavke, rBrojTxt.Text, cena, kolicina);
            }
            else
            {
                function.Create_StFakture(ID, rBrojTxt.Text, cena, kolicina);
            }
            
        }
        public void LoadUpdateList(int FaktID, int Update)
        {
            UpdateList = Update;
            IdStavke = FaktID;
        }

        private void CreateStavkeFaktura_Shown(object sender, EventArgs e)
        {
            MySQL eBase = new MySQL();
            eBase.Connect();
            Console.WriteLine(UpdateList);
            if (UpdateList == 1)
            {
                btnKreirajFakt.Text = "Izmeni stavku fakture";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT  xRedniBroj, xKolicina, xCena, xBrojFakture FROM stavke_faktura WHERE xID=@ID";
                cmd.Parameters.AddWithValue("@ID", IdStavke);
                cmd.Connection = eBase.connection;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        txtID.Text = reader["xBrojFakture"].ToString();
                        rBrojTxt.Text = reader["xRedniBroj"].ToString();
                        txtCena.Text = reader["xCena"].ToString();
                        txtKolicina.Text = reader["xKolicina"].ToString();
                    }
                }
            }
        }
    }
}
