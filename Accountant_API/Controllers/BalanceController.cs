using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Interfaces;

namespace Accountant_API.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase,IToken
    {
        private readonly IBalanceRepository _repository;

        public BalanceController(IBalanceRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Balance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Balance>>> GetBalances()
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var balances = await _repository.GetAll(user.Id);

            if (balances == null)
            {
                return NotFound();
            }

            return Ok(balances);
        }

        // GET: api/Balance
        [HttpGet]
        [Route("GetBalancesFromYear/{anualBalanceId}")]
        public async Task<ActionResult<IEnumerable<Balance>>> GetBalancesFromAnualBalance(int anualBalanceId)
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var balances = await _repository.BalancesFromYear(anualBalanceId,user.Id);

            if (balances == null)
            {
                return NotFound();
            }

            return Ok(balances);
        }

        // GET: api/Balance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> GetBalance(int id)
        {
            var user = await _repository.ValidUser(GetTokenUserId());

            if (user == null)
            {
                return NotFound();
            }

            var balance = await _repository.Get(id,user.Id);

            if (balance == null)
            {
                return NotFound();
            }

            return Ok(balance);
        }

        // PUT: api/Balance/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBalance(int id, Balance balance)
        {
            try
            {
                if (id != balance.Id)
                {
                    return BadRequest();
                }

                if (await _repository.Update(balance) == null)
                {
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception)
            {
                if (!_repository.Exist(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        public int GetTokenUserId()
        {
            //NameIdentifier has the UserId value in the token.
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }

}
