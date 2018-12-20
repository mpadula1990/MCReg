using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCReg
{
    public partial class frmnuevosocio : Form
    {
        public pacientes pacientesdata { get; set; } 
        public actions ejecutar = new actions();

        public frmnuevosocio()
        {
            InitializeComponent();
            pacientesdata = new pacientes();
            
        }

        private void frmnuevosocio_Load(object sender, EventArgs e)
        {
            lblfecha.Text = DateTime.Now.ToLongDateString();
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            pacientesdata.documento = txtdoc.Text;
            pacientesdata.nombre = txtnom.Text;
            pacientesdata.apellido = txtape.Text;
            pacientesdata.email = txtemail.Text;
            pacientesdata.direccion = txtdir.Text;
            pacientesdata.telefono = txttel.Text;
            pacientesdata.movil = txtmov.Text;

            
            label9.Text= ejecutar.Insertarpaciente(pacientesdata).ToString();

            txtdoc.Text= "";
            txtnom.Text = "";
            txtape.Text = "";
            txtemail.Text = "";
            txtdir.Text = "";
            txttel.Text = "";
            txtmov.Text = "";


        }
    }
}
