/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.MEF
*文件名： PaymentPlugin
*创建人： Lxsh
*创建时间：2019/1/11 17:27:51
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/11 17:27:51
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.MEF
{
   public abstract class PaymentPlugin: AsbBasePlugin
   {
       public abstract bool Payement(double amout);
   }
}