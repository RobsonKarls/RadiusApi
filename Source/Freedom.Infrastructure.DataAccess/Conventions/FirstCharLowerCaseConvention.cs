using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text.RegularExpressions;

namespace Freedom.Infrastructure.DataAccess.Conventions
{
    class FirstCharLowerCaseConvention : IStoreModelConvention<EdmProperty>
    {
        public void Apply(EdmProperty property, DbModel model)
        {
            property.Name = property.Name.Replace("_", "");

            if (property.Name == "Id")
            {
                property.Name = property.DeclaringType.Name + property.Name;
            }

            property.Name = Regex.Replace(property.Name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]).ToLower();
        }
    }
}
