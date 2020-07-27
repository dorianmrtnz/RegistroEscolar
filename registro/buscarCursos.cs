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
    public partial class buscarCursos : Form
    {
        public buscarCursos()
        {
            InitializeComponent();
        }

        private void buscarCursos_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.Hide();
            dateTimePicker1.Hide();
            dataGridView1.Hide();
            dateTimePicker2.Hide();
            label2.Hide();
            label3.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                comboBox2.Show();

                textBox1.Hide();
                dateTimePicker1.Hide();
                dateTimePicker2.Hide();
                label2.Hide();
                label3.Hide();

                llenarcb();
            }
            else if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Show();
                comboBox2.Hide();
                dateTimePicker1.Hide();
                dateTimePicker2.Hide();
                label2.Hide();
                label3.Hide();
            }
            else
            {
                dateTimePicker1.Show();
                dateTimePicker2.Show();
                label2.Show();
                label3.Show();
                comboBox2.Hide();
                textBox1.Hide();
            }
        }

        public void cargarXprofesor()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT dbo.cursos.curso_id, dbo.profesores.nombres as 'Nombre Tutor', " +
                    "dbo.profesores.apellidos as 'Apellidos Tutor', dbo.grados.grado, dbo.aulas.aula, dbo.secciones.seccion, " +
                    "dbo.cursos.fecha_inicio as 'Fecha de inicio', dbo.cursos.fecha_fin as 'Fecha de fin' " +
                    "FROM  dbo.cursos INNER JOIN " +
                    "dbo.grados ON dbo.cursos.grado_id = dbo.grados.grado_id INNER JOIN " +
                    "dbo.profesores ON dbo.cursos.profesor_id = dbo.profesores.profesor_id INNER JOIN " +
                    "dbo.aulas ON dbo.cursos.aula_id = dbo.aulas.aula_id INNER JOIN " +
                    "dbo.secciones ON dbo.cursos.seccion_id = dbo.secciones.seccion_id " +
                    "where profesores.nombres like '%"+ textBox1.Text +"%' or profesores.apellidos like '%"+ textBox1.Text +"%' " +
                    "order by fecha_inicio desc", cnn);
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

        public void cargarXgrado()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT dbo.cursos.curso_id, dbo.profesores.nombres as 'Nombre Tutor', " +
                    "dbo.profesores.apellidos as 'Apellidos Tutor', dbo.grados.grado, dbo.aulas.aula, dbo.secciones.seccion, " +
                    "dbo.cursos.fecha_inicio as 'Fecha de inicio', dbo.cursos.fecha_fin as 'Fecha de fin' " +
                    "FROM  dbo.cursos INNER JOIN " +
                    "dbo.grados ON dbo.cursos.grado_id = dbo.grados.grado_id INNER JOIN " +
                    "dbo.profesores ON dbo.cursos.profesor_id = dbo.profesores.profesor_id INNER JOIN " +
                    "dbo.aulas ON dbo.cursos.aula_id = dbo.aulas.aula_id INNER JOIN " +
                    "dbo.secciones ON dbo.cursos.seccion_id = dbo.secciones.seccion_id " +
                    "where grado = '"+ comboBox2.Text +"' " +
                    "order by fecha_inicio desc", cnn);
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

        public void cargarXfecha()
        {
            string fecha1 = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            string fecha2 = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT dbo.cursos.curso_id, dbo.profesores.nombres as 'Nombre Tutor', " +
                    "dbo.profesores.apellidos as 'Apellidos Tutor', dbo.grados.grado, dbo.aulas.aula, dbo.secciones.seccion, " +
                    "dbo.cursos.fecha_inicio as 'Fecha de inicio', dbo.cursos.fecha_fin as 'Fecha de fin' " +
                    "FROM  dbo.cursos INNER JOIN " +
                    "dbo.grados ON dbo.cursos.grado_id = dbo.grados.grado_id INNER JOIN " +
                    "dbo.profesores ON dbo.cursos.profesor_id = dbo.profesores.profesor_id INNER JOIN " +
                    "dbo.aulas ON dbo.cursos.aula_id = dbo.aulas.aula_id INNER JOIN " +
                    "dbo.secciones ON dbo.cursos.seccion_id = dbo.secciones.seccion_id " +
                    "where fecha_inicio BETWEEN '"+ fecha1 +"' AND '"+ fecha2 +"'  " +
                    "order by fecha_inicio desc", cnn);
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

        public void llenarcb()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from grados", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "grado";
                comboBox2.ValueMember = "grado_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                dataGridView1.Show();
                cargarXprofesor();
                dataGridView1.Columns[0].Visible = false;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                dataGridView1.Show();
                cargarXgrado();
                dataGridView1.Columns[0].Visible = false;
            }
            else
            {
                dataGridView1.Show();
                cargarXfecha();
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            conectar.cursoDetalleID = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
            new cursoDtallado().Show();
            //MessageBox.Show(conectar.cursoDetalleID.ToString());
        }
    }
}
