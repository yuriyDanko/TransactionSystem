using DataLayer.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Status : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
