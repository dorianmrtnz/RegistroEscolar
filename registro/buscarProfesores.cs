using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace registro
{
    public partial class buscarProfesores : Form
    {
        public buscarProfesores()
        {
            InitializeComponent();
        }

        private void buscarProfesores_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from profesores", cnn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from profesores where nombres like '%" + textBox1.Text + "%' or apellidos like '%" + textBox1.Text + "%'", cnn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (conectar.usuario_role == 1)
            {
                conectar.editarProfesoresID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                new editarProfesores().Show();
                this.Hide();
            }
            
        }

    }
}
