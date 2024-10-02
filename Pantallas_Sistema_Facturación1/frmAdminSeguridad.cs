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
    public partial class frmAdminSeguridad : Form
    {
        public frmAdminSeguridad()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Seguridad SeguridadEmpleado = new Cls_Seguridad();

        private void frmAdminSeguridad_Load(object sender, EventArgs e)
        {
            llenar_combo_Empleados();
        }

        private void llenar_combo_Empleados()
        {
            cboEmpleado.DataSource = SeguridadEmpleado.ConsultarEmpleados();
            cboEmpleado.DisplayMember = "strNombre";
            cboEmpleado.ValueMember = "IdEmpleado";
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void Consultar()
        {
            int IdEmpleado = int.Parse(cboEmpleado.SelectedValue.ToString());
            dt = SeguridadEmpleado.Consulta_SeguridadEmpleado(IdEmpleado);
            if (dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                {
                    txtUsuario.Text = row[0].ToString();
                    txtPassword.Text = row[1].ToString();
                }
            else
            {
                txtPassword.Text = "";
                txtUsuario.Text = "";
                MessageBox.Show("No se le ha asignado un ususario y clave a este empleado");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        public void Guardar()
        {
            string mensaje = "";
            if (Validar())
            {
                SeguridadEmpleado.C_IdEmpleado = int.Parse(cboEmpleado.SelectedValue.ToString());
                SeguridadEmpleado.C_StrUsuario = txtUsuario.Text;
                SeguridadEmpleado.C_StrClave = txtPassword.Text;
                SeguridadEmpleado.C_StrUsuarioModifico = "Usuario";
                mensaje = SeguridadEmpleado.ActualizarSeguridadEmpleado();
                MessageBox.Show(mensaje);
            }
            txtPassword.Text = "";
            txtUsuario.Text = "";
        }

        private Boolean Validar()
        {
            Boolean errorCampos = true;

            if (txtUsuario.Text == string.Empty)
            {
                MensajeError.SetError(txtUsuario, "Debe ingresar un valor de Usuario");
                txtUsuario.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtUsuario, ""); }

            if (txtPassword.Text == "")
            {
                MensajeError.SetError(txtPassword, "Debe ingresar un valor de clave");
                txtPassword.Focus();
                errorCampos = false;
            }
            else { MensajeError.SetError(txtPassword, ""); }
            return errorCampos;
        }

        private bool IsNumeric(string num)
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar(); 
        }

        public void Eliminar()
        {
            if (MessageBox.Show($"¿Está seguro de borrar el registro de:\n {cboEmpleado.Text}?", "CONFIRMACION",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SeguridadEmpleado.C_IdEmpleado = int.Parse(cboEmpleado.SelectedValue.ToString());
                string mensaje = SeguridadEmpleado.EliminarSeguridadEmpleado();

                if (mensaje != "")

                {
                    MessageBox.Show(mensaje);
                }
                else
                {
                    MessageBox.Show($"Borrando el registro");
                }
                txtPassword.Text = "";
                txtUsuario.Text = "";
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
