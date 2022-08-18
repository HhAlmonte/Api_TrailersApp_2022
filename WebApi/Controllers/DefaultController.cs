using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class DefaultController : BaseController
    {
        [HttpGet]
        public string Get()
        {
            return "Aplicación corriendo";
        }
    }
}
