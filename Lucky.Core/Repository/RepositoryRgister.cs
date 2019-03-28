/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.Repository
*文件名： RepositoryRgister
*创建人： Lxsh
*创建时间：2019/1/30 10:24:03
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/30 10:24:03
*修改人：Lxsh
*描述：
************************************************************************/
using Lucky.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Lucky.Core.Repository
{
    public class RepositoryRgister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {   
            container.RegisterType(typeof(IRepository<>), typeof(SqlSugarRepository<>));
        }
    }
}