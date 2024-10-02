using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_LogicaDeNegocios;

namespace Pantallas_Sistema_Facturación1
{
    public partial class frmListaEmpleados : Form
    {
        public frmListaEmpleados()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Empleados empleado = new Cls_Empleados();

        public void llenar_grid()
        {
            dgEmpleados.Rows.Clear();
            dt = empleado.ConsultaEmpleado();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                    dgEmpleados.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[4].ToString());
            }
            else
            {
                MessageBox.Show("No se encontraron registros");
            }
        }

        private void frmListaEmpleados_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEditarEmpleado Empleado = new frmEditarEmpleado();
            Empleado.IdEmpleado = 0;
            Empleado.ShowDialog();
            llenar_grid();
        }

        private void dgEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEmpleados.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                int posActual = dgEmpleados.CurrentRow.Index;

                if (MessageBox.Show("¿Seguro de borrar?", "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int IdEmpleado = Convert.ToInt32(dgEmpleados[0, posActual].Value.ToString());
                    empleado.C_IdEmpleado = IdEmpleado;
                    string Mensaje = empleado.EliminaEmpleado();
                    MessageBox.Show(Mensaje);
                    llenar_grid();
                }
            }
            if (dgEmpleados.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = dgEmpleados.CurrentRow.Index;
                frmEditarEmpleado Empleado = new frmEditarEmpleado();
                Empleado.IdEmpleado = int.Parse(dgEmpleados[0, posActual].Value.ToString());
                Empleado.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscarEmpleado.Text != "")
            {
                dgEmpleados.Rows.Clear();
                dt = empleado.Filtrar_Empleado(txtBuscarEmpleado.Text);

                if (dt.Rows.Count > 0)
                    foreach (DataRow row in dt.Rows)
                        dgEmpleados.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString());
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
            txtBuscarEmpleado.Text = "";
        }
    }
}
