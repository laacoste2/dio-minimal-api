using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Products.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Nationality { get; set; }

        public Product(Guid id, string name, string description, decimal price,string nationality)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Nationality = nationality;
        }

        public Product(string name, string description, decimal price, string nationality)
        {
            Name = name;
            Description = description;
            Price = price;
            Nationality = nationality;
        }

    }
}
