using System.Collections.Generic;
using RESTFulSampleServer.Model;
using RESTFulSampleServer.Repository.Generic;
using RESTFulSampleServer.Data.Converters;
using RESTFulSampleServer.Data.VO;

namespace RESTFulSampleServer.Business.Implementattions
{
    public class BookBusinessImpl : IBookBusiness
    {
        private IRepository<Book> _repository;

        private readonly BookConverter _converter;

        public BookBusinessImpl(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public BookVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public List<BookVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return _repository.Exists(id);
        }
    }
}