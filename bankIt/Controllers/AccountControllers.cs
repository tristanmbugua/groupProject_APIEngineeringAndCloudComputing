using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using bankIt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using bankIt.Repository;

namespace bankIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountById(int id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null) return NotFound();

            var accountDto = _mapper.Map<AccountDetailsDto>(account);
            return Ok(accountDto);
        }

        [HttpPost]
        public async Task<ActionResult<AccountDetailsDto>> CreateAccount(AccountCreateDto accountCreateDto)
        {
            var account = _mapper.Map<Account>(accountCreateDto);
            await _accountRepository.CreateAccountAsync(account);
            var accountReadDto = _mapper.Map<AccountDetailsDto>(account);

            return CreatedAtAction(nameof(GetAccountById), new { id = accountReadDto.Id }, accountReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountUpdateDto accountUpdateDto)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null) return NotFound();

            _mapper.Map(accountUpdateDto, account);
            await _accountRepository.UpdateAccountAsync(account);

            return NoContent();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferFunds(TransferDto transferDto)
        {
            var success = await _transactionRepository.TransferAsync(transferDto);
            if (!success) return BadRequest("Transfer failed.");

            return Ok("Transfer successful.");
        }

        [HttpGet("{id}/monthlyCharges")]
        public async Task<ActionResult<List<MonthlyChargeDto>>> GetMonthlyCharges(int id)
        {
            var charges = await _accountRepository.GetMonthlyChargesByIdAsync(id);
            if (charges == null) return NotFound("No monthly charges found for this account.");

            var chargeDtos = _mapper.Map<List<MonthlyChargeDto>>(charges);
            return Ok(chargeDtos);
        }

    }
}
