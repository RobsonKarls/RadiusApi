using Freedom.Domain.Entities;
using Freedom.Domain.Enum;
using Freedom.Infrastructure.DataAccess.Repositories;
using Machine.Specifications;

namespace Freedom.Infrastructure.Test.Repositories
{
    [Subject(typeof(Order))]
    public class When_adding_an_order : DatabaseSpec
    {
        static Repository<Order> _repository;
        static Order _order;
        static User _user;
        static OrderItem _items;
        static Product _product;
        static UserRepository _userRepository;
        static Repository<Product> _productRepository;
        static Repository<OrderItem> _ordeRepository;

        Establish _context = () =>
            _repository = new Repository<Order>(ContextFactory.GetContext());

        Because of = () =>
        {
            _userRepository = new UserRepository(ContextFactory.GetContext());

            _user = _userRepository.Find(x => x.Id == 1);
            _order = new Order
            {
                User = _user,
                AggregatorType = new AggregatorType { Name = "Caixa com 12 unidades", Quantity = 12},
                OrderStatus = OrderStatus.WaitingPayment,
                OrderType = OrderType.In,
                PaymentType = PaymentType.Money
            };

            _repository.Save(_order);
            UnitOfWork.Commit();

            _productRepository = new Repository<Product>(ContextFactory.GetContext());
            _product = _productRepository.Find(x => x.Id == 1);

            _ordeRepository = new Repository<OrderItem>(ContextFactory.GetContext());


            _items = new OrderItem
            {
                Order = _order,
                AskPrice = 14,
                BidPrice = 25,
                Product = _product,
                PriceType = PriceType.Regular,
                Volume = _order.AggregatorType.Quantity
            };

            _ordeRepository.Save(_items);
            UnitOfWork.Commit();
        };

        It should_have_an_order_with_id_int = () =>
            _order.Id.ShouldBeOfExactType(typeof(int));

        It should_have_an_order_with_user = () =>
            _order.User.ShouldNotBeNull();

        It should_have_an_order_with_aggregationType_of_12_units = () =>
            _order.AggregatorType.Quantity.ShouldEqual(12);

        It should_have_an_order_with_status = () =>
            _order.OrderStatus.ShouldNotBeNull();

        It should_have_an_order_with_payment_with_money = () =>
            _order.PaymentType.ShouldBeLike(PaymentType.Money);

        It should_have_an_order_of_type_in = () =>
            _order.OrderType.ShouldBeLike(OrderType.In);

        It should_have_an_order_with_an_item = () =>
            _order.OrderItems.ShouldNotBeNull();

        It should_have_an_order_and_items_with_same_order_id = () =>
            _items.Order.Id.ShouldEqual(_order.Id);

    }
}
