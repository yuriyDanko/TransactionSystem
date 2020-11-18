using Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
