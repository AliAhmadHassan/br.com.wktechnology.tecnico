using Com.WkTechnology.Tecnico.Domain.DTO.Category;
using System;

namespace Com.WkTechnology.Tecnico.Domain.DTO.Product
{
    public class ProductDTO : BaseDTO
    {
        /// <summary> 
        /// pt-BR: Nome 
        /// </summary> 
        public string Name { get; set; }

        /// <summary> 
        /// pt-BR: Descrição 
        /// </summary> 
        public string Description { get; set; }

        /// <summary> 
        /// pt-BR: Preço unitário 
        /// </summary> 
        public decimal UnitPrice { get; set; }

        /// <summary> 
        /// pt-BR: Quantidade 
        /// </summary> 
        public decimal Amount { get; set; }

        /// <summary> 
        /// pt-BR: Categoria 
        /// </summary> 
        public CategoryDTO Category { get; set; }
    }
}
