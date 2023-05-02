﻿using Catalogo_Blazor.Shared.Recursos;

namespace Catalogo_Blazor.Server.Utils
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginar<T>(this IQueryable<T> queryable, Paginacao paginacao)
        {
            return queryable
                .Skip((paginacao.Pagina - 1) * paginacao.QuantidadePagina)
                .Take(paginacao.QuantidadePagina);
        }
    }
}
