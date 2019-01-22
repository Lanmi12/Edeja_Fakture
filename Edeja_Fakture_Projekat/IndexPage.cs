using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edeja_Fakture_Projekat
{
    public partial class IndexPage : Form
    {
        public string Username;
        public static int status;
        public static int checkStatus;
        public IndexPage()
        {
            InitializeComponent();
        }

        private void IndexPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void IndexPage_Shown(object sender, EventArgs e)
        {
            Functions function = new Functions();
            MySQL ebase = new MySQL();
            ebase.Load_Status(Login.userName);
            usrName.Text = Login.userName;
            txtStatus.Text = function.Check_Status(status);
            switch(status)
            {
                case 0:
                {
                    btnCrUser.Enabled = false;
                    btnCrUser.Visible = false;
                    btnLisFaktura.Enabled = false;
                    btnLisFaktura.Visible = false;
                    edjLogo.Location = new Point(28, 84);
                    btnCrFakt.Location = btnLisFaktura.Location;
                    btnIzloguj.Location = btnCrUser.Location;
                    break;
                }
                case 1:
                {
                    btnCrUser.Enabled = false;
                    btnCrUser.Visible = false;
                    btnIzloguj.Location = btnCrUser.Location;
                    break;
                }
            }
        }

        private void btnIzloguj_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login LPage = new Login();
            LPage.Show();
        }

        private void btnCrUser_Click(object sender, EventArgs e)
        {
            labNoOpt.Visible = false;
            labKorIme.Visible = true;
            txtKorIme.Visible = true;
            labKorSifra.Visible = true;
            txtKorSifra.Visible = true;
            labStatus.Visible = true;
            chkKor.Visible = true;
            chkSar.Visible = true;
            chkVls.Visible = true;
            btnKreiraj.Visible = true;
        }

      

        private void chkKor_CheckedChanged(object sender, EventArgs e)
        {
            if(chkKor.Checked == true)
            {
                if (chkSar.Checked) chkSar.Checked = false;
                if (chkVls.Checked) chkVls.Checked = false;
                chkKor.Checked = true;
                checkStatus = 0;
            }
            
        }

        private void chkSar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSar.Checked == true)
            {
                if (chkKor.Checked) chkKor.Checked = false;
                if (chkVls.Checked) chkVls.Checked = false;
                chkSar.Checked = true;
                checkStatus = 1;
            }
        }

        private void chkVls_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVls.Checked == true)
            {
                if (chkKor.Checked) chkKor.Checked = false;
                if (chkSar.Checked) chkSar.Checked = false;
                chkVls.Checked = true;
                checkStatus = 2;
            }
        }
        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            Functions function = new Functions();
            if (txtKorIme.Text == "" || txtKorSifra.Text == "")
            {
                MessageBox.Show("Empty Fields Detected ! Please fill up all the fields");
                return;
            }
            if (checkStatus < 0)
            {
                MessageBox.Show("Please check one thing!");
                return;
            }

            function.Create_User(txtKorIme.Text, txtKorSifra.Text, checkStatus);
        }

        private void btnCrFakt_Click(object sender, EventArgs e)
        {
            CreateFakture fakturePage = new CreateFakture();
            fakturePage.ShowDialog();
        }

        private void btnLisFaktura_Click(object sender, EventArgs e)
        {
            ListaFaktura listaPage = new  ListaFaktura();
            listaPage.ShowDialog();
        }
    }
}
