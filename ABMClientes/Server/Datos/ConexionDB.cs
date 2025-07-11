﻿using ABMClientes.Server.Datos;
using ABMClientes.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace CineAPI.Datos.ADO.NET
{
    public class ConexionDB : IConexion
    {
        private readonly ConsultasSQL consultasSQL;
        private readonly ILogger logger;

        public ConexionDB( ConsultasSQL consultasSQL, ILogger<ConexionDB> logger) 
        {
            this.consultasSQL = consultasSQL;
            this.logger = logger;
        }


        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            string query = @"select * from CLIENTES";

            DataTable dt = await consultasSQL.Search(query);

            return dt.AsEnumerable().Select(row => new Cliente
            {
                CLIENTE_ID = row.Field<int>("CLIENTE_ID"),
                CLIENTE_NOMBRES = row.Field<string>("CLIENTE_NOMBRES"),
                CLIENTE_APELLIDOS = row.Field<string>("CLIENTE_APELLIDOS"),
                CLIENTE_FECNAC = row.Field<DateTime>("CLIENTE_FECNAC"),
                CLIENTE_CUIT = row.Field<string>("CLIENTE_CUIT"),
                CLIENTE_DOMICILIO = row.Field<string>("CLIENTE_DOMICILIO"),
                CLIENTE_TELEFONO = row.Field<string>("CLIENTE_TELEFONO"),
                CLIENTE_EMAIL = row.Field<string>("CLIENTE_EMAIL")
            });
        }

        public async Task<Cliente> GetCliente(int id)
        {
            consultasSQL.AddParameter("id", DbType.String, id);

            string query = @"select * from CLIENTES where CLIENTE_ID = @id";

            DataTable dt = await consultasSQL.Search(query);

            DataRow row = dt.Rows[0];

            return new Cliente
            {
                CLIENTE_ID = row.Field<int>("CLIENTE_ID"),
                CLIENTE_NOMBRES = row.Field<string>("CLIENTE_NOMBRES"),
                CLIENTE_APELLIDOS = row.Field<string>("CLIENTE_APELLIDOS"),
                CLIENTE_FECNAC = row.Field<DateTime>("CLIENTE_FECNAC"),
                CLIENTE_CUIT = row.Field<string>("CLIENTE_CUIT"),
                CLIENTE_DOMICILIO = row.Field<string>("CLIENTE_DOMICILIO"),
                CLIENTE_TELEFONO = row.Field<string>("CLIENTE_TELEFONO"),
                CLIENTE_EMAIL = row.Field<string>("CLIENTE_EMAIL")
            };
        }

        public async Task<IEnumerable<Cliente>> GetClientesId(int id)
        {
            consultasSQL.AddParameter("id", DbType.String, id);

            string query = @"select * from CLIENTES where CLIENTE_ID = @id";

            DataTable dt = await consultasSQL.Search(query);

            return dt.AsEnumerable().Select(row => new Cliente
            {
                CLIENTE_ID = row.Field<int>("CLIENTE_ID"),
                CLIENTE_NOMBRES = row.Field<string>("CLIENTE_NOMBRES"),
                CLIENTE_APELLIDOS = row.Field<string>("CLIENTE_APELLIDOS"),
                CLIENTE_FECNAC = row.Field<DateTime>("CLIENTE_FECNAC"),
                CLIENTE_CUIT = row.Field<string>("CLIENTE_CUIT"),
                CLIENTE_DOMICILIO = row.Field<string>("CLIENTE_DOMICILIO"),
                CLIENTE_TELEFONO = row.Field<string>("CLIENTE_TELEFONO"),
                CLIENTE_EMAIL = row.Field<string>("CLIENTE_EMAIL")
            });
        }

        public async Task<IEnumerable<Cliente>> GetClienteConFiltro(ClienteFiltro clienteFiltro)
        {
            try
            {
                consultasSQL.AddParameter("nombre", DbType.String, clienteFiltro.CLIENTE_NOMBRES);
                consultasSQL.AddParameter("apellido", DbType.String, clienteFiltro.CLIENTE_APELLIDOS);
                consultasSQL.AddParameter("cuit", DbType.String, clienteFiltro.CLIENTE_CUIT);

                string query = @"select * from CLIENTES where 1=1";

                if (!string.IsNullOrEmpty(clienteFiltro.CLIENTE_NOMBRES))
                {
                    query = query + " and CLIENTE_NOMBRES like '%'+@nombre+'%'";
                }

                if (!string.IsNullOrEmpty(clienteFiltro.CLIENTE_APELLIDOS))
                {
                    query = query + " and CLIENTE_APELLIDOS like '%'+@apellido+'%'";
                }

                if (!string.IsNullOrEmpty(clienteFiltro.CLIENTE_CUIT))
                {
                    query = query + " and CLIENTE_CUIT like '%'+@cuit+'%'";
                }

                DataTable dt = await consultasSQL.SearchWithParameters(query);

                return dt.AsEnumerable().Select(row => new Cliente
                {
                    CLIENTE_ID = row.Field<int>("CLIENTE_ID"),
                    CLIENTE_NOMBRES = row.Field<string>("CLIENTE_NOMBRES"),
                    CLIENTE_APELLIDOS = row.Field<string>("CLIENTE_APELLIDOS"),
                    CLIENTE_FECNAC = row.Field<DateTime>("CLIENTE_FECNAC"),
                    CLIENTE_CUIT = row.Field<string>("CLIENTE_CUIT"),
                    CLIENTE_DOMICILIO = row.Field<string>("CLIENTE_DOMICILIO"),
                    CLIENTE_TELEFONO = row.Field<string>("CLIENTE_TELEFONO"),
                    CLIENTE_EMAIL = row.Field<string>("CLIENTE_EMAIL")
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task EliminarCliente(int id)
        {
            consultasSQL.AddParameter("id",DbType.Int32, id);

            string query = @"delete from CLIENTES where CLIENTE_ID = @id";

            await consultasSQL.ExecuteQueryWithParameters(query); 
        }

        public async Task CrearCliente(Cliente cliente)
        {
            try
            {
                consultasSQL.AddParameter("nombres", DbType.String, cliente.CLIENTE_NOMBRES);
                consultasSQL.AddParameter("apellidos", DbType.String, cliente.CLIENTE_APELLIDOS);
                consultasSQL.AddParameter("nacimiento", DbType.DateTime, cliente.CLIENTE_FECNAC);
                consultasSQL.AddParameter("cuit", DbType.String, cliente.CLIENTE_CUIT);
                consultasSQL.AddParameter("domicilio", DbType.String, cliente.CLIENTE_DOMICILIO);
                consultasSQL.AddParameter("telefono", DbType.String, cliente.CLIENTE_TELEFONO);
                consultasSQL.AddParameter("email", DbType.String, cliente.CLIENTE_EMAIL);

                string query = @"insert into CLIENTES
                                         (
                                            CLIENTE_NOMBRES,
                                            CLIENTE_APELLIDOS,
                                            CLIENTE_FECNAC,
                                            CLIENTE_CUIT,
                                            CLIENTE_DOMICILIO,
                                            CLIENTE_TELEFONO,
                                            CLIENTE_EMAIL
                                         )
                                  Values(
                                            @nombres,
                                            @apellidos,
                                            @nacimiento,
                                            @cuit,
                                            @domicilio,
                                            @telefono,
                                            @email
                                        )";

                await consultasSQL.ExecuteQueryWithParameters(query);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task EditarCliente(Cliente cliente)
        {
            try
            {
                consultasSQL.AddParameter("id",DbType.String, cliente.CLIENTE_ID);  
                consultasSQL.AddParameter("nombres", DbType.String, cliente.CLIENTE_NOMBRES);
                consultasSQL.AddParameter("apellidos", DbType.String, cliente.CLIENTE_APELLIDOS);
                consultasSQL.AddParameter("nacimiento", DbType.DateTime, cliente.CLIENTE_FECNAC);
                consultasSQL.AddParameter("cuit", DbType.String, cliente.CLIENTE_CUIT);
                consultasSQL.AddParameter("domicilio", DbType.String, cliente.CLIENTE_DOMICILIO);
                consultasSQL.AddParameter("telefono", DbType.String, cliente.CLIENTE_TELEFONO);
                consultasSQL.AddParameter("email", DbType.String, cliente.CLIENTE_EMAIL);

                string query = @"update CLIENTES
                                        set                                        
                                        CLIENTE_NOMBRES = @nombres,
                                        CLIENTE_APELLIDOS = @apellidos,
                                        CLIENTE_FECNAC = @nacimiento,
                                        CLIENTE_CUIT = @cuit,
                                        CLIENTE_DOMICILIO = @domicilio,
                                        CLIENTE_TELEFONO = @telefono,
                                        CLIENTE_EMAIL = @email
                                        where CLIENTE_ID = @id";

                await consultasSQL.ExecuteQueryWithParameters(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<bool> VerificarCuit(string cuit)
        {
            consultasSQL.AddParameter("cuit",DbType.String, cuit);

            string query = @"select 1 from CLIENTES where CLIENTE_CUIT = @cuit";

            DataTable dt = await consultasSQL.SearchWithParameters(query);

            return dt.Rows.Count > 0;
        }

        public async Task PostError(LogErrores logErrores)
        {
            consultasSQL.AddParameter("fecha", DbType.DateTime, logErrores.LOG_FECHA);
            consultasSQL.AddParameter("ruta", DbType.String, logErrores.LOG_RUTA);
            consultasSQL.AddParameter("msg", DbType.String, logErrores.LOG_MSG);

            string query = @"insert into LOG_ERRORES
                                          (
                                            LOG_FECHA,
                                            LOG_RUTA,
                                            LOG_MSG
                                          )
                                        Values
                                          (
                                            @fecha,
                                            @ruta,
                                            @msg
                                          )";

            await consultasSQL.ExecuteQueryWithParameters(query);
        }

        public async Task<bool> VerificarCuitClienteExistente(Cliente cliente)
        {
            consultasSQL.AddParameter("cuit", DbType.String, cliente.CLIENTE_CUIT);
            consultasSQL.AddParameter("id", DbType.Int32, cliente.CLIENTE_ID);

            string query = @"select 1 from CLIENTES where CLIENTE_CUIT = @cuit and CLIENTE_ID <> @id";

            DataTable dt = await consultasSQL.SearchWithParameters(query);

            return dt.Rows.Count > 0;
        }

        
    }
}
