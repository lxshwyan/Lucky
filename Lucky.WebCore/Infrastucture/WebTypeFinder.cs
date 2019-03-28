
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Lucky.Core.TypeFinder;

namespace Lucky.WebCore.Infrastucture
{
    public class WebTypeFinder : AppDomainTypeFinder
    {
        private bool binFolderAssembliesLoaded = false;

        public virtual string GetBinDirectory()
        {
            if (System.Web.Hosting.HostingEnvironment.IsHosted)
            {
                return System.Web.HttpRuntime.BinDirectory;
            }

            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (!binFolderAssembliesLoaded)
            {
                binFolderAssembliesLoaded = true;
                LoadMatchingAssemblies(GetBinDirectory());
            }
            return base.GetAssemblies();
        }
    }
}
