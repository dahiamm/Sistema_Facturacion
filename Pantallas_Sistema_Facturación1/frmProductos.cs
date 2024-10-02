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
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Productos productos = new Cls_Productos();
        public void llenar_grid()
        {
            dgProductos.Rows.Clear();

            dt = productos.ConsultaProducto();

            foreach (DataRow row in dt.Rows)
            {
                dgProductos.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]);
            }
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEditarProductos Productos = new frmEditarProductos();
            Productos.IdProductos = 0;
            Productos.ShowDialog();
            llenar_grid();
        }

        private void dgProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgProductos.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                int posActual = dgProductos.CurrentRow.Index;

                if (MessageBox.Show("¿Seguro de borrar?", "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Id_Producto = Convert.ToInt32(dgProductos[0, posActual].Value.ToString());
                    productos.C_IdProducto = Id_Producto;
                    string Mensaje = productos.EliminarProducto();
                    MessageBox.Show(Mensaje);
                    llenar_grid();
                }
            }
            if (dgProductos.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = dgProductos.CurrentRow.Index;
                frmEditarProductos Producto = new frmEditarProductos();
                Producto.IdProductos = int.Parse(dgProductos[0, posActual].Value.ToString());
                Producto.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBuscar.Text != "")
                {
                    dgProductos.Rows.Clear();
                    dt = productos.FiltrarProducto(txtBuscar.Text);
                    foreach (DataRow row in dt.Rows)
                    {
                        dgProductos.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]);
                    }
                }
                else
                {
                    llenar_grid();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se encontraron datos similares");
                txtBuscar.Text = string.Empty;
            }
        }
    }
}
