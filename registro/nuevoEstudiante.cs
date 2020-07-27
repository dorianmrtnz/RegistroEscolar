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
    public partial class nuevoEstudiante : Form
    {
        public nuevoEstudiante()
        {
            InitializeComponent();
        }

        private void nuevoEstudiante_Load(object sender, EventArgs e)
        {
            llenarGrado();
            llenarSeccion();
            llenarEnfermedad();
            llenarAlergia();
            llenarsexo();
            textBox8.Enabled = false;
            textBox9.Enabled = false;


        }
        //para llenar el combobox de sexo
        public void llenarsexo()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from sexo", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox5.DataSource = dt;
                comboBox5.DisplayMember = "sexo";
                comboBox5.ValueMember = "sexo_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        //para llenar el combobox de grado
        public void llenarGrado()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from grados", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "grado";
                comboBox3.ValueMember = "grado_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        //para llenar el combobox de seccion
        public void llenarSeccion()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from secciones", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox4.DataSource = dt;
                comboBox4.DisplayMember = "seccion";
                comboBox4.ValueMember = "seccion_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        //para llenar el combobox de enfermedad
        public void llenarEnfermedad()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from bool", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "bool";
                comboBox1.ValueMember = "bool_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        //para llenar el combobox de alergia
        public void llenarAlergia()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from bool", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "bool";
                comboBox2.ValueMember = "bool_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                textBox8.Enabled = true;
            }
            else
            {
                textBox8.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                textBox9.Enabled = true;
            }
            else
            {
                textBox9.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Se crea el OpenFileDialog
            OpenFileDialog dialog = new OpenFileDialog();
            // Se muestra al usuario esperando una acción
            DialogResult result = dialog.ShowDialog();

            // Si seleccionó un archivo (asumiendo que es una imagen lo que seleccionó)
            // la mostramos en el PictureBox de la inferfaz
            if (result == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            }

        }

        public void guardarEstudiate()
        {
            if( string.IsNullOrEmpty(textBox1.Text) ||
                string.IsNullOrEmpty(textBox2.Text) ||
                string.IsNullOrEmpty(textBox3.Text) ||
                string.IsNullOrEmpty(textBox4.Text) ||
                string.IsNullOrEmpty(textBox5.Text)
              )
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else
            {
                SqlConnection cnn = conectar.getconnection();
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("INSERT INTO estudiantes VALUES (@nombres, @apellidos, @direccion," +
                        "@telefono, @celular, @nacimiento, @enfermedad, @Denfermedad, @alergia, @Dalergia, @grado_id," +
                        "@seccion_id, @foto, @sexo)", cnn);

                    comando.Parameters.Add("@nombres", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@celular", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@nacimiento", System.Data.SqlDbType.Date);
                    comando.Parameters.Add("@enfermedad", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@Denfermedad", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@alergia", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@Dalergia", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@grado_id", System.Data.SqlDbType.Int);
                    comando.Parameters.Add("@seccion_id", System.Data.SqlDbType.Int);
                    comando.Parameters.Add("@foto", System.Data.SqlDbType.Image);
                    comando.Parameters.Add("@sexo", System.Data.SqlDbType.VarChar);

                    comando.Parameters["@nombres"].Value = textBox1.Text;
                    comando.Parameters["@apellidos"].Value = textBox2.Text;
                    comando.Parameters["@direccion"].Value = textBox3.Text;
                    comando.Parameters["@telefono"].Value = textBox4.Text;
                    comando.Parameters["@celular"].Value = textBox5.Text;
                    comando.Parameters["@nacimiento"].Value = dateTimePicker1.Value.Date;
                    comando.Parameters["@enfermedad"].Value = comboBox1.Text;
                    comando.Parameters["@Denfermedad"].Value = textBox8.Text;
                    comando.Parameters["@alergia"].Value = comboBox2.Text;
                    comando.Parameters["@Dalergia"].Value = textBox9.Text;
                    comando.Parameters["@grado_id"].Value = comboBox3.SelectedValue;
                    comando.Parameters["@seccion_id"].Value = comboBox4.SelectedValue;
                    comando.Parameters["@sexo"].Value = comboBox5.Text;

                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    comando.Parameters["@foto"].Value = ms.GetBuffer();

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Regsitro guardado con exito");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox8.Text = "";
                    textBox9.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cnn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guardarEstudiate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
        } 
    }
}
