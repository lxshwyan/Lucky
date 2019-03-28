/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.IOC
*文件名： ServiceContainer
*创建人： Lxsh
*创建时间：2019/1/30 10:14:20
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/30 10:14:20
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Lucky.Core.IOC
{
    public static class ServiceContainer
    {
        static Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() => new UnityContainer());

        public static IUnityContainer Current { get { return Container.Value; } }

        public static T Resolve<T>() where T : class
        {
            return Container.Value.Resolve<T>();
        }

        public static IEnumerable<T> ResolveAll<T>() where T : class
        {
            return Container.Value.ResolveAll<T>();
        }
    }
}