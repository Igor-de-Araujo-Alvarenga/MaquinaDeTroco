using Domain;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MaquinaDeTroco.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly ICashRegisterUtils _machineUtils;
        private readonly ICashRegister _cashRegister;
        private readonly IRepository<Money> _repo;
        public RegisterController(ICashRegisterUtils machineUtils, ICashRegister cashRegister,
            IRepository<Money> repo)
        {
            _machineUtils = machineUtils;
            _cashRegister = cashRegister;
            _repo = repo;
        }

        [HttpPost]
        [Route("GerarTroco")]
        public IActionResult Cash([FromBody] InputMoney model)
        {
            try
            {
                _machineUtils.Init();
                if (_machineUtils.IsValid(model))
                {
                    var listMoney = _cashRegister.GenerateCash(model);
                    if (listMoney.Count == 0)
                        return BadRequest(JsonSerializer.Serialize(ErroResponse.Message = "Não há moedas sufucientes no caixa"));
                    return Ok(listMoney);
                }
                else
                {

                    return BadRequest(JsonSerializer.Serialize(ErroResponse.Message = "Não é necessário retornar trocos."));
                }
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Route("InserirMoedas")]
        public IActionResult InsertMoney([FromBody] IList<Money> listMoney)
        {
            try
            {
                if (listMoney.Count <= 0)
                    return BadRequest("Valor Invalido");

                foreach (var money in listMoney)
                {
                    if (money.MoneyValue <= 0 || money.Quantity <= 0)
                        continue;

                    _machineUtils.VerifyAndInsert(money);
                }

                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Sangria")]
        public IActionResult Sangria()
        {
            try
            {
                return Ok(_repo.GetAll());
            }catch(Exception ex)
            {
                return StatusCode(500);
            }
            
        }
    }
}
