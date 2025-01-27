// SPDX-License-Identifier: MIT

using System;
using System.Runtime.CompilerServices;

namespace IOHelper
{
    public static class BytesSwapper
    {
        #region Constants

        private const uint Mask0 = unchecked((uint)0xFF << 0 * 8);
        private const uint Mask1 = unchecked((uint)0xFF << 1 * 8);
        private const uint Mask2 = unchecked((uint)0xFF << 2 * 8);
        private const uint Mask3 = unchecked((uint)0xFF << 3 * 8);
        private const ulong Mask4 = unchecked((ulong)0xFF << 4 * 8);
        private const ulong Mask5 = unchecked((ulong)0xFF << 5 * 8);
        private const ulong Mask6 = unchecked((ulong)0xFF << 6 * 8);
        private const ulong Mask7 = unchecked((ulong)0xFF << 7 * 8);

        #endregion

        #region Methods

        #region short/ushort

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short Swap(short x) =>
            (short)Swap((ushort)x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort Swap(ushort x) =>
            (ushort)(x << 8 | (byte)(x >> 8));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short SwapIf(short x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort SwapIf(ushort x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion

        #region int/uint

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Swap(int x) =>
            (int)Swap((uint)x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Swap(uint x) =>
            /* bit 3 */ x << (3 * 8) |
            /* bit 2 */ x << (1 * 8) & Mask2 |
            /* bit 1 */ x >> (1 * 8) & Mask1 |
            /* bit 0 */ x >> (3 * 8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SwapIf(int x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint SwapIf(uint x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion

        #region long/ulong

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long Swap(long x) =>
            (long)Swap((ulong)x);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong Swap(ulong x) =>
            /* bit 7 */ x << (7 * 8) & Mask7 |
            /* bit 6 */ x << (5 * 8) & Mask6 |
            /* bit 5 */ x << (3 * 8) & Mask5 |
            /* bit 4 */ x << (1 * 8) & Mask4 |
            /* bit 3 */ x >> (1 * 8) & Mask3 |
            /* bit 2 */ x >> (3 * 8) & Mask2 |
            /* bit 1 */ x >> (5 * 8) & Mask1 |
            /* bit 0 */ x >> (7 * 8) & Mask0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long SwapIf(long x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong SwapIf(ulong x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion

        #region float/double

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Swap(float x) =>
            BitConverter.Int32BitsToSingle(
                Swap(BitConverter.SingleToInt32Bits(x)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Swap(double x) =>
            BitConverter.Int64BitsToDouble(
                Swap(BitConverter.DoubleToInt64Bits(x)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SwapIf(float x, bool condition) =>
            condition ? Swap(x) : x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double SwapIf(double x, bool condition) =>
            condition ? Swap(x) : x;

        #endregion

        #endregion
    }
}
