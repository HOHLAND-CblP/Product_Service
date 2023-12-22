using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hohland_cblp.ShopBackend.Infrastructure.DbEntities
{
    public record Product_v1()
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Currency { get; init; }
        public int ProductType { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
