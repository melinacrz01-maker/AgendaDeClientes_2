using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaDeClientes
{
    public partial class AgendaDeContactos : Form
    {
        private BindingList<Contacto> contactos = new BindingList<Contacto>();

        public AgendaDeContactos()
        {
            InitializeComponent();
            dgvContactos.AutoGenerateColumns = false;
            dgvContactos.DataSource = contactos;

        }

        private void DgvContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var contacto = new Contacto
            {
                Nombre = txtBoxName.Text,
                Apellido = txtBoxApellido.Text,
                Telefono = txtBoxTelefono.Text,
                Email = txtBoxCorreo.Text,
                Turno = DtpTurnos.Value,
                Categoria = cmbCategoria.SelectedItem.ToString()
            };
            contactos.Add(contacto);

            txtBoxName.Text = "";
            txtBoxApellido.Text = "";
            txtBoxTelefono.Text = "";
            txtBoxCorreo.Text = "";
            cmbCategoria.SelectedIndex = -1;
            DtpTurnos.Value = DateTime.Now;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvContactos.CurrentRow != null)
            {
                var contactoSeleccionado = dgvContactos.CurrentRow.DataBoundItem as Contacto;
                if (contactoSeleccionado != null)
                {
                    DialogResult result = MessageBox.Show($"¿Está seguro de eliminar el contacto: {contactoSeleccionado.Nombre} {contactoSeleccionado.Apellido}?",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result != DialogResult.Yes) return;

                    contactos.Remove(contactoSeleccionado);
                }
            }
        }

        private void dgvContactos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvContactos.CurrentRow == null) return;
            var contacto = (Contacto)dgvContactos.CurrentRow.DataBoundItem;
            txtBoxName.Text = contacto.Nombre;
            txtBoxApellido.Text = contacto.Apellido;
            txtBoxTelefono.Text = contacto.Telefono;
            txtBoxCorreo.Text = contacto.Email;
            DtpTurnos.Value = contacto.Turno;
            cmbCategoria.SelectedItem = contacto.Categoria;


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvContactos.CurrentRow == null) return; 

            if (string.IsNullOrWhiteSpace(txtBoxName.Text) || string.IsNullOrWhiteSpace(txtBoxApellido.Text) ||
                string.IsNullOrWhiteSpace(txtBoxTelefono.Text) || string.IsNullOrWhiteSpace(txtBoxCorreo.Text) ||
                cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, complete todos los campos antes de editar el contacto.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var contactoSeleccionado = (Contacto)dgvContactos.CurrentRow.DataBoundItem;

            contactoSeleccionado.Nombre = txtBoxName.Text;
            contactoSeleccionado.Apellido = txtBoxApellido.Text;
            contactoSeleccionado.Telefono = txtBoxTelefono.Text;
            contactoSeleccionado.Email = txtBoxCorreo.Text;
            contactoSeleccionado.Turno = DtpTurnos.Value;
            contactoSeleccionado.Categoria = cmbCategoria.SelectedItem.ToString();
            dgvContactos.Refresh();

            txtBoxName.Text = "";
            txtBoxApellido.Text = "";
            txtBoxTelefono.Text = "";
            txtBoxCorreo.Text = "";
            cmbCategoria.SelectedIndex = -1;
            DtpTurnos.Value = DateTime.Now;
        }

        private void txtboxBuscar_Enter(object sender, EventArgs e)
        {
            if (txtboxBuscar.Text == "🔍​ Buscar..")
            {
                txtboxBuscar.Text = "";
            }
        }

        private void txtboxBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtboxBuscar.Text))
            {
                txtboxBuscar.Text = "🔍​ Buscar..";
            }
        }

        private void txtboxBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string filtro = txtboxBuscar.Text.ToLower();

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    dgvContactos.DataSource = contactos;
                }
                else
                {
                    dgvContactos.DataSource = new BindingList<Contacto>(contactos.Where(c => c.Nombre.ToLower().Contains(filtro) || c.Apellido.ToLower().Contains(filtro))
                        .ToList());
                }
            }
        }

        private void AgendaDeContactos_Load(object sender, EventArgs e)
        {

        }
       
        private void txtboxBuscar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}