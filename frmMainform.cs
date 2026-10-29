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
    public partial class frmMainform : Form
    {
        public readonly MySQL_Database _db;
        public frmMainform(MySQL_Database db)
        {
            InitializeComponent();
            _db = db;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
            label5.Text = DateTime.Now.ToLongDateString();
        }

        private void frmMainform_Load(object sender, EventArgs e)
        {
            //_db.ConnectToDatabase();

            statusStrip1.Visible = false;

        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageProduct frm =new frmManageProduct();

            OpenChildForm(frm);
        }

        private void systemAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var accoumtform = new frmAccounts(_db);
            OpenChildForm(accoumtform);
        }
        private List<Form> _openChildForm = new List<Form>();

        private void OpenChildForm(Form childform)
        {
            foreach (Form form in _openChildForm)
            {
                form.Close();
            }
            _openChildForm.Clear();

            childform.TopLevel = false;
            panel2.Controls.Add(childform);
            childform.Show();
            _openChildForm.Add(childform);
            }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Log out",
                               "Logout",
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                var login = new frmLoginform();
                
                login.ShowDialog();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var pos = new frmPOS();
            OpenChildForm(pos);

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.statusStrip1.AllowMerge = false;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {

      
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void manageStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageStock frm = new frmManageStock();
            OpenChildForm(frm);
        }

        private void expiredItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExpiredItems frm = new frmExpiredItems();
            OpenChildForm(frm);
        }

        private void damageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDamage frm = new frmDamage();
            OpenChildForm(frm);
        }

        private void manageReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageReports frm = new frmManageReports();
            OpenChildForm(frm);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var Dashboard = new frmDashboard();
            OpenChildForm(Dashboard);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var Home = new frmHome();
            OpenChildForm(Home);
        }
    }
}
