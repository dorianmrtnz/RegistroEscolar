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
    public partial class AgregarCursos : Form
    {
        public AgregarCursos()
        {
            InitializeComponent();
        }

        private void AgregarCursos_Load(object sender, EventArgs e)
        {
            llenartutor();
            llenarAula();
            llenarGrado();
            llenarSecion();
        }

        public void llenartutor()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from profesores", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbTutor.DataSource = dt;
                cbTutor.DisplayMember = "nombres";
                cbTutor.ValueMember = "Profesor_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        public void llenarAula()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from Aulas", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbAula.DataSource = dt;
                cbAula.DisplayMember = "aula";
                cbAula.ValueMember = "aula_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        public void llenarGrado()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from grados", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbGrado.DataSource = dt;
                cbGrado.DisplayMember = "grado";
                cbGrado.ValueMember = "grado_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        public void llenarSecion()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from secciones", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cbSeccion.DataSource = dt;
                cbSeccion.DisplayMember = "seccion";
                cbSeccion.ValueMember = "seccion_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO cursos VALUES (@aula_id, @grado_id, @seccion_id, @profesor_id, " +
                    "@fecha_inicio, @fecha_fin); SELECT SCOPE_IDENTITY()", cnn);

                comando.Parameters.Add("@aula_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@grado_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@seccion_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@profesor_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@fecha_inicio", System.Data.SqlDbType.Date);
                comando.Parameters.Add("@fecha_fin", System.Data.SqlDbType.Date);

                comando.Parameters["@aula_id"].Value = cbAula.SelectedValue;
                comando.Parameters["@grado_id"].Value = cbGrado.SelectedValue;
                comando.Parameters["@seccion_id"].Value = cbSeccion.SelectedValue;
                comando.Parameters["@profesor_id"].Value = cbTutor.SelectedValue;
                comando.Parameters["@fecha_inicio"].Value = dateTimePicker1.Value.Date;
                comando.Parameters["@fecha_fin"].Value = dateTimePicker2.Value.Date;

                conectar.cursoID = Convert.ToInt32( comando.ExecuteScalar().ToString());
                new agregarEstudiateCurso().Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO cursos VALUES (@aula_id, @grado_id, @seccion_id, @profesor_id, " +
                    "@fecha_inicio, @fecha_fin); SELECT SCOPE_IDENTITY()", cnn);

                comando.Parameters.Add("@aula_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@grado_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@seccion_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@profesor_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@fecha_inicio", System.Data.SqlDbType.Date);
                comando.Parameters.Add("@fecha_fin", System.Data.SqlDbType.Date);

                comando.Parameters["@aula_id"].Value = cbAula.SelectedValue;
                comando.Parameters["@grado_id"].Value = cbGrado.SelectedValue;
                comando.Parameters["@seccion_id"].Value = cbSeccion.SelectedValue;
                comando.Parameters["@profesor_id"].Value = cbTutor.SelectedValue;
                comando.Parameters["@fecha_inicio"].Value = dateTimePicker1.Value.Date;
                comando.Parameters["@fecha_fin"].Value = dateTimePicker2.Value.Date;

                comando.ExecuteNonQuery();
                MessageBox.Show("Curso guardado con exito");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
