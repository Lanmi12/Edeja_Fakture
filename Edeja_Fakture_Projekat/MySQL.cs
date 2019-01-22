using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Edeja_Fakture_Projekat
{
    public class MySQL
    {
        public string conn;
        public MySqlConnection connection;
        public void Connect()
        {
            try
            {
                conn = "Server=localhost;Database=edeja_base;Uid=root;Pwd=;SslMode=none;";
                connection = new MySqlConnection(conn);
                connection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Load_Status(string user)
        {
            var statusNum = 0;
            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT xStatus FROM korisnici WHERE xUser=@user";
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Connection = connection;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statusNum = Int32.Parse(reader["xStatus"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                IndexPage.status = statusNum;
            }
        }
        public bool mSQL_InsertUser(string user, string password, int status)
        {
            Functions function = new Functions();
            try
            {
                Connect();
                MySqlCommand check_username = new MySqlCommand();
                check_username.CommandText = "SELECT COUNT(*) FROM korisnici WHERE xUser=@user";
                check_username.Parameters.AddWithValue("@user", user);
                check_username.Connection = connection;
                var userCheck = check_username.ExecuteScalar();
                int value = Convert.ToInt32(userCheck);

                if (value > 0)
                {
                    MessageBox.Show("To korisnicko ime vec postoji!");
                    return false;
                }
                MySqlCommand insert = new MySqlCommand();
                insert.CommandText = "INSERT INTO korisnici(xUser,xPassword,xStatus) VALUES(@user,@password, @status)";
                insert.Parameters.AddWithValue("@user", user);
                insert.Parameters.AddWithValue("@password", function.MD5Hash(password));
                insert.Parameters.AddWithValue("@status", status);
                insert.Connection = connection;
                insert.ExecuteScalar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
        public bool mSQL_InsertFakture(string BrojDokument, DateTimePicker dPicker)
        {
            try
            {
                Connect();
                MySqlCommand check_brDokumenta = new MySqlCommand();
                check_brDokumenta.CommandText = "SELECT COUNT(*) FROM fakture WHERE xBroj=@BrojDokument";
                check_brDokumenta.Parameters.AddWithValue("@BrojDokument", BrojDokument);
                check_brDokumenta.Connection = connection;
                var checkNumber = check_brDokumenta.ExecuteScalar();
                int value = Convert.ToInt32(checkNumber);

                if (value > 0)
                {
                    MessageBox.Show("Taj broj dokumenta vec postoji u bazi!");
                    return false;
                }
                MySqlCommand insert = new MySqlCommand();
                insert.CommandText = "INSERT INTO fakture(xID,xBroj,xDatum,xUkupno) VALUES('',@BrojDokument,@dPicker,@ukupno )";
                insert.Parameters.AddWithValue("@BrojDokument", BrojDokument);
                insert.Parameters.AddWithValue("@dPicker", dPicker.Text);
                insert.Parameters.AddWithValue("@ukupno", 0);
                insert.Connection = connection;
                insert.ExecuteScalar();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        public bool mSQL_InsertStavkeFakt(int ID, string RedniBroj, int Cena, int Kolicina)
        {
            try
            {
                Connect();
                MySqlCommand check_RBroj = new MySqlCommand();
                check_RBroj.CommandText = "SELECT COUNT(*) FROM stavke_faktura WHERE xRedniBroj=@RedniBroj";
                check_RBroj.Parameters.AddWithValue("@RedniBroj", RedniBroj);
                check_RBroj.Connection = connection;
                var checkNumber = check_RBroj.ExecuteScalar();
                int value = Convert.ToInt32(checkNumber);

                if (value > 0)
                {
                    MessageBox.Show("Taj Redni Broj vec postoji u stavkama bazi!");
                    return false;
                }
                MySqlCommand check_ID = new MySqlCommand();
                check_ID.CommandText = "SELECT COUNT(*) FROM fakture WHERE xID=@ID";
                check_ID.Parameters.AddWithValue("@ID", ID);
                check_ID.Connection = connection;
                var checkNumberID = check_ID.ExecuteScalar();
                int valueID = Convert.ToInt32(checkNumberID);

                if (valueID > 0)
                {
                    MySqlCommand insert = new MySqlCommand();
                    insert.CommandText = "INSERT INTO stavke_faktura(xID,xRedniBroj,xKolicina,xCena,xUkupno,xBrojFakture) VALUES('',@RedniBroj,@Kolicina,@Cena,@ukupno , @ID )";
                    insert.Parameters.AddWithValue("@RedniBroj", RedniBroj);
                    insert.Parameters.AddWithValue("@Kolicina", Kolicina);
                    insert.Parameters.AddWithValue("@Cena", Cena);
                    insert.Parameters.AddWithValue("@ukupno", (Cena * Kolicina));
                    insert.Parameters.AddWithValue("@ID", ID);
                    insert.Connection = connection;
                    insert.ExecuteScalar();
                    mSQL_UpdateFaktura(ID);
                    return true;
                }
                else
                {
                    MessageBox.Show("Taj ID Fakture ne postoji u bazi!");
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }
        public void mSQL_UpdateFaktura(int ID)
        {
            var currentNum = 0;
            try
            {
                Connect();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "SELECT xKolicina FROM stavke_faktura WHERE xBrojFakture=@ID";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = connection;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentNum += Int32.Parse(reader["xKolicina"].ToString());
                    }
                }
                MySqlCommand insert = new MySqlCommand();
                insert.CommandText = "UPDATE fakture SET xUkupno = @ukupno WHERE xID = @ID";
                insert.Parameters.AddWithValue("@ukupno", currentNum);
                insert.Parameters.AddWithValue("@ID", ID);
                insert.Connection = connection;
                insert.ExecuteScalar();
                Console.WriteLine(currentNum);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public bool DeleteFaktura(int ID)
        {
            MySqlCommand check_ID = new MySqlCommand();
            check_ID.CommandText = "SELECT COUNT(*) FROM fakture WHERE xID=@ID";
            check_ID.Parameters.AddWithValue("@ID", ID);
            check_ID.Connection = connection;
            var checkNumberID = check_ID.ExecuteScalar();
            int valueID = Convert.ToInt32(checkNumberID);

            if (valueID > 0)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM fakture WHERE xID = @ID";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = connection;
                var check = cmd.ExecuteScalar();
                int value = Convert.ToInt32(check);
                return true;
            }
            else
            {
                MessageBox.Show("Taj ID Fakture ne postoji");
                return false;
            }
        }
        public bool DeleteSFaktura(int ID)
        {
            MySqlCommand check_ID = new MySqlCommand();
            check_ID.CommandText = "SELECT COUNT(*) FROM stavke_faktura WHERE xID=@ID";
            check_ID.Parameters.AddWithValue("@ID", ID);
            check_ID.Connection = connection;
            var checkNumberID = check_ID.ExecuteScalar();
            int valueID = Convert.ToInt32(checkNumberID);

            if (valueID > 0)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "DELETE FROM stavke_faktura WHERE xID = @ID";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Connection = connection;
                var check = cmd.ExecuteScalar();
                int value = Convert.ToInt32(check);
                return true;
            }
            else
            {
                MessageBox.Show("Taj ID Fakture ne postoji");
                return false;
            }
        }
        public void mSQL_FakturaIzmeni(int ID, string Dbroj, DateTimePicker picker)
        {
            Connect();
            MySqlCommand check_ID = new MySqlCommand();
            check_ID.CommandText = "SELECT COUNT(*) FROM fakture WHERE xID=@ID";
            check_ID.Parameters.AddWithValue("@ID", ID);
            check_ID.Connection = connection;
            var checkNumberID = check_ID.ExecuteScalar();
            int valueID = Convert.ToInt32(checkNumberID);

            if (valueID > 0)
            {
                ListaFaktura faktPage = new ListaFaktura();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE fakture SET xBroj = @Dbroj, xDatum = @datum WHERE xID = @ID";
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@Dbroj", Dbroj);
                cmd.Parameters.AddWithValue("@datum",picker.Text);
                cmd.Connection = connection;
                var check = cmd.ExecuteScalar();
                int value = Convert.ToInt32(check);
                MessageBox.Show("Uspesno ste izmenili fakturu");
                faktPage.RefreshGrid();
            }
            else
            {
                MessageBox.Show("Taj ID Fakture ne postoji");
            }
        }
        public void mSQL_SFakturaIzmeni(int IDFakt, string RedniBroj, int cena, int kolicina)
        {
            Connect();
            MySqlCommand check_ID = new MySqlCommand();
            check_ID.CommandText = "SELECT COUNT(*) FROM stavke_faktura WHERE xID=@ID";
            check_ID.Parameters.AddWithValue("@ID", IDFakt);
            check_ID.Connection = connection;
            var checkNumberID = check_ID.ExecuteScalar();
            int valueID = Convert.ToInt32(checkNumberID);

            if (valueID > 0)
            {
                ListaStavki SfaktPage = new ListaStavki();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE stavke_faktura SET xRedniBroj = @RedniBroj, xCena = @cena, xKolicina = @Kolicina , xUkupno = @ukupno WHERE xID = @ID";
                cmd.Parameters.AddWithValue("@RedniBroj", RedniBroj);
                cmd.Parameters.AddWithValue("@cena", cena);
                cmd.Parameters.AddWithValue("@Kolicina", kolicina);
                cmd.Parameters.AddWithValue("@ukupno", (cena * kolicina));
                cmd.Parameters.AddWithValue("@ID", IDFakt);
                cmd.Connection = connection;
                var check = cmd.ExecuteScalar();
                int value = Convert.ToInt32(check);
                MessageBox.Show("Uspesno ste izmenili stavku fakture");
                SfaktPage.RefreshGrid();
            }
            else
            {
                MessageBox.Show("Taj ID Stavke ne postoji");
            }
        }
    }
}
