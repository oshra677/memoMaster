using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CustomCandidateNamingService : CandidateNamingService
    {
        public override string GetDependentEndCandidateNavigationPropertyName(IForeignKey foreignKey)
        {
            if (foreignKey.PrincipalKey.IsPrimaryKey())
                return foreignKey.PrincipalEntityType.ShortName();

            return base.GetDependentEndCandidateNavigationPropertyName(foreignKey);
        }
    }

    public class DesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            services.AddSingleton<ICandidateNamingService, CustomCandidateNamingService>();
        }
    }
}
