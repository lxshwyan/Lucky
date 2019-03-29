/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：AutoUpdate
*文件名： AUpdateMng
*创建人： Lxsh
*创建时间：2019/3/29 12:06:10
*描述
*=======================================================================
*修改标记
*修改时间：2019/3/29 12:06:10
*修改人：Lxsh
*描述：
************************************************************************/
using AutoUpdateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class AUpdateMng : InterfaceAutoUpdateMng
    {
        #region 属性
        private FrmUpdate frmU;
        #endregion

        public AUpdateMng()
        {

        }

        #region 方法
        /// <summary>
        /// 判断是否有更新
        /// </summary>
        /// <returns></returns>
        public bool IsUpdate()
        {
            if (frmU == null)
            {
                frmU = new FrmUpdate();
                frmU.Name = "frmU";
            }

            return frmU.IsUpdate;
        }
        #endregion
    }
}