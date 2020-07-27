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
    public partial class cursoDtallado : Form
    {
        public cursoDtallado()
        {
            InitializeComponent();
        }

        private void cursoDtallado_Load(object sender, EventArgs e)
        {
            curso();
            detalle();
            dataGridView1.Columns[0].Visible = false;
        }
        public void curso()
        {
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("SELECT dbo.cursos.curso_id, dbo.profesores.nombres, " +
                    "dbo.profesores.apellidos, dbo.grados.grado, dbo.aulas.aula, dbo.secciones.seccion, " +
                    "dbo.cursos.fecha_fin, dbo.cursos.fecha_inicio " +
                    "FROM  dbo.cursos INNER JOIN " +
                    "dbo.grados ON dbo.cursos.grado_id = dbo.grados.grado_id INNER JOIN " +
                    "dbo.profesores ON dbo.cursos.profesor_id = dbo.profesores.profesor_id INNER JOIN " +
                    "dbo.aulas ON dbo.cursos.aula_id = dbo.aulas.aula_id INNER JOIN " +
                    "dbo.secciones ON dbo.cursos.seccion_id = dbo.secciones.seccion_id " +
                    "where cursos.curso_id = '"+ conectar.cursoDetalleID +"' " +
                    "order by fecha_inicio desc", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    lbTutor.Text = dr["nombres"].ToString() +" "+ dr["apellidos"].ToString();
                    lbAula.Text = dr["aula"].ToString();
                    lbGrado.Text = dr["grado"].ToString();
                    lbSeccion.Text = dr["seccion"].ToString();
                    DateTime ff = Convert.ToDateTime( dr["fecha_fin"].ToString());
                    lbFf.Text = ff.ToString("dd-MM-yyyy");
                    DateTime fi = Convert.ToDateTime(dr["fecha_inicio"].ToString());
                    lbFi.Text = fi.ToString("dd-MM-yyyy");

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void detalle()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT     dbo.detalles.curso_id, dbo.estudiantes.nombres, dbo.estudiantes.apellidos, dbo.estudiantes.direccion, dbo.estudiantes.telefono, " +
                                                        "dbo.estudiantes.celular, dbo.estudiantes.nacimiento, dbo.estudiantes.enfermedad, dbo.estudiantes.alergia, dbo.estudiantes.sexo " +
                                                        "FROM dbo.detalles INNER JOIN " +
                                                        "dbo.estudiantes ON dbo.detalles.estudiante_id = dbo.estudiantes.estudiante_id INNER JOIN " +
                                                        "dbo.secciones ON dbo.estudiantes.seccion_id = dbo.secciones.seccion_id INNER JOIN " +
                                                        "dbo.grados ON dbo.estudiantes.grado_id = dbo.grados.grado_id INNER JOIN " +
                                                        "dbo.detalles AS detalles_1 ON dbo.estudiantes.estudiante_id = detalles_1.estudiante_id " +
                                                        "where detalles.curso_id = '"+ conectar.cursoDetalleID +"'", cnn);
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
        }
    }
}
