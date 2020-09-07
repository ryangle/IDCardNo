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
            var idCard = IDCardHelper.Parse("14010319660730425x");
            Assert.Equal("山西省太原市北城区", idCard.County);
            Assert.Equal(new DateTime(1966, 7, 30), idCard.Birthday);
        }
    }
}
