using BusinessLayer.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BusinessLayer.Models
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public Type Type { get; set; }
        public Client Client { get; set; }
        public decimal Amount { get; set; }
    }
}
