using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace SRBD
{
    public partial class Form1 : Form
    {
        string connString = @"Data Source=KAKA-KOMPUTER\BAZYPROJEKT;Initial Catalog=SRBD;Integrated Security=True";
        String a;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "select prod_id as ID, prod_nazwa as Nazwa_Producenta, prod_adres as Adres_Producenta from Producent";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
            da.Dispose();

            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Produkt", sqlConnection);
                SqlCommand sqlCmd2 = new SqlCommand("SELECT * FROM Producent", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    String check = sqlReader["pro_id"].ToString();
                    if (check != null)
                    {
                        comboBox1.Items.Add(sqlReader["pro_id"].ToString());
                        comboBox3.Items.Add(sqlReader["pro_id"].ToString());
                    }
                
                }
                sqlReader.Close();
      
            }
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                SqlCommand sqlCmd2 = new SqlCommand("SELECT * FROM Producent", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlReader2 = sqlCmd2.ExecuteReader();
                while (sqlReader2.Read())
                {
                    String check = sqlReader2["prod_id"].ToString();
                    if (check != null)
                    {
                        comboBox4.Items.Add(sqlReader2["prod_id"].ToString());
                        comboBox2.Items.Add(sqlReader2["prod_id"].ToString());
                        comboBox5.Items.Add(sqlReader2["prod_id"].ToString());
                        comboBox6.Items.Add(sqlReader2["prod_id"].ToString());

                    }

                }
                sqlReader2.Close();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          a= (dataGridView1.CurrentRow.Index+1).ToString();
            
            string query = "select pro_id as ID,pro_nazwa as Nazwa, pro_cena as Cena from Produkt where prod_id='" + a + "';";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            conn.Close();
            da.Dispose();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
     
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
            int maxid = 0;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            String query = "SELECT MAX(pro_id) FROM Produkt;";
            SqlCommand cmd = new SqlCommand(query, conn);
            maxid = (int)cmd.ExecuteScalar();
            maxid += 1;
            query = "Insert into Produkt values(" + maxid + ",'" + comboBox2.SelectedItem + "','" + textBox1.Text + "','" + textBox2.Text + "');";
            SqlCommand dmd = new SqlCommand(query, conn);
            dmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
           String query = "Delete from Produkt where pro_id ='" +comboBox1.SelectedItem + "';";
            SqlCommand dmd = new SqlCommand(query, conn);
            dmd.ExecuteNonQuery();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string query = "UPDATE Produkt set prod_id ='" + comboBox4.SelectedItem + "',pro_nazwa='" + textBox4.Text + "',pro_cena ='" + textBox3.Text + "' where pro_id='" + comboBox3.SelectedItem + "';";
            SqlCommand dmd = new SqlCommand(query, conn);
            dmd.ExecuteNonQuery();
            conn.Close();
            dataGridView1.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            String query = "Delete from Producent where prod_id ='" + comboBox5.SelectedItem + "';";
            SqlCommand dmd = new SqlCommand(query, conn);
            dmd.ExecuteNonQuery();
            conn.Close();
            dataGridView1.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maxid = 0;
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            String query = "SELECT MAX(prod_id) FROM Producent;";
            SqlCommand cmd = new SqlCommand(query, conn);
            maxid = (int)cmd.ExecuteScalar();
            maxid += 1;
            query = "Insert into Producent values(" + maxid + ",'"+ textBox6.Text + "','" + textBox5.Text + "');";
            SqlCommand dmd = new SqlCommand(query, conn);
            dmd.ExecuteNonQuery();
            conn.Close();
            dataGridView1.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            string query = "UPDATE Producent set prod_nazwa='" + textBox7.Text + "',prod_Adres ='" + textBox8.Text + "' where prod_id='" + comboBox6.SelectedItem + "';";
            SqlCommand dmd = new SqlCommand(query, conn);
            dmd.ExecuteNonQuery();
            conn.Close();
            dataGridView1.Update();
        }
    }
}
