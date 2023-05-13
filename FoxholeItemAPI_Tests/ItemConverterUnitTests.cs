using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FoxholeItemAPI.Converters;
using FoxholeItemAPI.Models;
using FoxholeItemAPI.Utils;

namespace FoxholeItemAPI_Tests
{
    public class ItemConverterUnitTests
    {
        [Fact]
        public void Read_MissingProperty_ThrowsException()
        {
            string json = "{ " +
                "\"imgName\": \"testImg\"," +
                "\"itemCategory\": \"small_arms\""+
            "}";

            var options = new JsonSerializerOptions();
            options.Converters.Add(new ItemConverter());

            Assert.Throws<NullReferenceException>(() =>
            {
                JsonSerializer.Deserialize<Item>(json, options);
            });
        }

        [Theory]
        [InlineData("test","testImg", "small_arms", Category.SmallArms)]
        [InlineData("testShippable", "testShippableImg", "shippable", Category.Shippables)]
        [InlineData("testUnknown", "testUnknownImg", "ukn", Category.Unknown)]
        public void Read_ReturnsItem(string expectedName, string expectedIconName, string category, Category expectedCategory)
        {
            string json = "{ " +
                "\"itemName\": \""+expectedName + "\"," +
                "\"imgName\": \""+expectedIconName+"\"," +
                "\"itemCategory\": \""+category+"\"" +
            "}";

            var options = new JsonSerializerOptions();
            options.Converters.Add(new ItemConverter());

            Item? item = JsonSerializer.Deserialize<Item>(json, options);

            Assert.NotNull(item);
            Assert.Equal(expectedName, item?.DisplayName);
            Assert.Equal(expectedIconName, item?.IconName);
            Assert.Equal(expectedCategory, item?.Category);
        }
    }
}
