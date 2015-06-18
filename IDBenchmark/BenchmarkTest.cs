using System;
using System.Collections.Generic;
using System.Diagnostics;
using TwofishTest;

namespace IDBenchmark
{
    class BenchmarkTest
    {
        private const long CountIterations = 1 << 15;
        private static double[] testCPUData = new double[FormProgress.TotalTests + 1];
        private static int[] testCountData = new int[FormProgress.TotalTests + 1];
        private static PerformanceCounter _theCpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        public static long GetScore(long totalTime)
        {
            var avg = 0.0;
            for (var i = 1; i <= FormProgress.TotalTests; i++)
            {
                avg += testCPUData[i]/testCountData[i];
            }
            avg /= FormProgress.TotalTests;
            return (long) avg * totalTime;
        }

        private static void GetCurrentCpuUsage(int testId)
        {
            testCPUData[testId] += _theCpuCounter.NextValue();
            testCountData[testId]++;
        }

        // Generate Twofish test.
        public static void Test1()
        {

            System.Threading.Tasks.Parallel.For(0, CountIterations, (i, l) =>
            {
                GetCurrentCpuUsage(1);
                // these are the ECB Tests
                byte[] pt128 =
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00
                };
                byte[] pt256 =
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
                };

                byte[] key128 =
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00
                };
                byte[] ct128 =
                {
                    0x9F, 0x58, 0x9F, 0x5C, 0xF6, 0x12, 0x2C, 0x32, 0xB6, 0xBF, 0xEC, 0x2F, 0x2A, 0xE8, 0xC3,
                    0x5A
                };

                byte[] key192 =
                {
                    0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54,
                    0x32, 0x10, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77
                };
                byte[] ct192 =
                {
                    0xCF, 0xD1, 0xD2, 0xE5, 0xA9, 0xBE, 0x9C, 0xDF, 0x50, 0x1F, 0x13, 0xB8, 0x92, 0xBD, 0x22,
                    0x48
                };

                byte[] key256 =
                {
                    0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF, 0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54,
                    0x32, 0x10, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE,
                    0xFF
                };
                byte[] ct256 =
                {
                    0x37, 0x52, 0x7B, 0xE0, 0x05, 0x23, 0x34, 0xB8, 0x9F, 0x0C, 0xFC, 0xCA, 0xE8, 0x7C, 0xFA,
                    0x20
                };

                TestTwofish.TestTwofishECB(ref key128, ref pt128, ref ct128);
                TestTwofish.TestTwofishECB(ref key192, ref pt128, ref ct192);
                TestTwofish.TestTwofishECB(ref key256, ref pt128, ref ct256);

                TestTwofish.TestTwofishCBC(ref key128, ref pt256, ref pt128, ref ct128);
                TestTwofish.TestTwofishCBC(ref key192, ref pt256, ref pt128, ref ct192);
                TestTwofish.TestTwofishCBC(ref key256, ref pt256, ref pt128, ref ct256);

                TestTwofish.Cascade(ref key128, ref key256);
            });
        }

        // Generate SHA1 + SHA2 test.
        public static void Test2()
        {
            var message = "Rhoncus magnis ac ut habitasse aliquet.";
            System.Threading.Tasks.Parallel.For(0, CountIterations, (i, l) =>
            {
                GetCurrentCpuUsage(2);
                message = Hash.GetHash(message, Hash.HashType.MD5);
                message += Hash.GetHash(message, Hash.HashType.SHA1);
                message += Hash.GetHash(message, Hash.HashType.SHA256);
                message = Hash.GetHash(message, Hash.HashType.SHA512);
            });
        }

        // Generate DateTime.
        public static void Test3()
        {
            var b = DateTime.Now.AddSeconds(10);
            System.Threading.Tasks.Parallel.For(0, 1 << 30, (i, l) =>
            {
                GetCurrentCpuUsage(3);
                if (DateTime.Now > b) l.Stop();
            });
        }

        // Generate Arithmetic operations test.
        public static void Test4()
        {
            decimal a = 1;
            decimal b;
            var t = DateTime.Now.AddSeconds(10);
            System.Threading.Tasks.Parallel.For(0, 1 << 30, (i, l) =>
            {
                GetCurrentCpuUsage(4);
                b = a++ * a++;
                b = b + b;
                a /= 23;
                ++a;
                a *= 23;
                b++;
                b -= 2;
                if (DateTime.Now > t) l.Stop();
            });
           
        }

        // Generate list test.
        public static void Test5()
        {

            var list = new List<long>();
            var random = new Random();
            for (var i = 0; i < CountIterations; ++i)
            {
                GetCurrentCpuUsage(5);
                list.Add(random.Next());
                list.Sort();
            }
        }
    }
}
