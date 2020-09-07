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
    public partial class RegistrarUsuario : Form
    {
        private Usuario _usuario;
        private UsuarioServicio _usuarioServicio;
        private PantallaUsuarios _pantallaUsuarios;
        private int id;

        public RegistrarUsuario(PantallaUsuarios PantallaUsuarios)
        {
            _usuarioServicio = new UsuarioServicio();
            _pantallaUsuarios = PantallaUsuarios;
            InitializeComponent();
        }

        //Boton Guardar. Confirma los valores ingresados, luego confirma la accion y procede a realizarla
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ConfirmarOperacion())
                    return;
                if (!ValidarUsuario())
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

        //Registra al Usuario
        private void Registrar()
        {
            if (_usuarioServicio.RegistrarUsuario(_usuario))
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
        private bool ValidarUsuario()
        {
            var password = TxtPassword.Text;
            var repassword = TxtRePassword.Text;
            if (password != repassword)
            {
                MessageBox.Show("La constraseña no coincide", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            var nombre = TxtNombre.Text;
            var id = TxtId.Text;

            var usuarioIngresado = new Usuario();
            usuarioIngresado.Nombre = nombre;
            usuarioIngresado.Password = password;
            usuarioIngresado.Id = Convert.ToInt64(id);
            _usuarioServicio.ValidarUsuario(usuarioIngresado);
            _usuario = usuarioIngresado;
            return true;
        }

        //Cerrar el formulario
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

        private void RegistrarUsuario_Load(object sender, EventArgs e)
        {
            //Generar un numero de id al azar al cargar
            var random = new Random();
            id = random.Next(999999999);
            TxtId.Text = id.ToString();

        }

        private void TxtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
