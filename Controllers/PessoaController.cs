using ExerciciosH1.Classes;
using Microsoft.AspNetCore.Mvc;

namespace ExerciciosH1.Controllers
{
    public class PessoaController : Controller
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        [HttpPost]
        public IActionResult Adicionar(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
            return Ok("Pessoa adicionada com sucesso.");
        }

        [HttpPut]
        public IActionResult Atualizar(string cpf, Pessoa pessoaAtualizada)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }
            pessoa.Nome = pessoaAtualizada.Nome;
            pessoa.Peso = pessoaAtualizada.Peso;
            pessoa.Altura = pessoaAtualizada.Altura;
            return Ok("Pessoa atualizada com sucesso.");
        }

        [HttpDelete]
        public IActionResult Remover(string cpf)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }
            pessoas.Remove(pessoa);
            return Ok("Pessoa removida com sucesso.");
        }

        [HttpGet]
        public IActionResult BuscarTodas()
        {
            return Ok(pessoas);
        }

        [HttpGet("{cpf}")]
        public IActionResult BuscarPorCpf(string cpf)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }
            return Ok(pessoa);
        }

        [HttpGet("imc-bom")]
        public IActionResult BuscarPorIMC()
        {
            var pessoasComIMCBom = pessoas.Where(p =>
                p.CalcularIMC() >= 18 && p.CalcularIMC() <= 24).ToList();
            return Ok(pessoasComIMCBom);
        }

        [HttpGet("buscar-por-nome/{nome}")]
        public IActionResult BuscarPorNome(string nome)
        {
            var pessoasEncontradas = pessoas.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(pessoasEncontradas);
        }
    }
}
