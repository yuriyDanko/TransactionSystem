using BusinessLayer.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Models
{
    public class Status : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
