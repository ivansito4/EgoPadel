using Braintree;
using EgoPadel.Datos;
using EgoPadel.Models;
using EgoPadel.Models.ViewModels;
using EgoPadel.Utilidades;
using EgoPadel.Utilidades.BrainTree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace EgoPadel.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly IBrainTreeGate _brain;

        [BindProperty]
        public ProductoUsuarioVM productoUsuarioVM { get; set; }

        public CarritoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, 
                                IEmailSender emailSender, IBrainTreeGate brain)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _brain = brain;
        }
        public IActionResult Index()
        {
            List<Carrito> carritoLista = new List<Carrito>();

            if(HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito)!=null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            List<int> prodCarrito = carritoLista.Select(i=>i.ProductoId).ToList();
            List<Producto> prodLista = _db.Producto.Where(p => prodCarrito.Contains(p.Id)).ToList();

            return View(prodLista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(IEnumerable<Producto> ProdLista)
        {
            List<Carrito> carroCompraLista = new List<Carrito>();

            foreach (Producto prod in ProdLista)
            {
                carroCompraLista.Add(new Carrito
                {
                    ProductoId = prod.Id
                });
            }
            HttpContext.Session.Set(WC.SessionCarrito, carroCompraLista);
            return RedirectToAction(nameof(Resumen));
        }

        public IActionResult Resumen()
        {
            //Pillar usuario conectado
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            UsuarioApp userActual = _db.UsuarioApp.FirstOrDefault(u => u.Id == claim.Value);

            List<Carrito> carritoLista = new List<Carrito>();

            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito) != null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            List<int> prodCarrito = carritoLista.Select(i => i.ProductoId).ToList();
            List<Producto> prodLista = _db.Producto.Where(p => prodCarrito.Contains(p.Id)).ToList();

            productoUsuarioVM = new ProductoUsuarioVM()
            {
                UsuarioApp = userActual,
                ListaProducto = prodLista
            };
            var gateway = _brain.GetGateWay();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;

            return View(productoUsuarioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Resumen")]
        public async Task<IActionResult> ResumenPost(IFormCollection collection, ProductoUsuarioVM productoUsuarioVM)
        {
            var claimsidentity = (ClaimsIdentity)User.Identity;     //para pillar el usuario conectado
            var claim = claimsidentity.FindFirst(ClaimTypes.NameIdentifier);
            UsuarioApp userActual = _db.UsuarioApp.FirstOrDefault(u => u.Id == claim.Value);

            //Creamos el pedido
            Pedido pedido = new Pedido()
            {
                    UsuarioId = claim.Value,
                    PrecioTotal = productoUsuarioVM.ListaProducto.Sum(x =>x.Precio),
                    Telefono = userActual.PhoneNumber,
                    Login = userActual.UserName,
                    Email = userActual.Email,
                    FechaOrden = DateTime.Now,
                    EstadoVenta = WC.EstadoPendiente
                };

                _db.Pedido.Add(pedido);
                _db.SaveChanges();

            foreach (var prod in productoUsuarioVM.ListaProducto)
                {
                    PedidoDetalle pedidoDetalle = new PedidoDetalle()
                    {
                        PedidoId = pedido.Id,
                        ProductoId = prod.Id
                    };
                    _db.PedidoDetalles.Add(pedidoDetalle);
                }
                _db.SaveChanges();

                string nonceFromTheClient = collection["payment_method_nonce"];
                var request = new TransactionRequest
                {
                    Amount = Convert.ToDecimal(pedido.PrecioTotal),
                    PaymentMethodNonce = nonceFromTheClient,
                    OrderId = pedido.Id.ToString(),
                    Options = new TransactionOptionsRequest
                    {
                        SubmitForSettlement = true
                    }
                };

                var gateway = _brain.GetGateWay();
                Result<Transaction> result = gateway.Transaction.Sale(request);

                //Modificar el pedido

                if (result.Target.ProcessorResponseText == "Approved") //Transaccion correcta
                {
                    pedido.TransaccionId = result.Target.Id;
                    pedido.EstadoVenta = WC.EstadoAprobado;
                }
                else
                {
                    pedido.EstadoVenta = WC.EstadoCancelado;
                }
                _db.SaveChanges();

               
                //Creamos la Orden
                var rutaTemplate = _webHostEnvironment.WebRootPath + Path.DirectorySeparatorChar.ToString()
                                            + "templates" + Path.DirectorySeparatorChar.ToString()
                                            + "PlantillaOrden.html";

                var subject = "Nueva Oreden";
                string HtmlBody = "";

                using (StreamReader sr = System.IO.File.OpenText(rutaTemplate))
                {
                    HtmlBody = sr.ReadToEnd();
                }

                /*
                 * Nombre : {0}
                 * Email  : {1}
                 * Telefono : {2}
                 * Productos : {3}
                 */

                StringBuilder productoListaSB = new StringBuilder();

                foreach (var prod in productoUsuarioVM.ListaProducto)
                {
                    productoListaSB.Append($" - Nombre : {prod.Nombre} <span style='font-size:14px;'> (ID: {prod.Id})</span><br/>");
                }

                // string messageBody = string.Format(HtmlBody, productoUsuarioVM.UsuarioApp.Nombre,
                //                                             productoUsuarioVM.UsuarioApp.Apellidos,
                //productoUsuarioVM.UsuarioApp.UserName,
                //                                             productoUsuarioVM.UsuarioApp.Email,
                //                                             productoUsuarioVM.UsuarioApp.PhoneNumber,
                // productoListaSB.ToString());

                string messageBody = "AAAAAAAAAAAA";
                string correoUsuario = _db.UsuarioApp.FirstOrDefault(p => p.Id == claim.Value).Email;


				await _emailSender.SendEmailAsync(correoUsuario, subject, messageBody);





            return RedirectToAction(nameof(Confirmacion), new { id = pedido.Id });
        }

        public IActionResult Confirmacion(int id = 0)
        {
            Pedido pedido = _db.Pedido.FirstOrDefault(v => v.Id == id);
            HttpContext.Session.Clear(); //Para limpiar el carrito
            return View(pedido);
        }


        public IActionResult Quitar (int Id)
        {
            List<Carrito> carritoLista = new List<Carrito>();

            if (HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito) != null &&
                HttpContext.Session.Get<IEnumerable<Carrito>>(WC.SessionCarrito).Count() > 0)
            {
                carritoLista = HttpContext.Session.Get<List<Carrito>>(WC.SessionCarrito);
            }

            carritoLista.Remove(carritoLista.FirstOrDefault(p => p.ProductoId == Id));
            HttpContext.Session.Set(WC.SessionCarrito, carritoLista);
            return RedirectToAction("Index");
        }
        public IActionResult Limpiar()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

}
