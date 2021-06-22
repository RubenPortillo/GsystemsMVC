using GSystemsMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GSystemsMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private static List<EmpleadoViewModel> Empleados;
        private static List<IncidenciaViewModel> Incidencias;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }



        public IActionResult Index()
        {
            var token = GetTokenFromSesssion();

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = client
                .GetAsync("https://localhost:44393/api/empleados/")
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                Empleados = JsonConvert.DeserializeObject<List<EmpleadoViewModel>>(content);

                return View(Empleados);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Info(string idUser)
        {
            var token = GetTokenFromSesssion();

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(idUser))
            {
                return View(new EmpleadoViewModel());
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = client
                .GetAsync($"https://localhost:44393/api/incidencias/idUser=" + idUser)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                Incidencias = JsonConvert.DeserializeObject<List<IncidenciaViewModel>>(content);
                ViewBag.Empleado = Empleados.Find(x => x.ID == idUser);

                return View(Incidencias);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult CrearIncidencia(string IncidenciaDesc, string Ubicacion,string FechaInc, string IncidenciaAsignadaHidden, int Prioridad)
        {
            var token = GetTokenFromSesssion();

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            if (string.IsNullOrEmpty(IncidenciaDesc) || string.IsNullOrEmpty(Ubicacion) || string.IsNullOrEmpty(FechaInc) || string.IsNullOrEmpty(IncidenciaAsignadaHidden) ||
                Prioridad == -1)
            {
                return View(new EmpleadoViewModel());
            }

            var incidenciaDesc = IncidenciaDesc;
            var ubicacion = Ubicacion;
            var fIncidencia = DateTime.Parse(FechaInc).ToString("yyyy-MM-dd'T'HH:mm:ss");
            var empleadoID = Convert.ToInt32(IncidenciaAsignadaHidden);
            var prioridad = Prioridad;


            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var contentPost = $"{{\"incidenciaDesc\":\"{incidenciaDesc}\",\"ubicacion\":\"{ubicacion}\" ,\"fIncidencia\":\"{fIncidencia}\",\"empleadoID\":{empleadoID},\"prioridad\":{prioridad}}}";

            var response = client
                .PostAsync($"https://localhost:44393/api/incidencias/", new StringContent(contentPost, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                var incidencia = JsonConvert.DeserializeObject<IncidenciaViewModel>(content);
                Incidencias.Add(incidencia);
                ViewBag.Empleado = Empleados.Find(x => x.ID == IncidenciaAsignadaHidden);

                return View("Info", Incidencias);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            var token = GetTokenFromSesssion();

            if (string.IsNullOrEmpty(token))
            {
                return new JsonResult(new { StatusCode = 401 });
            }

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = client
                .DeleteAsync($"https://localhost:44356/api/reminders/{id}")
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(null);
            }
            else
            {
                return new JsonResult(new { StatusCode = response.StatusCode, Value = response.ReasonPhrase });
            }
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            try
            {
                var client = _clientFactory.CreateClient();
                var content = $"{{\"mail\":\"{email}\",\"password\":\"{password}\"}}";
                var response = client
                    .PostAsync("https://localhost:44393/api/empleadologin/authenticate", new StringContent(content, Encoding.UTF8, "application/json"))
                    .ConfigureAwait(false).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var token = response.Content.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    var claims = new List<Claim> { new Claim(ClaimTypes.Email, email) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var props = new AuthenticationProperties();

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                    HttpContext.Session.SetString("ExpiryToken", token);

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetTokenFromSesssion()
        {
            return HttpContext.Session.GetString("ExpiryToken");
        }
    }
}
