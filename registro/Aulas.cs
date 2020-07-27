using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace registro
{
    public partial class Aulas : Form
    {
        public Aulas()
        {
            InitializeComponent();
        }

        private void Aulas_Load(object sender, EventArgs e)
        {
            llenarGrid();
        }

        public void llenarGrid()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from aulas order by aula asc", cnn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();

            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe llenar el campo de texto");
            }
            else
            {
                SqlConnection cnn = conectar.getconnection();
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("INSERT INTO aulas VALUES (@aula)", cnn);

                    comando.Parameters.Add("@aula", System.Data.SqlDbType.VarChar);

                    comando.Parameters["@aula"].Value = textBox1.Text;

                    comando.ExecuteNonQuery();

                    textBox1.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cnn.Close();

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                llenarGrid();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int seleccionado = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            if (seleccionado > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Seguro que desea eliminar este registro?", "Advertencia", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    SqlConnection cnn = conectar.getconnection();
                    try
                    {
                        cnn.Open();
                        SqlCommand comando = new SqlCommand("DELETE FROM aulas WHERE aula_id = '" + seleccionado + "'", cnn);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Registro eliminado con exito");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Registro en uso, no se puede eliminar");
                    }
                    cnn.Close();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una linea");
            }

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            llenarGrid();
        }
    }
}
