using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net.Mail;
using System.Net;



namespace pelis.Controllers
{
    public class VariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        /*public IActionResult Suma()
        {            
            return View();
        }*/
        public IActionResult Suma(int num1, int num2)
        {
            int resultado = num1 + num2;
            ViewBag.Num1 = num1;
            ViewBag.Num2 = num2;
            ViewBag.resultado = resultado;
            return View();
        }

        public IActionResult usuario(string name, string pin)
        {

            string user = name;
            string pass = pin;
            ViewBag.username = user;
            ViewBag.password = pass;

            user = "usuario12";
            pass = "1234";

            if (user != "usuario12" || pass != "1234")
            {

                ViewBag.user = "";
                ViewBag.pass = "";

            }
            else
            {

            }
            return View();

        }


        public IActionResult Crear()
        {
            return View();  // Esto debería cargar la vista Crear.cshtml
        }

        // Acción POST para procesar el formulario
        [HttpPost]
        public IActionResult Crear(string nombre, DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (fechaNacimiento > DateTime.Now.AddYears(-edad)) edad--;

            bool puedeVotar = edad >= 18;

            ViewBag.Nombre = nombre;
            ViewBag.FechaNacimiento = fechaNacimiento.ToShortDateString();
            ViewBag.PuedeVotar = puedeVotar;

            return View("Resultado");
        }


        // Acción GET para mostrar el formulario
        [HttpGet]
        public IActionResult MostrarPregunta()
        {
            return View();
        }

        // Acción POST para procesar la respuesta
        [HttpPost]
        public IActionResult MostrarPregunta(string respuesta)
        {
            // Definimos la respuesta correcta
            string respuestaCorrecta = "opcion2";

            // Verificar si la respuesta es correcta o incorrecta
            bool esCorrecta = respuesta == respuestaCorrecta;

            // Pasamos el resultado a la vista
            ViewBag.EsCorrecta = esCorrecta;

            return View("Resultado2");
        }





        [HttpGet]
        public IActionResult Whtas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Whtas(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                // Si el número es nulo o vacío, regresar la vista con un error.
                ViewBag.Error = "Por favor ingrese un número de teléfono válido.";
                return View("Whtas");
            }

            // Construir la URL de WhatsApp con el número de teléfono
            string whatsappUrl = $"https://wa.me/{phoneNumber}";
            ViewBag.WhatsAppUrl = whatsappUrl;

            return View("Whtas");
        }

        private const string ValidUsername = "usuario";  // Puedes cambiar el usuario válido
        private const string ValidPassword = "12345";    // Puedes cambiar la contraseña válida

        [HttpGet]
        public IActionResult Users()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Users(string username, string password)
        {
            if (username == ValidUsername && password == ValidPassword)
            {
                ViewBag.Message = "Acceso concedido";
            }
            else
            {
                ViewBag.Message = "Acceso denegado";
            }

            return View("Users");
        }




        private readonly IConfiguration _configuration;

        public VariosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult EnviarCorreo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarCorreo(string toEmail, string subject, string body)
        {
            if (string.IsNullOrEmpty(toEmail) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
            {
                ViewBag.Message = "Por favor, complete todos los campos.";
                return View("EnviarCorreo");
            }

            try
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = "smtp.tuservidor.com";
                    smtpClient.Port = 587;  // O el puerto que corresponda
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("tuusuario@tuservidor.com", "tucontraseña");

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("tuusuario@tuservidor.com");
                    mailMessage.To.Add("destinatario@dominio.com");
                    mailMessage.Subject = "Asunto del correo";
                    mailMessage.Body = "Contenido del correo";

                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        ViewBag.Message = "Correo enviado exitosamente.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = $"Error al enviar el correo: {ex.Message}";
                    }

                }
                return View("EnviarCorreo");
            }
        }
    }
}
    
