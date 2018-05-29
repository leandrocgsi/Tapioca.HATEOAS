using System.Collections.Generic;
using Tapioca.HATEOAS.Abstract;

namespace Tapioca.HATEOAS.Filters
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ObjectContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}