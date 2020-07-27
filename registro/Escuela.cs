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
    public partial class Escuela : Form
    {
        public Escuela()
        {
            InitializeComponent();
        }

        private void Escuela_Load(object sender, EventArgs e)
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand comando = new SqlCommand("select * from escuela",cnn);
                comando.CommandType = CommandType.Text;
                dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["nombre"].ToString();
                    textBox2.Text = dr["direccion"].ToString();
                    textBox3.Text = dr["telefono"].ToString();

                    if (string.IsNullOrEmpty(dr["foto"].ToString()))
                    {
                        //MessageBox.Show("No hay imagen para mostrar");
                    }
                    else
                    {
                        byte[] imageBuffer = (byte[])dr["foto"];
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        //pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) ||
               string.IsNullOrEmpty(textBox2.Text) ||
               string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else
            {
                SqlConnection cnn = conectar.getconnection();
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("update escuela set nombre = @nombre, direccion = @direccion, telefono = @telefono, foto = @foto", cnn);
                    
                    comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@foto", System.Data.SqlDbType.Image);

                    comando.Parameters["@nombre"].Value = textBox1.Text;
                    comando.Parameters["@direccion"].Value = textBox2.Text;
                    comando.Parameters["@telefono"].Value = textBox3.Text;

                    if (string.IsNullOrEmpty(pictureBox1.Image.Size.ToString()))
                    {
                    }
                    else
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        comando.Parameters["@foto"].Value = ms.GetBuffer();
                    }

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Datos actualizados correctamente");
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
