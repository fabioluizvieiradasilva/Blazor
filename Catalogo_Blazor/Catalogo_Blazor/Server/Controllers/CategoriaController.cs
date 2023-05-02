using Catalogo_Blazor.Server.Context;
using Catalogo_Blazor.Server.Utils;
using Catalogo_Blazor.Shared.Models;
using Catalogo_Blazor.Shared.Recursos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            this._context = context;

        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Categoria>>> Get()
        {
            return _context.Categorias.AsNoTracking()
                .OrderBy(order => order.Nome)
                .ToList();
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> Get([FromQuery] Paginacao paginacao, [FromQuery] string? nomeFiltro)
        {
            var queryable = _context.Categorias.AsQueryable();

            if (!string.IsNullOrEmpty(nomeFiltro))
            {
                queryable = queryable.Where(x => x.Nome.ToUpper().Contains(nomeFiltro.ToUpper()));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, paginacao.QuantidadePagina);

            return await queryable.Paginar(paginacao).ToListAsync();
            
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(
                    c => c.CategoriaId == id
                );

            if (categoria == null)
                return NotFound();

            return categoria;
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            _context.Add(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCategoria",
                new { id = categoria.CategoriaId }, categoria);

        }

        [HttpPut]
        public async Task<ActionResult<Categoria>> Put(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(
                    c => c.CategoriaId == id
                );

            if (categoria == null)
                return NotFound();

            _context.Remove(categoria);
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }

    }
}
