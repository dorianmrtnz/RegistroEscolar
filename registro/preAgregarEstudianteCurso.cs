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
    public partial class preAgregarEstudianteCurso : Form
    {
        public preAgregarEstudianteCurso()
        {
            InitializeComponent();
        }

        private void preAgregarEstudianteCurso_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT  dbo.cursos.curso_id, dbo.profesores.nombres as 'Nombres del tutor', dbo.profesores.apellidos as 'Apellidos del tutor', " +
                                                        "dbo.secciones.seccion, dbo.grados.grado,  dbo.aulas.aula, dbo.cursos.fecha_inicio, dbo.cursos.fecha_fin " +
                                                        "FROM dbo.aulas INNER JOIN " +
                                                        "dbo.cursos ON dbo.aulas.aula_id = dbo.cursos.aula_id INNER JOIN " +
                                                        "dbo.grados ON dbo.cursos.grado_id = dbo.grados.grado_id INNER JOIN " +
                                                        "dbo.profesores ON dbo.cursos.profesor_id = dbo.profesores.profesor_id INNER JOIN " +
                                                        "dbo.secciones ON dbo.cursos.seccion_id = dbo.secciones.seccion_id order by fecha_inicio desc", cnn);
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
            conectar.cursoID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            new agregarEstudiateCurso().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
