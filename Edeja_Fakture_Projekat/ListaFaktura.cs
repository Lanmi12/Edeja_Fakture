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
    public partial class ListaFaktura : Form
    {
        MySQL eBase = new MySQL();
        public int IdFakture ;
        public static int Update;
        public ListaFaktura()
        {
            InitializeComponent();
        }

        private void ListaFaktura_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        public void RefreshGrid()
        {
           
            eBase.Connect();
            string mSQL_Query = "SELECT xID, xDatum, xUkupno FROM fakture";
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mSQL_Query, eBase.connection);
            adapter.Fill(table);
            dbGrid.DataSource = table;
        }
        private void dbGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (!String.IsNullOrEmpty(dbGrid.CurrentRow.Cells[0].Value.ToString()))
            {
                IdFakture = Int32.Parse(dbGrid.CurrentRow.Cells[0].Value.ToString());
            }
            else MessageBox.Show("Nema nijedne fakture");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool deleteQuery = eBase.DeleteFaktura(IdFakture);
            if (deleteQuery)
            {
                MessageBox.Show("Uspesno ste obrisali Fakturu!");
                RefreshGrid();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateFakture faktPage = new CreateFakture();
            
            Update = 1;
            faktPage.LoadUpdateList(IdFakture, Update);
            faktPage.ShowDialog();
            
        }
    }
}
