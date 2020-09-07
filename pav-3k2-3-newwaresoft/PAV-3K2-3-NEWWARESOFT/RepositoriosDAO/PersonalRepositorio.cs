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
     class PersonalRepositorio
    {
        //Obtener personal de la BD sin parametros
        public List<Personal> Obtener()
        {
            List<Personal> personal = new List<Personal>();
            var sentenciaSql = "SELECT * FROM Personal";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            foreach (DataRow fila in tabla.Rows)
            {
                var per = new Personal();
                per.Legajo = Convert.ToInt64(fila["legajo"].ToString());
                per.Tipo = Convert.ToInt32(fila["tipo"].ToString());
                per.Documento = Convert.ToInt64(fila["documento"].ToString());
                per.Apellido =  (fila["apellido"].ToString());
                per.Nombres = (fila["nombres"].ToString());
                per.IdUsuario = Convert.ToInt64(fila["id_usuario"]);
                per.FechaIngreso = Convert.ToDateTime(fila["fechaIngreso"]);
              //  per.FechaEgreso = Convert.ToDateTime(fila["fechaEgreso"]);
                per.FechaNacimiento = Convert.ToDateTime(fila["fecNacimiento"]);
             //   per.CodigoProyecto = Convert.ToInt64(fila["codProyecto"]);
             //   per.MotivoEgreso = Convert.ToInt32(fila["motivoEgreso"]);


                personal.Add(per);
            }
            return personal;
        }

        //Obtener personal de la BD con los filtros como parametro
        public List<Personal> ObtenerListaPersonal(PersonalFiltros filtros)
        {
            List<Personal> personal = new List<Personal>();
            var sentenciaSql = $"SELECT * FROM Personal WHERE ";
            if (filtros.FechaIngresoDesde.HasValue)
                sentenciaSql += $"  Personal.fechaIngreso >= '{filtros.FechaIngresoDesde.Value.ToString("yyyy-MM-dd")}' ";
            if (filtros.FechaIngresoHasta.HasValue)
                sentenciaSql += $"AND Personal.fechaIngreso <= '{filtros.FechaIngresoHasta.Value.ToString("yyyy-MM-dd")}' ";
            if (filtros.FechaEgresoDesde.HasValue)
                sentenciaSql += $"  Personal.fechaEgreso >= '{filtros.FechaEgresoDesde.Value.ToString("yyyy-MM-dd")}' ";
            if (filtros.FechaEgresoHasta.HasValue)
                sentenciaSql += $"AND Personal.fechaEgreso <= '{filtros.FechaEgresoHasta.Value.ToString("yyyy-MM-dd")}' ";


            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            foreach (DataRow fila in tabla.Rows)
            {
                var per = new Personal();
                per.Legajo = Convert.ToInt64(fila["legajo"].ToString());
                per.Tipo = Convert.ToInt32(fila["tipo"].ToString());
                per.Documento = Convert.ToInt64(fila["documento"].ToString());
                per.Apellido = (fila["apellido"].ToString());
                per.Nombres = (fila["nombres"].ToString());
                per.IdUsuario = Convert.ToInt64(fila["id_usuario"]);
                per.FechaIngreso = Convert.ToDateTime(fila["fechaIngreso"]);
                per.FechaEgreso = Convert.ToDateTime(fila["fechaEgreso"]);
                per.FechaNacimiento = Convert.ToDateTime(fila["fechaNacimiento"]);
                per.CodigoProyecto = Convert.ToInt64(fila["codProyecto"]);
                per.MotivoEgreso = Convert.ToInt32(fila["motivoEgreso"]);
                personal.Add(per);

            }
            return personal;
        }

        //Obtener personal de la BD con el parametro legajo
        public Personal ObtenerPersonal(long legajo)
        {
            Personal personalResultado = null;
            var sentenciaSql = $"SELECT * FROM Personal WHERE legajo={legajo}";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            if (tabla.Rows.Count == 1)
            {
                var row = tabla.Rows[0];
                var per = new Personal();
                per.Legajo = Convert.ToInt64(row["legajo"].ToString());
                per.Tipo = Convert.ToInt32(row["tipo"].ToString());
                per.Documento = Convert.ToInt64(row["documento"].ToString());
                per.Apellido = (row["apellido"].ToString());
                per.Nombres = (row["nombres"].ToString());
                per.IdUsuario = Convert.ToInt64(row["id_usuario"]);
                per.FechaIngreso = Convert.ToDateTime(row["fechaIngreso"]);
                per.FechaEgreso = Convert.ToDateTime(row["fechaEgreso"]);
                per.FechaNacimiento = Convert.ToDateTime(row["fechaNacimiento"]);
                per.CodigoProyecto = Convert.ToInt64(row["codProyecto"]);
                per.MotivoEgreso = Convert.ToInt32(row["motivoEgreso"]);
                return per;
            }
            return personalResultado;
        }

        //Registrar un nuevo Personal en la BD
        public int RegistrarPersonal(Personal p)
        {
            var sentenciaSql = $"INSERT INTO Personal (legajo, tipo, documento, apellido,nombres,id_usuario,fechaIngreso,fechaNacimiento,codProyecto) " +
                $"VALUES('{p.Legajo}','{p.Tipo}', '{p.Documento}', '{p.Apellido}', '{p.Nombres}', '{p.IdUsuario}', '{p.FechaIngreso}', '{p.FechaEgreso}', '{p.CodigoProyecto}')";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

        //Eliminar un personal de la BD
        public int DarBajaPersonal(Personal p)
        {
            var sentenciaSql = $"DELETE FROM Personal WHERE legajo={p.Legajo}";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

        //Actualizar un personal en la BD
        public int ActualizarPersonal(Personal p)
        {
            var sentenciaSql = $"UPDATE Personal SET legajo='{p.Legajo}', " +
                $"apellido='{p.Apellido}', " +
                $"nombres='{p.Nombres}' " +
                $"apellido='{p.Apellido}' " +
                $"fechaEgreso='{p.FechaEgreso}' " +
                $"codProyecto='{p.CodigoProyecto}' " +


                $"WHERE legajo={p.Legajo}";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }
    }
}
