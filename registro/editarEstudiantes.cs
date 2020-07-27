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
    public partial class editarEstudiantes : Form
    {
        public editarEstudiantes()
        {
            InitializeComponent();
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
                cbEnfermedad.DataSource = dt;
                cbEnfermedad.DisplayMember = "bool";
                cbEnfermedad.ValueMember = "bool_id";
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
                cbAlergia.DataSource = dt;
                cbAlergia.DisplayMember = "bool";
                cbAlergia.ValueMember = "bool_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
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
                cbSexo.DataSource = dt;
                cbSexo.DisplayMember = "sexo";
                cbSexo.ValueMember = "sexo_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void editarEstudiantes_Load(object sender, EventArgs e)
        {
            llenarsexo();
            llenarAlergia();
            llenarEnfermedad();
            llenarGrado();
            llenarSeccion();

            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("SELECT * FROM estudiantes WHERE estudiante_id = '" + conectar.estidianteID + "'", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {

                    byte[] imageBuffer = (byte[])dr["foto"];
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                    pictureBox1.Image = Image.FromStream(ms);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.BorderStyle = BorderStyle.Fixed3D;


                    txtApellidos.Text = dr["apellidos"].ToString();
                    txtNombres.Text = dr["nombres"].ToString();
                    txtTelefono.Text = dr["telefono"].ToString();
                    txtCelular.Text = dr["celular"].ToString();
                    txtDireccion.Text = dr["direccion"].ToString();
                    dateTimePicker1.Text = dr["nacimiento"].ToString();
                    cbGrado.SelectedIndex = (Convert.ToInt32( dr["grado_id"].ToString())-1);
                    cbSeccion.SelectedIndex = (Convert.ToInt32(dr["seccion_id"].ToString()) - 1);
                    txtDenfermedad.Text = dr["Denfermedad"].ToString();
                    txtDalergia.Text = dr["Dalergia"].ToString();
                    if (dr["enfermedad"].ToString().Equals("No"))
                    {
                        cbEnfermedad.SelectedIndex = 0;
                    }
                    else
                    {
                        cbEnfermedad.SelectedIndex = 1;
                    }

                    if (dr["alergia"].ToString().Equals("No"))
                    {
                        cbAlergia.SelectedIndex = 0;
                    }
                    else
                    {
                        cbAlergia.SelectedIndex = 1;
                    }

                    if (dr["sexo"].ToString().Equals("M"))
                    {
                        cbSexo.SelectedIndex = 0;
                    }
                    else
                    {
                        cbSexo.SelectedIndex = 1;
                    }
                 
                }
                else
                {

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        public void actualizarEstudiantes()
        {
            if (string.IsNullOrEmpty(txtNombres.Text) ||
                string.IsNullOrEmpty(txtApellidos.Text) ||
                string.IsNullOrEmpty(txtDireccion.Text) ||
                string.IsNullOrEmpty(txtTelefono.Text) ||
                string.IsNullOrEmpty(txtCelular.Text)
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
                    SqlCommand comando = new SqlCommand("update estudiantes set nombres = @nombres, apellidos = @apellidos, " +
                        "direccion = @direccion, telefono = @telefono, celular = @celular, nacimiento = @nacimiento, enfermedad = @enfermedad, " +
                        "dEnfermedad = @dEnfermedad, alergia = @alergia, dAlergia = @dAlergia, grado_id = @grado_id, seccion_id = @seccion_id, " +
                        "foto = @foto, sexo = @sexo where estudiante_id = '"+ conectar.estidianteID +"'", cnn);

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

                    comando.Parameters["@nombres"].Value = txtNombres.Text;
                    comando.Parameters["@apellidos"].Value = txtApellidos.Text;
                    comando.Parameters["@direccion"].Value = txtDireccion.Text;
                    comando.Parameters["@telefono"].Value = txtTelefono.Text;
                    comando.Parameters["@celular"].Value = txtCelular.Text;
                    comando.Parameters["@nacimiento"].Value = dateTimePicker1.Value.Date;
                    comando.Parameters["@enfermedad"].Value = cbEnfermedad.Text;
                    comando.Parameters["@Denfermedad"].Value = txtDenfermedad.Text;
                    comando.Parameters["@alergia"].Value = cbAlergia.Text;
                    comando.Parameters["@Dalergia"].Value = txtDalergia.Text;
                    comando.Parameters["@grado_id"].Value = cbGrado.SelectedValue;
                    comando.Parameters["@seccion_id"].Value = cbSeccion.SelectedValue;
                    comando.Parameters["@sexo"].Value = cbSexo.Text;



                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    comando.Parameters["@foto"].Value = ms.GetBuffer();

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Regsitro actualizado con exito");

                    new verEstudiantes().Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cnn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            actualizarEstudiantes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new verEstudiantes().Show();
            this.Hide();
        }

    }
}
