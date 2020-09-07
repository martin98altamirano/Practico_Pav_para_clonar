using PAV_3K2_3_NEWWARESOFT.LogicaDeNegocio;
using PAV_3K2_3_NEWWARESOFT.Modelos;
using PAV_3K2_3_NEWWARESOFT.Pantallas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV_3K2_3_NEWWARESOFT
{
    public partial class PantallaPrincipal : Form
    {
        private Usuario _usuario;

        public PantallaPrincipal()
        {
            InitializeComponent();
        }

        //Se encarga de primero mostrar la pantalla de Login. 
        //Tambien pone el nombre del Usuario al lado del ¨Bienvenido¨
        private void PantallaPrincipal_Load(object sender, EventArgs e)
        {
            new PantallaLogin().ShowDialog();
            this._usuario = UsuarioServicio.UsuarioLogueado;
            this.LblUsuario.Text = this._usuario.Nombre;
        }

        //Barra para entrar a la pantalla de Productos
        private void productoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pantallaProductos = new PantallaProductos(this);
            pantallaProductos.Show();
            this.Hide();
        }

        //Barra para entrar a la pantalla de Usuarios
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pantallaUsuarios = new PantallaUsuarios(this);
            pantallaUsuarios.Show();
            this.Hide();
        }

        private void personalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PantallaPersonal pantallaPersonal = new PantallaPersonal();
            this.Hide();
            pantallaPersonal.ShowDialog();
           
        }
    }
}
