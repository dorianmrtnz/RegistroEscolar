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
    public partial class Escolar : Form
    {
        public Escolar()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Seguro que desea Cambiar de Año Escolar", "Advertencia", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                cambiar();
            }
            else
            {

            }
        }
        public void cambiar()
        {
            SqlConnection cnn = conectar.getconnection();
            try
            {
                cnn.Open();
                SqlCommand comando = new SqlCommand("update estudiantes set grado_id = grado_id + 1 where grado_id < 12", cnn);

                comando.ExecuteNonQuery();

                MessageBox.Show("Actualizacion Correcta");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cnn.Close();
        }
    }
}
