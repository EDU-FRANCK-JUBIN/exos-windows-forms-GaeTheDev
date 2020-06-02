using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models {
    public class BookStore : IBookStore {

        public List<BookItem> Store { get; } = new List<BookItem>();

        public int AddBook(BookItem book) {
            book.Id = Store.Any() ? Store.Max(b => b.Id) +1 : 1;
            Store.Add(book);
            return book.Id;
        }

    }
}
