﻿using System;
using System.Threading;
using TwofishTest;

namespace IDBenchmark
{
    class BenchmarkTest
    {
        private const int _countIterations = 10000;
      
        // Generate Twofish test.
        public static void Test1()
        {
            for (var i = 0; i < _countIterations; i++) {
                // these are the ECB Tests
                byte[] pt128 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                byte[] pt256 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                byte[] key128 = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                byte[] ct128 = { 0x9F, 0x58, 0x9F, 0x5C, 0xF6, 0x12, 0x2C, 0x32, 0xB6, 0xBF, 0xEC, 0x2F, 0x2A, 0xE8, 0xC3, 0x5A };

                byte[] key192 = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 };
                byte[] ct192 = { 0xCF, 0xD1, 0xD2, 0xE5, 0xA9, 0xBE, 0x9C, 0xDF, 0x50, 0x1F, 0x13, 0xB8, 0x92, 0xBD, 0x22, 0x48 };

                byte[] key256 = { 0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };
                byte[] ct256 = { 0x37, 0x52, 0x7B, 0xE0, 0x05, 0x23, 0x34, 0xB8, 0x9F, 0x0C, 0xFC, 0xCA, 0xE8, 0x7C, 0xFA, 0x20 };

                TestTwofish.TestTwofishECB(ref key128, ref pt128, ref ct128);
                TestTwofish.TestTwofishECB(ref key192, ref pt128, ref ct192);
                TestTwofish.TestTwofishECB(ref key256, ref pt128, ref ct256);

                TestTwofish.TestTwofishCBC(ref key128, ref pt256, ref pt128, ref ct128);
                TestTwofish.TestTwofishCBC(ref key192, ref pt256, ref pt128, ref ct192);
                TestTwofish.TestTwofishCBC(ref key256, ref pt256, ref pt128, ref ct256);

                TestTwofish.Cascade(ref key128, ref key256);
            }
        }

        public static void Test2()
        {

            Thread.Sleep(500);
        }

        public static void Test3()
        {
        }

        public static void Test4()
        {
        }

        public static void Test5()
        {

            Thread.Sleep(2000);
        }

        public static void Test6()
        {
        }

        public static void Test7()
        {
        }

        public static void Test8()
        {

            Thread.Sleep(3000);
        }

        public static void Test9()
        {
        }

        public static void Test10()
        {
        }
    }
}
