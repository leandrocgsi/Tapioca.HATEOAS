# Tapioca.HATEOAS
This is a smart library to implements HATEOAS pattern in your RESTFul API's, implemented based in [this project](https://github.com/SotirisH/HyperMedia).

> ## How to use

>### 1 - Import Tapioca.HATEOAS to your projetct
#### Import with command line
```bash
Install-Package Tapioca.HATEOAS -Version 1.0.4
```

#### Import with nuget package manager

![Nuget Package Mannager](https://github.com/leandrocgsi/Tapioca.HATEOAS/blob/master/images/nuget_package_mannager.png?raw=true?raw=true)

>### 2 - Implements *ISupportsHyperMedia* in your exposed object.

```csharp
namespace RESTFulSampleServer.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public long? Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public DateTime LaunchDate { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
```

>### 3 - Implements your enricher with *ObjectContentResponseEnricher<T>*.

```csharp
namespace RESTFulSampleServer.HyperMedia
{
    public class BookEnricher : ObjectContentResponseEnricher<BookVO>
    {
        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/books/v1";
            var url = new { controller = path, id = content.Id };

            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = urlHelper.Link("DefaultApi", url),
                Rel = RelationType.self,
                Type = RensponseTypeFormat.DefaultGet
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = urlHelper.Link("DefaultApi", url),
                Rel = RelationType.self,
                Type = RensponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = urlHelper.Link("DefaultApi", url),
                Rel = RelationType.self,
                Type = RensponseTypeFormat.DefaultPost
            });
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = urlHelper.Link("DefaultApi", url),
                Rel = RelationType.self,
                Type = "int"
            });
            return null;
        }
    }
}
```

>### 4 - Add annotation *[TypeFilter(typeof(HyperMediaFilter))]* to your controller methods.

```csharp
namespace RESTFulSampleServer.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksController : Controller
    {
        private IBookBusiness _bookBusiness;

        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        //Add HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return new OkObjectResult(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        //Add HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null) return NotFound();
            return new OkObjectResult(book);
        }

        [HttpPost]
        //Add HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody]BookVO book)
        {
            if (book == null) return BadRequest();
            return new OkObjectResult(_bookBusiness.Create(book));
        }

        [HttpPut]
        //Add HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody]BookVO book)
        {
            if (book == null) return BadRequest();
            var updatedBook = _bookBusiness.Update(book);
            if (updatedBook == null) return BadRequest();
            return new OkObjectResult(updatedBook);
        }

        [HttpDelete("{id}")]
        //Add HyperMediaFilter
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }
    }
}
```

>### 5 - Add *HyperMediaFilterOptions* to your startup.

```csharp
    var filtertOptions = new HyperMediaFilterOptions();
    filtertOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
    filtertOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
    services.AddSingleton(filtertOptions);
```

>### 6 - Add a *MapRoute* to your route like was defined in your enricher.

```csharp
    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "DefaultApi",
            template: "{controller=Values}/{id?}");
    });
```
>### 7 - Enjoy

#### Response as JSON

![Response As JSON](https://github.com/leandrocgsi/Tapioca.HATEOAS/blob/master/images/response_in_json.png?raw=true)

#### Response as XML

![Response As XML](https://github.com/leandrocgsi/Tapioca.HATEOAS/blob/master/images/response_in_xml.png?raw=true)

>### Suggestions are welcome. Feel free to sugest improvments.
