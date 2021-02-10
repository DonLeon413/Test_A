using Microsoft.VisualStudio.TestTools.UnitTesting;
using Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_A.Tests
{
    [TestClass()]
    public class MathUtilsTests
    {
        [TestMethod()]
        public void IsPowerOfTwoTest()
        {

            
            Assert.IsTrue( ((UInt32)1).IsPowerOfTwo() );
            Assert.IsTrue( ((UInt32)4).IsPowerOfTwo() );
            Assert.IsTrue( ((UInt32)16).IsPowerOfTwo() );

            Assert.IsTrue( false == ((UInt32)0).IsPowerOfTwo() );
            Assert.IsTrue( false == ((UInt32)3).IsPowerOfTwo() );

            Assert.IsTrue( false == ((UInt32)5).IsPowerOfTwo() );
            Assert.IsTrue( false == ((UInt32)100).IsPowerOfTwo() );
        }
    }
}