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

namespace PAV_3K2_3_NEWWARESOFT.Pantallas.ABM_usuarios
{
    public partial class EliminarUsuario : Form
    {
        private Usuario _usuario;
        private UsuarioServicio _usuarioServicio;
        private PantallaUsuarios _pantallaUsuarios;
        public EliminarUsuario(PantallaUsuarios pantallaUsuarios, long id)
        {
            _usuarioServicio = new UsuarioServicio();
            _pantallaUsuarios = pantallaUsuarios;
            _usuario = _usuarioServicio.ObtenerUsuario(id);
            InitializeComponent();
        }

        private void EliminarUsuario_Load(object sender, EventArgs e)
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
                ActualizarUsuario();
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

        //Elimina al usuario
        private void ActualizarUsuario()
        {
            _usuarioServicio.DarBajaUsuario(_usuario);
            MessageBox.Show("La operación se realizó con éxito", "Información");
        }

        //Carga los datos en pantalla del Usuario seleccionado
        private void CargarDatos()
        {
            TxtNombre.Text = _usuario.Nombre;
            TxtId.Text = _usuario.Id.ToString();

        }

        //Cerrar el Formulario
        private void CerrarFormulario()
        {
            _pantallaUsuarios.Show();
            _pantallaUsuarios.ConsultarUsuarios();
            this.Dispose();
        }

        //Boton Cancelar
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            CerrarFormulario();
        }
    }
}
