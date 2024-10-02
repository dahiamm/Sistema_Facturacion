using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_LogicaDeNegocios;

namespace Pantallas_Sistema_Facturación1
{
    public partial class frmEditarFacturas : Form
    {
        public frmEditarFacturas()
        {
            InitializeComponent();
        }

        public int IdFactura { get; set; }

        DataTable dt = new DataTable();
        Cls_Facturas Facturacion = new Cls_Facturas();

        private void Llenar_Campos()
        {
            dt = Facturacion.Filtrar_Factura(IdFactura);

            foreach (DataRow row in dt.Rows)
            {
                txtNroFactura.Text = row[0].ToString();
                dtpFechaCreacion.Value = Convert.ToDateTime(row[1].ToString());
                txtCliente.Text = row[2].ToString();
                txtEmpleado.Text = row[3].ToString();
                txtDescuento.Text = row[4].ToString();
                txtIVA.Text = row[5].ToString();
                txtTotal.Text = row[6].ToString();
                cboEstado.ValueMember = row[7].ToString();
                dtpFechaModifica.Value = Convert.ToDateTime(row[8].ToString());

                dtpMostrarFechaCreacion.Value = dtpFechaCreacion.Value;
                dtpMostrarFechaModificacion.Value = dtpFechaModifica.Value;
            }
        }
        private void LLENAR_FACTURA()
        {
            if (IdFactura == 0)
            {
                lblTitulo.Text = "Ingreso nueva factura";
            }
            else
            {
                lblTitulo.Text = "Modificar factura";
                txtNroFactura.Text = "";
                Llenar_Campos();
            }
        }
        public void Filtrar_Combo_EstadoFactura()
        {
            cboEstado.DataSource = Facturacion.Consultar_Estado_Factura(Convert.ToInt32(cboEstado.ValueMember.ToString()));
            cboEstado.DisplayMember = "StrDescripcion";
            cboEstado.ValueMember = "IdEstadoFactura";
        }

        public void Llenar_Combo_EstadoFactura()
        {
            cboEstado.DataSource = Facturacion.ConsultarEstado();
            cboEstado.DisplayMember = "StrDescripcion";
            cboEstado.ValueMember = "IdEstadoFactura";
        }
        private void frmEditarFacturas_Load(object sender, EventArgs e)
        {
            LLENAR_FACTURA();

            if (txtNroFactura.Text != string.Empty)
            {
                Llenar_Campos();
                Filtrar_Combo_EstadoFactura();
            }

            if (txtNroFactura.Text == string.Empty)
            {
                Llenar_Combo_EstadoFactura();
                cboEstado.Text = "";
            }
        }

        private Boolean validar()
        {
            Boolean errorCampos = true;

            if (!esNumerico(txtEmpleado.Text))
            {
                MensajeError.SetError(txtEmpleado, "Debe ingresar el Id del Empleado registrado ");
                txtEmpleado.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtEmpleado, ""); }

            if (!esNumerico(txtCliente.Text))
            {
                MensajeError.SetError(txtCliente, "Debe ingresar el Id del Cliente existente");
                txtCliente.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtCliente, ""); }

            if (!esNumerico(txtDescuento.Text))
            {
                MensajeError.SetError(txtDescuento, "Debe ingresar un valor de descuento");
                txtDescuento.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtDescuento, ""); }

            if (!esNumerico(txtIVA.Text))
            {
                MensajeError.SetError(txtIVA, "Debe ingresar un valor de impuesto");
                txtIVA.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtIVA, ""); }

            if (!esNumerico(txtTotal.Text))
            {
                MensajeError.SetError(txtTotal, "Debe ingresar un valor total");
                txtTotal.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtTotal, ""); }

            if (cboEstado.Text == string.Empty)
            {
                MensajeError.SetError(cboEstado, "Debe de seleccionar un Estado");
                cboEstado.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(cboEstado, ""); }

            return errorCampos;
        }

        private bool esNumerico(string num)
        {
            try
            {
                double x = Convert.ToDouble(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Actualizar()
        {
            string mensaje = "";

            if (validar())
            {
                try
                {
                    Facturacion.IdFactura = IdFactura;
                    Facturacion.IdCliente = int.Parse(txtCliente.Text);
                    Facturacion.IdEmpleado = int.Parse(txtEmpleado.Text);
                    Facturacion.IdEstado = int.Parse(cboEstado.SelectedValue.ToString());
                    Facturacion.NumDescuento = int.Parse(txtDescuento.Text);
                    Facturacion.NumImpuesto = int.Parse(txtIVA.Text);
                    Facturacion.NumValorTotal = int.Parse(txtTotal.Text);
                    Facturacion.DtmFecha = dtpFechaCreacion.Value;
                    Facturacion.DtmFechaModifica = dtpFechaModifica.Value;

                    mensaje = Facturacion.Actualizar_Factura();

                    MessageBox.Show(mensaje);
                }
                catch (Exception ex)
                {
                    mensaje = "Falló la actualización de la factura" + ex.Message;
                }
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
