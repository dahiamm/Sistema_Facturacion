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
    public partial class frmEditarEmpleado : Form
    {
        public frmEditarEmpleado()
        {
            InitializeComponent();
        }

        public int IdEmpleado { get; set; }
        DataTable dt = new DataTable();
        Cls_Empleados empleado = new Cls_Empleados();

        private void frmEditarEmpleado_Load(object sender, EventArgs e)
        {
            llenar_combo();
            if (IdEmpleado == 0)
            { //Registro nuevo
                lbTitulo.Text = "Ingreso nuevo empleado";
            }
            else
            {
                lbTitulo.Text = "Modificar empleado";
                llenar_campos();
            }
        }
        private void llenar_combo()
        {
            cboRol.DataSource = empleado.ConsultarRol();
            cboRol.DisplayMember = "StrDescripcion";
            cboRol.ValueMember = "IdRolEmpleado";
        }
        private void llenar_campos()
        {
            dt = empleado.Consulta_Empleado(IdEmpleado);
            if (dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                {
                    txtNombre.Text = row[1].ToString();
                    txtDocumento.Text = row[2].ToString();
                    txtDireccion.Text = row[3].ToString();
                    txtTelefono.Text = row[4].ToString();
                    txtEmail.Text = row[5].ToString();
                    cboRol.SelectedValue = int.Parse(row[6].ToString());
                    dtIngreso.Value = Convert.ToDateTime(row[7].ToString());
                    dtRetiro.Value = Convert.ToDateTime(row[7].ToString());
                    txtDetalles.Text = row[9].ToString();
                }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            string mensaje = "";
            if(Validar())
            {
                empleado.C_IdEmpleado = IdEmpleado;
                empleado.C_strNombre = txtNombre.Text;
                empleado.C_NumDocumento = double.Parse(txtDocumento.Text);
                empleado.C_StrDireccion = txtDireccion.Text;
                empleado.C_StrTelefono = txtTelefono.Text;
                empleado.C_StrEmail = txtEmail.Text;
                empleado.C_IdRolEmpleado = int.Parse(cboRol.SelectedValue.ToString());
                empleado.C_DtmIngreso = dtIngreso.Value;
                empleado.C_DtmRetiro = dtRetiro.Value;
                empleado.C_strDatosAdicionales = txtDetalles.Text;
                empleado.C_StrUsuarioModifico = "usuario";
                mensaje = empleado.ActualizarEmpleado();
                MessageBox.Show(mensaje);
            }
        }

        private Boolean Validar()
        {
            Boolean erroCampos = true;
            if (txtNombre.Text == string.Empty)
            {
                MensajeError.SetError(txtNombre, "Debe ingresar el nombre del empleado");
                txtNombre.Focus();
                erroCampos = false;
            }
            else { MensajeError.SetError(txtNombre, ""); }

            if(txtDocumento.Text == "")
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
