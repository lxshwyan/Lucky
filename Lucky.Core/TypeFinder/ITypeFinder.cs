/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.TypeFinder
*文件名： ITypeFinder
*创建人： Lxsh
*创建时间：2019/1/30 9:31:30
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/30 9:31:30
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.TypeFinder
{
   public  interface ITypeFinder
    {
        IList<Assembly> GetAssemblies();

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

    } 
}