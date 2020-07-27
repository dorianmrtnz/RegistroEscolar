using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using registro.Properties;
using System.Configuration;


namespace registro
{
    class conectar
    {
        //variable publica
        public static int editarProfesoresID; 
        public static int cursoDetalleID;
        public static int cursoID;
        public static String usuarioNombre;
        public static int usuario_role;
        public static int usuario_id;
        public static int estidianteID;
        public static DateTime Hoy = DateTime.Today;
        public static string fechasql = Hoy.ToString("yyyy-MM-dd");

        //conextin string del app config
        public static string cadena()
        {
            return Settings.Default.registroConnectionString;
        }

        //metodo para guardar el conexionString
        public static SqlConnection getconnection()
        {
            //SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=registro;Integrated Security=True");
            SqlConnection con = new SqlConnection(cadena());
            return con;
        }
    }
}
