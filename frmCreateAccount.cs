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

namespace Inventory_Management_System_with_POS
{
    public partial class frmCreateAccount : Form
    {
        private readonly MySQL_Database _db;

        public frmCreateAccount(MySQL_Database db)
        {
            InitializeComponent();
            _db = db;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAccName.Text == "" || cbAccountType.Text == "" || txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_db.conn.State != ConnectionState.Open)
                {
                    _db.conn.Open();
                }

                if (Account_Variables.isEditMode)
                {
                    // UPDATE existing account
                    string sql = @"UPDATE tbl_account 
                                   SET acc_name = @acc_name, 
                                       acc_type = @acc_type, 
                                       gender = @gender, 
                                       user_name = @user_name, 
                                       pass_word = @pass_word,
                                       phone = @phone,
                                       email = @email
                                   WHERE acc_id = @acc_id";

                    using (MySqlCommand cmd = new MySqlCommand(sql, _db.conn))
                    {
                        cmd.Parameters.AddWithValue("@acc_id", txtAccID.Text);
                        cmd.Parameters.AddWithValue("@acc_name", txtAccName.Text);
                        cmd.Parameters.AddWithValue("@acc_type", cbAccountType.Text);
                        cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "Male" : "Female");
                        cmd.Parameters.AddWithValue("@user_name", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@pass_word", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@phone", txtphone.Text);
                        cmd.Parameters.AddWithValue("@email", txtmail.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Account updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Account_Variables.isEditMode = false;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // INSERT new account
                    string sql = @"INSERT INTO tbl_account (acc_id, acc_name, user_name, pass_word, gender, acc_type, phone, email) 
                                   VALUES (@acc_id, @acc_name, @user_name, @pass_word, @gender, @acc_type, @phone, @email)";

                    using (MySqlCommand cmd = new MySqlCommand(sql, _db.conn))
                    {
                        cmd.Parameters.AddWithValue("@acc_id", txtAccID.Text);
                        cmd.Parameters.AddWithValue("@acc_name", txtAccName.Text);
                        cmd.Parameters.AddWithValue("@user_name", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@pass_word", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@gender", radioButton1.Checked ? "Male" : "Female");
                        cmd.Parameters.AddWithValue("@acc_type", cbAccountType.Text);
                        cmd.Parameters.AddWithValue("@phone", txtphone.Text);
                        cmd.Parameters.AddWithValue("@email", txtmail.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Account created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_db.conn.State == ConnectionState.Open)
                {
                    _db.conn.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtAccID.Clear();
            txtAccID.ReadOnly = false;
            txtAccName.Clear();
            cbAccountType.SelectedIndex = -1;
            cbAccountType.Enabled = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtUsername.Clear();
            txtPassword.Clear();
            txtphone.Clear();
            txtmail.Clear();
            checkBox1.Checked = false;
            label1.Text = "Create Account";
            btnSave.Text = "Save";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                checkBox1.Text = "Hide Password";
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                checkBox1.Text = "Show Password";
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void frmCreateAccount_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            cbAccountType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAccountType.Items.Clear();
            cbAccountType.Items.AddRange(new string[] { "Admin", "Inventory Staff", "Cashier", "Manager" });

            if (Account_Variables.isEditMode)
            {
                cbAccountType.Enabled = true;
            }
        }

        private void frmCreateAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            Account_Variables.isEditMode = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}