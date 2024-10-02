using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace Pantallas_Sistema_Facturación1
{
    public partial class FrmPrincipal : MaterialForm
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            frmListaClientes ListaClientes = new frmListaClientes();
            AbrirForm(ListaClientes);
        }

        public void AbrirForm(Form formHijo)
        {
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(formHijo);
            formHijo.Show();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos ListaProductos = new frmProductos();
            AbrirForm(ListaProductos);
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            frmListaCategorias ListaCategoria = new frmListaCategorias();
            AbrirForm(ListaCategoria);
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            frmListaFacturas ListaFactura = new frmListaFacturas();
            AbrirForm(ListaFactura);
        }

        private void btnInformes_Click(object sender, EventArgs e)
        {
            frmInformes Informe = new frmInformes();
            AbrirForm(Informe);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            frmListaEmpleados Empleados = new frmListaEmpleados();
            AbrirForm(Empleados);
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles Rol = new frmRoles();
            AbrirForm(Rol);
        }

        private void btnSeguridad_Click(object sender, EventArgs e)
        {
            frmAdminSeguridad adminSeguridad = new frmAdminSeguridad();
            AbrirForm(adminSeguridad);
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            frmAyuda Ayuda = new frmAyuda();
            AbrirForm(Ayuda);
        }

        private void btnAcerca_Click(object sender, EventArgs e)
        {
            frmAcercaDe Acerca = new frmAcercaDe();
            AbrirForm(Acerca);
        }
    }
}
