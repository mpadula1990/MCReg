using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;

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

            if (ejecutar.Insertarpaciente(pacientesdata) == true)
            {
                label9.Text = "Operacion Exitosa";

                txtdoc.Text = "";
                txtnom.Text = "";
                txtape.Text = "";
                txtemail.Text = "";
                txtdir.Text = "";
                txttel.Text = "";
                txtmov.Text = "";
            }
            else
            {
                label9.ForeColor = Color.Red;
                label9.Text = "No se completo la operacion, vuelve a intentarlo";
            }

        }

        private void btnmbuscar_Click(object sender, EventArgs e)
        {
            //Busco
            pacientes busqueda = ejecutar.Pacientepordoc(txtdocm.Text);
            if (busqueda != null)
            {
                lblid.Text = busqueda._id.ToString();
                txtnombrem.Text = busqueda.nombre;
                txtapellidom.Text = busqueda.apellido;
                txtemailm.Text = busqueda.email;
                txtdirem.Text = busqueda.direccion;
                txttelm.Text = busqueda.telefono;
                txtmovilm.Text = busqueda.movil;

                txtnombrem.Enabled = true;
                txtapellidom.Enabled = true;
                txtemailm.Enabled = true;
                txtdirem.Enabled = true;
                txttelm.Enabled = true;
                txtmovilm.Enabled = true;
            }
            else
            {
                label20.Text = "Paciente no encontrado";
            }

        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {

            pacientes pmodificar = new pacientes();
            var __id = new ObjectId(lblid.Text);
            pmodificar._id = __id;
            pmodificar.documento = txtdocm.Text;
            pmodificar.nombre = txtnombrem.Text;
            pmodificar.apellido = txtapellidom.Text;
            pmodificar.email = txtemailm.Text;
            pmodificar.direccion = txtdirem.Text;
            pmodificar.telefono = txttelm.Text;
            pmodificar.movil = txtmovilm.Text;

            bool actualizar = ejecutar.Actualizarpaciente(pmodificar);
            if (actualizar == true)
            {
                label20.Text = "Datos Actualizados";
            }
            else
            {
                label20.Text = "Error al actualizar datos";
            }
        }



    }
}
