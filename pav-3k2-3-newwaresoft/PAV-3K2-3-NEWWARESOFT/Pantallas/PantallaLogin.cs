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

namespace PAV_3K2_3_NEWWARESOFT.Pantallas
{
    public partial class PantallaLogin : Form
    {
        private UsuarioServicio _usuarioServicio;
        public PantallaLogin()
        {
            InitializeComponent();
            _usuarioServicio = new UsuarioServicio();
        }

        //Boton Ingresar. Le pasa al servicio los datos ingresados para que compruebe si los datos son
        //correctos y son parte de la BD
        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            var nombreIngresado = TxtUsuario.Text;
            var passwordIngresado = TxtPassword.Text;
            Usuario usuario = new Usuario(nombreIngresado, passwordIngresado);
            var usuarioSistema = _usuarioServicio.Login(usuario);
            if (usuarioSistema == null)
            {                // no es usuario del sistema
                MessageBox.Show("Usuario/Contraseña incorrecta", "Información", MessageBoxButtons.OK);
            }
            else
            {
                this.Dispose();
            }
        }

        //Boton Salir
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void PantallaLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
