using System.Text;

namespace NFinal.Advanced
{
    public sealed class Settings
    {
        public static Settings Global
        {
            get { return global ?? (global = new Settings()); }
            set { global = value; }
        }

        private static Settings global;

        private Encoding defaultEncoding;

        public Settings()
        {
            IsLittleEndian = Default.IsLittleEndian;
            IsUpperCaseInHexadecimal = Default.IsUpperCaseInHexadecimal;
        }

        public bool IsLittleEndian { get; set; }

        public Encoding DefaultEncoding
        {
            get { return defaultEncoding ?? (defaultEncoding = Default.DefaultEncoding); }
            set { defaultEncoding = value; }
        }

        public bool IsUpperCaseInHexadecimal { get; set; }

        private static class Default
        {
            public const bool IsLittleEndian = false;
            public const bool IsUpperCaseInHexadecimal = true;
            public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        }
    }
}
