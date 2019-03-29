using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdateInterface
{
    
    /// <summary>
  /// 自动更新接口
  /// </summary>
    public interface InterfaceAutoUpdateMng
    {
        /// <summary>
        /// 判断是否有更新
        /// </summary>
        /// <returns></returns>
        bool IsUpdate();
    }
}
