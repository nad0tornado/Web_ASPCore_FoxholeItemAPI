using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoxholeItemAPI.Utils;

namespace FoxholeItemAPI_Tests
{
    public class UtilsUnitTests
    {
        [Theory]
        [InlineData("small_arms", Category.SmallArms)]
        [InlineData("shipables",Category.Shippables)]
        [InlineData("barneythedinosaur", Category.Unknown)]
        public void ToCategoryUnitTest(string value,Category expectedCategory)
        {
            Category category = value.ToCategory();
            Assert.Equal(expectedCategory, category);
        }

        private enum MockEnumType
        {
            MockValueA, MockValueB
        }

        [Fact]
        public void EnumTypesToStringUnitTest()
        {
            var categoryTypesToString = ApiUtils.EnumTypesToString<MockEnumType>();
            var expectedTypesToString = new Dictionary<int, string>()
            {
                {0,"MockValueA" },
                {1,"MockValueB" }
            };

            Assert.Equal(expectedTypesToString.Keys, categoryTypesToString.Keys);
            Assert.Equal(expectedTypesToString.Values, categoryTypesToString.Values);
        }
    }
}
