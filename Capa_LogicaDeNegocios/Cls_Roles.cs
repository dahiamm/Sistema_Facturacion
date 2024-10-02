﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_AccesoDatos;

namespace Capa_LogicaDeNegocios
{
    public class Cls_Roles
    {
        public int C_IdRol { get; set; }
        public string C_strDescripcion { get; set; }

        Cls_Acceso_Datos AccesoDatos = new Cls_Acceso_Datos();
        DataTable dt = new DataTable();

        public DataTable Consultar_RolEmpleado()
        {
            string sentencia;
            try
            {
                sentencia = "SELECT * FROM TBLROLES";
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable FiltrarRolEmpleado(string filtro)
        {
            string sentencia = "";
            try
            {
                sentencia = $"SELECT * FROM TBLROLES WHERE StrDescripcion LIKE '%{filtro}%'";
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
