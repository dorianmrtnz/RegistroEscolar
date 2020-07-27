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
    public partial class editarUsuarios : Form
    {
        public editarUsuarios()
        {
            InitializeComponent();
        }

        private void editarUsuarios_Load(object sender, EventArgs e)
        {
            agregarPosicion();
            agregarRole();
            cargarDatos();
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

        public void cargarDatos()
        {
            int posicion, role;
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("SELECT * FROM usuarios WHERE usuario_id = '" + conectar.usuario_id + "'", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    textBox1.Text = dr["usuario"].ToString();
                    textBox2.Text = dr["pass"].ToString();
                    textBox3.Text = dr["pass"].ToString();
                    textBox4.Text = dr["nombre"].ToString();
                    textBox5.Text = dr["apellido"].ToString();
                    textBox6.Text = dr["direccion"].ToString();
                    textBox7.Text = dr["telefono"].ToString();
                    textBox8.Text = dr["celular"].ToString();
                    textBox9.Text = dr["cedula"].ToString();
                    posicion = Convert.ToInt32(dr["posicion_id"].ToString());
                    role = Convert.ToInt32(dr["role_id"].ToString());
                    comboBox1.SelectedIndex = posicion - 1;
                    comboBox2.SelectedIndex = role - 1;
                    
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) ||
               String.IsNullOrEmpty(textBox2.Text) ||
               String.IsNullOrEmpty(textBox3.Text) ||
               String.IsNullOrEmpty(textBox4.Text) ||
               String.IsNullOrEmpty(textBox5.Text) ||
               String.IsNullOrEmpty(textBox6.Text) ||
               String.IsNullOrEmpty(textBox7.Text) ||
               String.IsNullOrEmpty(textBox8.Text) ||
               String.IsNullOrEmpty(textBox9.Text)
                )
            {
                MessageBox.Show("Debe llenar todos los campos");
            }
            else if (textBox2.Text.Equals(textBox3.Text))
            {
                actualizar();
            }
            else
            {
                MessageBox.Show("La contraseña debe ser igual en los dos campos.");
            }
        }

        public void actualizar()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("update usuarios set usuario = @usuario, pass = @pass, nombre = @nombre, apellido = " +
                    "@apellido, posicion_id = @posicion_id, role_id = @role_id, direccion = @direccion, telefono = @telefono, " +
                    "celular = @celular, cedula = @cedula where usuario_id = '"+ conectar.usuario_id +"'", cnn);

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

                MessageBox.Show("Regsitro actualizado con exito");

                new verUsuarios().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new verUsuarios().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Seguro que desea eliminar este registro?", "Advertencia", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                SqlConnection cnn = conectar.getconnection();
                try
                {
                    cnn.Open();
                    SqlCommand comando = new SqlCommand("DELETE FROM usuarios WHERE usuario_id = '" + conectar.usuario_id + "'", cnn);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Usuario eliminado con exito");

                    new verUsuarios().Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                cnn.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
    }
}
