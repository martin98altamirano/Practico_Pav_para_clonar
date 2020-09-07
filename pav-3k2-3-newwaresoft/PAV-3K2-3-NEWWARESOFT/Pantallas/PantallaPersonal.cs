using PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio;
using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.Modelos.Filtros;
using PAV_3K2_3_NEWWARESOFT.Pantallas.ABM_personal;
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
    public partial class PantallaPersonal : Form
    {
        private PersonalServicio _personalServicio;

        public PantallaPersonal()
        {
            InitializeComponent();
            _personalServicio = new PersonalServicio();

        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            
            RegistrarPersonal pantallaRegistrar = new RegistrarPersonal(this);
            this.Hide();

            pantallaRegistrar.ShowDialog();


        }

        private void PantallaPersonal_Load(object sender, EventArgs e)
        {
            CargarPersonal();

        }

        //Primera carga de los datos para mostrarlos en Pantalla

        //Obtiene los productos
        private void CargarPersonal()
        {
            var personal = _personalServicio.Obtener();
            CargarGrilla(personal);
        }

        //Carga la grilla con los Productos que entran como parametro
        private void CargarGrilla(List<Personal> personal)
        {
            DgvPersonal.Rows.Clear();
            foreach (var per in personal)
            {
                var fila = new string[] {
                    per.Legajo.ToString(),
                    per.Apellido,
                    per.Nombres,
                    per.FechaNacimiento.ToString("dd/MM/yyyy"),
                    per.FechaIngreso.ToString("dd/MM/yyyy")

                };
                DgvPersonal.Rows.Add(fila);
            }
        }

        //Carga la grilla con los Productos que entran como paramentro DESPUES de ser filtrados
        public void ConsultarPersonal()
        {
            var filtros = new PersonalFiltros
            {
                FechaIngresoDesde = DtpFechaDesde.Value,
                FechaIngresoHasta = DtpFechaHasta.Value,
            };
            var productos = _personalServicio.ObtenerListaPersonal(filtros);
            CargarGrilla(productos);
        }

        //Boton consultar. Obtiene los Productos que entren en las condiciones de Filtro
        private void BtnConsultar_Click_1(object sender, EventArgs e)
        {
            try
            {
                ConsultarPersonal();
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
            var _pantallaPrincipal = new PantallaPrincipal();

            _pantallaPrincipal.Show();
            this.Dispose();
        }

    
        //Boton Eliminar
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (DgvPersonal.SelectedRows.Count == 1)
            {
                var id = Convert.ToInt64(DgvPersonal.SelectedRows[0].Cells["legajo"].Value.ToString());
  //(this,id)
                new EliminarPersonal(this, id).Show();
                this.Hide();
            }
            else
                MessageBox.Show("Debe seleccionar solo una fila", "Información");
        }

        //Boton Modificar
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (DgvPersonal.SelectedRows.Count == 1)
            {
                var id = Convert.ToInt64(DgvPersonal.SelectedRows[0].Cells["legajo"].Value.ToString());
                new ModificarPersonal(this, id).Show();
                this.Hide();
            }
            else
                MessageBox.Show("Debe seleccionar solo una fila", "Información");
        }
    }
}

