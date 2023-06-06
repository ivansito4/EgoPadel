using System.Drawing;

namespace EgoPadel
{
    public static class WC  //Aqui están los atributos estáticos
    {

        public static string SessionCarrito = "SessionCarrito";

        //Los distintos roles
        public static string AdminRol = "Admin";
        public static string UsuarioRol = "Usuario";

        //Rutas imagenes
        public static string FotoEscudo = @"\images\Equipos\";
        public static string FotoProducto = @"\images\Productos\";
        

        //Mensajes
        public const string Exitoso = "Exitoso";
        public const string Error = "Error";

        //Estados de Pedido
        public const string EstadoPendiente = "Pendiente";
        public const string EstadoAprobado = "Aprobado";
        public const string EstadoEnProceso = "Procesando";
        public const string EstadoEnviado = "Enviado";
        public const string EstadoCancelado = "Cancelado";
        public const string EstadoDevuelto = "Devuelto";
    }
}
