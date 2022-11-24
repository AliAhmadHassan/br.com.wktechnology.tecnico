using System;
using System.ComponentModel.DataAnnotations;

namespace Com.WkTechnology.Tecnico.Domain.DTO.Category
{
    public class CategoryDTO: BaseDTO
    {
        /// <summary> 
        /// pt-BR: Nome 
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "{0} max lenght is 50")]
        public String Name { get; set; }

        /// <summary> 
        /// pt-BR: Descrição 
        /// </summary> 
        [StringLength(100, ErrorMessage = "{0} max lenght is 100")]
        public String Description { get; set; }
    }
}
