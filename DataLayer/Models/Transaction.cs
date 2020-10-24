using DataLayer.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DataLayer.Models
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public Type Type { get; set; }
        public int TypeId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
    }
}
