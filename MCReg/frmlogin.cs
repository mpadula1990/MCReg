using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace MCReg
{
    
    public partial class frmlogin : Form
    {
        
        public frmlogin()
        {
            InitializeComponent();
            
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void frmlogin_Load(object sender, EventArgs e)
        {
            txtuser.MaxLength = 12;
            txtpass.MaxLength = 14;
            btnrecpas.TabIndex = 0;
            txtuser.TabIndex = 1;
            txtpass.TabIndex = 2;
            btnlogin.TabIndex = 3;

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnmin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtuser_Enter(object sender, EventArgs e)
        {
            if (txtuser.Text == "USUARIO")
            {
                txtuser.Text = "";
                txtuser.ForeColor = Color.LightGray;
            }
        }

        private void txtuser_Leave(object sender, EventArgs e)
        {
            if (txtuser.Text=="")
            {
                txtuser.Text = "USUARIO";
                txtuser.ForeColor = Color.DimGray;
            }
        }

        private void txtpass_Enter(object sender, EventArgs e)
        {
            if (txtpass.Text == "CONTRASEÑA")
            {
                txtpass.Text = "";
                txtpass.ForeColor = Color.LightGray;
                txtpass.UseSystemPasswordChar = true;
            }
        }

        private void txtpass_Leave(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                txtpass.Text = "CONTRASEÑA";
                txtpass.ForeColor = Color.DimGray;
                txtpass.UseSystemPasswordChar = false;
            }
        }



        private void frmlogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012,0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            frmapp frm = new frmapp();
            actions log = new actions();

            int respuesta = log.Login(txtuser.Text, txtpass.Text);
            switch (respuesta)
            {
                case 0:
                    lblerror.Text = "Error de conexion, intente nuevamente o contacte con el administrador.";
                    break;
                case 1:
                    //busco el usuario con el id del txtusuario
                    //enviar datos del usuario al otro formulario
                    Classapp.usuario = txtuser.Text;
                    frm.Show();
                    this.Hide();
                    break;
                case 2:
                    lblerror.Text = "Autentificacion fallida, error de usuario o contraseña.";
                    break;
            }


        }
    }
}
