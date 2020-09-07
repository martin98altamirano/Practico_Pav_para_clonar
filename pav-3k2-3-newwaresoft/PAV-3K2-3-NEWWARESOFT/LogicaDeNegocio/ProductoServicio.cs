using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.Modelos.Filtros;
using PAV_3K2_3_NEWWARESOFT.RepositoriosDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio
{
    public class ProductoServicio
    {
        private ProductoRepositorio _productoRepositorio;

        public ProductoServicio()
        {
            _productoRepositorio = new ProductoRepositorio();
        }

        //Obtener al Producto SIN parametros
        public List<Producto> Obtener()
        {
            var productos = _productoRepositorio.Obtener();
            return productos;
        }

        //Obtener al Producto usando filtros como parametro
        public List<Producto> ObtenerProductos(ProductoFiltros filtros)
        {
            if (filtros.FechaDesde.HasValue)
                filtros.FechaDesde = new DateTime(filtros.FechaDesde.Value.Year,
                                                   filtros.FechaDesde.Value.Month,
                                                   filtros.FechaDesde.Value.Day);
            if (filtros.FechaHasta.HasValue)
                filtros.FechaHasta = new DateTime(filtros.FechaHasta.Value.Year,
                                                   filtros.FechaHasta.Value.Month,
                                                   filtros.FechaHasta.Value.Day);
            if (filtros.FechaDesde.HasValue && filtros.FechaHasta.HasValue &&
                filtros.FechaDesde > filtros.FechaHasta)
                throw new ApplicationException("La fecha desde no puede ser mayor a fecha hasta");
            return _productoRepositorio.ObtenerProductos(filtros);
        }

        //Obtener al Producto usando el Codigo como parametro
        public Producto ObtenerProducto(long id)
        {
            var usuario = _productoRepositorio.ObtenerProducto(id);
            return usuario;
        }

        //Validar al Producto que ingresa como parametro
        public void ValidarProducto(Producto p)
        {
            p.ValidarCodigo();
            p.ValidarDenominacion();
            p.ValidarDescripcion();
        }

        //Registrar al Producto que ingresa como parametro
        public bool RegistrarProducto(Producto producto)
        {
            var filasAfectadas = _productoRepositorio.RegistrarProducto(producto);
            if (filasAfectadas == 1)
                return true;
            return false;
        }

        //Eliminar al Producto que ingresa como parametro
        public void DarBajaProducto(Producto p)
        {
            var filasAfectadas = _productoRepositorio.DarBajaProducto(p);
            if (filasAfectadas != 1)
                throw new ApplicationException("Hubo un problema al actualizar");
        }

        //Actualizar al Producto que ingresa como parametro
        public void ActualizarProducto(Producto p)
        {
            var filasAfectadas = _productoRepositorio.ActualizarProducto(p);
            if (filasAfectadas != 1)
                throw new ApplicationException("Hubo un problema al actualizar");
        }
    }
}
