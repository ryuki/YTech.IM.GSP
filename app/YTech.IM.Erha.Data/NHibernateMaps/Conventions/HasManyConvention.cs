﻿using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace YTech.IM.GSP.Data.NHibernateMaps.Conventions
{
    public class HasManyConvention : IHasManyConvention
    {
        public void Apply(FluentNHibernate.Conventions.Instances.IOneToManyCollectionInstance instance)
        {
            instance.Key.Column(instance.EntityType.Name + "Fk");
        }
    }
}
