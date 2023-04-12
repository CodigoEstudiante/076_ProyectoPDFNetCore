using Microsoft.AspNetCore.Mvc;
using proyectopdf.Models;
using System.Diagnostics;
using proyectopdf.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace proyectopdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBPRUEBASContext _dbcontex;


        public HomeController(DBPRUEBASContext _context)
        {
            _dbcontex = _context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ImprimirVenta(int idventa)
        {
            //TODO ESTO LO REEMPLAZAS CON TU PROPIA LÓGICA HACIA TU BASE DE DATOS
            ViewModelVenta modelo = _dbcontex.Venta.Include(dv => dv.DetalleVenta).Where(v => v.IdVenta == idventa)
                .Select(v => new ViewModelVenta()
                {
                    numeroventa = v.NumeroVenta,
                    documentocliente = v.DocumentoCliente,
                    nombrecliente = v.NombreCliente,
                    subtotal = v.SubTotal.ToString(),
                    impuesto = v.ImpuestoTotal.ToString(),
                    total = v.Total.ToString(),
                    detalleventa = v.DetalleVenta.Select(dv => new ViewModelDetalleVenta()
                    {
                        producto = dv.NombreProducto,
                        cantidad = dv.Cantidad.ToString(),
                        precio = dv.Precio.ToString(),
                        total = dv.Total.ToString(),
                    }).ToList()
                }).FirstOrDefault();


            //return View();

            return new ViewAsPdf("ImprimirVenta", modelo)
            {
                FileName = $"Venta {modelo.numeroventa}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}