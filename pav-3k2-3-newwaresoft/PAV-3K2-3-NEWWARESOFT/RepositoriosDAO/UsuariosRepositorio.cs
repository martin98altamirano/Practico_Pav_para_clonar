using PAV_3K2_3_NEWWARESOFT.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K2_3_NEWWARESOFT.RepositoriosDAO
{
    class UsuariosRepositorio
    {
        // Ingresar al sistema con usuario en BD
        public Usuario Login(Usuario usuario)
        {
            Usuario usuarioResultado = null;
            var sentenciaSql = $"SELECT * FROM Usuarios WHERE nombreUsuario='{usuario.Nombre}' and contraseña='{usuario.Password}'";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            if (tabla.Rows.Count > 0)
            {
                var row = tabla.Rows[0];
                var usuarioRsultado = new Usuario();
                var id_usuario = row["id_usuario"];
                usuarioRsultado.Id = Convert.ToInt32(id_usuario.ToString());
                usuarioRsultado.Nombre = row["nombreUsuario"].ToString();
                usuarioRsultado.Password = row["contraseña"].ToString();
                return usuarioRsultado;
            }

            return usuarioResultado;
        }

        // Obtener Usuarios de la BD sin parametros
        public List<Usuario> Obtener()
        {
            List<Usuario> usuarios = new List<Usuario>();
            var sentenciaSql = "SELECT * FROM Usuarios";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            foreach (DataRow fila in tabla.Rows)
            {
                var usr = new Usuario();
                usr.Id = Convert.ToInt64(fila["id_usuario"].ToString());
                usr.Nombre = fila["nombreUsuario"].ToString();
                usr.Password = fila["contraseña"].ToString();
                usuarios.Add(usr);
            }
            return usuarios;
        }

        //Obtener Usuarios de la BD con el parametro Nombre
        public List<Usuario> ObtenerUsuarios(string nombre)
        {
            List<Usuario> usuarios = new List<Usuario>();
            var sentenciaSql = $"SELECT * FROM Usuarios WHERE nombreUsuario like '%{nombre}%'";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            foreach (DataRow fila in tabla.Rows)
            {
                var usr = new Usuario();
                usr.Id = Convert.ToInt64(fila["id_usuario"].ToString());
                usr.Nombre = fila["nombreUsuario"].ToString();
                usr.Password = fila["contraseña"].ToString();
                usuarios.Add(usr);
            }
            return usuarios;
        }

        //Obtener Usuarios de la BD con el parametro id_usuario
        public Usuario ObtenerUsuario(long id)
        {
            Usuario usuarioResultado = null;
            var sentenciaSql = $"SELECT * FROM Usuarios WHERE id_usuario={id}";
            var tabla = DBHelper.GetDBHelper().ConsultaSQL(sentenciaSql);
            if (tabla.Rows.Count == 1)
            {
                var row = tabla.Rows[0];
                var usuarioBD = new Usuario();
                var id_usuario = row["id_usuario"];
                usuarioBD.Id = Convert.ToInt64(id_usuario.ToString());
                usuarioBD.Nombre = row["nombreUsuario"].ToString();
                usuarioBD.Password = row["contraseña"].ToString();
                return usuarioBD;
            }
            return usuarioResultado;
        }

        //Registra nuevo Usuario en la BD
        public int RegistrarUsuario(Usuario u)
        {
            var sentenciaSql = $"INSERT INTO Usuarios (id_usuario, nombreUsuario, contraseña) VALUES('{u.Id}','{u.Nombre}', '{u.Password}')";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

        //Elimina Usuario de la BD
        public int DarBajaUsuario(Usuario u)
        {
            var sentenciaSql = $"DELETE FROM Usuarios WHERE id_usuario={u.Id}";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

        //Actualiza Usuario de la BD
        public int ActualizarUsuario(Usuario u)
        {
            var sentenciaSql = $"UPDATE Usuarios SET contraseña='{u.Password}' WHERE id_usuario={u.Id}";
            var filasAfectadas = DBHelper.GetDBHelper().EjecutarSQL(sentenciaSql);
            return filasAfectadas;
        }

    }
}
