﻿using Algorithm.Sandbox.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Algorithm.Sandbox.Tests.DataStructures
{
    [TestClass]
    public class AVLTree_Tests
    {
        /// <summary>
        /// Smoke test
        /// </summary>
        [TestMethod]
        public void AVLTree_Test()
        {
            //insert test
            var tree = new AsAVLTree<int>();
            Assert.AreEqual(-1, tree.GetHeight());

            tree.Insert(1);
            Assert.AreEqual(0, tree.GetHeight());

            tree.Insert(2);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(3);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Insert(4);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(5);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(6);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(7);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Insert(8);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(9);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(10);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Insert(11);
            Assert.AreEqual(3, tree.GetHeight());

            //delete
            tree.Delete(1);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Delete(2);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Delete(3);
            Assert.AreEqual(3, tree.GetHeight());

            tree.Delete(4);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(5);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(6);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(7);
            Assert.AreEqual(2, tree.GetHeight());

            tree.Delete(8);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Delete(9);
            Assert.AreEqual(1, tree.GetHeight());

            tree.Delete(10);
            Assert.AreEqual(0, tree.GetHeight());

            tree.Delete(11);
            Assert.AreEqual(tree.GetHeight(), -1);

            Assert.AreEqual(tree.Count, 0);

            tree.Insert(31);
        }

        [TestMethod]
        public void AVLTree_StressTest()
        {
            var nodeCount = 1000 * 10;

            var tree = new AsAVLTree<int>();

            for (int i = 0; i < nodeCount; i++)
            {
                tree.Insert(i);
            }

            for (int i = 0; i < nodeCount; i++)
            {
                Assert.IsTrue(tree.HasItem(i));
            }

            var actualHeight = tree.GetHeight();

            var maxHeight = Math.Log(nodeCount, 2);

            Assert.IsTrue(actualHeight < maxHeight);
        }
    }
}
