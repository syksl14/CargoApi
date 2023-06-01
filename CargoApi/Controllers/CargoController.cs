using CargoApi.Model;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CargoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CargoController : ControllerBase
    { 

        private readonly ILogger<CargoController> _logger;
        private readonly IConfiguration _config;
        public CargoController(ILogger<CargoController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("/cargo/login", Name = "Login")]
        public ActionResult Login([FromBody] UserLogin userLogin)
        { 
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return new JsonResult(new { success = false, errors = "Invalid username/password" });
        }

        [Authorize(Roles = "User")]
        [HttpGet("/cargo/ups/{Waybill}", Name = "Cargo")]

        public IEnumerable<Data> GetCargoUpsDetail(string Waybill)
        {
            List<Data> data = new List<Data>();

            List<UpsLog> upsLogs = new List<UpsLog>();
            using var client = new HttpClient();
            var content = client.GetStringAsync("https://www.ups.com.tr/WaybillSorgu.aspx?Waybill=" + Waybill); 
            
            var dom = new HtmlDocument();
            dom.LoadHtml(content.Result);
            var table = dom.DocumentNode.SelectNodes("//table[@id='ctl00_MainContent_DataListSonIslem']//table");
            if(table != null)
            {
                var i = 0;
                foreach (HtmlNode item in table)
                {
                    if (i > 0)
                    {
                        upsLogs.Add(new Model.UpsLog(item.SelectNodes("./tr//td//span")[0].InnerText, item.SelectNodes("./tr//td//span")[1].InnerText, item.SelectNodes("./tr//td//span")[2].InnerText));
                    }
                    i++;
                }

                String sonDurum = String.Format("{0} tarihinde: {1} konumunda, {2}.", upsLogs[0].Tarih, upsLogs[0].IslemYeri, Cevir(upsLogs[0].Bilgi!));

                data.Add(new Model.Data(upsLogs, sonDurum));
            }
            else
            {
                data.Add(new Model.Data(upsLogs, Waybill + " ile eþleþen bir kargo bulunamadý."));
            }
          
            return data;
        }

        private String Cevir(String input)
        {
            if (input.Contains("ALICININ ISTEGI ILE BASKA GUMRUKCUYE VERILDI"))
            {
                return "Alýcýnýn isteði ile baþka gümrükçüye verildi";
            }
            else
            {
                return input;
            }
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = UserConstants.Users.FirstOrDefault(x => x.Username.ToLower() ==
                userLogin.Username.ToLower() && x.Password == userLogin.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}