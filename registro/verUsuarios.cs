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
    public partial class verUsuarios : Form
    {
        public verUsuarios()
        {
            InitializeComponent();
        }

        private void verUsuarios_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT     dbo.usuarios.usuario_id, dbo.usuarios.usuario, dbo.usuarios.nombre, " +
                    "dbo.usuarios.apellido, dbo.usuarios.direccion, dbo.usuarios.telefono, dbo.usuarios.celular, dbo.usuarios.cedula,  " +
                    "dbo.posiciones.posicion, dbo.roles.role FROM dbo.posiciones INNER JOIN dbo.usuarios ON dbo.posiciones.posicion_id = " +
                    "dbo.usuarios.posicion_id INNER JOIN dbo.roles ON dbo.usuarios.role_id = dbo.roles.role_id", cnn);
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT     dbo.usuarios.usuario_id, dbo.usuarios.usuario, dbo.usuarios.nombre, " +
                    "dbo.usuarios.apellido, dbo.usuarios.direccion, dbo.usuarios.telefono, dbo.usuarios.celular, dbo.usuarios.cedula,  " +
                    "dbo.posiciones.posicion, dbo.roles.role FROM dbo.posiciones INNER JOIN dbo.usuarios ON dbo.posiciones.posicion_id = " +
                    "dbo.usuarios.posicion_id INNER JOIN dbo.roles ON dbo.usuarios.role_id = dbo.roles.role_id " +
                    "where nombre like '%" + textBox1.Text + "%' or apellido like '%" + textBox1.Text + "%'", cnn);
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
                paraEditar();
            }
        }
        public void paraEditar()
        {
            conectar.usuario_id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            new editarUsuarios().Show();
            this.Hide();
        }
    }
}
