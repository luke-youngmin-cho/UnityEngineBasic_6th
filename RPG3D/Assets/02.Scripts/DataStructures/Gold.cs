using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace RPG.DataStructures
{
    [Serializable]
    public struct Gold
    {
        public static Gold max => new Gold
        {
            tsp0 = 999999999,
            tsp1 = 999999999,
            tsp2 = 999999999,
            tsp3 = 999999999
        };

        public static Gold min => new Gold
        {
            tsp0 = 0,
            tsp1 = 0,
            tsp2 = 0,
            tsp3 = 0
        };


        // ten to six power
        public int tsp0; // 0 ~ m (10^6)
        public int tsp1; // g(10^9) ~ p(10^15)
        public int tsp2; // e(10^18) ~ y(10^24)
        public int tsp3; // r(10^27) ~ ak(10^33)
        public static StringBuilder stringBuilder
        {
            get
            {
                if (_stringBuilder == null)
                    _stringBuilder = new StringBuilder();
                return _stringBuilder;
            }
        }
        private static StringBuilder _stringBuilder;

        public Gold(int tsp3, int tsp2, int tsp1, int tsp0)
        {
            this.tsp3 = tsp3;
            this.tsp2 = tsp2;
            this.tsp1 = tsp1;
            this.tsp0 = tsp0;
        }

        public override string ToString()
        {
            stringBuilder.Clear();

            if (tsp3 >= 1_000_000) _stringBuilder.Append(tsp3 / 1_000_000).Append(".").Append(tsp3 % 1_000_000 / 100_000).Append("ak");
            else if (tsp3 >= 1_000) _stringBuilder.Append(tsp3 / 1_000).Append(".").Append(tsp3 % 1_000 / 100).Append("q");
            else if (tsp3 >= 1) _stringBuilder.Append(tsp3 / 1).Append(".").Append(tsp2 / 100_000_000).Append("r");

            else if (tsp2 >= 1_000_000) _stringBuilder.Append(tsp2 / 1_000_000).Append(".").Append(tsp2 % 1_000_000 / 100_000).Append("y");
            else if (tsp2 >= 1_000) _stringBuilder.Append(tsp2 / 1_000).Append(".").Append(tsp2 % 1_000 / 100).Append("z");
            else if (tsp2 >= 1) _stringBuilder.Append(tsp2 / 1).Append(".").Append(tsp1 / 100_000_000).Append("e");

            else if (tsp1 >= 1_000_000) _stringBuilder.Append(tsp1 / 1_000_000).Append(".").Append(tsp1 % 1_000_000 / 100_000).Append("p");
            else if (tsp1 >= 1_000) _stringBuilder.Append(tsp1 / 1_000).Append(".").Append(tsp1 % 1_000 / 100).Append("t");
            else if (tsp1 >= 1) _stringBuilder.Append(tsp1 / 1).Append(".").Append(tsp0 / 100_000_000).Append("g");

            else if (tsp0 >= 1_000_000) _stringBuilder.Append(tsp0 / 1_000_000).Append(".").Append(tsp0 % 1_000_000 / 100_000).Append("m");
            else if (tsp0 >= 1_000) _stringBuilder.Append(tsp0 / 1_000).Append(".").Append(tsp0 % 1_000 / 100).Append("k");
            else if (tsp0 >= 1) _stringBuilder.Append(tsp0 / 1);
            else _stringBuilder.Append(0);

            return _stringBuilder.ToString();
        }

        public static Gold operator +(Gold op1, Gold op2)
        {
            int tmp0 = op1.tsp0 + op2.tsp0;
            int tmp1 = op1.tsp1 + op2.tsp1 + tmp0 / 1000000000;
            int tmp2 = op1.tsp2 + op2.tsp2 + tmp1 / 1000000000;
            int tmp3 = op1.tsp3 + op2.tsp3 + tmp2 / 1000000000;

            return new Gold()
            {
                tsp0 = tmp0 % 1000000000,
                tsp1 = tmp1 % 1000000000,
                tsp2 = tmp2 % 1000000000,
                tsp3 = tmp3 % 1000000000,
            };
        }

        public static Gold operator -(Gold op1, Gold op2)
        {
            int tmp0, tmp1, tmp2, tmp3;

            tmp0 = op1.tsp0 - op2.tsp0;
            tmp1 = op1.tsp1 - op2.tsp1;
            if (tmp0 < 0)
            {
                tmp1 -= 1;
                tmp0 += 1000000000;
            }
            tmp2 = op1.tsp2 - op2.tsp2;
            if (tmp1 < 0)
            {
                tmp2 -= 1;
                tmp1 += 1000000000;
            }
            tmp3 = op1.tsp3 - op2.tsp3;
            if (tmp2 < 0)
            {
                tmp3 -= 1;
                tmp2 += 1000000000;
            }

            return new Gold
            {
                tsp0 = tmp0,
                tsp1 = tmp1,
                tsp2 = tmp2,
                tsp3 = tmp3,
            };
        }

        public static Gold operator*(Gold op, int multiplier)
        {
            int tmp0 = op.tsp0 * multiplier;
            int tmp1 = op.tsp1 * multiplier + tmp0 / 1_000_000_000;
            int tmp2 = op.tsp2 * multiplier + tmp1 / 1_000_000_000;
            int tmp3 = op.tsp3 * multiplier + tmp2 / 1_000_000_000;

            return new Gold
            {
                tsp0 = tmp0 % 1_000_000_000,
                tsp1 = tmp1 % 1_000_000_000,
                tsp2 = tmp2 % 1_000_000_000,
                tsp3 = tmp3 % 1_000_000_000,
            };
        }

        public static Gold operator *(Gold op, float multiplier)
        {
            double tmp0 = op.tsp0 * multiplier;
            double tmp1 = op.tsp1 * multiplier;
            double tmp2 = op.tsp2 * multiplier;
            double tmp3 = op.tsp3 * multiplier;
            
            double int3 = Math.Truncate(tmp3);
            double sig3 = tmp3 - int3;
            tmp2 += sig3 * 100_000_000.0;

            double int2 = Math.Truncate(tmp2);
            double sig2 = tmp2 - int2;
            tmp1 += sig2 * 100_000_000.0;

            double int1 = Math.Truncate(tmp1);
            double sig1 = tmp1 - int1;
            tmp0 += sig1 * 100_000_000.0;

            double int0 = Math.Truncate(tmp0);
            double sig0 = tmp0 - int0;

            tmp1 += tmp0 / 1_000_000_000.0;
            tmp2 += tmp1 / 1_000_000_000.0;
            tmp3 += tmp2 / 1_000_000_000.0;

            if (tmp1 > 1_000_000_000.0)
                tmp2 += tmp1 / 1_000_000_000.0;
            if (tmp2 > 1_000_000_000.0)
                tmp3 += tmp2 / 1_000_000_000.0;

            return new Gold
            {
                tsp0 = (int)(tmp0 % 1_000_000_000),
                tsp1 = (int)(tmp1 % 1_000_000_000),
                tsp2 = (int)(tmp2 % 1_000_000_000),
                tsp3 = (int)(tmp3 % 1_000_000_000),
            };
        }


        public static bool operator <(Gold op1, Gold op2)
        {
            if (op1.tsp3 < op2.tsp3)
                return true;

            if (op1.tsp3 == op2.tsp3 && op1.tsp2 < op2.tsp2)
                return true;

            if (op1.tsp3 == op2.tsp3 && op1.tsp2 == op2.tsp2 && op1.tsp1 < op2.tsp1)
                return true;

            if (op1.tsp3 == op2.tsp3 && op1.tsp2 == op2.tsp2 && op1.tsp1 == op2.tsp1 && op1.tsp0 < op2.tsp0)
                return true;

            return false;
        }

        public static bool operator >(Gold op1, Gold op2)
        {
            if (op1.tsp3 > op2.tsp3)
                return true;

            if (op1.tsp3 == op2.tsp3 && op1.tsp2 > op2.tsp2)
                return true;

            if (op1.tsp3 == op2.tsp3 && op1.tsp2 == op2.tsp2 && op1.tsp1 > op2.tsp1)
                return true;

            if (op1.tsp3 == op2.tsp3 && op1.tsp2 == op2.tsp2 && op1.tsp1 == op2.tsp1 && op1.tsp0 > op2.tsp0)
                return true;

            return false;
        }

        public static bool operator ==(Gold op1, Gold op2)
        {
            return (op1.tsp0 == op2.tsp0) &&
                   (op1.tsp1 == op2.tsp1) &&
                   (op1.tsp2 == op2.tsp2) &&
                   (op1.tsp3 == op2.tsp3);
        }

        public static bool operator !=(Gold op1, Gold op2)
            => !(op1 == op2);

        public static bool operator <=(Gold op1, Gold op2)
            => (op1 < op2) || (op1 == op2);

        public static bool operator >=(Gold op1, Gold op2)
            => (op1 > op2) || (op1 == op2);

        
    }
}