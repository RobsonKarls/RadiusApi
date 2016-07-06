using Freedom.Domain.Entities;
using Freedom.Domain.Enum;
using Freedom.Infrastructure.DataAccess.Repositories;
using Freedom.Labs.Components;
using Machine.Specifications;
using System;

namespace Freedom.Infrastructure.Test.Repositories
{
    [Subject(typeof(User))]
    class When_user_data_is_saved : DatabaseSpec
    {
        static UserRepository _repository;
        static User _usuario;

        Establish context = () =>
            _repository = new UserRepository(ContextFactory.GetContext());

        Because of = () =>
        {
            _usuario = new User
            {
                Cpf = "00116949120",
                LastAccess = DateTime.Now,
                Email = "robison.karls@outlook.com",
                FirstName = "Robson Karls",
                LastName = "Custódio",
                Password = "Novasenha",
                Status = UserStatus.Active,
                Group = new Group { Active = true, Description = "Admins group", Name = "Admin", Modified = DateTime.Now, Created = DateTime.Now },
                Address = new Address { AddressLine1 = "Rua 1600", Number = "32", ZipCode = "78075790", City = "Cuiabá", State = "MT", Country = "Brasil", }
            };

            _repository.Save(_usuario);

            UnitOfWork.Commit();
        };

        It shoudl_create_id = () =>
           _usuario.Id.ShouldBeOfExactType<int>();

        It should_have_date_of_creation = () =>
            _usuario.Created.ShouldNotEqual(DateTime.MinValue);

        It should_persist = () =>
            _repository.Exists(c => c.Id == _usuario.Id).ShouldBeTrue();

        It should_not_have_user_without_group = () =>
            _usuario.Group.ShouldNotBeNull();

        It should_not_have_user_without_address = () =>
            _usuario.Address.ShouldNotBeNull();
    }

    [Subject(typeof(User))]
    class when_change_user_data : DatabaseSpec
    {
        static UserRepository _repository;
        static User _user;
        private static User _oldUser;

        static private int _userId = 2;

        /// <summary>
        /// Data de update antes da atualizacao
        /// </summary>
        static DateTime? _oldModifiedDate;

        Establish context = () =>
           _repository = new UserRepository(ContextFactory.GetContext());

        Because of = () =>
        {
            _user = _repository.Find(x => x.Id == _userId);
            _oldUser = _repository.Find(x => x.Id == _userId);
            _user.Cpf = "00000000000";
            _repository.Save(_user);
            _oldModifiedDate = _user.Modified;

            UnitOfWork.Commit();
        };

        It deve_passar_pelo_check_de_password = () =>
            _repository.Find(u => u.Id == _userId).Password.ShouldEqual<String>(Password.CreateHashFrom("Novasenha"));

        It data_de_atualizacao_deve_ser_maior_que_antes_da_atualizacao = () =>
            _repository.Find(u => u.Id == _userId).Modified.ShouldBeGreaterThan(_oldModifiedDate);

        It campo_cpf_deve_estar_preenchido = () =>
            _repository.Find(u => u.Id == _userId).Cpf.ShouldNotBeNull();

        It cpf_should_change = () =>
            _repository.Find(u => u.Id == _userId).Cpf.ShouldBeEqualIgnoringCase("00000000000");
    }
}
