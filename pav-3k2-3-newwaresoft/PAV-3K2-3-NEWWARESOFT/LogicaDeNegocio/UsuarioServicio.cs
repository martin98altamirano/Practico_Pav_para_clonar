using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.RepositoriosDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio
{
    class UsuarioServicio
    {
        private UsuariosRepositorio _usuariosRepositorio;

        public string MensajeErrorUsuarioContraseniaIncorrecta = "Usuario o contraseña incorrectas";
        public static Usuario UsuarioLogueado { get; set; }

        public UsuarioServicio()
        {
            _usuariosRepositorio = new UsuariosRepositorio();
        }

        // Login con BD
        public Usuario Login(Usuario usuario)
        {
            UsuarioServicio.UsuarioLogueado = _usuariosRepositorio.Login(usuario);
            return UsuarioServicio.UsuarioLogueado;
        }

        // Obtener datos del Usuario SIN parametros
        public List<Usuario> Obtener()
        {
            var usuarios = _usuariosRepositorio.Obtener();
            return usuarios;
        }

        //Obtener datos del Usuarion CON parametro (nombre del usuario)
        public List<Usuario> ObtenerUsuarios(string nombre)
        {
            var usuarios = _usuariosRepositorio.ObtenerUsuarios(nombre);
            return usuarios;
        }

        //Obtener datos del Usuarion CON parametro (ID del usuario)
        public Usuario ObtenerUsuario(long id)
        {
            var usuario = _usuariosRepositorio.ObtenerUsuario(id);
            return usuario;
        }

        //Validar a un Usuario. Entra como parametro el usuario a validar
        public void ValidarUsuario(Usuario u)
        {
            u.ValidarNombre();
            u.ValidarPassword();
            u.ValidarId();
        }

        //Registrar al Usuario
        public bool RegistrarUsuario(Usuario usuario)
        {
            var filasAfectadas = _usuariosRepositorio.RegistrarUsuario(usuario);
            if (filasAfectadas == 1)
                return true;
            return false;
        }

        //Eliminar al Usuario
        public void DarBajaUsuario(Usuario u)
        {
            var filasAfectadas = _usuariosRepositorio.DarBajaUsuario(u);
            if (filasAfectadas != 1)
                throw new ApplicationException("Hubo un problema al actualizar");
        }

        //Modificar al Usuario
        public void ActualizarUsuario(Usuario u)
        {
            var filasAfectadas = _usuariosRepositorio.ActualizarUsuario(u);
            if (filasAfectadas != 1)
                throw new ApplicationException("Hubo un problema al actualizar");
        }
    }
}
