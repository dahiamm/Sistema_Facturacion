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
    public partial class frmEditarCliente : Form
    {
        public frmEditarCliente()
        {
            InitializeComponent();
        }
        public int IdCliente { get; set; }
        DataTable dt = new DataTable();
        Cls_Clientes cliente = new Cls_Clientes();

        private void llenar_campos()
        {
            dt = cliente.Consulta_Cliente(IdCliente);
            if (dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                {
                    txtNombreCliente.Text = row[1].ToString();
                    txtDocumento.Text = row[2].ToString();
                    txtDireccion.Text = row[3].ToString();
                    txtTelefono.Text = row[4].ToString();
                    txtEmail.Text = row[5].ToString();
                }
        }

        private void frmEditarCliente_Load(object sender, EventArgs e)
        {
            if (IdCliente == 0)
            { 
                lbTitulo.Text = "Ingreso nuevo cliente";
            }
            else
            {
                lbTitulo.Text = "Modificar cliente";
                llenar_campos();
            }
        }

        private void Guardar()
        {
            string mensaje = "";
            if (Validar())
            {
                cliente.C_IdCliente = IdCliente;
                cliente.C_strNombre = txtNombreCliente.Text;
                cliente.C_NumDocumento = double.Parse(txtDocumento.Text);
                cliente.C_StrDireccion = txtDireccion.Text;
                cliente.C_StrTelefono = txtTelefono.Text;
                cliente.C_StrEmail = txtEmail.Text;
                cliente.C_StrUsuarioModifico = "usuario";
                mensaje = cliente.ActualizarCliente();
                MessageBox.Show(mensaje);
            }
        }

        private Boolean Validar()
        {
            Boolean erroCampos = true;
            if (txtNombreCliente.Text == string.Empty)
            {
                MensajeError.SetError(txtNombreCliente, "Debe ingresar el nombre del cliente");
                txtNombreCliente.Focus();
                erroCampos = false;
            }
            else { MensajeError.SetError(txtNombreCliente, ""); }

            if (txtDocumento.Text == "")
            {
                MensajeError.SetError(txtDocumento, "Debe ingresar el documento");
                txtDocumento.Focus();
                erroCampos = false;
            }
            else { MensajeError.SetError(txtDocumento, ""); }

            if (!esNumerico(txtDocumento.Text))
            {
                MensajeError.SetError(txtDocumento, "El documento debe ser númerico");
                txtDocumento.Focus();
                return false;
            }
            MensajeError.SetError(txtDocumento, "");
            return erroCampos;
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
            Guardar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult Rta;
            Rta = MessageBox.Show("¿Desea salir de la edición?", "Mensaje de advertencia",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (Rta == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
