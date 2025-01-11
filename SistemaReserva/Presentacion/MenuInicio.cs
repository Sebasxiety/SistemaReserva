using SistemaReserva.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaReserva
{
    public partial class MenuInicio : Form
    {
        public MenuInicio()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes();
            frm.ShowDialog();
        }

        private void btnReservaciones_Click(object sender, EventArgs e)
        {
            frmReservaciones frm = new frmReservaciones();
            frm.ShowDialog();
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            frmMesas frm = new frmMesas();
            frm.ShowDialog();
        }
    }
}
