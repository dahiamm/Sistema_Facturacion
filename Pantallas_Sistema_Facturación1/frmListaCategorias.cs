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
    public partial class frmListaCategorias : Form
    {
        public frmListaCategorias()
        {
            InitializeComponent();
        }

        public int IdCategorias { get; set; }
        DataTable dt = new DataTable();
        Cls_Categorias categorias = new Cls_Categorias();
        public void llenar_grid()
        {
            dgCategoria.Rows.Clear();

            dt = categorias.ConsultaCategoria();

            foreach (DataRow row in dt.Rows)
            {
                dgCategoria.Rows.Add(row[0], row[1]);
            }
        }

        public void Consultar()
        {
            try
            {
                if (txtBuscar.Text != string.Empty)
                {
                    dt = categorias.Filtrar_Categoria(txtBuscar.Text);

                    foreach (DataRow row in dt.Rows)
                    {
                        dgCategoria.Rows.Add(row[0], row[1]);
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

        private void frmListaCategorias_Load(object sender, EventArgs e)
        {
            llenar_grid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmEditarCategoria Cliente = new frmEditarCategoria();
            Cliente.IdCategoria = 0;
            Cliente.ShowDialog();
            llenar_grid();
        }

        private void dgCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgCategoria.Columns[e.ColumnIndex].Name == "btnBorrar")
            {
                int posActual = dgCategoria.CurrentRow.Index;

                if (MessageBox.Show("¿Seguro de borrar?", "CONFIRMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int IdCategoria = Convert.ToInt32(dgCategoria[0, posActual].Value.ToString());
                    categorias.C_IdCategoria = IdCategoria;
                    string Mensaje = categorias.EliminaCategoria();
                    MessageBox.Show(Mensaje);
                    llenar_grid();
                }
            }
            if (dgCategoria.Columns[e.ColumnIndex].Name == "btnEditar")
            {
                int posActual = dgCategoria.CurrentRow.Index;
                frmEditarCategoria Categoria = new frmEditarCategoria();
                Categoria.IdCategoria = int.Parse(dgCategoria[0, posActual].Value.ToString());
                Categoria.ShowDialog();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                dgCategoria.Rows.Clear();
                dt = categorias.Filtrar_Categoria(txtBuscar.Text);

                if (dt.Rows.Count > 0)
                    foreach (DataRow row in dt.Rows)
                        dgCategoria.Rows.Add(row[0].ToString(), row[1].ToString());
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
            txtBuscar.Text = "";
        }
    }
}
