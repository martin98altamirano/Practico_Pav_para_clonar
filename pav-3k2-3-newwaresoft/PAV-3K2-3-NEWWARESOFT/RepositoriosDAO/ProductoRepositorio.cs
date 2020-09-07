using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.Modelos.Filtros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K2_3_NEWWARESOFT.RepositoriosDAO
{
    class ProductoRepositorio
    {
        //Obtener Productos de la BD sin parametros
        public List<Producto> Obtener()
        {
            List<Producto> productos = new List<Producto>();
            var sentenciaSql = "SELECT * FROM Productos";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            foreach (DataRow fila in tabla.Rows)
            {
                var pro = new Producto();
                pro.Denominacion = fila["Denominacion"].ToString();
                pro.Descripcion = fila["Descripcion"].ToString();
                pro.Codigo = Convert.ToInt64(fila["Codigo"].ToString());
                pro.FechaFinDesarrollo = Convert.ToDateTime(fila["fecFinDesarrollo"].ToString());
                productos.Add(pro);
            }
            return productos;
        }

        //Obtener Productos de la BD con los filtros como parametro
        public List<Producto> ObtenerProductos(ProductoFiltros filtros)
        {
            List<Producto> productos = new List<Producto>();
            var sentenciaSql = $"SELECT * FROM Productos WHERE ";
            if (filtros.FechaDesde.HasValue)
                sentenciaSql += $"Productos.fecFinDesarrollo >= '{filtros.FechaDesde.Value.ToString("yyyy-MM-dd")}' ";
            if (filtros.FechaHasta.HasValue)
                sentenciaSql += $"AND Productos.fecFinDesarrollo <= '{filtros.FechaHasta.Value.ToString("yyyy-MM-dd")}' ";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            foreach (DataRow fila in tabla.Rows)
            {
                var pro = new Producto();
                pro.Denominacion = fila["Denominacion"].ToString();
                pro.Descripcion = fila["Descripcion"].ToString();
                pro.Codigo = Convert.ToInt64(fila["Codigo"].ToString());
                pro.FechaFinDesarrollo = Convert.ToDateTime(fila["fecFinDesarrollo"].ToString());
                productos.Add(pro);
            }
            return productos;
        }

        //Obtener Productos de la BD con el parametro Codigo
        public Producto ObtenerProducto(long id)
        {
            Producto prodResultado = null;
            var sentenciaSql = $"SELECT * FROM Productos WHERE codigo={id}";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            if (tabla.Rows.Count == 1)
            {
                var row = tabla.Rows[0];
                var prodBD = new Producto();
                var codigo = row["codigo"];
                prodBD.Codigo = Convert.ToInt64(codigo.ToString());
                prodBD.Denominacion = row["Denominacion"].ToString();
                prodBD.Descripcion = row["Descripcion"].ToString();
                prodBD.FechaFinDesarrollo = Convert.ToDateTime(row["fecFinDesarrollo"].ToString());
                return prodBD;
            }
            return prodResultado;
        }

        //Registrar un nuevo Producto en la BD
        public int RegistrarProducto(Producto p)
        {
            var sentenciaSql = $"INSERT INTO Productos (codigo, denominacion, descripcion, fecFinDesarrollo) " +
                $"VALUES('{p.Codigo}','{p.Denominacion}', '{p.Descripcion}', '{p.FechaFinDesarrollo}')";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

        //Eliminar un Producto de la BD
        public int DarBajaProducto(Producto p)
        {
            var sentenciaSql = $"DELETE FROM Productos WHERE codigo={p.Codigo}";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

        //Actualizar un Producto en la BD
        public int ActualizarProducto(Producto p)
        {
            var sentenciaSql = $"UPDATE Productos SET denominacion='{p.Denominacion}', " +
                $"descripcion='{p.Descripcion}', " +
                $"fecFinDesarrollo='{p.FechaFinDesarrollo}' " +
                $"WHERE codigo={p.Codigo}";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }
    }
}
