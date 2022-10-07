using System;
using System.Collections.Generic;
using System.Text;

namespace sg.customer
{
    public class Customer
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }

    }
}
