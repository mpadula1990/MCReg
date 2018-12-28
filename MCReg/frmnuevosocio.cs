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
        public solicitud solicitudesdata { get; set; }
        public actions ejecutar = new actions();

        public frmnuevosocio()
        {
            InitializeComponent();
            pacientesdata = new pacientes();
            solicitudesdata = new solicitud();
        }

        private void frmnuevosocio_Load(object sender, EventArgs e)
        {

            //cargo labels de fechas y horas
            lblfecha.Text = DateTime.Now.ToLongDateString();
            lbld2.Text = DateTime.Now.ToLongDateString();
            lbld3.Text = DateTime.Now.ToLongDateString();
            lbld4.Text = DateTime.Now.ToLongDateString();
            //cargo datagrids
            dataGridView1.DataSource = ejecutar.ObtenerPacientes();
            //verifs
            if (txtauorizacion.Text == "")
            {
                btneliminar.Enabled = false;
            }

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

        private void btnmtodo_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ejecutar.ObtenerPacientes();
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            
            //Tenemos 3 casos, caso que no halla ninguna autorizacion, caso que sea generica, y caso que no lo sea.
            aut autdata = ejecutar.obtenerporid(txtauorizacion.Text);
            var tiempoahora = DateTime.UtcNow;
            var keyeliminar = gridEliminar.CurrentRow.Cells[0].Value.ToString();
            string tipoaut = autdata.tipo;
            DateTime auttiempo = ejecutar.Cadenafecha(autdata.tiempo);




            if (autdata != null)
            {

                if (autdata.tipo == "generica")
                {
                    //cuando es generica solo comprovamos que este activa y que pertenezca al usuario.
                    if(autdata.usuario == autdata.usuario_destino && autdata.usuario_destino == Classapp.usuario && autdata.estado == true)
                    {
                        ejecutar.Eliminarpaciente(keyeliminar);//eliminamos con la key eliminar, este usuario tiene permiso de borrar cualquier paciente.
                        gridEliminar.DataSource = null;
                        lblinfo.ForeColor = Color.Green;
                        lblinfo.Text = "Paciente eliminado";
                    }
                }
                else if(auttiempo < tiempoahora && keyeliminar == autdata.llave && autdata.usuario_destino== Classapp.usuario)
                {
                    //en este caso la autorizacion no es generica, por lo tanto es una aut proporcionada por otro usuario. Hacemos las comprovaciones.
                    //no puede estar caducada, debe coinsidir la llave, el usuario logeado con el usuario destino
                    
                    ejecutar.Eliminarpaciente(keyeliminar);
                    gridEliminar.DataSource = null;
                    lblinfo.ForeColor = Color.Green;
                    lblinfo.Text = "Paciente eliminado";

                }
                else
                {
                    lblinfo.ForeColor = Color.Red;
                    lblinfo.Text = "Autorizacion rechazada o caducada, contacte al administrador o solicite otra autorizacion";
                }

            }
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            /*VAMOS A EXPORTAR 3 VIAS, 
             UN PDF,
             UN HTML Y UN ARCHIVO JSON CON LOS DATOS, ESTE DESDE EL PANEL DE ADMINISTRACION SE PUEDE USAR PARA RESTABLECER UN USUARIO ELIMINADO O DADO DE BAJA*/
        }

        private void btnbusqueda_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ejecutar.pacientesdoclist(txtbusqueda.Text);

        }

        private void btnbuseli_Click(object sender, EventArgs e)
        {
            gridEliminar.DataSource = ejecutar.pacientesdoclist(txtbuseli.Text);
        }

        private void btnsolicitud_Click(object sender, EventArgs e)
        {
            solicitudesdata.comentario = txtsolicitud.Text;
            solicitudesdata.usuario = Classapp.usuario;
            solicitudesdata.estado = true;
            /*FECHA investigar y probar, de lo contrario hay que formatear y enviar con un metodo*/
            //solicitudesdata.fecha_hora = 
            solicitudesdata.fecha_hora = DateTime.UtcNow.ToString();
            /*ACA IGUALO LA LLAVE AL ID SELECCIONADO EN EL DATAGRID
             * solicitudesdata.llave = */
            solicitudesdata.llave = gridEliminar.CurrentRow.Cells[0].Value.ToString();
            //LO SUBO A LA BASE DE DATOS
            ejecutar.enviarsolicitud(solicitudesdata);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GridAut.DataSource = ejecutar.Obteneraut(Classapp.usuario);
        }

        private void btncargaraut_Click(object sender, EventArgs e)
        {
            //gridEliminar.CurrentRow.Cells[0].Value.ToString();
            //int index = GridAut.CurrentCell.RowIndex;
            try
            {
                txtauorizacion.Text = "";
                txtauorizacion.Text = GridAut.CurrentRow.Cells[0].Value.ToString();
            }
            catch (Exception)
            {
                lblinfo.Text = "No hay autorizaciones para cargar...";
            }


        }



        private void txtauorizacion_Validating(object sender, CancelEventArgs e)
        {
            char[] allowedChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'a', 'b', 'c', 'd', 'e', 'f' };

            foreach (char character in txtauorizacion.Text.ToArray())
            {
                if (!allowedChars.Contains(character))
                {
                    lblinfo.Text = "Autorizacion: Formato invalido";
                    e.Cancel = true;
                }
            }
        }

        private void tmrealtime_Tick(object sender, EventArgs e)
        {
            try { 
            if (txtauorizacion.Text != "" && gridEliminar.CurrentRow.Cells[0].Value.ToString() != null)
            {
                btneliminar.Enabled = true;
            }
            else
            {
                btneliminar.Enabled = false;
            }
            }
            catch (Exception)
            {
                btneliminar.Enabled = false;
            }
        }
    }
}
