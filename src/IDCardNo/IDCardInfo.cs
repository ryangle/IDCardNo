using System;
using System.Collections.Generic;
using System.Text;

namespace IDCardNo
{
    public class IDCardInfo
    {
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Prefecture { get; set; }
        /// <summary>
        /// 县
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// 女=0,男=1
        /// </summary>
        public int Gender { get; set; }
    }
}
