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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent(); ActualizarDGV();
        }
        cls_Clientes logicaClientes = new cls_Clientes();
        private void frmClientes_Load(object sender, EventArgs e)
        {
            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDatos.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un cliente para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int clienteID = Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);

                dto_Clientes cliente = new dto_Clientes
                {
                    Nombre = txtNombre.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text
                };

                logicaClientes.ActualizarCliente(clienteID, cliente);
                MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarControles();
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dto_Clientes cliente = new dto_Clientes
                {
                    Nombre = txtNombre.Text,
                    Telefono = txtTelefono.Text,
                    Email = txtEmail.Text
                };

                logicaClientes.AgregarCliente(cliente);
                MessageBox.Show("Cliente registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarControles();
                ActualizarDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Validar que no sea el encabezado
            {
                DataGridViewRow row = dgvDatos.Rows[e.RowIndex];
                txtNombre.Text = row.Cells[1].Value.ToString();
                txtTelefono.Text = row.Cells[2].Value.ToString();
                txtEmail.Text = row.Cells[3].Value.ToString();
            }
        }
        private void LimpiarControles()
        {
            txtNombre.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            dgvDatos.ClearSelection();
        }

        private void ActualizarDGV()
        {
            try
            {
             
                var listaClientes = logicaClientes.ConsultarClientes();

                
                dgvDatos.DataSource = null; 
                dgvDatos.DataSource = listaClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
