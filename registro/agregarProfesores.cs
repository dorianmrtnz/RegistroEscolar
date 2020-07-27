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
    public partial class agregarProfesores : Form
    {
        public agregarProfesores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox1.Text) || 
               String.IsNullOrEmpty(textBox2.Text) ||
               String.IsNullOrEmpty(textBox3.Text) ||
               String.IsNullOrEmpty(textBox4.Text) ||
               String.IsNullOrEmpty(textBox5.Text) ||
               String.IsNullOrEmpty(textBox6.Text)  )
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else
            {
                SqlConnection cnn = conectar.getconnection();
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("INSERT INTO profesores VALUES (@nombres, @apellidos, @direccion, @telefono, @celular, @cedula, @activo)", cnn);

                    comando.Parameters.Add("@nombres", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@celular", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@cedula", System.Data.SqlDbType.VarChar);
                    comando.Parameters.Add("@activo", System.Data.SqlDbType.VarChar);

                    comando.Parameters["@nombres"].Value = textBox1.Text;
                    comando.Parameters["@apellidos"].Value = textBox2.Text;
                    comando.Parameters["@direccion"].Value = textBox3.Text;
                    comando.Parameters["@telefono"].Value = textBox4.Text;
                    comando.Parameters["@celular"].Value = textBox5.Text;
                    comando.Parameters["@cedula"].Value = textBox6.Text;
                    comando.Parameters["@activo"].Value = comboBox1.Text;

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Regsitro guardado con exito");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cnn.Close();
            }
        }

        private void agregarProfesores_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
