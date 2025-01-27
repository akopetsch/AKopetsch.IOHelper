// SPDX-License-Identifier: MIT

namespace AKopetsch.IOHelper.Tests
{
    public class BitsHelperTest
    {
        #region Methods ([Fact])

        [Fact]
        public void Test_GetBits_0x01_LsbFirst()
        {
            bool[] expected = GetAllZeroesBitsArray();
            expected[0] = true;
            bool[] actual = BitsHelper.GetBits(0x01, BitOrder.LsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBits_0x01_MsbFirst()
        {
            bool[] expected = GetAllZeroesBitsArray();
            expected[7] = true;
            bool[] actual = BitsHelper.GetBits(0x01, BitOrder.MsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBits_0x80_MsbFirst()
        {
            bool[] expected = GetAllZeroesBitsArray();
            expected[0] = true;
            bool[] actual = BitsHelper.GetBits(0x80, BitOrder.MsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBits_0xFF()
        {
            bool[] expected = GetAllOnesBitsArray();
            bool[] actual = BitsHelper.GetBits(0xFF, BitOrder.MsbFirst);
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Test_GetBitMask_1_MsbFirst() =>
            Assert.Equal(
                expected: 0x80,
                actual: BitsHelper.GetBitMask(0, BitOrder.MsbFirst));

        [Fact]
        public void Test_GetBitMask_1_LsbFirst() =>
            Assert.Equal(
                expected: 0x01,
                actual: BitsHelper.GetBitMask(0, BitOrder.LsbFirst));

        #endregion

        #region Methods (helper)

        private static bool[] GetAllZeroesBitsArray() =>
            Enumerable.Repeat(false, BitsHelper.BitsPerByte).ToArray();

        private static bool[] GetAllOnesBitsArray() =>
            Enumerable.Repeat(true, BitsHelper.BitsPerByte).ToArray();

        #endregion
    }
}
