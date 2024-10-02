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
    public partial class frmEditarCategoria : Form
    {
        public frmEditarCategoria()
        {
            InitializeComponent();
        }
        public int IdCategoria { get; set; }
        DataTable dt = new DataTable();
        Cls_Categorias categorias = new Cls_Categorias();

        private void llenar_campos()
        {
            dt = categorias.Consulta_Categoria(IdCategoria);
            if (dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                {
                    txtID.Text = row[0].ToString();
                    tbxNombre.Text = row[1].ToString();
                }
        }
        private void frmEditarCategoria_Load(object sender, EventArgs e)
        {
            if (IdCategoria == 0)
            {
                lbTitulo.Text = "Ingreso nueva categoría";
            }
            else
            {
                lbTitulo.Text = "Editar categoría";
                llenar_campos();
            }
        }

        private void Guardar()
        {
            string mensaje = "";
            try
            {
                if (Validar())
                {
                    categorias.C_IdCategoria = IdCategoria;
                    categorias.C_StrDescripcion = tbxNombre.Text;
                    categorias.C_DtmFechaModifica = dtpFechaModifica.Value;
                    categorias.C_StrUsuarioModifico = "Usuario";
                    mensaje = categorias.ActualizarCategoria();
                    MessageBox.Show(mensaje);
                }
            }
            catch (Exception ex)
            {
                mensaje = "Falló en la insercción" + ex.Message;
            }
        }

        private Boolean Validar()
        {
            Boolean erroCampos = true;
            if (tbxNombre.Text == string.Empty)
            {
                MensajeError.SetError(tbxNombre, "Debe ingresar el nombre de la categoría");
                tbxNombre.Focus();
                erroCampos = false;
            }
            else { MensajeError.SetError(tbxNombre, ""); }
            return erroCampos;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
