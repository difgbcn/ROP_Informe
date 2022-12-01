using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ROP_WS.Models
{
    public class Oferta
    {
        public string EMPRESA { get; set; }
        public string NOMBRE_OFERTA { get; set; }
        public string FACTURACION { get; set; }
        public string FACTURACION_ALQUILER { get; set; }
        public string FACTURACION_VENTA { get; set; }
       }

    public class OfertaListaResult
    {

        public OfertaListaResult()
        {
            @return = new List<Oferta>();
        }

        public string result { get; set; }
        public List<Oferta> @return { get; set; }
        public object msg { get; set; }
    }
}