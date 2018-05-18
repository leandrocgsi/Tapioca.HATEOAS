using System.Collections.Generic;

namespace Tapioca.HATEOAS
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ObjectContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}