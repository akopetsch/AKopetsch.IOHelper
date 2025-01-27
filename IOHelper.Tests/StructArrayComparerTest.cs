// SPDX-License-Identifier: MIT

namespace IOHelper.Tests
{
    public class StructArrayComparerTest
    {
        [Fact]
        public void Test_Simple()
        {
            bool[] left = [true, true, false];
            bool[] right = [true, true, true];

            var differences = StructArrayComparer.Compare(left, right);
            Assert.Single(differences);
            AssertDifference(differences.Single(), false, true);
        }

        [Fact]
        public void Test_DifferentLengths()
        {
            bool[] left = [true,];
            bool[] right = [true, false, true];

            var differences = StructArrayComparer.Compare(left, right);
            AssertDifference(differences[0], null, false);
            AssertDifference(differences[1], null, true);
        }

        private static void AssertDifference<TItem>(
            StructArrayComparer.Difference<TItem> difference, TItem? expectedLeft, TItem? expectedRight)
            where TItem : struct
        {
            Assert.Equal(expectedLeft, difference.Left);
            Assert.Equal(expectedRight, difference.Right);
        }
    }
}
