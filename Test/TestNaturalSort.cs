using System;
using System.Diagnostics;
using CommonSenseCSharp.datastructures;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class NaturalSortTest {
        [Test]
        public void TestSimpleTime() {
            var lst = new NonNullList<string>();
            lst.Add("x10");
            lst.Add("x113");
            lst.Add("x1");
            lst.Add("x111");
            var time = Time(() =>
            {
                1000.PerformEachTime(x => { var res = lst.SortNatural(); });
            });
            Console.WriteLine("time : {0}", time.Milliseconds);
            Assert.IsTrue(time < TimeSpan.FromMilliseconds(50));
        }


        [Test]
        public void TestLongSort() {
            var lst = new NonNullList<string>();
            var rand1 = new Random();
            var rand2 = new Random();

            5000.PerformEachTime(x =>
            {
                lst.Add("x" + rand1.Next() + "y" + rand2.Next());
            });
            var time = Time(() =>
            {
                1000.PerformEachTime(x => { var res = lst.SortNatural(); });
            });

            var time2 = Time(() =>
            {
                1000.PerformEachTime(x => { var res = lst.SortNatural(); });
            });

            Console.WriteLine("time (avg) for 1000 regular sorting : {0}", time2.Milliseconds/1000.0d);
            Console.WriteLine("time (avg) for 1000 sorts : {0}", time.Milliseconds/1000.0d);
            Assert.IsTrue(time.Milliseconds / 1000.0d < 5, "sorting 5000 strings should take no more than 5 miliseconds on avg");
            Assert.IsTrue(time.Milliseconds / 1000.0d < (time2.Milliseconds * 1.5d /1000.0d) , "Natural sort should be at max 1.5 times slower than regular sort.");

        }

        private TimeSpan Time(Action toTime) {
            var timer = Stopwatch.StartNew();
            toTime();
            timer.Stop();
            return timer.Elapsed;
        }
    }


}
