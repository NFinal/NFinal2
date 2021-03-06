﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SixLabors.Fonts.Utilities;
using SixLabors.Fonts.WellKnownIds;

namespace SixLabors.Fonts.Tables.General.Name
{
    internal class NameRecord
    {
        private readonly string value;

        public PlatformIDs Platform { get; }

        public ushort LanguageID { get; }

        public NameIds NameID { get; }

        internal StringLoader StringReader { get; private set; }

        public string Value => this.StringReader?.Value ?? this.value;

        public NameRecord(PlatformIDs platform, ushort languageId, NameIds nameId, string value)
        {
            this.Platform = platform;
            this.LanguageID = languageId;
            this.NameID = nameId;
            this.value = value;
        }

        public static NameRecord Read(BinaryReader reader)
        {
            var platform = reader.ReadUInt16<PlatformIDs>();
            EncodingIDs encodingId = reader.ReadUInt16<EncodingIDs>();
            Encoding encoding = encodingId.AsEncoding();
            var languageID = reader.ReadUInt16();
            var nameID = reader.ReadUInt16<NameIds>();

            var stringReader = StringLoader.Create(reader, encoding);

            return new NameRecord(platform, languageID, nameID, null)
            {
                StringReader = stringReader
            };
        }
    }
}
