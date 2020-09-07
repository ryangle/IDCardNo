using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace IDCardNo
{
    public partial class IDCardHelper
    {
        static IDCardHelper()
        {

        }
        public static IDCardInfo Parse(string id)
        {
            if (!TryVerify(id, out var error))
            {
                throw new ArgumentException(error, "id");
            }

            var region_code = id.Substring(0, 6);

            string str_birthday;
            int gender_code;
            if (id.Length == 15)
            {
                str_birthday = "19" + id.Substring(6, 6);
                gender_code = id[14] - '0';
            }
            else
            {
                str_birthday = id.Substring(6, 8);
                gender_code = id[16] - '0';
            }

            return new IDCardInfo
            {
                County = DivisionCodeData[region_code],
                Prefecture = DivisionCodeData[region_code.Substring(0, 4)],
                Province = DivisionCodeData[region_code.Substring(0, 2)],
                Birthday = DateTime.ParseExact(str_birthday, "yyyyMMdd", null),
                Gender = gender_code % 2 == 0 ? 0 : 1
            };
        }
        public static bool TryParse(string id, out IDCardInfo idCard)
        {
            try
            {
                idCard = Parse(id);
                return true;
            }
            catch
            {
                idCard = null;
                return false;
            }
        }
        public static bool TryVerify(string id, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(id))
            {
                error = "空身份证号";
            }
            if (!Regex.IsMatch(id, @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X|x)$"))
            {
                error = "格式错误";
            }

            string region_code;
            string str_birthday;
            if (id.Length == 15)
            {
                if (!VerifyIDCard1G(id))
                {
                    error = "校验位错误";
                }
                region_code = id.Substring(0, 6);
                str_birthday = "19" + id.Substring(6, 6);
            }
            else if (id.Length == 18)
            {
                if (!VerifyIDCard2G(id))
                {
                    error = "校验位错误";
                };
                region_code = id.Substring(0, 6);
                str_birthday = id.Substring(6, 8);
            }
            else
            {
                error = "号码位数错误";
                return false;
            }

            if (!DivisionCodeData.ContainsKey(region_code))
            {
                error = "区域码错误";
                return false;
            }

            DateTime birthday;
            if (!DateTime.TryParseExact(str_birthday, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out birthday)
                || birthday.ToString("yyyyMMdd") != str_birthday)
            {
                error = "出生日期错误";
                return false;
            }
            if (error != null)
            {
                return false;
            }
            return true;
        }
        private static bool VerifyIDCard2G(string id)
        {
            id = id.ToUpper();
            var weights = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            var checks = "10X98765432";
            var val = 0;
            for (var i = 0; i < 17; i++)
            {
                val += (id[i] - '0') * weights[i];
            }
            return id[17] == checks[val % 11];
        }
        private static bool VerifyIDCard1G(string id)
        {
            if (long.TryParse(id, out long n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(id.Remove(2)) == -1)
            {
                return false;//省份验证
            }

            string birth = id.Substring(6, 6).Insert(4, "-").Insert(2, "-");

            if (DateTime.TryParse(birth, out _) == false)
            {
                return false;//生日验证
            }

            return true;//符合15位身份证标准
        }
    }
}
