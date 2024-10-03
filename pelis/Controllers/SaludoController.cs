using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace pelis.Controllers
{
    public class SaludoController : Controller
    {
       /* public IActionResult Index()
        {           
            return View();
        }*/

        public string Index()
        {
            return "Hola 🙂";
        }

        public string en()
        {
            return "Hello world 🌎";
        }

        public string es()
        {
            return "Hola mundo 🌎 !";
        }

        public string multiple(string nombre, int x)
        {
            string result = "", salida;
            for (int i = 1; i <= x; i++)
            {
                salida = HtmlEncoder.Default.Encode($"{i} Hola {nombre}");
                result = result + salida + "\n";
            }
            return result;
        }

        public IActionResult multiplevista()
        {
            return View();
        }

    }
}
