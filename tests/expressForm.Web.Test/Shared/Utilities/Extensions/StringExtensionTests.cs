using expressForm.Shared.Utilities.Extensions;
using Xunit;

namespace expressForm.UnitTest.Shared.Utilities.Extensions
{
    public class StringExtensionTests
    {
        [Theory]
        [InlineData("expressForm", "expressForm")]
        [InlineData(null, "")]
        public void ToStringOrEmpty(string value, string result)
        {
            Assert.Equal(result, value.ToStringOrEmpty());
        }
        
    }
}
