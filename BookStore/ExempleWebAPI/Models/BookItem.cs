using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Autor { get; set; }
        public bool IsRead { get; set; }
    }
}
