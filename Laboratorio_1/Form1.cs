using Laboratorio_1.Models;
using Laboratorio_1.Service;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Laboratorio_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSeeding();
            ConfigurarListView();
        }

        public void FinalizaCompra()
        {
            int totalArticulos = 0;
            decimal descuentoTotal = 0m;
            decimal totalAPagar = 0m;

            foreach (var compra in ComprasService.Compras)
            {
                totalArticulos += compra.cantidad;
                descuentoTotal += compra.descuento;
                totalAPagar += compra.total;
            }

            StringBuilder mensaje = new StringBuilder();
            mensaje.AppendLine($"Total de artículos: {totalArticulos}");
            mensaje.AppendLine($"Descuento total: {descuentoTotal.ToString("C")}");
            mensaje.AppendLine($"Total a pagar: {totalAPagar.ToString("C")}");

            MessageBox.Show(mensaje.ToString(), "Usted compró:", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Application.Exit();
        }

        public void LimpiaCompra()
        {
            cbAtributos.SelectedIndex = -1;
            txbCantidad.Value = 1;
            this.lblPrecioUnitario.Text = Articulo.ZeroPrice;
            this.lblSubTotal.Text = Articulo.ZeroPrice;
        }

        private void ConfigurarListView()
        {
            listComprados.View = View.Details;

            listComprados.Columns.Add("Nombre del Producto", 120);
            listComprados.Columns.Add("Precio Unitario", 105);
            listComprados.Columns.Add("Subtotal", 70);
            listComprados.Columns.Add("Descuento", 70);
            listComprados.Columns.Add("Porcentaje Descuento", 136);
            listComprados.Columns.Add("Total", 75);

            listComprados.Scrollable = true;
        }

        public void AgregarCompra()
        {
            if (this.cbAtributos.SelectedItem == null)
                return;
            if (string.IsNullOrEmpty(lblPrecioUnitario.Text))
                return;
            if (txbCantidad.Value < 1)
                return;

            var compra = new Compra(this.cbAtributos.SelectedItem.ToString(), decimal.Parse(lblPrecioUnitario.Text.Replace("$", "")), (int)txbCantidad.Value);

            ComprasService.Compras.Add(compra);
        }

        public void ActualizarListView()
        {
            listComprados.Items.Clear();
            foreach (var compra in ComprasService.Compras)
            {
                var item = new ListViewItem(new[]
                {
                     compra.nombreProducto,
                     compra.precioUnitario.ToString("C"),
                     compra.subtotal.ToString("C"),
                     compra.descuento.ToString("C"),
                     compra.porcentaje.ToString()+"%",
                     compra.total.ToString("C")
                 });

                listComprados.Items.Add(item);
            }
        }

        public void CalculateDescuentoTotal()
        {
            decimal descuentoTotal = 0;
            decimal totalAPagar = 0;

            foreach (var compra in ComprasService.Compras)
            {
                descuentoTotal += compra.descuento;
                totalAPagar += compra.total;
            }

            lblDescuentoTotal.Text = descuentoTotal.ToString("C");
            lblTotal.Text = totalAPagar.ToString("C");
        }

        public void CalculateSubTotal()
        {
            this.lblSubTotal.Text = Articulo.ZeroPrice;

            if (string.IsNullOrEmpty(lblPrecioUnitario.Text))
                return;
            if (txbCantidad.Value < 1)
                return;

            this.lblSubTotal.Text = (decimal.Parse(lblPrecioUnitario.Text.Replace("$","")) * txbCantidad.Value).ToString("C");
        }

        public void LoadSeeding()
        {
            this.cbAtributos.Items.AddRange(ArticloService.listaArticulos.Select(x=>x.nombreArticulo).ToArray());
            this.lblDescuentoTotal.Text = Articulo.ZeroPrice;
            this.lblPrecioUnitario.Text = Articulo.ZeroPrice;
            this.lblSubTotal.Text = Articulo.ZeroPrice;
            this.lblTotal.Text = Articulo.ZeroPrice;
        }

        public void ShowPriceByArtc()
        {
            if (this.cbAtributos.SelectedItem == null)
                return;

            var currentArtTxt = this.cbAtributos.SelectedItem.ToString();

            var currentArtPrice = ArticloService.listaArticulos.FirstOrDefault(a => a.nombreArticulo == currentArtTxt);

            if (currentArtPrice != null)
                this.lblPrecioUnitario.Text = currentArtPrice.precio.ToString("C");
            else
                this.lblPrecioUnitario.Text = Articulo.ZeroPrice;
        }

        private void cbAtributos_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShowPriceByArtc();
            CalculateSubTotal();
        }

        private void txbCantidad_ValueChanged(object sender, System.EventArgs e)
        {
            CalculateSubTotal();
        }

        private void btnAgregarProducto_Click(object sender, System.EventArgs e)
        {
            AgregarCompra();
            ActualizarListView();
            CalculateDescuentoTotal();
            LimpiaCompra();
        }

        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void btnComprar_Click(object sender, System.EventArgs e)
        {
            FinalizaCompra();
        }
    }
}
