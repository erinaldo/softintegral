﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using System.Data;
using System.Data.SqlClient;

namespace Capa_negocio
{
  public class NegocioOrdenpedido
    {
        public static string insertar(DateTime fecha,int codcliente,int idusuario,string tipoorden, string estado, string observacion, DataTable midata)
        {
            List<Datodetalleordenpedido> detalles = new List<Datodetalleordenpedido>();


            foreach (DataRow row in midata.Rows)
            {
                //asigno los valores de los detalle de ventas a cada objeto detalle
                Datodetalleordenpedido detalle = new Datodetalleordenpedido();
                detalle.Idproducto = Convert.ToInt32(row["codigo"].ToString());
                detalle.Cantidadparcial  = Convert.ToDecimal(row["cantidadparcial"].ToString());
                detalle.Cantidadtotal  = Convert.ToDecimal(row["cantidadtotal"].ToString());
                detalle.Detalle = row["detalle"].ToString();
                
                
                //agrego el item a la lista detalles
                detalles.Add(detalle);
            }

            DatosOrdenpedido miorden = new DatosOrdenpedido(fecha,codcliente,idusuario,tipoorden,estado,observacion);
            miorden.Insertarymodificar(miorden, detalles);
            string msg = "";
           return msg;
        }

        public static string modificarcantidad(int nroorden, string estado, DataTable midata,int codcliente, string tipo)
        {
            List<Datodetalleordenpedido> detalles = new List<Datodetalleordenpedido>();


            foreach (DataRow row in midata.Rows)
            {
                //asigno los valores de los detalle de ventas a cada objeto detalle
                Datodetalleordenpedido detalle = new Datodetalleordenpedido();
                detalle.Idproducto = Convert.ToInt32(row["codigo"].ToString());
                detalle.Cantidadparcial = Convert.ToDecimal(row["cantidadparcial"].ToString());
                detalle.Cantidadtotal = Convert.ToDecimal(row["cantidadtotal"].ToString());
                detalle.Cantidadactual = Convert.ToDecimal(row["cantidadactual"].ToString());
                detalle.Detalle = row["detalle"].ToString();
                detalle.Norden = nroorden;

                //agrego el item a la lista detalles
                detalles.Add(detalle);
            }

            DatosOrdenpedido miorden = new DatosOrdenpedido(estado,nroorden,codcliente,tipo) ;
            
            
            return miorden.Insertarymodificar(miorden, detalles, "modificarestado", false); ;
        }

        public static DataTable buscarconcatenacion(int varcodcliente, string varestado,string  vartipoorden,string varfechaini,string varfechafin, bool varporcliente, bool varporestado, bool varportipo, bool varporfecha)
        {
            DatosOrdenpedido Dorden = new DatosOrdenpedido(varcodcliente, varestado, vartipoorden, varfechaini, varfechafin, varporcliente, varporestado, varportipo, varporfecha);
            return Dorden.Busquedaconcatenada(Dorden);
                 

        }

        public static DataTable buscarlistadodetalle(int norden)
        {
            Datodetalleordenpedido  objorden = new Datodetalleordenpedido(norden);
            return objorden.consultadetalla(objorden);
        }

        public static string eliminardetalle(int norden)
        {
            Datodetalleordenpedido objorden = new Datodetalleordenpedido(norden);
            return objorden.eliminardetalle(objorden);
        }

        
    }
}