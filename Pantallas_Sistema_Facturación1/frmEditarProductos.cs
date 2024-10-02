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
    public partial class frmEditarProductos : Form
    {
        public frmEditarProductos()
        {
            InitializeComponent();
        }
        public int IdProductos { get; set; }
        DataTable dt = new DataTable();
        Cls_Productos Producto = new Cls_Productos();

        private void frmEditarProductos_Load(object sender, EventArgs e)
        {
            if (IdProductos == 0)
            {
                lblTitulo.Text = "Nuevo producto";
            }
            else
            {
                lblTitulo.Text = "Modificar producto";
                llenar_campos();
            }
        }

        private void llenar_campos()
        {
            dt = Producto.Filtrar_Producto(IdProductos);
            foreach (DataRow row in dt.Rows)
            {
                txtNombreProducto.Text = row[1].ToString();
                txtCodigo.Text = row[2].ToString();
                txtPrecioCompra.Text = row[3].ToString();
                txtPrecioVenta.Text = row[4].ToString();
                txtCategoria.Text = row[5].ToString();
                tbxDetalles.Text = row[6].ToString();
                txtStock.Text = row[8].ToString();
                dtpFechaModifica.Value = Convert.ToDateTime(row[9].ToString());
            }
        }
        private Boolean validar()
        {
            Boolean errorCampos = true;

            if (txtNombreProducto.Text == string.Empty)
            {
                MensajeError.SetError(txtNombreProducto, "Debe de ingresar el nombre del producto");
                txtNombreProducto.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtNombreProducto, ""); }

            if (!esNumerico(txtCodigo.Text))
            {
                MensajeError.SetError(txtCodigo, "El código de referencia debe ser númerico");
                txtCodigo.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtCodigo, ""); }

            if (!esNumerico(txtPrecioCompra.Text))
            {
                MensajeError.SetError(txtPrecioCompra, "El precio de compra debe ser númerico");
                txtPrecioCompra.Focus();
                return false;
            }
            MensajeError.SetError(txtPrecioCompra, "");

            if (!esNumerico(txtPrecioVenta.Text))
            {
                MensajeError.SetError(txtPrecioVenta, "El precio de venta debe ser númerico");
                txtPrecioVenta.Focus();
                return false;
            }
            MensajeError.SetError(txtPrecioCompra, "");

            if (!esNumerico(txtCategoria.Text))
            {
                MensajeError.SetError(txtCategoria, "El Id Categoria debe ser númerico");
                txtCategoria.Focus();
                return false;
            }
            MensajeError.SetError(txtCategoria, "");

            if (!esNumerico(txtStock.Text))
            {
                MensajeError.SetError(txtStock, "El Stock debe ser númerico");
                txtStock.Focus();
                return false;
            }
            MensajeError.SetError(txtStock, "");

            if (tbxDetalles.Text == string.Empty)
            {
                MensajeError.SetError(tbxDetalles, "Debe de ingresar una descripción del producto en este campo");
                tbxDetalles.Focus();
                return false;
            }
            MensajeError.SetError(tbxDetalles, "");

            return errorCampos;
        }

        public void Guardar()
        {
            string mensaje = "";

            try
            {
                if (validar())
                {
                    Producto.C_IdProducto = IdProductos;
                    Producto.C_strNombre = txtNombreProducto.Text;
                    Producto.C_StrCodigo = int.Parse(txtCodigo.Text);
                    Producto.C_NumPrecioCompra = int.Parse(txtPrecioCompra.Text);
                    Producto.C_NumPrecioVenta = int.Parse(txtPrecioVenta.Text);
                    Producto.C_IdCategoria = int.Parse(txtCategoria.Text);
                    Producto.C_StrDetalle = tbxDetalles.Text;
                    Producto.C_NumStock = int.Parse(txtStock.Text);
                    Producto.C_DtmFechaModifica = dtpFechaModifica.Value;
                    mensaje = Producto.ActualizarProducto();
                    MessageBox.Show(mensaje);
                }
            }
            catch (Exception ex)
            {
                mensaje = "Falló insercción" + ex.Message;
            }
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Datos actualizados");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
