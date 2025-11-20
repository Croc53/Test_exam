using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using probkic;

namespace probkic
{

    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (login == "" || password == "")
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            using (SqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();

                    string sql = @"
                            SELECT u.UserID, u.FIO, r.RoleName 
                            FROM user_syst u
                            INNER JOIN Roles r ON u.RoleID = r.RoleID
                            WHERE u.login = @login AND u.pas = @pass";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@pass", password);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string role = reader["RoleName"].ToString();
                        string fio = reader["FIO"].ToString();

                        FormMain main = new FormMain(role, fio);
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            // Вход в режиме гостя
            FormMain main = new FormMain("Гость", "Гость");
            main.Show();
            this.Hide();
        }

      
    }
}