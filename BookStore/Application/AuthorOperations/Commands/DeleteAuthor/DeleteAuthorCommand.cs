using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            var authorBooks = _dbContext.Books.SingleOrDefault(x => x.AuthorId == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Silinecek yazar bulunamadı!");

            if (authorBooks is not null)
                throw new InvalidOperationException(author.Name + " " + author.Surname + " adlı yazarı silmeden önce kitabını siliniz!");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
