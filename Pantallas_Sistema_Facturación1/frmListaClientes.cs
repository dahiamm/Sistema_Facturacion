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
    public partial class frmListaClientes : Form
    {
        public frmListaClientes()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Clientes clientes = new Cls_Clientes();

        public void llenar_grid()
        {
            dgClientes.Rows.Clear();
            dt = clientes.ConsultaCliente();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                    dgClientes.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[4].ToString());
            }
            else
            {
                MessageBox.Show("No se encontraron registros");
            }
        }

        private void frmListaClientes_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEditarCliente Cliente = new frmEditarCliente();
            Cliente.IdCliente = 0;
            Cliente.ShowDialog();
            llenar_grid();
        }

        private void dgClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgClientes.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                int posActual = dgClientes.CurrentRow.Index;

                if (MessageBox.Show("¿Seguro de borrar?", "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int IdCliente = Convert.ToInt32(dgClientes[0, posActual].Value.ToString());
                    clientes.C_IdCliente = IdCliente;
                    string Mensaje = clientes.EliminaCliente();
                    MessageBox.Show(Mensaje);
                    llenar_grid();
                }
            }
            if (dgClientes.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = dgClientes.CurrentRow.Index;
                frmEditarCliente Cliente = new frmEditarCliente();
                Cliente.IdCliente = int.Parse(dgClientes[0, posActual].Value.ToString());
                Cliente.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarCliente.Text != "")
            {
                dgClientes.Rows.Clear();
                dt = clientes.Filtrar_Cliente(txtBuscarCliente.Text);

                if (dt.Rows.Count > 0)
                    foreach (DataRow row in dt.Rows)
                        dgClientes.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
                else
                {
                    MessageBox.Show("No se encontraron registros por la busqueda solicitada");
                    llenar_grid();
                }
            }
            else
            {
                llenar_grid();
            }
            txtBuscarCliente.Text = "";
        }
    }
}
