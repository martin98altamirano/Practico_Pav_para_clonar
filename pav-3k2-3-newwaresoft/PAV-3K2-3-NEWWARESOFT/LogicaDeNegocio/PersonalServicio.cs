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
    public class PersonalServicio
    {
        private PersonalRepositorio _personalRepositorio;

        public PersonalServicio()
        {
            _personalRepositorio = new PersonalRepositorio();
        }

        //Obtener al Producto SIN parametros
        public List<Personal> Obtener()
        {
            var listaPersonal = _personalRepositorio.Obtener();
            return listaPersonal;
        }

        //Obtener al Producto usando filtros como parametro
        public List<Personal> ObtenerListaPersonal(PersonalFiltros filtros)
        {
            if (filtros.FechaIngresoDesde.HasValue)
                filtros.FechaIngresoDesde = new DateTime(filtros.FechaIngresoDesde.Value.Year,
                                                   filtros.FechaIngresoDesde.Value.Month,
                                                   filtros.FechaIngresoDesde.Value.Day);
            if (filtros.FechaIngresoHasta.HasValue)
                filtros.FechaIngresoHasta = new DateTime(filtros.FechaIngresoHasta.Value.Year,
                                                   filtros.FechaIngresoHasta.Value.Month,
                                                   filtros.FechaIngresoHasta.Value.Day);
            if (filtros.FechaIngresoDesde.HasValue && filtros.FechaIngresoHasta.HasValue &&
                filtros.FechaIngresoDesde > filtros.FechaIngresoHasta)
                throw new ApplicationException("La fecha desde no puede ser mayor a fecha hasta");
            return _personalRepositorio.ObtenerListaPersonal(filtros);
        }

        //Obtener al Producto usando el Codigo como parametro
        public Personal ObtenerPersonal(long legajo)
        {
            var personal = _personalRepositorio.ObtenerPersonal(legajo);
            return personal;
        }

        //Validar al Producto que ingresa como parametro
    //    public void ValidarPersonal(Personal p)
   //     {
   //         p.va();
   //         p.ValidarDenominacion();
   //         p.ValidarDescripcion();
    //    }

        //Registrar al Producto que ingresa como parametro
        public bool RegistrarPersonal(Personal personal)
        {
            var filasAfectadas = _personalRepositorio.RegistrarPersonal(personal);
            if (filasAfectadas == 1)
                return true;
            return false;
        }

        //Eliminar al Producto que ingresa como parametro
        public void DarBajaPersonal(Personal p)
        {
            var filasAfectadas = _personalRepositorio.DarBajaPersonal(p);
            if (filasAfectadas != 1)
                throw new ApplicationException("Hubo un problema al actualizar");
        }

        //Actualizar al Producto que ingresa como parametro
        public void ActualizarPersonal(Personal p)
        {
            var filasAfectadas = _personalRepositorio.ActualizarPersonal(p);
            if (filasAfectadas != 1)
                throw new ApplicationException("Hubo un problema al actualizar");
        }
    }
}
