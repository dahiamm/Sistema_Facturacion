using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_AccesoDatos;

namespace Capa_LogicaDeNegocios
{
    public class Cls_Categorias
    {
        public int C_IdCategoria { get; set; }
        public string C_StrDescripcion { get; set; }
        public DateTime C_DtmFechaModifica { get; set; }
        public string C_StrUsuarioModifico = "Usuario";

        Cls_Acceso_Datos AccesoDatos = new Cls_Acceso_Datos();
        DataTable dt = new DataTable();

        public DataTable ConsultaCategoria()
        {
            string sentencia;
            try
            {
                sentencia = $"SELECT IdCategoria, StrDescripcion FROM TBLCATEGORIA_PROD";
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Consulta_Categoria(int IdCategoria)
        {
            string sentencia;
            try
            {
                sentencia = $"SELECT * FROM TBLCATEGORIA_PROD WHERE IdCategoria = {IdCategoria}";
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Filtrar_Categoria(string filtro)
        {
            string sentencia;
            try
            {
                sentencia = $"Select * from TBLCATEGORIA_PROD where StrDescripcion like '%{filtro}%'";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string EliminaCategoria()
        {
            string mensaje = "";

            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>();

                lst.Add(new Cls_parametros("@IdCategoria", C_IdCategoria));

                mensaje = AccesoDatos.Ejecutar_procedimiento("Eliminar_CategoriaProducto", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Falló borrado en categoría " + ex.Message;
            }
            return mensaje;
        }

        public string ActualizarCategoria()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>();

                lst.Add(new Cls_parametros("@IdCategoria", C_IdCategoria));
                lst.Add(new Cls_parametros("@StrDescripcion", C_StrDescripcion));
                lst.Add(new Cls_parametros("@DtmFechaModifica", C_DtmFechaModifica));
                lst.Add(new Cls_parametros("@StrUsuarioModifico", C_StrUsuarioModifico));
                mensaje = AccesoDatos.Ejecutar_procedimiento("actualizar_CategoriaProducto", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Falló la actualización " + ex.Message;
            }
            return mensaje;
        }
    }
}
