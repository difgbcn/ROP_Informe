using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ROP_Informe
{
    public static class funcionalidades
    {

        public static bool validarUsuarioEjecucion(string usuario, string concepto, string empresa, string numero, string usuarioAXAPTA)
        {
            string strConexion;
            SqlConnection conexion;
            SqlCommand comando;

            try
            {
                bool enviar = false;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionUsuarioConsulta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
                parametroUsuario.Value = usuario;
                comando.Parameters.Add(parametroUsuario);
                if (System.Web.HttpContext.Current.Session["tipoEnviado"].ToString() != "")
                    comando.Parameters.AddWithValue("@Acceso", "AXAPTA");
                else
                    comando.Parameters.AddWithValue("@Acceso", "WEB");
                comando.Parameters.AddWithValue("@Entorno", Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["TipoVersion"]).ToUpper());
                comando.Parameters.AddWithValue("@TipoCalculo", concepto);
                comando.Parameters.AddWithValue("@Empresa", empresa);
                comando.Parameters.AddWithValue("@Numero", numero);
                comando.Parameters.AddWithValue("@incluir", 1);
                comando.Parameters.AddWithValue("@usuarioAXAPTA", usuarioAXAPTA);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows)
                    enviar = true;
                else
                    enviar = false;
                dr.Close();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                return enviar;
            }
            catch 
            {
                return false;
            }
        }

        public static bool validarMostrarTablas(string usuario)
        {
            string strConexion;
            SqlConnection conexion;
            SqlCommand comando;

            try
            {
                bool enviar = false;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionUsuarioConsulta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Clear();
                 SqlParameter parametroUsuario = new SqlParameter("@usuario", SqlDbType.NVarChar, 100);
                parametroUsuario.Value = usuario;
                comando.Parameters.Add(parametroUsuario);
                comando.Parameters.AddWithValue("@incluir", 0);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    enviar = dr.GetBoolean(9);
                }
                else
                    enviar = false;
                dr.Close();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                return enviar;
            }
            catch
            {
                return false;
            }
        }
        
        public static string  recuperarVersionSoftware()
        {
            string version = "";
            string strConexion;

            strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
            SqlConnection conexion;
            SqlDataAdapter adaptador;
            System.Data.DataTable dtTabla = new System.Data.DataTable();
            SqlCommand comando;

            conexion = new SqlConnection(strConexion);
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "sp_ROP_ConfiguracionVersionSoftwareConsulta";
            comando.CommandTimeout = 60;
            comando.CommandType = CommandType.StoredProcedure;
            dtTabla = new System.Data.DataTable();
            adaptador = new SqlDataAdapter(comando);
            adaptador.Fill(dtTabla);
            adaptador.Dispose();
            comando.Dispose();
            conexion.Close();
            conexion.Dispose();
            if (dtTabla.Rows.Count > 0)
                version = dtTabla.Rows[0]["Version"].ToString();
            return version;
        }

        public static string  recuperarNumeroHilos()
        {
            string hilos = "0";
            string strConexion;

            strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
            SqlConnection conexion;
            SqlDataAdapter adaptador;
            System.Data.DataTable dtTabla = new System.Data.DataTable();
            SqlCommand comando;

            conexion = new SqlConnection(strConexion);
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "sp_ROP_ConfiguracionTecnicoConsulta";
            comando.CommandTimeout = 60;
            comando.CommandType = CommandType.StoredProcedure;
            dtTabla = new System.Data.DataTable();
            adaptador = new SqlDataAdapter(comando);
            adaptador.Fill(dtTabla);
            adaptador.Dispose();
            comando.Dispose();
            conexion.Close();
            conexion.Dispose();
            if (dtTabla.Rows.Count > 0)
                hilos = dtTabla.Rows[0]["Hilos"].ToString();
            return hilos;
        }

        public static void calcularCodigosTransporte(ref DataTable dtTiempos, ref string codigosTransporte, string empresa, string delegacion, string cualPedido, string usuarioRed)
        {
            DataRow oItemTiempo;
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            string strConexion;

            try 
            { 
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                SqlConnection conexion;
                SqlDataAdapter adaptador;
                System.Data.DataTable dtTabla = new System.Data.DataTable();
                SqlCommand comando;
            
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionTransporteSubfamilia";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                if (delegacion != "")
                    comando.Parameters.AddWithValue("@delegacion", delegacion);
                else
                    comando.Parameters.AddWithValue("@delegacion", DBNull.Value);
                dtTabla = new System.Data.DataTable();
                adaptador = new SqlDataAdapter(comando);
                adaptador.Fill(dtTabla);
                adaptador.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                for (int i = 0; i < dtTabla.Rows.Count; i++)
                {
                    codigosTransporte = codigosTransporte + dtTabla.Rows[i]["ProductId"].ToString() + "|";
                }
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL sp_ROP_ConfiguracionTransporteSubfamilia " + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
            }
            catch //(Exception ex)
            {
                
            }
        }
     
        public static void fechaBaseFichaArticuloNUEVO(ref DataTable dtTiempos, ref List<string> listaOfertas, ref List<DateTime> listaFechasOferta, ref List<string> listaArticulosPedido, ref List<DateTime> listaFechasPedido, string empresa, string obraID, int diasDiferencia, int diasRestar, string WSDominio, string WSUsuario, string WSContrasena, string cualPedido, string usuarioRed)
        {
            bool hayDatosWS = false;
            bool datosSQL = false;
            bool datosIniciales = false;
            string pedidos = "";
            string pedido = "";
            //int lineaCapitulo;
            int numeroArticulos = 0;
            DateTime fechaArticulo;
            DateTime fechaOfertaPedido;
            SqlDataReader dr;
            SqlConnection conexion;
            string strConexion;
            SqlCommand comando;
            List<string> listaPedidos = new List<string>();

            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            DataRow oItemTiempo;
          
            try
            {
                fechaArticulo = new DateTime();

                //// Buscar la fecha base pre-calculada en el SQL
                //hora_1 = DateTime.Now;
                //strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                //conexion = new SqlConnection(strConexion);
                //conexion.Open();
                //comando = conexion.CreateCommand();
                //comando.CommandText = "FichasFechasBasesConsulta";
                //comando.CommandTimeout = 60;
                //comando.CommandType = CommandType.StoredProcedure;
                //comando.Parameters.AddWithValue("@empresa", empresa);
                //comando.Parameters.AddWithValue("@ficha", obraID);
                //dr = comando.ExecuteReader();
                //if (dr.HasRows)
                //{
                //    datosSQL = true;
                //    while (dr.Read())
                //    {
                //        numeroArticulos = listaArticulosPedido.FindIndex(x => x.Equals(dr["ItemId"].ToString()));
                //        if (numeroArticulos >= 0)
                //            listaFechasPedido[numeroArticulos] = Convert.ToDateTime(dr["FechaBase"].ToString());
                //    }
                //}
                //conexion.Close();
                //hora_2 = DateTime.Now.Subtract(hora_1);
                //oItemTiempo = dtTiempos.NewRow();
                //oItemTiempo[0] = "TIEMPO llamar SQL FichasFechasBasesConsulta (precalculado): " + obraID;
                //oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                //dtTiempos.Rows.Add(oItemTiempo);

                if (!datosSQL)
                {
                    hora_1 = DateTime.Now;

                    WSGeneral.CallContext contexto_Pedido = new WSGeneral.CallContext();
                    contexto_Pedido.Company = empresa.ToString().Trim();

                    WSGeneral.QueryCriteria criterio_Pedido = new WSGeneral.QueryCriteria();
                    criterio_Pedido.CriteriaElement = new WSGeneral.CriteriaElement[1];
                    criterio_Pedido.CriteriaElement[0] = new WSGeneral.CriteriaElement();
                    criterio_Pedido.CriteriaElement[0].DataSourceName = "SalesTable";
                    criterio_Pedido.CriteriaElement[0].FieldName = "OBRAID";
                    criterio_Pedido.CriteriaElement[0].Operator = WSGeneral.Operator.Equal;
                    criterio_Pedido.CriteriaElement[0].Value1 = obraID.ToString().Trim();
       
                    WSGeneral.WSROPServiceClient proxy_Pedido = new WSGeneral.WSROPServiceClient();
                    proxy_Pedido.ClientCredentials.Windows.ClientCredential.Domain = WSDominio;
                    proxy_Pedido.ClientCredentials.Windows.ClientCredential.UserName = WSUsuario;
                    proxy_Pedido.ClientCredentials.Windows.ClientCredential.Password = WSContrasena;

                    WSGeneral.WSSalesTableContract[] axdtablaPedidos = proxy_Pedido.getSalesOrders(contexto_Pedido, criterio_Pedido);

                    IEnumerable<WSGeneral.WSSalesTableContract> enumerable = axdtablaPedidos;
                    IEnumerator<WSGeneral.WSSalesTableContract> enumerator = enumerable.GetEnumerator();

                    hora_2 = DateTime.Now.Subtract(hora_1);
                    oItemTiempo = dtTiempos.NewRow();
                    oItemTiempo[0] = "TIEMPO llamar al ws Pedidos con obra: " + obraID + cualPedido;
                    oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                    guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                    dtTiempos.Rows.Add(oItemTiempo);
                    proxy_Pedido.Close();
                    while (enumerator.MoveNext())
                    {
                        hayDatosWS = true;
                        WSGeneral.WSSalesTableContract enumerator_Pedido = enumerator.Current;

                        if (enumerator_Pedido.parmSalesStatus.ToString().ToUpper() != WSGeneral.SalesStatus.Canceled.ToString().ToUpper())
                        {
                            if (!listaPedidos.Contains(enumerator_Pedido.parmSalesId.ToString()) && !String.IsNullOrEmpty(enumerator_Pedido.parmQuotationId))
                            {
                                listaPedidos.Add(enumerator_Pedido.parmSalesId.ToString());
                                fechaOfertaPedido = new DateTime();
                                funcionalidades.fechaOfertaNUEVO(ref dtTiempos, ref fechaOfertaPedido, empresa, enumerator_Pedido.parmQuotationId.ToString(), WSDominio, WSUsuario, WSContrasena, cualPedido, usuarioRed);
                                listaOfertas.Add(enumerator_Pedido.parmQuotationId.ToString());
                                listaFechasOferta.Add(fechaOfertaPedido);
                                //datosIniciales = true;
                            }

                            pedidos = pedidos + " - " + enumerator_Pedido.parmSalesId.ToString();
                            pedido = enumerator_Pedido.parmSalesId.ToString();

                            IEnumerable<WSGeneral.WSSalesLineContract> enumerableLineas = enumerator_Pedido.parmSalesLines;
                            IEnumerator<WSGeneral.WSSalesLineContract> enumeratorLineas = enumerableLineas.GetEnumerator();

                            while (enumeratorLineas.MoveNext())
                            {
                                WSGeneral.WSSalesLineContract enumerator_PedidoLineas = enumeratorLineas.Current;

                                fechaOfertaPedido = new DateTime();

                                if (!String.IsNullOrEmpty(enumerator_Pedido.parmQuotationId))
                                {
                                    if (!listaOfertas.Contains(enumerator_Pedido.parmQuotationId.ToString()))
                                    {
                                        // no hay oferta
                                    }
                                    else
                                    {
                                        numeroArticulos = listaOfertas.FindIndex(x => x.Equals(enumerator_Pedido.parmQuotationId.ToString()));
                                        if (numeroArticulos >= 0)
                                            fechaOfertaPedido = listaFechasOferta[numeroArticulos];
                                    }
                                }

                                if (!listaArticulosPedido.Contains(enumerator_PedidoLineas.parmItemId.ToString()))
                                {
                                    listaArticulosPedido.Add(enumerator_PedidoLineas.parmItemId.ToString());
                                    listaFechasPedido.Add(DateTime.Today);
                                }

                                if (listaArticulosPedido.Contains(enumerator_PedidoLineas.parmItemId.ToString()))
                                {
                                    // calcular la fecha para esta artículo
                                    fechaArticulo = new DateTime();

                                    if (!String.IsNullOrEmpty(enumerator_Pedido.parmQuotationId))
                                    {
                                        //if (fechaOfertaPedido != new DateTime() && enumerator_Pedido.parmCreatedDateTime.ToString().Length > 0)
                                        //{
                                        //    if (Math.Abs(Convert.ToDateTime(enumerator_Pedido.parmCreatedDateTime).Subtract(fechaOfertaPedido).Days) > diasDiferencia)
                                        //    {
                                        //        fechaArticulo = Convert.ToDateTime(enumerator_Pedido.parmCreatedDateTime).AddDays((-1) * diasRestar);
                                        //    }
                                        //    else
                                        //    {
                                        //        fechaArticulo = fechaOfertaPedido;
                                        //    }
                                        //}
                                        // correo Nacho 29/04/2025
                                        if (fechaOfertaPedido != new DateTime() && enumerator_PedidoLineas.parmCreatedDateTime.ToString().Length > 0)
                                        {
                                            if (Math.Abs(Convert.ToDateTime(enumerator_PedidoLineas.parmCreatedDateTime).Subtract(fechaOfertaPedido).Days) > diasDiferencia)
                                            {
                                                fechaArticulo = Convert.ToDateTime(enumerator_PedidoLineas.parmCreatedDateTime).AddDays((-1) * diasRestar);
                                            }
                                            else
                                            {
                                                fechaArticulo = fechaOfertaPedido;
                                            }
                                        }
                                        else
                                        {
                                            if (enumerator_PedidoLineas.parmCreatedDateTime.ToString().Length > 0)
                                            {
                                                fechaArticulo = Convert.ToDateTime(enumerator_PedidoLineas.parmCreatedDateTime);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //fechaArticulo = Convert.ToDateTime(enumerator_Pedido.parmCreatedDateTime);
                                        fechaArticulo = Convert.ToDateTime(enumerator_PedidoLineas.parmCreatedDateTime);
                                    }

                                    if (fechaArticulo != new DateTime())
                                    {
                                        numeroArticulos = listaArticulosPedido.FindIndex(x => x.Equals(enumerator_PedidoLineas.parmItemId.ToString()));
                                        if (numeroArticulos >= 0)
                                            if (listaFechasPedido.ElementAt(numeroArticulos) > fechaArticulo)
                                                listaFechasPedido[numeroArticulos] = fechaArticulo;
                                    }
                                }
                            }
                        }
                    }
                  

                   if (!hayDatosWS)
                    {
                        hora_1 = DateTime.Now;

                        strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                        conexion = new SqlConnection(strConexion);
                        conexion.Open();
                        comando = conexion.CreateCommand();
                        comando.CommandText = "ROP_FechaBaseConsulta";
                        comando.CommandTimeout = 60;
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@empresa", empresa);
                        comando.Parameters.AddWithValue("@obra", obraID);
                        dr = comando.ExecuteReader();
                        hora_2 = DateTime.Now.Subtract(hora_1);
                        oItemTiempo = dtTiempos.NewRow();
                        oItemTiempo[0] = "TIEMPO llamar SQL ROP_FechaBaseConsulta porque no recuperó datos del WS de pedidos para la obra: " + obraID + cualPedido;
                        oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                        guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                        dtTiempos.Rows.Add(oItemTiempo);

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                fechaOfertaPedido = new DateTime();
                                // fecha oferta
                                if (!datosIniciales && !String.IsNullOrEmpty(dr["QuotationDate"].ToString()))
                                {
                                    listaOfertas.Add(obraID);
                                    listaFechasOferta.Add(Convert.ToDateTime(dr["QuotationDate"].ToString()));
                                    datosIniciales = true;
                                }

                                // fecha pedido
                                if (listaArticulosPedido.Contains(dr["ProductId"].ToString()))
                                {
                                    // calcular la fecha para esta artículo
                                    fechaArticulo = new DateTime();
                                    if (!String.IsNullOrEmpty(dr["QuotationDate"].ToString()))
                                    {
                                        if (fechaOfertaPedido != new DateTime() && !String.IsNullOrEmpty(dr["SalesOrderDate"].ToString()))
                                        {
                                            if (Math.Abs(Convert.ToDateTime(dr["SalesOrderDate"].ToString()).Subtract(fechaOfertaPedido).Days) > diasDiferencia)
                                            {
                                                fechaArticulo = Convert.ToDateTime(dr["SalesOrderDate"].ToString()).AddDays((-1) * diasRestar);
                                            }
                                            else
                                            {
                                                fechaArticulo = fechaOfertaPedido;
                                            }
                                        }
                                        else
                                        {
                                            if (!String.IsNullOrEmpty(dr["SalesOrderDate"].ToString()))
                                            {
                                                fechaArticulo = Convert.ToDateTime(dr["SalesOrderDate"].ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fechaArticulo = Convert.ToDateTime(dr["SalesOrderDate"].ToString());
                                    }

                                    if (fechaArticulo != new DateTime())
                                    {
                                        numeroArticulos = listaArticulosPedido.FindIndex(x => x.Equals(dr["ProductId"].ToString()));
                                        if (numeroArticulos >= 0)
                                            if (listaFechasPedido.ElementAt(numeroArticulos) > Convert.ToDateTime(dr["SalesOrderDate"].ToString()))
                                                listaFechasPedido[numeroArticulos] = Convert.ToDateTime(dr["SalesOrderDate"].ToString());
                                    }
                                }
                            }
                        }
                        dr.Close();
                        comando.Dispose();
                        conexion.Close();
                        conexion.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                string incidencia = ex.Message;
            }
        }

        public static void fechaOfertaNUEVO(ref DataTable dtTiempos, ref DateTime fechaPrecios, string empresa, string idOferta, string WSDominio, string WSUsuario, string WSContrasena, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                WSGeneral.CallContext contexto_Oferta = new WSGeneral.CallContext();
                contexto_Oferta.Company = empresa.ToString();

                WSGeneral.QueryCriteria criterio_Oferta = new WSGeneral.QueryCriteria();
                criterio_Oferta.CriteriaElement = new WSGeneral.CriteriaElement[1];
                criterio_Oferta.CriteriaElement[0] = new WSGeneral.CriteriaElement();
                criterio_Oferta.CriteriaElement[0].FieldName = "QuotationId";
                criterio_Oferta.CriteriaElement[0].DataSourceName = "SalesQuotationMasterTable";
                criterio_Oferta.CriteriaElement[0].Operator = WSGeneral.Operator.Equal;
                criterio_Oferta.CriteriaElement[0].Value1 = idOferta;

                WSGeneral.WSROPServiceClient proxy_Oferta = new WSGeneral.WSROPServiceClient();
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.Domain = WSDominio;
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.UserName = WSUsuario;
                proxy_Oferta.ClientCredentials.Windows.ClientCredential.Password = WSContrasena;

                WSGeneral.WSSalesQuotationMasterTableContract[] axdtablaOfertas = proxy_Oferta.getQuotations(contexto_Oferta, criterio_Oferta);

                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al ws Oferta con la oferta: " + idOferta.ToString() + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);

                IEnumerable<WSGeneral.WSSalesQuotationMasterTableContract> enumerableOferta = axdtablaOfertas;
                IEnumerator<WSGeneral.WSSalesQuotationMasterTableContract> enumeratorOferta = enumerableOferta.GetEnumerator();
                proxy_Oferta.Close();
                while (enumeratorOferta.MoveNext())
                {
                    WSGeneral.WSSalesQuotationMasterTableContract enumerator_MasterOferta = enumeratorOferta.Current;
                 
                    if (enumerator_MasterOferta.parmCreatedDateTime.ToString().Length > 0)
                        fechaPrecios = Convert.ToDateTime(enumerator_MasterOferta.parmCreatedDateTime);
                    break;
                }
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "fechaOferta // " + ex.Message;
            }
        }


        public static DataTable localizarPreciosArticulos(ref DataTable dtTiempos, string empresa, string delegacion, string articulos, string moneda, string articulosCambio, string ficha, string usuario, string version, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosLPA = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            SqlCommand comando;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_PreciosArticulos";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                if (delegacion == "")
                    comando.Parameters.AddWithValue("@delegacion", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@delegacion", delegacion);
                comando.Parameters.AddWithValue("@articulos", articulos);
                comando.Parameters.AddWithValue("@moneda", moneda);
                comando.Parameters.AddWithValue("@articulosCambio", articulosCambio);
                if (ficha == "")
                    comando.Parameters.AddWithValue("@ficha", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@ficha", ficha);
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@usuario", usuario);
                adaptadorArticulos = new SqlDataAdapter(comando);
                adaptadorArticulos.Fill(dtArticulosLPA);
                adaptadorArticulos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_PreciosArticulos" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtArticulosLPA;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR localizar precios articulos // " + ex.Message + " // " + articulos;
                return dtArticulosLPA;
            }
        }

        public static DataTable localizarPreciosVentaArticulos(ref DataTable dtTiempos, string empresa, string articulos, string moneda, string articulosCambio, string ficha, string usuario, string version, string grupoPrecio, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            string tipo = "";
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosLPA = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionNivel4Consulta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    tipo = Convert.ToString(dr["CFGNVL4_Valor"]);
                }
                dr.Close();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();

                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_PreciosVentaArticulos";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@articulos", articulos);
                comando.Parameters.AddWithValue("@moneda", moneda);
                comando.Parameters.AddWithValue("@articulosCambio", articulosCambio);
                if (ficha == "")
                    comando.Parameters.AddWithValue("@ficha", DBNull.Value);
                else
                    comando.Parameters.AddWithValue("@ficha", ficha);
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@grupoPrecio", grupoPrecio);
                adaptadorArticulos = new SqlDataAdapter(comando);
                adaptadorArticulos.Fill(dtArticulosLPA);
                adaptadorArticulos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_PreciosVentaArticulos" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtArticulosLPA;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR localizar precios articulos // " + ex.Message + " // " + articulos;
                return dtArticulosLPA;
            }
        }


        public static DataTable gastosPatio(ref DataTable dtTiempos, string empresa, string articulos, DateTime fecha, string moneda, string usuario, string cualPedido, string usuarioRed, string delegacion, string version)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosLPA = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_GastosPatio";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@articulos", articulos);
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@monedaFicha", moneda);
                comando.Parameters.AddWithValue("@usuario", usuario);
                if (delegacion == "")
                    comando.Parameters.AddWithValue("@delegacion", null);
                else
                    comando.Parameters.AddWithValue("@delegacion", delegacion);
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                adaptadorArticulos = new SqlDataAdapter(comando);
                adaptadorArticulos.Fill(dtArticulosLPA);
                adaptadorArticulos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_GastosPatio" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtArticulosLPA;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos patio // " + ex.Message + " // " + articulos;
                return dtArticulosLPA;
            }
        }

        public static DataTable efectividades(ref DataTable dtTiempos, string fechas, DateTime fecha, string usuarioRed, string almacen, string version)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosLPA = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_EfectividadEstimadas";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@fechas", fechas);
                 if (almacen == "")
                    comando.Parameters.AddWithValue("@almacen", null);
                else
                    comando.Parameters.AddWithValue("@almacen", almacen);
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                adaptadorArticulos = new SqlDataAdapter(comando);
                adaptadorArticulos.Fill(dtArticulosLPA);
                adaptadorArticulos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_EfectividadEstimada";
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtArticulosLPA;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos patio // " + ex.Message + " // " + articulos;
                return dtArticulosLPA;
            }
        }

        public static DataTable ocupacion(ref DataTable dtTiempos, string version, string empresa, string delegacion, string articulos, string masterObra, bool esFicha, DateTime fecha, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosLPA = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();

                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_AjustesOcupacionProducto";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@articulos", articulos);
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@esFicha", esFicha);
                comando.Parameters.AddWithValue("@fecha_base", fecha);
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@delegacion", delegacion);
                adaptadorArticulos = new SqlDataAdapter(comando);
                //adaptadorArticulos.Fill(dtArticulosLPA);
                DataSet data = new DataSet();
                adaptadorArticulos.Fill(data, "TODO");
                adaptadorArticulos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_AjustesOcupacionProducto" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                //return dtArticulosLPA;
                Informe.dtTiempoOcupacion = data.Tables[1];
                return data.Tables[0];
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos patio // " + ex.Message + " // " + articulos;
                return dtArticulosLPA;
            }
        }

        public static DataTable gastosOficina(ref DataTable dtTiempos, string empresa, string masterObra, DateTime fecha, string moneda, int mesPresupuestoValido, decimal porcentajeGastoAdmministrativoVariable, decimal porcentajeGastoComercialVariable, string usuario, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorGastos;
            System.Data.DataTable dtGastosOficina = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_GastosOficina";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@fecha_base", fecha);
                comando.Parameters.AddWithValue("@currency", moneda);
                comando.Parameters.AddWithValue("@new_budget_month", mesPresupuestoValido);
                comando.Parameters.AddWithValue("@porcentajeGastoPersonalAdmministrativoVariable", porcentajeGastoAdmministrativoVariable);
                comando.Parameters.AddWithValue("@porcentajeGastoPersonalComercialVariable", porcentajeGastoComercialVariable);
                comando.Parameters.AddWithValue("@usuario", usuario);
                adaptadorGastos = new SqlDataAdapter(comando);
                adaptadorGastos.Fill(dtGastosOficina);
                adaptadorGastos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_GastosOficina" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtGastosOficina;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos oficina // " + ex.Message;
                return dtGastosOficina;
            }
        }

        public static DataTable gastosOficinaFicha(ref DataTable dtTiempos, string empresa, string masterObra, string obra, DateTime fecha, string moneda, int mesPresupuestoValido, decimal porcentajeGastoAdmministrativoVariable, decimal porcentajeGastoComercialVariable, string usuario, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorGastos;
            System.Data.DataTable dtGastosOficina = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_GastosOficinaFicha";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@ficha", obra);
                comando.Parameters.AddWithValue("@fecha_base", fecha);
                comando.Parameters.AddWithValue("@currency", moneda);
                comando.Parameters.AddWithValue("@new_budget_month", mesPresupuestoValido);
                comando.Parameters.AddWithValue("@porcentajeGastoPersonalAdmministrativoVariable", porcentajeGastoAdmministrativoVariable);
                comando.Parameters.AddWithValue("@porcentajeGastoPersonalComercialVariable", porcentajeGastoComercialVariable);
                comando.Parameters.AddWithValue("@usuario", usuario);
                adaptadorGastos = new SqlDataAdapter(comando);
                adaptadorGastos.Fill(dtGastosOficina);
                adaptadorGastos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_GastosOficinaFicha" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtGastosOficina;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos oficina ficha // " + ex.Message;
                return dtGastosOficina;
            }
        }

        public static void gastosOficinaEliminar(string usuario)
        {
            System.Data.DataTable dtGastosOficina = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;

            try
            {
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_GastosOficinaEliminar";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.ExecuteNonQuery();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos oficina ficha // " + ex.Message;
            }
        }

        public static DataTable gastosFijos(ref DataTable dtTiempos, string empresa, bool gastosFijosPorFacturacion, string masterObra, DateTime fecha, string moneda, int mesPresupuestoValido, decimal porcentajeGastoAdmministrativoVariable, decimal porcentajeGastoComercialVariable, decimal @coeficienteGastosFijosAlquilerVenta, string usuario, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorGastos;
            System.Data.DataTable dtGastosFijos = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_GastosFijos";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@fecha_base", fecha);
                comando.Parameters.AddWithValue("@currency", moneda);
                comando.Parameters.AddWithValue("@new_budget_month", mesPresupuestoValido);
                comando.Parameters.AddWithValue("@porcentajeGastoPersonalAdmministrativoVariable", porcentajeGastoAdmministrativoVariable);
                comando.Parameters.AddWithValue("@porcentajeGastoPersonalComercialVariable", porcentajeGastoComercialVariable);
                comando.Parameters.AddWithValue("@coeficienteGastosFijosAlquilerVenta", coeficienteGastosFijosAlquilerVenta);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@gastosFijosPorFacturacion", gastosFijosPorFacturacion);
                adaptadorGastos = new SqlDataAdapter(comando);
                adaptadorGastos.Fill(dtGastosFijos);
                adaptadorGastos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_GastosFijos" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtGastosFijos;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos fijos // " + ex.Message;
                return dtGastosFijos;
            }
        }

        public static DataTable tiposAlmacenes(ref DataTable dtTiempos, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorAlmacenes;
            System.Data.DataTable dtAlmacenes = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_AlmacenesConsulta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                adaptadorAlmacenes = new SqlDataAdapter(comando);
                adaptadorAlmacenes.Fill(dtAlmacenes);
                adaptadorAlmacenes.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_AlmacenesConsulta" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtAlmacenes;
            }
            catch //(Exception ex)
            {
                //    imgWarning.Visible = true;
                //    lblMensajeError.Visible = true;
                //    lblMensajeError.Text = "ERROR almacenes // " + ex.Message;
                return dtAlmacenes;
            }
        }

        public static decimal interpolacion(DataTable dtAjustesDuracionGF, int duracion)
        {
            int duracionInicial = 0;
            int duracionFinal = 0;
            decimal porcentajeInicial = 0;
            decimal porcentajeFinal = 0;
            decimal porcentaje = 0;

            try
            {
                for (int i = 0; i < dtAjustesDuracionGF.Rows.Count; i++)
                {
                    duracionFinal = Convert.ToInt32(dtAjustesDuracionGF.Rows[i]["DURACION"].ToString());
                    porcentajeFinal = Convert.ToDecimal(dtAjustesDuracionGF.Rows[i]["AJUSTE"].ToString());

                    if (duracion > duracionInicial && duracion < duracionFinal)
                    {
                        if ((duracionFinal - duracionInicial) != 0)
                        {
                            porcentaje = porcentajeInicial + ((Convert.ToDecimal(duracion - duracionInicial) / Convert.ToDecimal(duracionFinal - duracionInicial)) * (porcentajeFinal - porcentajeInicial));
                            break;
                        }
                    }
                    duracionInicial = duracionFinal;
                    porcentajeInicial = porcentajeFinal;
                }
                return porcentaje;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "interpolacion // " + ex.Message;
                return 0;
            }
        }

        public static DataTable localizarPaneles(ref DataTable dtTiempos, DateTime fecha, string version, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorPaneles;
            System.Data.DataTable dtPaneles = new System.Data.DataTable();
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                SqlConnection conexion;
                string strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionPanelesConsulta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@fecha", fecha);

                adaptadorPaneles = new SqlDataAdapter(comando);
                adaptadorPaneles.Fill(dtPaneles);
                adaptadorPaneles.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL sp_ROP_ConfiguracionPanelesConsulta" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtPaneles;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR localizar paneles // " + ex.Message;
                return dtPaneles;
            }
        }

        public static DataTable localizarEstadoArticulos(ref DataTable dtTiempos, string usuario, string articulos, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorArticulos;
            System.Data.DataTable dtArticulosEstado = new System.Data.DataTable();
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                SqlConnection conexion;
                string strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_EstadoArticulos";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@articulos", articulos);
                adaptadorArticulos = new SqlDataAdapter(comando);
                adaptadorArticulos.Fill(dtArticulosEstado);
                adaptadorArticulos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL sp_ROP_EstadoArticulos" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtArticulosEstado;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR localizar estado articulos // " + ex.Message;
                return dtArticulosEstado;
            }
        }

        public static bool noEsManual(string item)
        {
            string strConexion;
            SqlConnection conexion;
            bool esManual = false;
          
            try
            {
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ItemManual";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@ITEM", item);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows)
                {
                    esManual = true;
                }
                dr.Close();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                return !esManual;
            }
            catch //(Exception ex)
            {
                return !esManual;
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR localizar versión // " + ex.Message;
            }
        }

        public static void localizarVersion(ref DataTable dtTiempos, ref string version, int idVersion, DateTime fecha, string cualPedido, string usuarioRed, string idioma)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "sp_ROP_ConfiguracionUtilizar";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (idVersion == -1)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", idVersion);
                comando.Parameters.AddWithValue("@fecha", fecha);
                comando.Parameters.AddWithValue("@idioma", idioma);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    version = Convert.ToString(dr["CFG_Version"]);
                }
                dr.Close();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL sp_ROP_ConfiguracionUtilizar" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR localizar versión // " + ex.Message;
            }
        }

        public static void guardarIncidencia(string empresa, string tipo, string numero, string item, string incidencia, int nivel, string usuario, string usuarioRed)
        {
            try
            {
                string strConexion;
                SqlConnection conexion;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_IncidenciasAlta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@empresa", empresa);
                comando.Parameters.AddWithValue("@tipo", tipo);
                comando.Parameters.AddWithValue("@numero", numero);
                comando.Parameters.AddWithValue("@item", item);
                comando.Parameters.AddWithValue("@incidencia", incidencia);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@usuarioRed", usuarioRed);
                comando.Parameters.AddWithValue("@nivel", nivel);
                comando.ExecuteNonQuery();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static void guardarTiempo(string usuario, string descripcion, decimal tiempo)
        {
            try
            {
                string strConexion;
                SqlConnection conexion;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "temporalTIEMPOS_Alta";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                comando.Parameters.AddWithValue("@tiempo", tiempo);
                comando.ExecuteNonQuery();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public static void ajustesBU(ref DataTable dtTiempos, ref DataTable dtAjustesDatosBU, ref DataTable dtAjustesBU, string masterObra, DateTime fecha, string moneda, string articulos, string version, bool esFicha, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adatadorBU;
            string strConexion;
            SqlConnection conexion;
            DataSet dsUno = new DataSet();
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;

                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_AjustesBU";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@fecha_base", fecha);
                comando.Parameters.AddWithValue("@currency", moneda);
                comando.Parameters.AddWithValue("@articulos", articulos);
                //comando.Parameters.AddWithValue("@esFicha", esFicha);
                // 09/06/2025 permitimos que tantos para fichas como para pedidos y ofertas tome la fecha que le pasamos y no use la más pequeña de ellas
                comando.Parameters.AddWithValue("@esFicha", true);

                adatadorBU = new SqlDataAdapter(comando);
                adatadorBU.Fill(dsUno);
                adatadorBU.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();

                dtAjustesDatosBU = dsUno.Tables[0].Copy();
                dtAjustesBU = dsUno.Tables[1].Copy();

                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_AjustesBU" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
            }
            catch (Exception ex)
            {
                string ei = ex.Message;
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos fijos // " + ex.Message;
            }
        }


        public static DataTable topesAjustes(ref DataTable dtTiempos, string version, string masterObra, DateTime fecha, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adaptadorTopes;
            System.Data.DataTable dtTopes = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_AjustesTopes";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@fecha_base", fecha);
                adaptadorTopes = new SqlDataAdapter(comando);
                adaptadorTopes.Fill(dtTopes);
                adaptadorTopes.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_AjustesTopes" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtTopes;
            }
            catch //(Exception ex)
            {
                //imgWarning.Visible = true;
                //lblMensajeError.Visible = true;
                //lblMensajeError.Text = "ERROR gastos fijos // " + ex.Message;
                return dtTopes;
            }
        }

        public static DataTable fallidos(ref DataTable dtTiempos, string masterObra, DateTime fecha, string version, string cliente, string cualPedido, string usuarioRed)
        {
            DateTime hora_1 = default(DateTime);
            TimeSpan hora_2 = default(TimeSpan);
            SqlDataAdapter adatadorFallidos;
            System.Data.DataTable dtFallidos = new System.Data.DataTable();
            string strConexion;
            SqlConnection conexion;
            DataRow oItemTiempo;

            try
            {
                hora_1 = DateTime.Now;
                strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
                conexion = new SqlConnection(strConexion);
                conexion.Open();
                SqlCommand comando;
                
                comando = conexion.CreateCommand();
                comando.CommandText = "ROP_ConfiguracionFallidos";
                comando.CommandTimeout = 60;
                comando.CommandType = CommandType.StoredProcedure;
                if (version == "" || version is null)
                    comando.Parameters.AddWithValue("@CFG_ID", null);
                else
                    comando.Parameters.AddWithValue("@CFG_ID", Convert.ToInt32(version));
                comando.Parameters.AddWithValue("@masterObra", masterObra);
                comando.Parameters.AddWithValue("@cliente", cliente);
                comando.Parameters.AddWithValue("@fecha", fecha);

                adatadorFallidos = new SqlDataAdapter(comando);
                adatadorFallidos.Fill(dtFallidos);
                adatadorFallidos.Dispose();
                comando.Dispose();
                conexion.Close();
                conexion.Dispose();
                hora_2 = DateTime.Now.Subtract(hora_1);
                oItemTiempo = dtTiempos.NewRow();
                oItemTiempo[0] = "TIEMPO llamar al SQL ROP_ConfiguracionFallidos" + cualPedido;
                oItemTiempo[1] = ((Convert.ToDecimal(hora_2.Minutes) * 60) + Convert.ToDecimal(hora_2.Seconds) + (Convert.ToDecimal(hora_2.Milliseconds) / 1000)).ToString("#,##0.000000");
                guardarTiempo(usuarioRed, oItemTiempo[0].ToString(), Convert.ToDecimal(oItemTiempo[1].ToString()));
                dtTiempos.Rows.Add(oItemTiempo);
                return dtFallidos;
            }
            catch
            {
                return dtFallidos;
            }
        }

        public static string recuperarTexto(string cadena)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(cadena);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        public static string colocarTexto(string cadena)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static void incluirError(string error)
        {
            string strConexion;
            SqlConnection conexion;
            SqlCommand comando;

            strConexion = funcionalidades.recuperarTexto(ConfigurationManager.ConnectionStrings["SQL_ROP"].ToString());
            conexion = new SqlConnection(strConexion);
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "LogErroresROPAlta";
            comando.CommandTimeout = 60;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Usuario", (System.Security.Principal.WindowsIdentity.GetCurrent().Name).Replace(@"ALSINA\", ""));
            if (System.Web.HttpContext.Current.Session["tipoEnviado"].ToString() != "")
                comando.Parameters.AddWithValue("@Acceso", "AXAPTA");
            else
                comando.Parameters.AddWithValue("@Acceso", "WEB");
            comando.Parameters.AddWithValue("@Entorno", Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["TipoVersion"]).ToUpper());
            comando.Parameters.AddWithValue("@TipoCalculo", System.Web.HttpContext.Current.Session["Concepto"].ToString());
            comando.Parameters.AddWithValue("@Empresa", System.Web.HttpContext.Current.Session["Empresa"].ToString());
            comando.Parameters.AddWithValue("@Numero", System.Web.HttpContext.Current.Session["Numero"].ToString());
            comando.Parameters.AddWithValue("@error", error);
            comando.ExecuteNonQuery();
            comando.Dispose();
            conexion.Close();
            conexion.Dispose();
        }
    }
}