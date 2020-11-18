using Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Status : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
