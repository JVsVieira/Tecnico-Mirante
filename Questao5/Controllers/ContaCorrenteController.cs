using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<List<ContaCorrente>> GetContaCorrenteListAsync()
        {
            var ContaCorrente = await _mediator.Send(new GetContaCorrenteListQuery());

            return ContaCorrente;
        }

        [HttpGet("consultar-saldo")]
        public async Task<IActionResult> ConsultarSaldoAsync(string contaCorrenteId)
        {
            var response = await _mediator.Send(new ConsultarSaldoContaCorrenteQuery() { Id = contaCorrenteId });
            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}