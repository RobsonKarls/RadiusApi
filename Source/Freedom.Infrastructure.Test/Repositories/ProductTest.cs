using Freedom.Domain.Entities;
using Freedom.Infrastructure.DataAccess.Repositories;
using Machine.Specifications;

namespace Freedom.Infrastructure.Test.Repositories
{
    [Subject(typeof(Product))]
    public class When_adding_products : DatabaseSpec
    {
        private static Product _product { get; set; }
        private static Repository<Product> _repository { get; set; }

        private Establish c = () => _repository = new Repository<Product>(ContextFactory.GetContext());

        Because of = () =>
        {
            _product = new Product
            {
                Category = new Category {Parent = null, Title = "Queijos", CategoryImage = "http://superbeal.com.br/files/post_foto/photo/190/20141013154559543c1de78fa0a.jpg" },
                Farm = new Farm {Name = "Sitio do Gilmar"},
                Volume = 1,
                Name = "Queijo Minas"
            };
            _repository.Save(_product);
            UnitOfWork.Commit();
        };

        It should_Id_be_type_of_int = () =>
            _product.Id.ShouldBeOfExactType(typeof(int));

        It should_have_a_farm = () =>
            _product.Farm.ShouldNotBeNull();

        It should_have_a_category = () =>
            _product.Category.ShouldNotBeNull();

    }
}
