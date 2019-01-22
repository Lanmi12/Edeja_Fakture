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
    public partial class Login : Form
    {
        public static string userName;
        public Login()
        {
            InitializeComponent();
        }

        private bool Check_login(string user, string pass)
        {
            Functions function = new Functions();
            MySQL e_base = new MySQL();
            e_base.Connect();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT count(*) FROM korisnici WHERE xUser=@user AND xPassword=@pass";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", function.MD5Hash(pass));
            cmd.Connection = e_base.connection;
            var login = cmd.ExecuteScalar();
            userName = user;

            int value = Convert.ToInt32(login);

            return value != 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string user = userBox.Text;
            string pass = pswBox.Text;
            if (user == "" || pass == "")
            {
                MessageBox.Show("Sva polja su obavezna da se popune");
                return;
            }
            bool r = Check_login(user, pass);
            if (r)
            {
                IndexPage Page = new IndexPage();
                this.Hide();
                Page.ShowDialog();
            }
            else
                MessageBox.Show("Pogresni podaci !");
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnPrijava;
        }
    }
}
