using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DataStructures
{
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
        public uint tsp0; // 0 ~ m (10^6)
        public uint tsp1; // g(10^9) ~ p(10^15)
        public uint tsp2; // e(10^18) ~ y(10^24)
        public uint tsp3; // r(10^27) ~ ak(10^33)

        public static Gold operator+(Gold op1, Gold op2)
        {
            uint tmp0 = op1.tsp0 + op2.tsp0;
            uint tmp1 = op1.tsp1 + op2.tsp1 + tmp0 / 1000000000;
            uint tmp2 = op1.tsp2 + op2.tsp2 + tmp1 / 1000000000;
            uint tmp3 = op1.tsp3 + op2.tsp3 + tmp2 / 1000000000;

            return new Gold()
            {
                tsp0 = tmp0 % 1000000000,
                tsp1 = tmp1 % 1000000000,
                tsp2 = tmp2 % 1000000000,
                tsp3 = tmp3 % 1000000000,
            };
        }
    }
}