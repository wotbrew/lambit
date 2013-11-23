using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambit
{
    public static class Parse
    {
        public static Maybe<byte> Byte(string str)
        {
            byte res;
            return Maybe.When(byte.TryParse(str, out res), res);
        }

        public static Maybe<sbyte> SignedByte(string str)
        {
            sbyte res;
            return Maybe.When(sbyte.TryParse(str, out res), res);
        }

        public static Maybe<ushort> UnsignedShort(string str)
        {
            ushort res;
            return Maybe.When(ushort.TryParse(str, out res), res);
        }

        public static Maybe<short> Short(string str)
        {
            short res;
            return Maybe.When(short.TryParse(str, out res), res);
        }

        public static Maybe<uint> UnsignedInt(string str)
        {
            uint res;
            return Maybe.When(uint.TryParse(str, out res), res);
        }

        public static Maybe<int> Int(string str)
        {
            int res;
            return Maybe.When(int.TryParse(str, out res), res);
        }

        public static Maybe<ulong> UnsignedLong(string str)
        {
            ulong res;
            return Maybe.When(ulong.TryParse(str, out res), res);
        }

        public static Maybe<long> Long(string str)
        {
            long res;
            return Maybe.When(long.TryParse(str, out res), res);
        }


        public static Maybe<float> Float(string str)
        {
            float res;
            return Maybe.When(float.TryParse(str, out res), res);
        }

        public static Maybe<double> Double(string str)
        {
            double res;
            return Maybe.When(double.TryParse(str, out res), res);
        }

        public static Maybe<decimal> Decimal(string str)
        {
            decimal res;
            return Maybe.When(decimal.TryParse(str, out res), res);
        }
    }
}
