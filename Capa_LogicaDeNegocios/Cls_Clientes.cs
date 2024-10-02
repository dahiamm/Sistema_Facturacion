using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Capa_AccesoDatos;

namespace Capa_LogicaDeNegocios
{
    public class Cls_Clientes
    {
        public int C_IdCliente { get; set; }
        public string C_strNombre { get; set; }
        public double C_NumDocumento { get; set; }
        public string C_StrDireccion { get; set; }
        public string C_StrTelefono { get; set; }
        public string C_StrEmail { get; set; }
        public DateTime C_DtmFechaModifica { get; set; }
        public string C_StrUsuarioModifico { get; set; }

        Cls_Acceso_Datos AccesoDatos = new Cls_Acceso_Datos();

        public DataTable ConsultaCliente()
        {
            string sentencia;
            try
            {
                sentencia = " Select * from TBLCLIENTES ";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Consulta_Cliente(int IdCliente)
        {
            string sentencia = "";
            try
            {
                sentencia = $"Select * from TBLCLIENTES where IdCliente = {IdCliente}";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable Filtrar_Cliente(string filtro)
        {
            string sentencia;
            try
            {
                sentencia = $"Select * from TBLCLIENTES where StrNombre like '%{filtro}%'";
                DataTable dt = new DataTable();
                dt = AccesoDatos.EjecutarConsulta(sentencia);
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string EliminaCliente()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>();

                lst.Add(new Cls_parametros("@IdCliente", C_IdCliente));
                mensaje = AccesoDatos.Ejecutar_procedimiento("Eliminar_Cliente", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Falló borrado de cliente " + ex.Message;
            }
            return mensaje;
        }

        public string ActualizarCliente()
        {
            string mensaje = "";
            try
            {
                List<Cls_parametros> lst = new List<Cls_parametros>();
                lst.Add(new Cls_parametros("@IdCliente", C_IdCliente));
                lst.Add(new Cls_parametros("@StrNombre", C_strNombre));
                lst.Add(new Cls_parametros("@NumDocumento", C_NumDocumento));
                lst.Add(new Cls_parametros("@StrDireccion", C_StrDireccion));
                lst.Add(new Cls_parametros("@StrTelefono", C_StrTelefono));
                lst.Add(new Cls_parametros("@StrEmail", C_StrEmail));
                lst.Add(new Cls_parametros("@DtmFechaModifica", DateTime.Now));
                lst.Add(new Cls_parametros("@StrUsuarioModifica", C_StrUsuarioModifico));

                mensaje = AccesoDatos.Ejecutar_procedimiento("actualizar_Cliente", lst);
            }
            catch (Exception ex)
            {
                mensaje = "Falló la actualización " + ex.Message;
            }
            return mensaje;
        }
    }
}
