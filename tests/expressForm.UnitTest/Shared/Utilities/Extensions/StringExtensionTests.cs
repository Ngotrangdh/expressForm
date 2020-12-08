using Microsoft.VisualStudio.TestTools.UnitTesting;
using expressForm.Shared.Utilities.Extensions;

namespace expressForm.UnitTest.Shared.Utilities.Extensions
{
    [TestClass]
    public class StringExtensionTests
    {
        [DataTestMethod]
        [DataRow("expressForm", "expressForm")]
        [DataRow(null, "")]
        public void ToStringOrEmpty(string value, string result)
        {
            Assert.AreEqual(result, value.ToStringOrEmpty());
        }
        
    }
}
