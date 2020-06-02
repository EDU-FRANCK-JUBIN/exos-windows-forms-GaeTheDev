using System.Collections.Generic;

namespace BookStore.Models {
    public interface IBookStore {

        List<BookItem> Store { get; }

        int AddBook (BookItem bookItem);

    }
}