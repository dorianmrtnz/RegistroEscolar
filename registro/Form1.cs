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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void entrar()
        {
            String usuario = textBox1.Text;
            String pass = textBox2.Text;
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("select * from usuarios where usuario = '" + usuario + "' and pass = '" + pass + "'", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    conectar.usuarioNombre = dr["nombre"].ToString();
                    conectar.estidianteID = Convert.ToInt32(dr["usuario_id"].ToString());
                    conectar.usuario_role = Convert.ToInt32(dr["role_id"].ToString());
                    MessageBox.Show("Bienvenido");

                    panel1.Hide();
                    this.Text = "Registro";
                    menuStrip1.Show();
                    if (conectar.usuario_role == 1)
                    {
                        administracionToolStripMenuItem.Enabled = true;
                    }

                    cnn.Close();

                    sacarDatos();
                }
                else
                {
                    MessageBox.Show("contrasena o usuario incorrecto");
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void sacarDatos()
        {
            try
            {
                SqlConnection cnn = conectar.getconnection();
                cnn.Open();
                SqlDataReader dr = null;
                SqlCommand consulta = new SqlCommand("select * from escuela", cnn);
                consulta.CommandType = CommandType.Text;
                dr = consulta.ExecuteReader();
                if (dr.Read())
                {
                    textBox3.Text = dr["nombre"].ToString() + Environment.NewLine + dr["direccion"].ToString() + Environment.NewLine + dr["telefono"].ToString();
                    textBox3.Show();

                    if (string.IsNullOrEmpty(dr["foto"].ToString())) 
                    {
                    }
                    else
                    {
                        byte[] imageBuffer = (byte[])dr["foto"];
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);
                        //this.BackgroundImage = global::registro.Properties.Resources.escuela;
                        //this.BackgroundImage = Image.FromStream(ms);
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                    }
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
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Debe entrar un usuario y una contraseña");
            }
            else
            {
                entrar();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {

                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Debe entrar un usuario y una contraseña");
                }
                else
                {
                    entrar();
                }
            }
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new nuevoEstudiante().Show();
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new verEstudiantes().Show();
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AgregarCursos().Show();
        }

        private void agregarEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new preAgregarEstudianteCurso().Show();
        }

        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new buscarCursos().Show();
        }

        private void agregarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new agregarProfesores().Show();
        }

        private void buscarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            new buscarProfesores().Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Hide();
            administracionToolStripMenuItem.Enabled = false;
            textBox3.Hide();
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new verUsuarios().Show();
        }

        private void agregarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            new agregarUsuario().Show();
        }

        private void añoEscolarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Escolar().Show();
        }

        private void aulasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Aulas().Show();
        }

        private void gradoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Grados().Show();
        }

        private void seccionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Secciones().Show();
        }

        private void escuelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Posiciones().Show();
        }

        private void posicionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Escuela().Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
