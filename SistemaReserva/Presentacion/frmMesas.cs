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
    public partial class frmMesas : Form
    {
        cls_Mesas logicaMesas = new cls_Mesas();
        public frmMesas()
        {
            InitializeComponent(); ActualizarDGV();
        }

        private void frmMesas_Load(object sender, EventArgs e)
        {

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNumeroMesa.Text) ||
                    string.IsNullOrWhiteSpace(txtCapacidad.Text) ||
                    string.IsNullOrWhiteSpace(txtUbicacion.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dto_Mesas mesa = new dto_Mesas
                {
                    NumeroMesa = Convert.ToInt32(txtNumeroMesa.Text),
                    Capacidad = Convert.ToInt32(txtCapacidad.Text),
                    Ubicacion = txtUbicacion.Text
                };

                logicaMesas.AgregarMesa(mesa);
                MessageBox.Show("Mesa registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControles();
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la mesa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una mesa para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int mesaID = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);

                dto_Mesas mesa = new dto_Mesas
                {
                    NumeroMesa = Convert.ToInt32(txtNumeroMesa.Text),
                    Capacidad = Convert.ToInt32(txtCapacidad.Text),
                    Ubicacion = txtUbicacion.Text
                };

                logicaMesas.ActualizarMesa(mesaID, mesa);
                MessageBox.Show("Mesa actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControles();
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la mesa: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDatos.Rows[e.RowIndex];
                txtNumeroMesa.Text = row.Cells[1].Value.ToString();
                txtCapacidad.Text = row.Cells[2].Value.ToString();
                txtUbicacion.Text = row.Cells[3].Value.ToString();
            }
        }
        private void ActualizarDGV()
        {
            try
            {
                var listaMesas = logicaMesas.Listar();
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = listaMesas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarControles()
        {
            txtNumeroMesa.Clear();
            txtCapacidad.Clear();
            txtUbicacion.Clear();
            dgvDatos.ClearSelection();
        }
    }
}
