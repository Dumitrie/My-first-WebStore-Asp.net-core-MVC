using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domeins;

namespace WebStore.BusinessLogic
{
    public class Cart
    {
        public string ReturUrl { get; set; }
        private readonly List<CartRecord> listRecords;

        public List<CartRecord> Records => listRecords;

        public Cart()
        {
            listRecords = new List<CartRecord>();
        }

        public virtual void Add(Product product, int quantity = 1)
        {
            CartRecord record = listRecords
                .FirstOrDefault(p => p.Product.Id == product.Id);
            if (record != null)
            {
                record.Quantity += quantity;
            }
            else
            {
                record = new CartRecord
                {
                    Product = product,
                    Quantity = quantity
                };
                listRecords.Add(record);
            }
        }

        public virtual void Remove(Product product)
        {
            CartRecord record = listRecords
                .FirstOrDefault(p => p.Product.Id == product.Id);
            if (record == null)
            {
                return;
            }

            record.Quantity--;
            if (record.Quantity == 0)
            {
                listRecords.Remove(record);
            }
        }

        public virtual void Clear()
        {
            listRecords.Clear();
        }
        public long TotalCost => listRecords.Sum(r => (long)r.Product.Price * r.Quantity);
    }

    public class CartRecord
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public long Cost => (long) (Product.Price * Quantity);
    }
}
