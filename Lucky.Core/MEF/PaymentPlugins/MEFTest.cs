/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lucky.Core.MEF.PaymentPlugins
*文件名： MEFTest
*创建人： Lxsh
*创建时间：2019/1/12 13:33:39
*描述
*=======================================================================
*修改标记
*修改时间：2019/1/12 13:33:39
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky.Core.MEF.PaymentPlugins
{
  public  class MEFTest
    {
        #region MEF测试
        [ImportMany]
        public IEnumerable<PaymentPlugin> CustomerPlugins { get; set; }

        /// <summary>
        ///  MEF测试
        /// </summary>
        public  MEFTest()
        {
         //string dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CustomerPlugins");
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryCatalog catelog = new DirectoryCatalog(dir);
            var container = new CompositionContainer(catelog);
            container.ComposeParts(this); 
        }

        public void Test()
        {     
            foreach (PaymentPlugin item in CustomerPlugins)
            {

                item.Payement(1.2);
                Console.WriteLine(
                    $"当前支付信息=支付方式：{item.PluginDescripor.PluginName}支付版本:{item.PluginDescripor.PluginVesion}");
            }
        }

        #endregion
}
}