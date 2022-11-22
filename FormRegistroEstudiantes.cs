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
using TR01_ControlRegistroEstudiantes.CrystalReports;
using System.Data.Common;
using CrystalDecisions.ReportAppServer;

namespace TR01_ControlRegistroEstudiantes
{
    public partial class FormRegistroEstudiantes : Form
    {
        public FormRegistroEstudiantes()
        {
            InitializeComponent();
            

        }

        private void FormRegistroEstudiantes_Load(object sender, EventArgs e)
        {
            Conexion.Connect();
            MessageBox.Show("Conexión exitosa");

            dgvRegistroEstudiantes.DefaultCellStyle.Font = new Font("Arial", 10);
            dgvRegistroEstudiantes.DataSource = Fill();
            dgvRegistroEstudiantes.AutoResizeColumns();
            dgvRegistroEstudiantes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgvRegistroEstudiantes.Columns[0].HeaderText = "ID";
            dgvRegistroEstudiantes.Columns[1].HeaderText = "PrimerNombre";
            dgvRegistroEstudiantes.Columns[2].HeaderText = "SegundoNombre";
            dgvRegistroEstudiantes.Columns[3].HeaderText = "PrimerApellido";
            dgvRegistroEstudiantes.Columns[4].HeaderText = "SegundoApellido";
            dgvRegistroEstudiantes.Columns[5].HeaderText = "Teléfono";
            dgvRegistroEstudiantes.Columns[6].HeaderText = "Celular";
            dgvRegistroEstudiantes.Columns[7].HeaderText = "Dirección";
            dgvRegistroEstudiantes.Columns[8].HeaderText = "Email";
            dgvRegistroEstudiantes.Columns[9].HeaderText = "FechaNacimiento";
            dgvRegistroEstudiantes.Columns[10].HeaderText = "Observaciones";
        }

        public DataTable Fill()
        {
            Conexion.Connect();
            DataTable dt = new DataTable();
            string consult = "SELECT * FROM Estudiantes";
            SqlCommand cmd = new SqlCommand(consult, Conexion.Connect());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        private void dgvRegistroEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvRegistroEstudiantes.ReadOnly = true;

                txtID.Text = dgvRegistroEstudiantes.CurrentRow.Cells[0].Value.ToString();
                txtPrimerNombre.Text = dgvRegistroEstudiantes.CurrentRow.Cells[1].Value.ToString();
                txtSegundoNombre.Text = dgvRegistroEstudiantes.CurrentRow.Cells[2].Value.ToString();
                txtPrimerApellido.Text = dgvRegistroEstudiantes.CurrentRow.Cells[3].Value.ToString();
                txtSegundoApellido.Text = dgvRegistroEstudiantes.CurrentRow.Cells[4].Value.ToString();
                txtTelefono.Text = dgvRegistroEstudiantes.CurrentRow.Cells[5].Value.ToString();
                txtCelular.Text = dgvRegistroEstudiantes.CurrentRow.Cells[6].Value.ToString();
                txtDireccion.Text = dgvRegistroEstudiantes.CurrentRow.Cells[7].Value.ToString();
                txtEmail.Text = dgvRegistroEstudiantes.CurrentRow.Cells[8].Value.ToString();
                txtFechaNacimiento.Text = dgvRegistroEstudiantes.CurrentRow.Cells[9].Value.ToString();
                txtObservaciones.Text = dgvRegistroEstudiantes.CurrentRow.Cells[10].Value.ToString();
            }
            catch
            {
            }
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {

            Conexion.Connect();

            if (MessageBox.Show("¿Desea insertar un nuevo registro?", "NUEVO REGISTRO",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string insert = "INSERT INTO Estudiantes (id, primernombre, segundonombre, primerapellido, segundoapellido, telefono, celular, direccion, email, f_nacimiento, observaciones)" +
                " VALUES (@id, @primernombre, @segundonombre, @primerapellido, @segundoapellido, @telefono, @celular, @direccion, @email, @f_nacimiento, @observaciones)";
                SqlCommand cmd1 = new SqlCommand(insert, Conexion.Connect());
                cmd1.Parameters.AddWithValue("@id", txtID.Text);
                cmd1.Parameters.AddWithValue("@primernombre", txtPrimerNombre.Text);
                cmd1.Parameters.AddWithValue("@segundonombre", txtSegundoNombre.Text);
                cmd1.Parameters.AddWithValue("@primerapellido", txtPrimerApellido.Text);
                cmd1.Parameters.AddWithValue("@segundoapellido", txtSegundoApellido.Text);
                cmd1.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd1.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd1.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd1.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd1.Parameters.AddWithValue("@f_nacimiento", txtFechaNacimiento.Text);
                cmd1.Parameters.AddWithValue("@observaciones", txtObservaciones.Text);

                //PARA EJECUTAR EL COMANDO
                cmd1.ExecuteNonQuery();
                MessageBox.Show("LOS DATOS SE INGRESARON CORRECTAMENTE");

                //ACTUALIZA EL DATAGRIDVIEW
                dgvRegistroEstudiantes.DataSource = Fill();
            }

            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Conexion.Connect();

            if (MessageBox.Show("¿Desea actualizar el registro seleccionado?", "ACTUALIZAR REGISTRO",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string update = "UPDATE Estudiantes SET id = @id, " +
                                                   "primernombre = @primernombre, " +
                                                   "segundonombre = @segundonombre, " +
                                                   "primerapellido = @primerapellido, " +
                                                   "segundoapellido = @segundoapellido," +
                                                   "telefono = @telefono, " +
                                                   "celular = @celular, " +
                                                   "direccion = @direccion, " +
                                                   "email = @email, " +
                                                   "f_nacimiento = @f_nacimiento, " +
                                                   "observaciones = @observaciones " +
                                                   "WHERE id = @id";

                SqlCommand cmd2 = new SqlCommand(update, Conexion.Connect());

                cmd2.Parameters.AddWithValue("@id", txtID.Text);
                cmd2.Parameters.AddWithValue("@primernombre", txtPrimerNombre.Text);
                cmd2.Parameters.AddWithValue("@segundonombre", txtSegundoNombre.Text);
                cmd2.Parameters.AddWithValue("@primerapellido", txtPrimerApellido.Text);
                cmd2.Parameters.AddWithValue("@segundoapellido", txtSegundoApellido.Text);
                cmd2.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd2.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd2.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd2.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd2.Parameters.AddWithValue("@f_nacimiento", txtFechaNacimiento.Text);
                cmd2.Parameters.AddWithValue("@observaciones", txtObservaciones.Text);

                cmd2.ExecuteNonQuery();
                MessageBox.Show("DATOS ACTUALIZADOS CORRECTAMENTE");

                dgvRegistroEstudiantes.DataSource = Fill();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Conexion.Connect();

            if (MessageBox.Show("¿Desea eliminar el registro seleccionado?", "ELIMINAR REGISTRO",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string delete = "DELETE FROM Estudiantes WHERE id = @id";

                SqlCommand cmd3 = new SqlCommand(delete, Conexion.Connect());

                cmd3.Parameters.AddWithValue("@id", txtID.Text);

                cmd3.ExecuteNonQuery();
                MessageBox.Show("DATOS ELIMINADOS CORRECTAMENTE");

                dgvRegistroEstudiantes.DataSource = Fill();
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            FormReports reporte = new FormReports();

            if (MessageBox.Show("¿Desea generar reporte?", "GENERAR REPORTE",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                reporte.Show();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "CERRAR PROGRAMA",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
