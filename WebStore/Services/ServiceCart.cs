using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WebStore.BusinessLogic;
using WebStore.Domeins;
using WebStore.Helpers;

namespace WebStore.Services
{
    public class ServiceCart : Cart
    {
        private ISession _session;

        public static Cart GEtCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider
                .GetRequiredService<IHttpContextAccessor>()
                .HttpContext.Session;
            ServiceCart serviceCart = session.GetSubject<ServiceCart>("Cart") ?? new ServiceCart();

            serviceCart._session = session;

            return serviceCart;
        }

        public override void Add(Product product, int quantity = 1)
        {
            base.Add(product, quantity);
            _session.SetSubject("Cart", this);
        }

        public override void Remove(Product product)
        {
            base.Remove(product);
            _session.SetSubject("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            _session.SetSubject("Cart", this);
        }
    }
}
