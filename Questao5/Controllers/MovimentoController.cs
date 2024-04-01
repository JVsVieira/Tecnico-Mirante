using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Dtos;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimentoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimentoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentar-conta-corrente")]
        public async Task<IActionResult> AddMovimentoAsync(MovimentoRequestDto movimentoRequestDto)
        {
            var response = await _mediator.Send(new CreateMovimentoCommand(
                movimentoRequestDto.IdRequisicao,
                movimentoRequestDto.IdContaCorrente,
                movimentoRequestDto.Valor,
                movimentoRequestDto.TipoMovimento
                ));
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
            
        }
    }
}