﻿using CQRS.Domain.Customers.Orders;
using CQRS.Domain.Customers.Orders.Events;
using CQRS.Domain.Customers.Rules;
using CQRS.Domain.ForeignExchange;
using CQRS.Domain.Products;
using CQRS.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRS.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public CustomerId Id { get; private set; }

        private readonly string _email;

        private readonly string _name;

        private readonly List<Order> _orders;

        private bool _welcomeEmailWasSent;

        public Customer(bool welcomeEmailWasSent)
        {
            _welcomeEmailWasSent = welcomeEmailWasSent;
        }

        private Customer()
        {
            _orders = new List<Order>();
        }

        private Customer(string email, string name)
        {
            Id = new CustomerId(Guid.NewGuid());
            _email = email;
            _name = name;
            _welcomeEmailWasSent = false;
            _orders = new List<Order>();

            AddDomainEvent(new CustomerRegisteredEvent(Id));
        }

        public static Customer CreateRegistered(
            string email,
            string name,
            ICustomerUniquenessChecker customerUniquenessChecker)
        {
            CheckRule(new CustomerEmailMustBeUniqueRule(customerUniquenessChecker, email));

            return new Customer(email, name);
        }

        public OrderId PlaceOrder(
            List<OrderProductData> orderProductsData,
            List<ProductPriceData> allProductPrices,
            string currency,
            List<ConversionRate> conversionRates)
        {
            CheckRule(new CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule(_orders));
            CheckRule(new OrderMustHaveAtLeastOneProductRule(orderProductsData));

            var order = Order.CreateNew(orderProductsData, allProductPrices, currency, conversionRates);

            _orders.Add(order);

            AddDomainEvent(new OrderPlacedEvent(order.Id, Id, order.GetValue()));

            return order.Id;
        }

        public void ChangeOrder(
            OrderId orderId,
            List<ProductPriceData> existingProducts,
            List<OrderProductData> newOrderProductsData,
            List<ConversionRate> conversionRates,
            string currency)
        {
            CheckRule(new OrderMustHaveAtLeastOneProductRule(newOrderProductsData));

            var order = _orders.Single(x => x.Id == orderId);
            order.Change(existingProducts, newOrderProductsData, conversionRates, currency);

            AddDomainEvent(new OrderChangedEvent(orderId));
        }

        public void RemoveOrder(OrderId orderId)
        {
            var order = _orders.Single(x => x.Id == orderId);
            order.Remove();

            AddDomainEvent(new OrderRemovedEvent(orderId));
        }

        public void MarkAsWelcomedByEmail()
        {
            _welcomeEmailWasSent = true;
        }
    }
}