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
    public partial class agregarUsuario : Form
    {
        public agregarUsuario()
        {
            InitializeComponent();
        }

        public void agregarPosicion()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from posiciones", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "posicion";
                comboBox1.ValueMember = "posicion_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }
        public void agregarRole()
        {
            SqlConnection cnn = conectar.getconnection();
            cnn.Open();
            try
            {
                SqlCommand comando = new SqlCommand("select * from roles", cnn);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataTable dt = new DataTable("suplidor");
                da.Fill(dt);
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "role";
                comboBox2.ValueMember = "role_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void agregarUsuario_Load(object sender, EventArgs e)
        {
            agregarRole();
            agregarPosicion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox1.Text)||
               String.IsNullOrEmpty(textBox2.Text)||
               String.IsNullOrEmpty(textBox3.Text)||
               String.IsNullOrEmpty(textBox4.Text)||
               String.IsNullOrEmpty(textBox5.Text)||
               String.IsNullOrEmpty(textBox6.Text)||
               String.IsNullOrEmpty(textBox7.Text)||
               String.IsNullOrEmpty(textBox8.Text)||
               String.IsNullOrEmpty(textBox9.Text)
                )
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else if (textBox2.Text.Equals(textBox3.Text))
            {
                insertar();
            }
            else
            {
                MessageBox.Show("La contraseña debe ser igual en los dos campos.");
            }
        }

        public void insertar()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO usuarios VALUES (@usuario, @pass, @nombre, @apellido, @posicion_id, " +
                    "@role_id, @direccion, @telefono, @celular, @cedula)", cnn);

                comando.Parameters.Add("@usuario", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@pass", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@apellido", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@posicion_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@role_id", System.Data.SqlDbType.Int);
                comando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@celular", System.Data.SqlDbType.VarChar);
                comando.Parameters.Add("@cedula", System.Data.SqlDbType.VarChar);

                comando.Parameters["@usuario"].Value = textBox1.Text;
                comando.Parameters["@pass"].Value = textBox2.Text;
                comando.Parameters["@nombre"].Value = textBox4.Text;
                comando.Parameters["@apellido"].Value = textBox5.Text;
                comando.Parameters["@posicion_id"].Value = comboBox1.SelectedIndex + 1;
                comando.Parameters["@role_id"].Value = comboBox2.SelectedIndex + 1;
                comando.Parameters["@direccion"].Value = textBox6.Text;
                comando.Parameters["@telefono"].Value = textBox7.Text;
                comando.Parameters["@celular"].Value = textBox8.Text;
                comando.Parameters["@cedula"].Value = textBox9.Text;


                comando.ExecuteNonQuery();

                MessageBox.Show("Regsitro guardado con exito");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
        }
    }
}
