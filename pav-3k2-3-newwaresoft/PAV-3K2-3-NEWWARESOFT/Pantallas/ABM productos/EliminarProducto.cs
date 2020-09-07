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
    public partial class EliminarProducto : Form
    {
        private Producto _producto;
        private ProductoServicio _productoServicio;
        private PantallaProductos _pantallaProductos;
        public EliminarProducto(PantallaProductos PantallaProducto, long id)
        {
            _productoServicio = new ProductoServicio();
            _pantallaProductos = PantallaProducto;
            _producto = _productoServicio.ObtenerProducto(id);
            InitializeComponent();
        }

        private void EliminarProducto_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        //Boton Guardar. Confirma los valores ingresados, luego confirma la accion y procede a realizarla
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("Confirmar operación", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.Cancel)
                    return;
                ActualizarProducto();
                CerrarFormulario();
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

        //Elimina al Producto seleccionado
        private void ActualizarProducto()
        {
            _productoServicio.DarBajaProducto(_producto);
            MessageBox.Show("La operación se realizó con éxito", "Información");
        }

        //Carga los datos en pantalla del Producto seleccionado
        private void CargarDatos()
        {
            TxtCodigo.Text = _producto.Codigo.ToString();
            TxtDenominacion.Text = _producto.Denominacion;
            TxtDescripcion.Text = _producto.Descripcion;
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
    }
}
