/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.MEF.Plugins
*文件名： WeChatPaymentPlugin
*创建人： Lxsh
*创建时间：2019/1/12 13:13:35
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/12 13:13:35
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.MEF.PaymentPlugins
{
    [PluginDescription("微信支付", "7.0")]
    [Export(typeof(PaymentPlugin))]
    public class WeChatPaymentPlugin : PaymentPlugin
    {
        public override bool Payement(double amout)
        {
            
            Console.WriteLine("微信支付成功");
            return true;
        }
    }
}