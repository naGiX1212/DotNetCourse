using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models.ViewModels
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1,1000,ErrorMessage="PLEASE ENTER A AVALUE BETWEEN 1 AND 100")]
        public int Count { get; set; }
    }
    
}
