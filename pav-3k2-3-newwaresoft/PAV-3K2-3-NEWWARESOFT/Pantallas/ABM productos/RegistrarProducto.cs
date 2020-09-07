using PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio;
using PAV_3K2_3_NEWWARESOFT.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV_3K2_3_NEWWARESOFT.Pantallas.ABM_productos
{
    public partial class RegistrarProducto : Form
    {
        private Producto _producto;
        private ProductoServicio _productoServicio;
        private PantallaProductos _pantallaProductos;
        public RegistrarProducto(PantallaProductos PantallaProducto)
        {
            _productoServicio = new ProductoServicio();
            _pantallaProductos = PantallaProducto;
            InitializeComponent();
        }

        //Boton Guardar. Confirma los valores ingresados, luego confirma la accion y procede a realizarla
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmarOperacion())
                    return;
                if (!ValidarProducto())
                    return;
                Registrar();
            }
            catch (ApplicationException aex)
            {
                MessageBox.Show(aex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un problema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Registra al nuevo Producto
        private void Registrar()
        {
            if (_productoServicio.RegistrarProducto(_producto))
            {
                MessageBox.Show("La operación se realizó con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CerrarFormulario();
            }
            else
            {
                MessageBox.Show("Hubo un error. Intente nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Confirma que el Usuario quiere realizar la accion
        private bool ConfirmarOperacion()
        {
            DialogResult resultado = MessageBox.Show("Confirmar operación", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (resultado == DialogResult.Cancel)
                return false;
            return true;
        }

        //Valida los datos ingresados
        private bool ValidarProducto()
        {
            var codigo = TxtCodigo.Text;
            var denominacion = TxtDenominacion.Text;
            var Descripcion = TxtDescripcion.Text;
            var FechaFin = DtpFechaFin.Value;

            var prodIngresado = new Producto();
            prodIngresado.Codigo = Convert.ToInt64(codigo);
            prodIngresado.Denominacion = denominacion;
            prodIngresado.Descripcion = Descripcion;
            prodIngresado.FechaFinDesarrollo = FechaFin;
            _productoServicio.ValidarProducto(prodIngresado);
            _producto = prodIngresado;
            return true;
        }

        //Cerrar el formulario
        private void CerrarFormulario()
        {
            _pantallaProductos.Show();
            _pantallaProductos.ConsultarProductos();
            this.Dispose();
        }

        //Boton Cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            CerrarFormulario();
        }

        private void RegistrarProducto_Load(object sender, EventArgs e)
        {

        }
    }
}
