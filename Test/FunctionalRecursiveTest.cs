using System.Linq;
using CommonSenseCSharp.datastructures;
using NUnit.Framework;

namespace Tests{
    [TestFixture]
    public class FunctionalRecursiveTest{
        [Test]
        public void TestDfsSimple(){
            var nodes = new TestItem();
            var counter = 0;
            FunctionalRecursive.OnEachDepthFirstDecent(nodes, x => counter++, x => x.Children);
            Assert.AreEqual(counter, 1);
        }

        [Test]
        public void TestDfsSimpleRecursiveCount(){
            var nodes = new TestItem();
            nodes.Children.Add(new TestItem());
            var counter = 0;
            FunctionalRecursive.OnEachDepthFirstDecent(nodes, x => counter++, x => x.Children);
            Assert.AreEqual(counter, 2);
        }

        [Test]
        public void TestDfsSimpleRecursiveOrder(){
            var nodes = new TestItem{MagicName = "2"};
            nodes.Children.Add(new TestItem{MagicName = "1"});
            var itterationList = new NonNullList<string>();
            FunctionalRecursive.OnEachDepthFirstDecent(nodes, x => itterationList.Add(x.MagicName), x => x.Children);
            Assert.AreEqual(itterationList.Count, 2);
            Assert.AreEqual(itterationList.First(), "1");
            Assert.AreEqual(itterationList.Last(), "2");
        }

        /// <summary>
        /// this is a pretty large test.
        /// creates a rather large tree, which should exercise it thougl "all" weierd cases.
        /// </summary>
        [Test]
        public void TestDfsSimpleRecursiveOrderLarge(){
            var startNode = new TestItem("11");
            //create smaller subtrees
            var node3 = new TestItem("3");
            node3.Children.AddRange(new TestItem("1"), new TestItem("2"));
            //create a single line of trees
            var node5 = new TestItem("5");
            node5.Children.Add(new TestItem("4"));

            //create a large subtree
            var node10 = new TestItem("10");
            var node7 = new TestItem("7");
            node7.Children.Add(new TestItem("6"));

            var node9 = new TestItem("9");
            node9.Children.Add(new TestItem("8"));
            node10.Children.AddRange(node7, node9);

            startNode.Children.AddRange(node3, node5, node10);


            var itterationList = new NonNullList<string>();
            FunctionalRecursive.OnEachDepthFirstDecent(startNode, x => itterationList.Add(x.MagicName),
                x => x.Children);
            Assert.AreEqual(itterationList.Count, 11);
            Assert.AreEqual(itterationList,
                NonNullList<string>.CreateFrom("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"));
        }
    }

    class TestItem{
        public NonNullList<TestItem> Children = new NonNullList<TestItem>();
        public string MagicName = "";

        public TestItem(string name){
            MagicName = name;
        }

        public TestItem(){
        }
    }
}