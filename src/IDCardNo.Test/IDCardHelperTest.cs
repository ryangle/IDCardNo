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
            Assert.Equal("北城区", idCard.County);
            Assert.Equal(new DateTime(1966, 7, 30), idCard.Birthday);
        }

        [Fact]
        public void TryVerify_Test()
        {
            var verify = IDCardHelper.TryVerify("610402196903194412", out var error);
            Assert.Null(error);
            Assert.True(verify);

            var verify2 = IDCardHelper.TryVerify("610402196903194413", out var error2);
            Assert.Equal("校验位错误",error2);
            Assert.False(verify2);
        }

        [Fact]
        public void TryVerify15_Test()
        {
            var verify = IDCardHelper.TryVerify("632123820927051", out var error);
            Assert.Null(error);
            Assert.True(verify);
        }

        [Fact]
        public void Parse15_Test()
        {
            var idCard = IDCardHelper.Parse("632123820927051");
            Assert.Equal("乐都县", idCard.County);
            Assert.Equal(new DateTime(1982, 9, 27), idCard.Birthday);
        }
    }
}
