using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace M4WS_sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginManagerController : ControllerBase
    {
        private M4LoginManager.MicroareaLoginManagerSoapClient getLoginManager()
        {
            M4LoginManager.MicroareaLoginManagerSoapClient m4Login = new M4LoginManager.MicroareaLoginManagerSoapClient(M4LoginManager.MicroareaLoginManagerSoapClient.EndpointConfiguration.MicroareaLoginManagerSoap);

            m4Login.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://localhost/mago4/LoginManager/LoginManager.asmx");
            
            return m4Login;
        }

        [HttpGet("isAlive")]
        public async Task<ActionResult<bool>> IsAlive()
        {
            try 
            {
                using (M4LoginManager.MicroareaLoginManagerSoapClient m4Login = getLoginManager())
                {
                    var data = await m4Login.IsAliveAsync();
                    return data;
                }
            }
            catch (System.Exception ex)
            {
                ContentResult err = Content(ex.Message);
                err.StatusCode = 500;
                return err;
            }
        }
    }
}