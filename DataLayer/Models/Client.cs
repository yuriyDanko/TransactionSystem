using BusinessLayer.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Client : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
