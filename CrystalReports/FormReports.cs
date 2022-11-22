using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//IMPORTAMOS LAS LIBRERIAS NECESARIAS PARA LA CONEXION
using System.Data.SqlClient;
using Conexion_db;

namespace TR01_ControlRegistroEstudiantes.CrystalReports
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
        }


        public DataSet reportar()
        {
            Conexion.Connect();
            string sql = "SELECT * FROM Estudiantes";
            SqlDataAdapter da = new SqlDataAdapter(sql, Conexion.Connect());
            DataSet dst = new DataSet();
            da.Fill(dst);
            Conexion.Connect();
            Conexion.Disconnect();
            return dst;
        }

        private void FormReports_Load(object sender, EventArgs e)
        {

            DataSet dst = new DataSet();
            dst = reportar();

            //Verificamos si los registros son mayor a 0
            if (dst.Tables[0].Rows.Count -1 > 0)
            {
                //Instanciando el CrystalReport
                ReporteEstudiantes reporte = new ReporteEstudiantes();
                //Enviando registros filtrados
                reporte.SetDataSource(dst.Tables[0]);
                crystalReportViewer1.ReportSource = reporte;
            }
            else
            {
                MessageBox.Show("No hay registros");
            }
        }
    }
}
