using PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio;
using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.Modelos.Filtros;
using PAV_3K2_3_NEWWARESOFT.Pantallas.ABM_productos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV_3K2_3_NEWWARESOFT.Pantallas
{
    public partial class PantallaProductos : Form
    {
        private ProductoServicio _productoServicio;
        private PantallaPrincipal _pantallaPrincipal;

        public PantallaProductos(PantallaPrincipal pantallaPrincipal)
        {
            _productoServicio = new ProductoServicio();
            _pantallaPrincipal = pantallaPrincipal;
            InitializeComponent();
        }

        //Primera carga de los datos para mostrarlos en Pantalla
        private void PantallaProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        //Obtiene los productos
        private void CargarProductos()
        {
            var productos = _productoServicio.Obtener();
            CargarGrilla(productos);
        }

        //Carga la grilla con los Productos que entran como parametro
        private void CargarGrilla(List<Producto> productos)
        {
            DgvProductos.Rows.Clear();
            foreach (var producto in productos)
            {
                var fila = new string[] {
                    producto.Codigo.ToString(),
                    producto.Denominacion,
                    producto.Descripcion,
                    producto.FechaFinDesarrollo.ToString("dd/MM/yyyy")
                };
                DgvProductos.Rows.Add(fila);
            }
        }

        //Carga la grilla con los Productos que entran como paramentro DESPUES de ser filtrados
        public void ConsultarProductos()
        {
            var filtros = new ProductoFiltros
            {
                FechaDesde = DtpFechaDesde.Value,
                FechaHasta = DtpFechaHasta.Value,
            };
            var productos = _productoServicio.ObtenerProductos(filtros);
            CargarGrilla(productos);
        }

        //Boton consultar. Obtiene los Productos que entren en las condiciones de Filtro
        private void BtnConsultar_Click_1(object sender, EventArgs e)
        {
            try
            {
                ConsultarProductos();
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

        //Boton Salir
        private void BtnSalir_Click_1(object sender, EventArgs e)
        {
            _pantallaPrincipal.Show();
            this.Dispose();
        }

        //Boton Registrar
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            new RegistrarProducto(this).Show();
            this.Hide();
        }

        //Boton Eliminar
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (DgvProductos.SelectedRows.Count == 1)
            {
                var id = Convert.ToInt64(DgvProductos.SelectedRows[0].Cells["codigo"].Value.ToString());
                new EliminarProducto(this, id).Show();
                this.Hide();
            }
            else
                MessageBox.Show("Debe seleccionar solo una fila", "Información");
        }

        //Boton Modificar
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (DgvProductos.SelectedRows.Count == 1)
            {
                var id = Convert.ToInt64(DgvProductos.SelectedRows[0].Cells["codigo"].Value.ToString());
                new ModificarProducto(this, id).Show();
                this.Hide();
            }
            else
                MessageBox.Show("Debe seleccionar solo una fila", "Información");
        }

        private void DgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
