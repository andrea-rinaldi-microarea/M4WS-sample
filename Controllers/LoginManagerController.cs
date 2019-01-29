using System.Threading.Tasks;
using M4WS_sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace M4WS_sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginManagerController : ControllerBase
    {
        private IOptions<ConnectInfo> _connectInfo { get; }

        public LoginManagerController(IOptions<ConnectInfo> connectInfo)
        {
            _connectInfo = connectInfo;
        }

        private M4LoginManager.MicroareaLoginManagerSoapClient getLoginManager()
        {
            M4LoginManager.MicroareaLoginManagerSoapClient m4Login = new M4LoginManager.MicroareaLoginManagerSoapClient(M4LoginManager.MicroareaLoginManagerSoapClient.EndpointConfiguration.MicroareaLoginManagerSoap);

            m4Login.Endpoint.Address = new System.ServiceModel.EndpointAddress($"http://{_connectInfo.Value.Server}/{_connectInfo.Value.Instance}/LoginManager/LoginManager.asmx");
            
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

        [HttpGet("enumCompanies")]
        public async Task<ActionResult<string[]>> EnumCompanies(string user)
        {
            try 
            {
                using (M4LoginManager.MicroareaLoginManagerSoapClient m4Login = getLoginManager())
                {
                    var data = await m4Login.EnumCompaniesAsync(user);
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

        [HttpPost("login")]
        public async Task<ActionResult<M4LoginManager.LoginCompactResponse>> Login(LoginInfo info)
        {
            try 
            {
                using (M4LoginManager.MicroareaLoginManagerSoapClient m4Login = getLoginManager())
                {
                    var data = await m4Login.LoginCompactAsync(new M4LoginManager.LoginCompactRequest(info.User, info.Company, info.Password, "M4WSSample", true));
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