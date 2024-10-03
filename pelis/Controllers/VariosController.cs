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
            return View();
        }

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


        [HttpGet]
        public IActionResult MostrarPregunta()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MostrarPregunta(string respuesta)
        {
            string respuestaCorrecta = "opcion2";

            bool esCorrecta = respuesta == respuestaCorrecta;

            ViewBag.EsCorrecta = esCorrecta;

            return View("Resultado2");
        }


        [HttpGet]
        public IActionResult EnviarCorreo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EnviarCorreo(string toEmail, string subject, string body)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("juan.urrego482@pascualbravo.com", "haauxvitanmvxfsn"), // Cambia por tus credenciales
                        EnableSsl = true,
                    };


                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("juan.urrego482@pascualbravo.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(toEmail);

                    await smtpClient.SendMailAsync(mailMessage);

                    ViewBag.Message = "Correo enviado exitosamente.";
                }
                catch (SmtpException ex)
                {
                    ViewBag.Message = $"Error al enviar correo: {ex.Message}";
                }
            }
            return View();
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
                ViewBag.Error = "Por favor ingrese un número de teléfono válido.";
                return View("Whtas");
            }

            string whatsappUrl = $"https://wa.me/{phoneNumber}";
            ViewBag.WhatsAppUrl = whatsappUrl;

            return View("Whtas");
        }

        private const string ValidUsername = "usuario";
        private const string ValidPassword = "12345";

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


    }
}

