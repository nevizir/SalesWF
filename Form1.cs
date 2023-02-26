using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SalesWF
{
    public partial class Form1 : Form
    {
        string connectionString = null;
        SqlConnection connection = null;
        SqlDataAdapter adapter = null;
        DataSet set = new DataSet();

        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["SalesDb"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (TableComBox.SelectedIndex == -1)
                MessageBox.Show("Select table");

            string table = TableComBox.Text;
            string cmd = null;

            if (table == "Buyers")
                cmd = "select * from Buyers";
            if (table == "Sellers")
                cmd = "select * from Sellers";
            if (table == "Sales")
                cmd = "select * from Sales";

            adapter = new SqlDataAdapter(cmd, connection);
            new SqlCommandBuilder(adapter);

            set.Clear();
            dataGridView1.DataSource = null;

            adapter.Fill(set);

            dataGridView1.DataSource = set.Tables[0];
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            adapter.Update(set);
        }
    }
}