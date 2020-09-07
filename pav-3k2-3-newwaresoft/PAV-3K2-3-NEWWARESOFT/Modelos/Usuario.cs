using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K2_3_NEWWARESOFT.Modelos
{
    public class Usuario
    {
        #region Propiedades
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constantes
        public string MensajeErrorNombre = "El campo nombre es requerido y no debe superar los 50 caracteres";
        public string MensajeErrorPassword = "El campo contraseña es requerido y debe tener mas de 8 caracteres";
        #endregion

        #region Constructores
        public Usuario()
        {

        }
        public Usuario(string nombre, string password)
        {
            Nombre = nombre;
            Password = password;
        }
        public Usuario(string nombre, string password, long id)
        {
            Id = id;
            Nombre = nombre;
            Password = password;
        }

        #endregion

        #region Validacion
        public void ValidarNombre()
        {
            if (string.IsNullOrEmpty(this.Nombre))
                throw new ApplicationException("El nombre es requerido");
            if (!string.IsNullOrEmpty(this.Nombre) && this.Nombre.Length > 50)
                throw new ApplicationException("Nombre inválido. El nombre no debe superar los 50 caracteres");
        }
        public void ValidarPassword()
        {
            if (string.IsNullOrEmpty(this.Password))
                throw new ApplicationException("La contraseña es requerida");
            if (!string.IsNullOrEmpty(this.Password) && this.Password.Length > 10)
                throw new ApplicationException("Contraseña inválida. La contraseña no debe superar los 10 caracteres");
        }

        public void ValidarId()
        {
            if (this.Id == 0)
                throw new ApplicationException("La Id es requerida");
            if (this.Id == 0 && (this.Id.ToString()).Length > 10)
                throw new ApplicationException("Id inválida. La Id no debe superar los 10 caracteres");
        }
        #endregion
    }

}
