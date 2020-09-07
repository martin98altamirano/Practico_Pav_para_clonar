using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAV_3K2_3_NEWWARESOFT.Modelos
{
    public class Producto
    {
        #region Propiedades
        public string Denominacion { get; set; }
        public string Descripcion { get; set; }
        public long Codigo { get; set; }
        public DateTime FechaFinDesarrollo { get; set; }
        #endregion

        #region Validacion
        public void ValidarDenominacion()
        {
            if (string.IsNullOrEmpty(this.Denominacion))
                throw new ApplicationException("La Denominacion es requerida");
            if (!string.IsNullOrEmpty(this.Denominacion) && this.Denominacion.Length > 50)
                throw new ApplicationException("Denominacion inválida. No debe superar los 50 caracteres");
        }
        public void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(this.Descripcion))
                throw new ApplicationException("La descripcion es requerido");
        }
        public void ValidarCodigo()
        {
            if (this.Codigo == 0)
                throw new ApplicationException("El codigo es requerido");
            if (this.Codigo == 0 && (this.Codigo.ToString()).Length > 20)
                throw new ApplicationException("Codigo inválido. No debe superar los 20 caracteres");
        }
        #endregion
    }
}
