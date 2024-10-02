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
    public partial class frmListaFacturas : Form
    {
        public frmListaFacturas()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Facturas facturas = new Cls_Facturas();
        public void llenar_grid()
        {
            dgFacturas.Rows.Clear();

            dt = facturas.Consultar_Facturas();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                    dgFacturas.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
            }
            else
            {
                MessageBox.Show("No se encontraron registros");
            }
        }

        private void frmListaFacturas_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEditarFacturas Factura = new frmEditarFacturas();
            Factura.IdFactura = 0;
            Factura.ShowDialog();
            llenar_grid();
        }

        private void dgFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgFacturas.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = dgFacturas.CurrentRow.Index;
                frmEditarFacturas Factura = new frmEditarFacturas();
                Factura.IdFactura = int.Parse(dgFacturas[0, posActual].Value.ToString());
                Factura.ShowDialog();
            }
        }
        public void Consultar()
        {
            try
            {
                if (txtBuscar.Text != string.Empty)
                {
                    dgFacturas.Rows.Clear();
                    dt = facturas.Filtrar_Factura(int.Parse(txtBuscar.Text));

                    foreach (DataRow row in dt.Rows)
                    {
                        dgFacturas.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
                        txtBuscar.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron facturas con el número solicitado");
                    llenar_grid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar" + ex);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }
    }
}
