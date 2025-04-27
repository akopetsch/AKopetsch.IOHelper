// SPDX-License-Identifier: MIT

namespace AKopetsch.IOHelper.Tests
{
    public class ByteOrderMarkTest
    {
        [Fact]
        public void TestUtf32Le()
        {
            byte[] bytes = HexStringConverter.ToByteArray("ff fe 00 00 deadbeef");
            Assert.Equal(ByteOrderMark.Utf32Le, ByteOrderMark.Get(bytes));
        }
    }
}
