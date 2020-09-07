using PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio;
using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.Pantallas.ABM_usuarios;
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
    public partial class PantallaUsuarios : Form
    {
        private UsuarioServicio _usuarioServicio;
        private PantallaPrincipal _pantallaPrincipal;

        public PantallaUsuarios(PantallaPrincipal pantallaPrincipal)
        {
            _usuarioServicio = new UsuarioServicio();
            _pantallaPrincipal = pantallaPrincipal;
            InitializeComponent();
        }

        //Primera carga de los Usuarios
        private void PantallaUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            ConsultarUsuarios();
        }

        //Obtiene los Usuarios
        private void CargarUsuarios()
        {
            var usr = _usuarioServicio.Obtener();
            CargarGrilla(usr);
        }

        //Carga la grilla con los Usuarios que le entran como parametro
        private void CargarGrilla(List<Usuario> usuarios)
        {
            DgvUsuarios.Rows.Clear();
            foreach (var usuario in usuarios)
            {
                var fila = new string[] {
                    usuario.Id.ToString(),
                    usuario.Nombre,
                    usuario.Password
                };
                DgvUsuarios.Rows.Add(fila);
            }
        }

        //Carga la grilla con los Usuarios que entran como paramentro DESPUES de ser filtrados
        public void ConsultarUsuarios()
        {
            var nombreIngresado = TxtNombre.Text;
            if (!string.IsNullOrEmpty(nombreIngresado) && nombreIngresado.Length > 50)
            {
                MessageBox.Show("Nombre inválido", "Error", MessageBoxButtons.OK);
                return;
            }
            var usr = _usuarioServicio.ObtenerUsuarios(nombreIngresado);
            CargarGrilla(usr);

        }

        //Boton Salir
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            _pantallaPrincipal.Show();
            this.Dispose();
        }

        //Boton Consultar. Obtiene los Usuarios que entren en las condiciones de Filtro
        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultarUsuarios();
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

        //Boton Registrar
        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            new RegistrarUsuario(this).Show();
            this.Hide();
        }

        //Boton Eliminar
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (DgvUsuarios.SelectedRows.Count == 1)
            {
                var id = Convert.ToInt64(DgvUsuarios.SelectedRows[0].Cells["Id"].Value.ToString());
                new EliminarUsuario(this, id).Show();
                this.Hide();
            }
            else
                MessageBox.Show("Debe seleccionar solo una fila", "Información");
        }

        //Boton Modificar
        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (DgvUsuarios.SelectedRows.Count == 1)
            {
                var id = Convert.ToInt64(DgvUsuarios.SelectedRows[0].Cells["Id"].Value.ToString());
                new ModificarUsuario(this, id).Show();
                this.Hide();
            }
            else
                MessageBox.Show("Debe seleccionar solo una fila", "Información");
        }
    }
}
