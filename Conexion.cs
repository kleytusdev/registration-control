using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//IMPORTAMOS LAS LIBRERIAS NECESARIAS PARA LA CONEXION
using System.Data;
using System.Data.SqlClient;
using Conexion_db;

namespace Conexion_db
{
    class Conexion
    {
        //CREACIÓN DE LA CADENA DE CONEXIÓN
        public static SqlConnection Connect()
        {
            SqlConnection conn = new SqlConnection("SERVER = NAMESERVER; " +
                                                   "DATABASE = DATABASE;" +
                                                   "integrated security = true;");
            conn.Open();
            return conn;

        }

        public static SqlConnection Disconnect()
        {
            SqlConnection desconectar = new SqlConnection();
            

            if (Connect().State == System.Data.ConnectionState.Open)
            {
                Connect().Close();
            }
            return desconectar;
        }
    }
}
