using IDCardNo;
using System;
using Xunit;

namespace IDCardNo.Test
{
    public class IDCardHelperTest
    {
        [Fact]
        public void Parse_Test()
        {
            var idCard = IDCardHelper.Parse("14010319660730424x");
            Assert.Equal("ɽ��ʡ̫ԭ�б�����", idCard.County);
            Assert.Equal(new DateTime(1966, 7, 30), idCard.Birthday);
        }

        [Fact]
        public void TryVerify_Test()
        {
            var verify = IDCardHelper.TryVerify("610402196903194412", out var error);
            Assert.Null(error);
            Assert.True(verify);

            var verify2 = IDCardHelper.TryVerify("610402196903194413", out var error2);
            Assert.Equal("У��λ����",error2);
            Assert.False(verify2);
        }
    }
}
