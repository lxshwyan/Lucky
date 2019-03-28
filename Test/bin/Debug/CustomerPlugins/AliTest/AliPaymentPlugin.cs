using Lucky.Core.MEF;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliTest
{
    [PluginDescription("支付宝支付","2.1")]
    [Export(typeof(PaymentPlugin))]
    public class AliPaymentPlugin : PaymentPlugin
    {
        public override bool Payement(double amout)
        {
            Console.WriteLine("支付宝支付成功");
            return true;
        }
    }
}
