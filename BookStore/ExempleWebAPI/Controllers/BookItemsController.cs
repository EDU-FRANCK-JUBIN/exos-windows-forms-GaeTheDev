using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;

namespace BookStore.Controllers
{
    [Route("api/v1/books")]
    public class BookItemsController : ControllerBase
    {

        private IBookStore bookStore;

        public BookItemsController(IBookStore bookStore) {
            this.bookStore = bookStore;
        }
                
        [HttpGet]
        public BookItem[] GetBookItems() {
            return bookStore.Store.ToArray();
        }


        [HttpGet("{id}")]
        public ActionResult<BookItem> GetBookItem(int id)
        {
            BookItem bookItem = null;

            if (bookItem == null)
            {
                return NotFound();
            }
            return bookItem;
        }


        [HttpPost]
        public ActionResult<BookItem> AddBookItem(BookItem bookItem)
        {
            bookItem.Id = bookStore.AddBook(bookItem);
            return CreatedAtAction(nameof(GetBookItem), new { bookItem.Id }, bookItem);
        }

        
    }
}
