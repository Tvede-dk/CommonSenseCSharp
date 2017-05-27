using System.Linq;
using CommonSenseCSharp.datastructures;
using Xunit;

namespace Tests{
    
    public class FunctionalRecursiveTest{
        [Fact]
        public void TestDfsSimple(){
            var nodes = new TestItem();
            var counter = 0;
            FunctionalRecursive.OnEachDepthFirstDecent(nodes, x => counter++, x => x.Children);
            Assert.Equal(counter, 1);
        }

        [Fact]
        public void TestDfsSimpleRecursiveCount(){
            var nodes = new TestItem();
            nodes.Children.Add(new TestItem());
            var counter = 0;
            FunctionalRecursive.OnEachDepthFirstDecent(nodes, x => counter++, x => x.Children);
            Assert.Equal(counter, 2);
        }

        [Fact]
        public void TestDfsSimpleRecursiveOrder(){
            var nodes = new TestItem{MagicName = "2"};
            nodes.Children.Add(new TestItem{MagicName = "1"});
            var itterationList = new NonNullList<string>();
            FunctionalRecursive.OnEachDepthFirstDecent(nodes, x => itterationList.Add(x.MagicName), x => x.Children);
            Assert.Equal(itterationList.Count, 2);
            Assert.Equal(itterationList.First(), "1");
            Assert.Equal(itterationList.Last(), "2");
        }

        /// <summary>
        /// this is a pretty large test.
        /// creates a rather large tree, which should exercise it thougl "all" weierd cases.
        /// </summary>
        [Fact]
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
            Assert.Equal(itterationList.Count, 11);
            Assert.Equal(itterationList,
                NonNullList<string>.CreateFrom("1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"));
        }
    }

    class TestItem{
        public readonly NonNullList<TestItem> Children = new NonNullList<TestItem>();
        public string MagicName = string.Empty;

        public TestItem(string name) => MagicName = name;

        public TestItem(){
        }
    }
}