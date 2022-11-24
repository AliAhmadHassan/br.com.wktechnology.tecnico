using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Com.WkTechnology.Tecnico.Domain.Entities
{
    public class CategoryEntity: BaseEntity
    {

        /// <summary> 
        /// pt-BR: Nome 
        /// </summary>
        public String Name { get; set; }

        /// <summary> 
        /// pt-BR: Descrição 
        /// </summary> 
        public String Description { get; set; }
    }
}