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

namespace CreateReadUpdateDeleteProject
{
    public partial class Form1 : Form
    {
        SqlConnection connect
            = new SqlConnection(@"Data Source=.;Initial Catalog=test;Integrated Security=True;Encrypt=False");


        public Form1()
        {
            InitializeComponent();

            // TO DISPLAY THE DATA TO YOUR DATA GRID VIEW
            displayData();
        }

        public void displayData()
        {
            UserListData uld = new UserListData();

            List<UserListData> listData = uld.getListData();
            dataGridView1.DataSource = listData;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
         //-------------------------------- ADD BTN -------------------------------------------

        private void addBtn_Click(object sender, EventArgs e)
        {
            if(full_name.Text == ""
                || contact_number.Text == ""
                || email.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State != ConnectionState.Open)
                {
                    try
                    {
                        // TO GET THE DATE TODAY
                        DateTime today = DateTime.Today;
                        connect.Open();

                        string insertData = "INSERT INTO users " +
                            "(full_Name, contact, email, bith_date, date_insert) " +
                            "VALUES(@fullName, @contact, @email, @birthDate, @dateInsert)";

                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@fullName", full_name.Text.Trim());
                            cmd.Parameters.AddWithValue("@contact", contact_number.Text.Trim());
                            cmd.Parameters.AddWithValue("@email", email.Text.Trim());
                            cmd.Parameters.AddWithValue("@birthDate", birth_date.Value);
                            cmd.Parameters.AddWithValue("@dateInsert", today);

                            cmd.ExecuteNonQuery();

                 
                            displayData();

                            MessageBox.Show("Added successfully!", "Information Message"
                                , MessageBoxButtons.OK, MessageBoxIcon.Information);

                           
                            clearFields();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error: " + ex, "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        //---------------------------------- CLEAR BTN -----------------------------------------



        public void clearFields()
        {
            full_name.Text = "";
        
            contact_number.Text = "";
            email.Text = "";
        }

        private int tempID = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                tempID = (int)row.Cells[0].Value;
                full_name.Text = row.Cells[1].Value.ToString();
                contact_number.Text = row.Cells[3].Value.ToString();
                email.Text = row.Cells[4].Value.ToString();
                //birth_date.Text = row.Cells[5].Value.ToString();
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }


        //-------------------------------- UPDATE BTN -------------------------------------------

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (full_name.Text == ""
                || contact_number.Text == ""
                || email.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Update ID: "
                    + tempID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if(check == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            // TO GET THE DATE TODAY
                            DateTime today = DateTime.Today;
                            connect.Open();

                            string updateData = "UPDATE users SET " +
                                "full_name = @fullName, " +
                                "contact = @contact, email = @email, " +
                                "bith_date = @birthDate, date_update = @dateUpdate " +
                                "WHERE id = @id";

                            using(SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@fullName", full_name.Text.Trim());
                                cmd.Parameters.AddWithValue("@contact", contact_number.Text.Trim());
                                cmd.Parameters.AddWithValue("@email", email.Text.Trim());
                                cmd.Parameters.AddWithValue("@birthDate", birth_date.Value);
                                cmd.Parameters.AddWithValue("@dateUpdate", today);
                                cmd.Parameters.AddWithValue("@id", tempID);

                                cmd.ExecuteNonQuery();

                                displayData();

                                MessageBox.Show("Updated successfully!", "Information Message"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                                clearFields();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex, "Error Message"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled.", "Inforamtion Message"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
        }

        //-------------------------------------- DELETE BTN -------------------------------------

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (full_name.Text == ""
                || contact_number.Text == ""
                || email.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to Delete ID: "
                    + tempID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (check == DialogResult.Yes)
                {
                    if (connect.State != ConnectionState.Open)
                    {
                        try
                        {
                            // TO GET THE DATE TODAY
                            DateTime today = DateTime.Today;
                            connect.Open();

                            string updateData = "DELETE FROM users WHERE id = @id";

                            using (SqlCommand cmd = new SqlCommand(updateData, connect))
                            {
                                cmd.Parameters.AddWithValue("@id", tempID);

                                cmd.ExecuteNonQuery();

                                // TO DISPLAY THE DATA  
                                displayData();

                                MessageBox.Show("Deleted successfully!", "Information Message"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // TO CLEAR ALL FIELDS
                                clearFields();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex, "Error Message"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connect.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled.", "Inforamtion Message"
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }


        //---------------------------------------------------------------------------



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void full_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}


// THATS IT FOR THIS VIDEO, THANKS FOR WATCHING !! : )
// BTW, THANKS FOR YOUR SUPPORT, GUYS!
// WE ALMOST REACH 2K SUBSCRIBERS!! I'M SO HAPPY : ) 
// THANKS AGAIN!
// SEE YOU IN THE NEXT VIDEO TUTORIAL!! 