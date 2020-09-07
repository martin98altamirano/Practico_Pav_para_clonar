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
    public partial class ModificarUsuario : Form
    {
        private Usuario _usuario;
        private UsuarioServicio _usuarioServicio;
        private PantallaUsuarios _pantallaUsuarios;
        public ModificarUsuario(PantallaUsuarios pantallaUsuarios, long id)
        {
            _usuarioServicio = new UsuarioServicio();
            _pantallaUsuarios = pantallaUsuarios;
            _usuario = _usuarioServicio.ObtenerUsuario(id);
            InitializeComponent();
        }

        private void ModificarUsuario_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        //Carga los datos en pantalla del Usuario seleccionado
        private void CargarDatos()
        {
            TxtNombre.Text = _usuario.Nombre;
            TxtId.Text = _usuario.Id.ToString();

        }

        //Boton Guardar. Confirma los valores ingresados, luego confirma la accion y procede a realizarla
        private void BtnGuardar_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult resultado = MessageBox.Show("Confirmar operación", "Confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resultado == DialogResult.Cancel)
                    return;
                if (!ValidarUsuario())
                    return;
                RegistrarUsuario();
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

        //Cerrar el Formulario
        private void CerrarFormulario()
        {
            _pantallaUsuarios.Show();
            _pantallaUsuarios.ConsultarUsuarios();
            this.Dispose();
        }

        //Valida los datos ingresados
        private bool ValidarUsuario()
        {
            var password = TxtPassword.Text;
            var repassword = TxtRePassword.Text;
            if (password != repassword)
            {
                throw new ApplicationException("La constraseña no coincide");
            }
            var usuarioIngresado = new Usuario();
            usuarioIngresado.Nombre = _usuario.Nombre;
            usuarioIngresado.Password = password;
            usuarioIngresado.Id = Convert.ToInt64(_usuario.Id);
            _usuarioServicio.ValidarUsuario(usuarioIngresado);
            _usuario = usuarioIngresado;
            return true;
        }

        //Actualiza los datos del Usuario
        private void RegistrarUsuario()
        {
            _usuarioServicio.ActualizarUsuario(_usuario);
            MessageBox.Show("La operación se realizó con éxito", "Información");
        }

        //Boton Cancelar
        private void BtnSalir_Click_1(object sender, EventArgs e)
        {
            CerrarFormulario();
        }

    }
}
