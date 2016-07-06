using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Freedom.Domain.Entities;
using Freedom.Domain.Enum;
using Freedom.Infrastructure.DataAccess.Repositories;
using Machine.Specifications;

namespace Freedom.Infrastructure.Test.Repositories
{
    [Subject(typeof(Category))]
    public class When_adding_Category : DatabaseSpec
    {
        private static Category _category { get; set; }
        private static Repository<Category> _repository { get; set; }

        private Establish c = () => _repository = new Repository<Category>(ContextFactory.GetContext());

        Because of = () =>
        {
            _category = new Category
            {
                CategoryImage = "http://www.sulminas146.com.br/wp-content/uploads/2015/08/doce-de-leite.jpg",
                Title = "Doces",
            };

            //_categories.Add(new Category
            //{
            //    CategoryImage = "http://www.dicasfree.com/wp-content/uploads/produtos-de-milho-300x212.jpg",
            //    Title = "Derivados do Milho",
            //});

            //_categories.Add(new Category
            //{
            //    CategoryImage = "http://www.sulminas146.com.br/wp-content/uploads/2015/08/doce-de-leite.jpg",
            //    Title = "Doces",
            //});


            _repository.Save(_category);
            UnitOfWork.Commit();
        };

        It should_Id_be_type_of_int = () =>
            _category.Id.ShouldBeOfExactType(typeof(int));

        It should_have_a_farm = () =>
            _category.CategoryImage.ShouldNotBeNull();
    }
}
