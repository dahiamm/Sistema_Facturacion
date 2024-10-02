using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Capa_AccesoDatos
{
    public class Cls_parametros
    {
        //Parámetros atributos
        public string Nombre { get; set; } //Nombre del parámetro
        public object Valor { get; set; } //Valor parámetro
        public SqlDbType TipoDato { get; set; } //Tipo de dato
        public Int32 Tamaño { get; set; } //Longitud parámetro
        public ParameterDirection DireccionParametro { get; set; } //Dirección parámetro, Input, Output

        //Parametros de entrada
        public Cls_parametros(string ObjNombre, Object ObjValor)
        {
            Nombre = ObjNombre;
            Valor = ObjValor;
            DireccionParametro = ParameterDirection.Input;
        }

        public Cls_parametros(string ObjNombre, SqlDbType ObjTipoDato, Int32 ObjTamaño)
        {
            Nombre = ObjNombre;
            TipoDato = ObjTipoDato;
            Tamaño = ObjTamaño;
            DireccionParametro = ParameterDirection.Output;
        }
    }
    public class Cls_Acceso_Datos
    {
        SqlConnection conexion; //Variable para la conexión de tipo SqlConnection
        SqlCommand cmd; //Variable para realizar comandos en la BD.
        SqlDataReader LectorDatos = null;
        SqlDataAdapter da;
        DataTable dt;

        public string AbrirBd() //Método para abrir la base de datos
        {
            string resultado = "";
            try   //Captura de un error en caso que se presente
            {
                //Objeto de tipo conexión a la base de datos 
                conexion = new SqlConnection("Data Source=LAPTOP-RVVL1UUP\\MSSQLSERVER01;Initial Catalog=[DBFACTURAS];Integrated Security=True");
                conexion.Open(); //Invoca método para abrir la base de datos
            }
            catch (Exception ex) //Retorna mensaje en caso de error
            {
                resultado = "ERROR: No se estableció la conexión con la base de datos " + ex;
            }
            return resultado;
        }

        public string CerrarBd() //Método para cerrar la base de datos
        {
            string resultado = "";
            try //Captura error en caso que se presente
            {
                conexion.Close(); //Incoca método para cerrar la base de datos
            }
            catch (Exception ex)
            {
                resultado = "ERROR: Fálo al cerrar la conexión " + ex;
            }
            return resultado;
        }

        // Ejecuta procesos almacenados en la base de datos, los parámetros pasan por medio de una lista (lst)
        public string Ejecutar_procedimiento(string procedimiento, List<Cls_parametros> lst)
        {
            string salida = "";

            try
            {
                int retornado;

                AbrirBd();

                SqlCommand Comando = new SqlCommand(procedimiento, conexion);
                Comando.CommandType = CommandType.StoredProcedure;

                if (lst != null)
                {
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (lst[i].DireccionParametro == ParameterDirection.Input)
                        {
                            Comando.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                        }

                        if (lst[i].DireccionParametro == ParameterDirection.Output)
                        {
                            Comando.Parameters.Add(lst[i].Nombre, lst[i].TipoDato, lst[i].Tamaño).Direction = ParameterDirection.Output;
                        }
                    }
                }

                retornado = Comando.ExecuteNonQuery();
                CerrarBd();

                if (retornado > 0)
                {
                    salida = "Los datos fueron Actualizados";
                }
                else
                {
                    salida = "Los datos no fueron Actualizados";
                }

            }
            catch (Exception ex)
            {
                salida = "ERROR: Falló operación: " + ex.Message;
            }
            return salida;
        }

        public string EjecutarComando(string sentencia)
        {
            string salida = "";
            try
            {
                int retornado;
                AbrirBd();
                cmd = new SqlCommand(sentencia, conexion);
                retornado = cmd.ExecuteNonQuery();
                CerrarBd();
                if (retornado > 0)
                {
                    salida = "Los datos fueron actualizados";
                }
                else
                {
                    salida = "Los datos no fueron actualizados";
                }
            }
            catch (Exception ex)
            {
                salida = "ERROR: Falló la operación: " + ex;
            }
            return salida;
        }

        public DataTable EjecutarConsulta(string cmd)
        {
            try
            {
                AbrirBd();
                da = new SqlDataAdapter(cmd, conexion);
                dt = new DataTable();
                da.Fill(dt);
                CerrarBd();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
