namespace Laboratorio_1.Models
{
    public class Compra
    {
        public string nombreProducto { get; set; }
        public decimal precioUnitario { get; set; }
        public int cantidad { get; set; }
        public decimal subtotal { get; private set; }
        public decimal descuento { get; private set; }
        public decimal porcentaje { get; private set; }
        public decimal total => subtotal - descuento;

        public Compra(string nombreProducto, decimal precioUnitario, int cantidad)
        {
            this.nombreProducto = nombreProducto;
            this.precioUnitario = precioUnitario;
            this.cantidad = cantidad;
            CalcularSubtotal();
            CalcularDescuento();
        }

        private void CalcularSubtotal()
        {
            subtotal = precioUnitario * cantidad;
        }
        private void CalcularDescuento()
        {
            if (subtotal >= 500)
            {
                porcentaje = 0.30m;
                descuento = subtotal * 0.30m;
            }
            else if (subtotal >= 300)
            {
                porcentaje = 0.25m;
                descuento = subtotal * 0.25m;
            }
            else if (subtotal >= 100)
            {
                porcentaje = 0.15m;
                descuento = subtotal * 0.15m;
            }
            else
            {
                porcentaje = 0;
                descuento = 0;
            }
        }
    }

}
