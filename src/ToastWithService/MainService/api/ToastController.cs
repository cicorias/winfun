using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Windows.UI.Notifications;

//

namespace HelloService.api
{
    public class ToastController : ApiController
    {
        // GET api/values 
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5 
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values 
        public void Post([FromBody]string value)
        {
           new ToastManager().SetupToast(value);
        }

        //// PUT api/values/5 
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5 
        //public void Delete(int id)
        //{
        //}



    }
}
