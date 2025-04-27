// SPDX-License-Identifier: MIT

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AKopetsch.IOHelper
{
    /// <summary>
    /// Specificies the byte-order mark (BOM). 
    /// Covers all BOMs listed on
    /// <see href="https://en.wikipedia.org/wiki/Byte_order_mark#Byte-order_marks_by_encoding">en.wikipedia.org</see>
    /// <see href="https://web.archive.org/web/20250127131330/https://en.wikipedia.org/wiki/Byte_order_mark#Byte-order_marks_by_encoding">[archived]</see>.
    /// </summary>
    public sealed class ByteOrderMark
    {
        #region Fields

        /// <summary>
        /// UTF-8 encoding.
        /// </summary>
        public static ByteOrderMark Utf8 { get; } =
            new ByteOrderMark("ef bb bf", "UTF-8", new UTF8Encoding(encoderShouldEmitUTF8Identifier: true));

        /// <summary>
        /// UTF-16 (BE) encoding.
        /// </summary>
        public static ByteOrderMark Utf16Be { get; } =
            new ByteOrderMark("fe ff", "UTF-16 (BE)", new UnicodeEncoding(bigEndian: true, byteOrderMark: true));

        /// <summary>
        /// UTF-16 (LE) encoding.
        /// </summary>
        public static ByteOrderMark Utf16Le { get; } =
            new ByteOrderMark("ff fe", "UTF-16 (LE)", new UnicodeEncoding(bigEndian: false, byteOrderMark: true));

        /// <summary>
        /// UTF-32 (BE) encoding.
        /// </summary>
        public static ByteOrderMark Utf32Be { get; } =
            new ByteOrderMark("00 00 fe ff", "UTF-32 (BE)", new UTF32Encoding(bigEndian: true, byteOrderMark: true));

        /// <summary>
        /// UTF-32 (LE) encoding.
        /// </summary>
        public static ByteOrderMark Utf32Le { get; } =
            new ByteOrderMark("ff fe 00 00", "UTF-32 (LE)", new UTF32Encoding(bigEndian: false, byteOrderMark: true));

        /// <summary>
        /// UTF-7 encoding.
        /// </summary>
        public static ByteOrderMark Utf7 { get; } =
            new ByteOrderMark("2b 2f 76", "UTF-7", null);

        /// <summary>
        /// UTF-1 encoding.
        /// </summary>
        public static ByteOrderMark Utf1 { get; } =
            new ByteOrderMark("f7 64 4c", "UTF-1", null);

        /// <summary>
        /// UTF-EBCDIC encoding.
        /// </summary>
        public static ByteOrderMark UtfEbcdic { get; } =
            new ByteOrderMark("dd 73 66 73", "UTF-EBCDIC", null);

        /// <summary>
        /// SCSU encoding.
        /// </summary>
        public static ByteOrderMark Scsu { get; } =
            new ByteOrderMark("0e fe ff", "SCSU", null);

        /// <summary>
        /// BOCU-1 encoding.
        /// </summary>
        public static ByteOrderMark Bocu1 { get; } =
            new ByteOrderMark("fb ee 28", "BOCU-1", null);

        /// <summary>
        /// GB18030 encoding.
        /// </summary>
        public static ByteOrderMark Gb18030 { get; } =
            new ByteOrderMark("84 31 95 33", "GB18030", null);

        #endregion

        #region Properties

        public ReadOnlyCollection<byte> Bytes { get; }
        public string EncodingName { get; }
        public Encoding? Encoding { get; }

        public static ReadOnlyCollection<ByteOrderMark> All { get; } = Array.AsReadOnly(GetAll());

        #endregion

        #region Constructor

        private ByteOrderMark(string hexString, string encodingName, Encoding? encoding)
        {
            Bytes = Array.AsReadOnly(HexStringConverter.ToByteArray(hexString));
            EncodingName = encodingName;
            Encoding = encoding;
        }

        #endregion

        #region Methods

        private static ByteOrderMark[] GetAll() =>
            typeof(ByteOrderMark).GetProperties()
            .Where(x => x.PropertyType == typeof(ByteOrderMark))
            .Select(x => (x.GetValue(null) as ByteOrderMark)!)
            .ToArray();

        public static ByteOrderMark? Get(byte[] bytes)
        {
            int[] bomBytesCounts = All.Select(x => x.Bytes.Count).Distinct().OrderBy(x => x).ToArray();
            foreach (int bomBytesCount in bomBytesCounts)
            {
                if (bomBytesCount > bytes.Length)
                    return null;
                ByteOrderMark[] potentialBoms = All
                    .Where(bom => bom.Bytes.Count >= bomBytesCount)
                    .Where(bom => StartsWith(bytes, bom.Bytes.ToArray()))
                    .ToArray();
                if (potentialBoms.Length == 1)
                    return potentialBoms.Single();
            }
            return null;
        }

        private static bool StartsWith(byte[] left, params byte[] right)
        {
            if (right == null)
                return false;

            if (right.Length == 0)
                return false;

            if (right.Length > left.Length)
                return false;

            return left.Take(right.Length).SequenceEqual(right);
        }

        public override string ToString() =>
            EncodingName;

        #endregion
    }
}
