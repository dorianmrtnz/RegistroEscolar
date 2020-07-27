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
    public partial class verEstudiantes : Form
    {
        public verEstudiantes()
        {
            InitializeComponent();
        }


        private void verEstudiantes_Load(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.AutoPopDelay = 5000;
            tt.InitialDelay = 1000;
            tt.ReshowDelay = 500;

            tt.SetToolTip(button2, "Editar la informacion del Estudiante");
            tt.SetToolTip(button1, "Buscar por nombre o apellido");
            
            button2.Hide();
            
            dataGridView1.Hide();

            lbApellidos.Hide();
            lbNombres.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbApellidos.Hide();
            lbNombres.Hide();

            SqlConnection cnn = conectar.getconnection();
            try 
            { 
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("select estudiante_id, nombres, apellidos from estudiantes where nombres like '%" + textBox1.Text + "%' or apellidos like '%" + textBox1.Text + "%'", cnn);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(da);
                DataTable table = new DataTable();
                da.Fill(table);
                dataGridView1.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Show();
            }
            else
            {
                MessageBox.Show("No se encontro nada");
            }
            
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());

            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("SELECT *, dbo.grados.grado, dbo.secciones.seccion FROM estudiantes " +
                    "INNER JOIN dbo.grados ON dbo.estudiantes.grado_id = dbo.grados.grado_id " +
                    "INNER JOIN dbo.secciones ON dbo.estudiantes.seccion_id = dbo.secciones.seccion_id WHERE estudiante_id = '" + id + "'", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    conectar.estidianteID = Convert.ToInt32(dr["estudiante_id"].ToString());

                    byte[] imageBuffer = (byte[])dr["foto"];
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                    pictureBox1.Image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.BorderStyle = BorderStyle.Fixed3D;


                    lbApellidos.Text = dr["apellidos"].ToString();
                    lbNombres.Text = dr["nombres"].ToString();
                    lbTelefono.Text = dr["telefono"].ToString();
                    lbCelular.Text = dr["celular"].ToString();
                    lbDireccion.Text = dr["direccion"].ToString();
                    lbNacimiento.Text = dr["nacimiento"].ToString();
                    lbGrado.Text = dr["grado"].ToString();
                    lbSeccion.Text = dr["seccion"].ToString();
                    lbEnfermedad.Text = dr["enfermedad"].ToString();
                    lbDenfermedad.Text = dr["Denfermedad"].ToString();
                    lbAlergia.Text = dr["alergia"].ToString();
                    lbDalergia.Text = dr["Dalergia"].ToString();
                    lbSexo.Text = dr["sexo"].ToString();

                    lbApellidos.Show();
                    lbNombres.Show();

                    if (conectar.usuario_role == 1)
                    {
                        button2.Show();
                    }
                    

                    dataGridView1.Hide();

                    
                }
                else
                {
                    
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new editarEstudiantes().Show();
            this.Hide();
        }


       
    }
}
