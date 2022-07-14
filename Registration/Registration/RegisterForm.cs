using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registration
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();

            userName.Text = "Write your name";
            userSurname.Text = "Write your surname";

            userName.ForeColor = Color.Gray;
            userSurname.ForeColor = Color.Gray;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeButton_MouseEnter(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Gray;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
        }

        Point lastPoint;
        
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X,e.Y);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void userName_Enter(object sender, EventArgs e)
        {
            if(userName.Text == "Write your name" && userName.ForeColor == Color.Gray)
            {
                userName.Text = "";
                userName.ForeColor = Color.Black;
            }
                 

        }

        private void userName_Leave(object sender, EventArgs e)
        {
            if (userName.Text == "")
            {
                userName.Text = "Write your name";
                userName.ForeColor = Color.Gray;
            }
                
        }

        private void userSurname_Enter(object sender, EventArgs e)
        {
            if (userSurname.Text == "Write your surname" && userSurname.ForeColor == Color.Gray)
            {
                userSurname.Text = "";
                userSurname.ForeColor = Color.Black;
            }

        }

        private void userSurname_Leave(object sender, EventArgs e)
        {
            if (userSurname.Text == "")
            {
                userSurname.Text = "Write your surname";
                userSurname.ForeColor = Color.Gray;
            }
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Write your logIn" && loginField.ForeColor == Color.Gray)
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Write your logIn";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (userName.Text == "Write your name" || userName.Text == "" || userSurname.Text == "Write your surname" || userSurname.Text == "" || loginField.Text == "Write your logIn" || loginField.Text == "" || passField.Text == "")
            {
                MessageBox.Show("Fill in all the data");
                return;
            }

            if (checkUser())
                return;




            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`,`password`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", db.getConnecton());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userName.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = userSurname.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("The account has been created");
            else
                MessageBox.Show("the account was not created");

            db.closeConnection();

            
        }


        public Boolean checkUser()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.getConnecton());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;
            

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This login has already been registered. Enter another one, please.");
                return (true);
            }

            else
                return (false);
        }

        

        

        private void registerlabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
