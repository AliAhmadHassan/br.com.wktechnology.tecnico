using System;
using System.ComponentModel.DataAnnotations;

namespace Com.WkTechnology.Tecnico.Domain.Entities 
{
    public class BaseEntity {
        [Key]
        public Int64 Id { get; set; }
    }
}
