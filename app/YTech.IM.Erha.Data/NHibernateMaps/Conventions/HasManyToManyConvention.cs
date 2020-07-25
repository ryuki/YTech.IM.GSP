using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace YTech.IM.GSP.Data.NHibernateMaps.Conventions
{
    public class HasManyToManyConvention : IHasManyToManyConvention
    {
        public void Apply(IManyToManyCollectionInstance instance)
        {
            instance.Cascade.SaveUpdate();
        }
    }
}
