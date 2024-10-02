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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            panel2.BackColor = Color.FromArgb(40, Color.White);
            panel3.BackColor = Color.FromArgb(40, Color.White);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text != "" & txtUsuario.Text != string.Empty)
            {
                Validar_usuario Obj_validar = new Validar_usuario();
                Obj_validar.C_StrClave = txtPassword.Text;
                Obj_validar.C_StrUsuario = txtUsuario.Text;
                Obj_validar.ValidarUsuario();

                if (Obj_validar.C_IdEmpleado != 0)
                {
                    MessageBox.Show("Datos de verificación válidos");
                    FrmPrincipal frmpal = new FrmPrincipal();
                    this.Hide();
                    frmpal.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña no encontrados");
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                    txtPassword.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un usuario y una contraseña");
            }
        }
    }
}
