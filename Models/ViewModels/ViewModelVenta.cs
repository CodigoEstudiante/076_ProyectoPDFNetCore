namespace proyectopdf.Models.ViewModels
{
    public class ViewModelVenta
    {
        public string numeroventa { get; set; }
        public string documentocliente { get; set; }
        public string nombrecliente { get; set; }
        public string subtotal { get; set; }
        public string impuesto { get; set; }
        public string total { get; set; }

        public List<ViewModelDetalleVenta> detalleventa { get; set; }

    }

    public class ViewModelDetalleVenta
    {

        public string producto { get; set; }
        public string cantidad { get; set; }
        public string precio { get; set; }
        public string total { get; set; }
    }
}
