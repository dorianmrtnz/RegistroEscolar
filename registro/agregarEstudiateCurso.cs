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
    public partial class agregarEstudiateCurso : Form
    {
        public agregarEstudiateCurso()
        {
            InitializeComponent();
        }

        public int estudianteID, grado, seccion, seleccionado;

        private void agregarEstudiateCurso_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns[0].Visible = false;
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("SELECT dbo.profesores.nombres, dbo.profesores.apellidos, dbo.secciones.seccion, dbo.grados.grado, dbo.aulas.aula, " +
                    "secciones.seccion_id, grados.grado_id " +                                  
                    "FROM dbo.aulas INNER JOIN " +
                    "dbo.cursos ON dbo.aulas.aula_id = dbo.cursos.aula_id INNER JOIN " +
                    "dbo.grados ON dbo.cursos.grado_id = dbo.grados.grado_id INNER JOIN " +
                    "dbo.profesores ON dbo.cursos.profesor_id = dbo.profesores.profesor_id INNER JOIN " +
                    "dbo.secciones ON dbo.cursos.seccion_id = dbo.secciones.seccion_id "+
                    "where curso_id = '"+ conectar.cursoID +"'", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    lbProfesor.Text = dr["nombres"].ToString() + " " + dr["apellidos"];
                    lbSeccion.Text = dr["seccion"].ToString();
                    lbGrado.Text = dr["grado"].ToString();
                    lbAula.Text = dr["aula"].ToString();
                    grado = Convert.ToInt32(dr["grado_id"].ToString());
                    seccion = Convert.ToInt32(dr["seccion_id"].ToString());

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                MessageBox.Show("No se encontro nada");
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            seleccionado = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            string nombre = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString();
            string apellido = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString();


            dataGridView2.Rows.Add(seleccionado, nombre, apellido);
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count < 1)
            {
                MessageBox.Show("Debe agregar por lo menos un estudiante");
            }
            else
            {
                //para agregar los estudiantes al curso
                try
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        estudianteID = Convert.ToInt32(dataGridView2.Rows[i].Cells[0].Value.ToString());
                        SqlConnection cnn = conectar.getconnection();
                        cnn.Open();
                        SqlCommand comando = new SqlCommand("INSERT INTO detalles VALUES (@estudiante_id, @curso_id)", cnn);

                        comando.Parameters.Add("@estudiante_id", System.Data.SqlDbType.Int);
                        comando.Parameters.Add("@curso_id", System.Data.SqlDbType.Int);

                        comando.Parameters["@estudiante_id"].Value = estudianteID;
                        comando.Parameters["@curso_id"].Value = conectar.cursoID;

                        comando.ExecuteNonQuery();
                        cnn.Close();


                        //si el curso es actual, se actualiza el grado y la seccion de cada estudiante
                        if (checkBox1.Checked)
                        {
                            try
                            {
                                cnn.Open();
                                SqlCommand comando1 = new SqlCommand("update estudiantes set grado_id = @grado, seccion_id = @seccion where estudiante_id = '" + estudianteID + "'", cnn);
                                comando1.Parameters.Add("@grado", System.Data.SqlDbType.Int);
                                comando1.Parameters.Add("@seccion", System.Data.SqlDbType.Int);
                                comando1.Parameters["@grado"].Value = grado;
                                comando1.Parameters["@seccion"].Value = seccion;
                                comando1.ExecuteNonQuery();
                                MessageBox.Show("Estudiante Actualizado");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    MessageBox.Show("Estudiantes agregados con exito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
