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
    public partial class editarProfesores : Form
    {
        public editarProfesores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) ||
               String.IsNullOrEmpty(textBox2.Text) ||
               String.IsNullOrEmpty(textBox3.Text) ||
               String.IsNullOrEmpty(textBox4.Text) ||
               String.IsNullOrEmpty(textBox5.Text) ||
               String.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else
            {
                SqlConnection cnn = conectar.getconnection();
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("update profesores set nombres = @nombres, apellidos = @apellidos, " +
                        "direccion = @direccion, telefono = @telefono, celular = @celular, cedula = @cedula, activo = @activo " +
                        "WHERE profesor_id = '"+ conectar.editarProfesoresID +"'", cnn);

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

                    MessageBox.Show(" registro actualizado con exito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cnn.Close();
                new buscarProfesores().Show();
                this.Hide();

            }
        }

        private void editarProfesores_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("SELECT * FROM profesores WHERE profesor_id = '" + conectar.editarProfesoresID + "'", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["nombres"].ToString();
                    textBox2.Text = dr["apellidos"].ToString();
                    textBox3.Text = dr["direccion"].ToString();
                    textBox4.Text = dr["telefono"].ToString();
                    textBox5.Text = dr["celular"].ToString();
                    textBox6.Text = dr["cedula"].ToString();

                    if (dr["activo"].ToString().Equals("SI"))
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
