using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mobility_Android.Resources.global;


namespace Mobility_Android_Tests
{
    public class A
    {
        public int a;
        public String b;

        public A(int a, string b)
        {
            this.a = a;
            this.b = b;
        }

    }
    public class B
    {
        public A a;
        public double b;

        public B(A a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
    public class C
    {
        public B a;
        public int b;

        public C(B a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
    [TestClass]
    public class DataTransfertTest
    {
        //Test avec 1
        [TestMethod]
        public void TestParse1()
        {
            String value = DataTransfer.parse(1);
            Console.Write(value);
            if (!StringAssert.Equals(value, "{System.Int32 Int32: 1}"))
            {
                Assert.Fail("Not equals");
            }
        }
        //Test avec multiple chiffres
        [TestMethod]
        public void TestParse2()
        {
            for(var i = 0; i<10; i++)
            {
                
                String value = DataTransfer.parse(i);
                Console.Write(i);
                if (!StringAssert.Equals(value, "{System.Int32 Int32: "+i.ToString()+"}"))
                {
                    Assert.Fail("Not equals");
                }
            }
        }
        //Test avec une string
        [TestMethod]
        public void TestParse3()
        {
          
            String value = DataTransfer.parse("abcd");
            Console.Write(value);
            if (!StringAssert.Equals(value, "{System.String String: abcd}"))
            {
                Assert.Fail("Not equals");
            }
        
        }
        //Test avec 2 string
        [TestMethod]
        public void TestParse4()
        {

            String value = DataTransfer.parse("abcd","efgh");
            Console.Write(value);
            if (!StringAssert.Equals(value, "{System.String String: abcd, System.String String: efgh}"))
            {
                Assert.Fail("Not equals");
            }

        }

        //Test avec 4 string
        [TestMethod]
        public void TestParse5()
        {

            String value = DataTransfer.parse("abcd", "efgh", "ijkl","mnop");
            Console.Write(value);
            if (!StringAssert.Equals(value, "{System.String String: abcd, System.String String: efgh, System.String String: ijkl, System.String String: mnop}"))
            {
                Assert.Fail("Not equals");
            }

        }

        //Test avec 1 string 1 int
        [TestMethod]
        public void TestParse6()
        { 
            
            String value = DataTransfer.parse("abcd", 4);
            Console.Write(value);
            if (!StringAssert.Equals(value, "{System.String String: abcd, System.Int32 Int32: 4}"))
            {
                Assert.Fail("Not equals");
            }

        }
        //Test avec 1 classe simple
        [TestMethod]
        public void TestParse7()
        {

            String value = DataTransfer.parse(new A(1,"b"));
            Console.Write(value);
            if (!StringAssert.Equals(value, "{Mobility_Android_Tests.A A: (Int32 a : 1, System.String b : b)}"))
            {
                Assert.Fail("Not equals");
            }

        }
        //Test avec 1 string 1 int 1 classe
        [TestMethod]
        public void TestParse8()
        {

            String value = DataTransfer.parse(new A(1, "b"), "abcd", 4);
            Console.Write(value);
            if (!StringAssert.Equals(value, "{Mobility_Android_Tests.A A: (Int32 a : 1, System.String b : b), System.String String: abcd, System.Int32 Int32: 4}"))
            {
                Assert.Fail("Not equals");
            }

        }
        //Test avec 1 classe avec une autre classe dedans 
        [TestMethod]
        public void TestParse9()
        {

            String value = DataTransfer.parse(new B(new A(1,"a"), 2), "abcd", 4);
            Console.Write(value);
            if (!StringAssert.Equals(value, "{Mobility_Android_Tests.B B: (Mobility_Android_Tests.A A: (Int32 a : 1, System.String b : a), Double b : 2), System.String String: abcd, System.Int32 Int32: 4}"))
            {
                Assert.Fail("Not equals");
            }

        }
        //Test avec 1 classe avec une autre classe dedans qui a une autre classe dedans
        [TestMethod]
        public void TestParse10()
        {

            String value = DataTransfer.parse(new C(new B(new A(1, "a"), 2),6), "abcd", 4);
            Console.Write(value);
            if (!StringAssert.Equals(value, "{Mobility_Android_Tests.C C: (Mobility_Android_Tests.B B: (Mobility_Android_Tests.A A: (Int32 a : 1, System.String b : a), Double b : 2), Int32 b : 6), System.String String: abcd, System.Int32 Int32: 4}"))
            {
                Assert.Fail("Not equals");
            }

        }
    }
}
