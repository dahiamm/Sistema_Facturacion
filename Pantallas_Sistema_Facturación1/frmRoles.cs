﻿using System;
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
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        Cls_Roles Roles = new Cls_Roles();


        public void Llenar_grid()
        {
            dgRoles.Rows.Clear();
            dt = Roles.Consultar_RolEmpleado();

            foreach (DataRow row in dt.Rows)
            {
                dgRoles.Rows.Add(row[0], row[1]);
            }
        }

        public void Consultar()
        {
            try
            {
                if (txtBuscarNombre.Text != string.Empty)
                {
                    DataTable dt = new DataTable();

                    dgRoles.Rows.Clear();
                    dt = Roles.FiltrarRolEmpleado(txtBuscarNombre.Text);

                    foreach (DataRow row in dt.Rows)
                    {
                        dgRoles.Rows.Add(row[0], row[1]);
                    }
                }
                else
                {
                    Llenar_grid();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se encontraron datos similares");
                txtBuscarNombre.Text = string.Empty;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgRoles.Rows.Clear();
            Consultar();
        }

        private void dgRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Llenar_grid();
        }

        private void btnRegistros_Click(object sender, EventArgs e)
        {
            Llenar_grid();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dgRoles.Rows.Clear();
            txtBuscarNombre.Text = string.Empty;
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            Llenar_grid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
