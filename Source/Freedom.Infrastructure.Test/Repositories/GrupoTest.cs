using Freedom.Domain.Entities;
using Machine.Specifications;
using System;
using System.Linq;
using Freedom.Infrastructure.DataAccess.Factories;

namespace Freedom.Infrastructure.Test.Repositories
{

    [Subject(typeof(Group))]
    public class when_group_is_saved : DatabaseSpec
    {
        private static Group _grupo;
        private static FreedomDbContext _context;

        private Establish c = () => _context = ContextFactory.GetContext();

        Because of = () =>
            {
                _grupo = new Group
                {
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Description = "Adicionando grupo para teste",
                    Name = "Grupo Teste"
                };

                _context.Groups.Add(_grupo);

                UnitOfWork.Commit();
            };

        It should_generate_new_id = () =>
            _grupo.Id.ShouldBeOfExactType(typeof(int));

        It should_get_the_creation_date = () =>
            _grupo.Created.ShouldNotEqual(DateTime.MinValue);

        It should_persist = () =>
            _context.Groups.FirstOrDefault(g => g.Name == _grupo.Name);
    }
}
