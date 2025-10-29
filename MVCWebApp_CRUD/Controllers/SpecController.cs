using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp_CRUD.Controllers
{
    [Route("/myspecABC/{action}")] //dac thu rieng cho controller
    public class SpecController : Controller
    {
        [BindProperty(SupportsGet =true)]
        public int SpecId { get; set; }//property cua Controller

        public string Index(int? id, string name)
            //ten bien id trung voi key tren querystring/routedata/the name cua the input cua form
        {
            return $"Spec {SpecId} - Index: id={id} - name={name}";
        }

        public string List()
        {
            return $"Spec {SpecId} - List";
        }

        [Route("/abc/def")]//dac thu cho 1 action trong 1 controller
        public string Detail()
        {
            return $"Spec  {SpecId} - Detail";
        }
    }
}
