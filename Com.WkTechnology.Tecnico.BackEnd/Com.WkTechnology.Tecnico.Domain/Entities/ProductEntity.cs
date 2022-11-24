using System;
using System.Collections.Generic;
using System.Text;


namespace Com.WkTechnology.Tecnico.Domain.Entities
{
    public class ProductEntity: BaseEntity
    {

        /// <summary> 
        /// pt-BR: Nome 
        /// </summary> 
        public String Name { get; set; }

        /// <summary> 
        /// pt-BR: Descrição 
        /// </summary> 
        public String Description { get; set; }

        /// <summary> 
        /// pt-BR: Preço unitário 
        /// </summary> 
        public Decimal UnitPrice { get; set; }

        /// <summary> 
        /// pt-BR: Quantidade 
        /// </summary> 
        public Decimal Amount { get; set; }

        /// <summary> 
        /// pt-BR: Categoria 
        /// </summary> 
        public CategoryEntity Category { get; set; }
    }
}