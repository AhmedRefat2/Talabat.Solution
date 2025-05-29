using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public decimal Price { get; set; }

        // 1.Brand Relationship
        public int BrandId{ get; set; }
        public ProductBrand Brand { get; set; } = null!;

        // 2.Category Relationship
        public int CategoryId{ get; set; }
        public ProductCategory Category { get; set; } = null!;
    }
}
