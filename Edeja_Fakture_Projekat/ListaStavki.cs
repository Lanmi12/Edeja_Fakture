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
    public partial class ListaStavki : Form
    {
        MySQL eBase = new MySQL();
        public int IdStavke;
        public static int Update;
        public ListaStavki()
        {
            InitializeComponent();
        }

        private void ListaStavki_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        public void RefreshGrid()
        {

            eBase.Connect();
            string mSQL_Query = "SELECT xID, xRedniBroj, xKolicina, xCena, xUkupno, xBrojFakture FROM stavke_faktura";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mSQL_Query, eBase.connection);
            adapter.Fill(table);
            dbGrid.DataSource = table;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool deleteQuery = eBase.DeleteSFaktura(IdStavke);
            if (deleteQuery)
            {
                MessageBox.Show("Uspesno ste obrisali Fakturu!");
                RefreshGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateStavkeFaktura SfaktPage = new CreateStavkeFaktura();

            Update = 1;
            SfaktPage.LoadUpdateList(IdStavke, Update);
            SfaktPage.ShowDialog();
        }

        private void dbGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (!String.IsNullOrEmpty(dbGrid.CurrentRow.Cells[0].Value.ToString()))
            {
                IdStavke = Int32.Parse(dbGrid.CurrentRow.Cells[0].Value.ToString());
            }
            else MessageBox.Show("Nema nijedne stavke");
        }
    }
}
