using Catalogo_Blazor.Shared.Recursos;
using Catalogo_Blazor.Server.Context;
using Catalogo_Blazor.Server.Utils;
using Catalogo_Blazor.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Produto>>> Get()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            return produtos;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> Get([FromQuery] Paginacao paginacao, [FromQuery] string? nomeFiltro)
        {
            var queryable = _context.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(nomeFiltro))
            {
                queryable = queryable.Where(x => x.Nome.ToUpper().Contains(nomeFiltro.ToUpper()));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, paginacao.QuantidadePagina);

            return await queryable.Paginar(paginacao).ToListAsync();

        }

        [HttpGet("{id}", Name ="GetProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            return await _context.Produtos
                .FirstOrDefaultAsync(x => x.ProdutoId == id);
        }

        [HttpGet("categorias/{id}")]
        public async Task<ActionResult<List<Produto>>> GetProdutoPorCategoria(int id)
        {
            return await _context.Produtos
                .Where(p=>p.CategoriaId == id).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetProduto",
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut]
        public async Task<ActionResult<Produto>> Put(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;            
            _context.SaveChangesAsync();
            return Ok(produto);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(
                x=>x.ProdutoId==id);
            if (produto == null)
                return NotFound();

            _context.Remove(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }
    }
}
