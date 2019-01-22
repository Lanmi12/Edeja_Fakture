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
    public partial class CreateFakture : Form
    {
        public  int UpdateList;
        public int IDFakture;
        public CreateFakture()
        {
            InitializeComponent();
            this.datePicker.Value = DateTime.Now;
        }

        private void btnKreirajFakt_Click(object sender, EventArgs e)
        {
            MySQL eBase = new MySQL();
            if(BrojDBox.Text == "")
            {
                MessageBox.Show("Sva polja su obavezna da se popune!");
                return;
            }
            if(UpdateList == 1)
            {
                eBase.mSQL_FakturaIzmeni(IDFakture, BrojDBox.Text, datePicker);
            }
            else
            {
                Functions function = new Functions();
                function.Create_Fakture(BrojDBox.Text, datePicker);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateStavkeFaktura stavkePage = new CreateStavkeFaktura();
            stavkePage.ShowDialog();
        }

        private void CreateFakture_Load(object sender, EventArgs e)
        {
            
        }

        private void CreateFakture_Shown(object sender, EventArgs e)
        {
            if(IndexPage.status == 0)
            {
                btnKreirajFakt.Enabled = false;
            }
            MySQL eBase = new MySQL();
            eBase.Connect();
            if (UpdateList == 1)
            {
                btnKreirajFakt.Text = "Izmeni fakturu";
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT xBroj, xDatum FROM fakture WHERE xID=@ID";
                cmd.Parameters.AddWithValue("@ID", IDFakture);
                Console.WriteLine(IDFakture);
                cmd.Connection = eBase.connection;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BrojDBox.Text = reader["xBroj"].ToString();
                        datePicker.Value = reader.GetDateTime("xDatum");
                    }
                }
            }
            
        }
        public void LoadUpdateList(int FaktID,int Update)
        {
            UpdateList = Update;
            IDFakture = FaktID;
        }

        private void btnListaS_Click(object sender, EventArgs e)
        {
            ListaStavki listaPage = new ListaStavki();
            listaPage.ShowDialog();
        }
    }
}
