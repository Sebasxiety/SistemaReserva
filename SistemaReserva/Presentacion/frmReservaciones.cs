using SistemaReserva.Datos;
using SistemaReserva.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaReserva.Presentacion
{
    public partial class frmReservaciones : Form
    {
        cls_Reservaciones logicaReservaciones = new cls_Reservaciones();
        cls_Mesas logicaMesas = new cls_Mesas();
        cls_Clientes logicaClientes = new cls_Clientes();
        public frmReservaciones()
        {
            InitializeComponent(); CargarCombos();
            ActualizarDGV();
        }
        private void CargarCombos()
        {
            var mesas = logicaMesas.Listar();
            cmbMesa.DataSource = mesas;
            cmbMesa.DisplayMember = "NumeroMesa";
            cmbMesa.ValueMember = "MesaID";

            var clientes = logicaClientes.ConsultarClientes();
            cmbCliente.DataSource = clientes;
            cmbCliente.DisplayMember = "Nombre";
            cmbCliente.ValueMember = "ClienteID";
        }
        private void frmReservaciones_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMesa.SelectedIndex == -1 || cmbCliente.SelectedIndex == -1 ||
                    string.IsNullOrWhiteSpace(txtNumeroPersonas.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dto_Reservaciones reservacion = new dto_Reservaciones
                {
                    MesaID = Convert.ToInt32(cmbMesa.SelectedValue),
                    ClienteID = Convert.ToInt32(cmbCliente.SelectedValue),
                    FechaHora = dtpFecha.Value,
                    NumeroPersonas = Convert.ToInt32(txtNumeroPersonas.Text)
                };

                logicaReservaciones.AgregarReservacion(reservacion);
                MessageBox.Show("Reservación registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControles();
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la reservación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una reservación para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int reservacionID = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);

                dto_Reservaciones reservacion = new dto_Reservaciones
                {
                    MesaID = Convert.ToInt32(cmbMesa.SelectedValue),
                    ClienteID = Convert.ToInt32(cmbCliente.SelectedValue),
                    FechaHora = dtpFecha.Value,
                    NumeroPersonas = Convert.ToInt32(txtNumeroPersonas.Text)
                };

                logicaReservaciones.ActualizarReservacion(reservacionID, reservacion);
                MessageBox.Show("Reservación actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControles();
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la reservación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDatos.Rows[e.RowIndex];
                cmbMesa.SelectedValue = Convert.ToInt32(row.Cells[1].Value);
                cmbCliente.SelectedValue = Convert.ToInt32(row.Cells[2].Value);
                dtpFecha.Value = Convert.ToDateTime(row.Cells[3].Value);
                txtNumeroPersonas.Text = row.Cells[4].Value.ToString();
            }
        }
        private void ActualizarDGV()
        {
            try
            {
                var listaReservaciones = logicaReservaciones.Listar();
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = listaReservaciones;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarControles()
        {
            cmbMesa.SelectedIndex = -1;
            cmbCliente.SelectedIndex = -1;
            txtNumeroPersonas.Clear();
            dtpFecha.Value = DateTime.Now;
            dgvDatos.ClearSelection();
        }
    }
}
