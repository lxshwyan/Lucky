/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.IOC
*文件名： IDependencyRegister
*创建人： Lxsh
*创建时间：2019/1/30 10:17:51
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/30 10:17:51
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
    public interface IDependencyRegister
    {
        void RegisterTypes(IUnityContainer container);
    }
}