using System.Collections.Generic;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    public class ValuesController : ApiController
    {

        private static Dictionary<int, string> Values = new Dictionary<int, string>();
        // GET api/values
        /// <summary>
        /// Gets some very important data from the server.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return Values.Values;
        }

        // GET api/values/5
        public string Get(int id)
        {
            Values.TryGetValue(id, out string value);
            return value;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            var id = Values.Count;
            Values.Add(++id, value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            if (Values.ContainsKey(id))
            {
                Values[id] = value;
            }
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Values.Remove(id);
        }
    }
}
