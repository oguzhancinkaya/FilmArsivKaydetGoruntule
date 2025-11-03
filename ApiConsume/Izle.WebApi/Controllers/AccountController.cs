using Izle.BusinessLayer.Abstract;
using Izle.BusinessLayer.ExternalApi;
using Izle.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Izle.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly TmdbApiService _tmdbApiService;

        public AccountController(IAccountService accountService, TmdbApiService tmdbApiService)
        {
            _accountService = accountService;
            _tmdbApiService = tmdbApiService;
        }

        // 🔹 1. Tüm filmleri getir
        [HttpGet]
        public IActionResult AccountList()
        {
            var values = _accountService.TGetList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddAccount(Account account)
        {
            _accountService.TInsert(account);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            var values = _accountService.TGetByID(id);
            _accountService.TDelete(values);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateAccount(Account account)
        {
            _accountService.TUpdate(account);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetAccount(int id)
        {
            var values = _accountService.TGetByID(id);
            return Ok(values);
        }
    }
}
