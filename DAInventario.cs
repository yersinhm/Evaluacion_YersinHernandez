using BE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class DAInventario
    {
        public List<BEInventario> ListaInventario(string fechaIni, string fechaFin, string tipoDoc, string nroDoc) {
            
            List<BEInventario> beInventarioLista
                = new List<BEInventario>();

            string conectionS = ConfigurationManager.ConnectionStrings["MiConexionSqlServer"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(conectionS))
            {
                using (SqlCommand command = new SqlCommand("LISTA_INVENTARIO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DFECHAINI", fechaIni);
                    command.Parameters.AddWithValue("@DFECHAFIN", fechaFin);
                    command.Parameters.AddWithValue("@VTIPOMOVIMIENTO", tipoDoc);
                    command.Parameters.AddWithValue("@VNRODOCUMENTO", nroDoc);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BEInventario item = new BEInventario
                                {
                                    COD_CIA = reader["COD_CIA"].ToString(),
                                    COMPANIA_VENTA_3 = reader["COMPANIA_VENTA_3"].ToString(),
                                    ALAMCEN_VENTA = reader["ALMACEN_VENTA"].ToString(),
                                    TIPO_MOVIMIENTO = reader["TIPO_MOVIMIENTO"].ToString(),
                                    TIPO_DOCUMENTO = reader["TIPO_DOCUMENTO"].ToString(),
                                    NRO_DOCUMENTO = reader["NRO_DOCUMENTO"].ToString(),
                                    COD_ITEM_2 = reader["COD_ITEM_2"].ToString(),
                                    PROVEEDOR = reader["PROVEEDOR"].ToString(),
                                    ALMACEN_DESTINO = reader["ALMACEN_DESTINO"].ToString(),
                                    CANTIDAD = reader.GetInt32(reader.GetOrdinal("CANTIDAD"))
                                };
                                beInventarioLista.Add(item);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            return beInventarioLista;
        }
    }
}
