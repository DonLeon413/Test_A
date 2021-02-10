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
    public class MusicNumberTests
    {
        [TestMethod()]
        public void NormalizeTest()
        {
            MusicNum obj = MusicNum.Parse( "0/2" );
            var res = obj.Normalize();

            Assert.IsTrue( 0 == res.Numerator );    // числитель
            Assert.IsTrue( 2 == res.Denumerator );  // Знаменатель

            obj = MusicNum.Parse( "2/16" );
            res = obj.Normalize();

            Assert.IsTrue( 1 == res.Numerator );    // числитель
            Assert.IsTrue( 8 == res.Denumerator );  // Знаменатель

            obj = MusicNum.Parse( "16/8" );
            res = obj.Normalize();

            Assert.IsTrue( 2 == res.Numerator );    // числитель
            Assert.IsTrue( 1 == res.Denumerator );  // Знаменатель
        }

        [TestMethod()]
        public void EqualsTest()
        {
            var obj1 = MusicNum.Parse( "4/8" );
            var obj2 = MusicNum.Parse( "1/2" );
            Assert.IsTrue( obj1.Equals( obj2 ) );


            obj1 = MusicNum.Parse( "4/8" );
            obj2 = MusicNum.Parse( "4/8" );
            Assert.IsTrue( obj1.Equals( obj2 ) );


            obj1 = MusicNum.Parse( "1/8" );
            obj2 = MusicNum.Parse( "1/4" );
            Assert.IsTrue( false == obj1.Equals( obj2 ) );


            Assert.IsTrue( false == obj1.Equals( null ) );
        }

        [TestMethod()]
        public void EqualsTest_operators()
        {
            var obj1 = MusicNum.Parse( "4/8" );
            var obj2 = MusicNum.Parse( "1/2" );
            var obj3 = MusicNum.Parse( "1/16" );

            // operator ==
            Assert.IsTrue( false == ( obj1 == null ), "1" );
            Assert.IsTrue( obj1.Equals( obj2 ), "2" );

            // operator !=
            Assert.IsTrue( obj1 != null, "3" );
            Assert.IsTrue( null != obj1, "4" );
            Assert.IsTrue( obj3 != obj2, "5" );
            Assert.IsTrue( false == ( obj1 != obj2 ), "6" );

            // operator >
            Assert.IsTrue( false == ( obj1 > obj2 ), "7" );  // 4/8 == 1/2
            Assert.IsTrue( obj1 > obj3, "8" );               // 4/8 > 1/16
            Assert.IsTrue( false == ( obj3 > obj1 ), "9" );  // 1/16 < 4/8

            Assert.IsTrue( obj3 > null, "9" );               // 1/16 > null
            Assert.IsTrue( false == ( null > obj1 ), "9" );  // null < 4/8

            // operator <
            Assert.IsTrue( false == ( obj1 < obj2 ), "10" );  // 4/8 == 1/2
            Assert.IsTrue( false == ( obj1 < obj3 ), "11" );  // 4/8 > 1/16
            Assert.IsTrue( obj3 < obj1, "12" );               // 1/16 < 4/8

            Assert.IsTrue( false == ( obj3 < null ), "9" );   // 1/16 > null
            Assert.IsTrue( null < obj1, "9" );                // null < 4/8

            // operator+
            var result = obj1 + obj3; // 4/8 + 1/16 = 9/16

            Assert.IsTrue( 9 == result.Numerator );
            Assert.IsTrue( 16 == result.Denumerator );

            result = MusicNum.Parse( "0/2" ) + obj2; // 0/2 + 1/2 = 1/2

            Assert.IsTrue( 1 == result.Numerator );
            Assert.IsTrue( 2 == result.Denumerator );

        }

        [TestMethod()]
        public void DeltaTest()
        {
            var obj1 = MusicNum.Parse( "1/2" );
            var obj2 = MusicNum.Parse( "1/4" );
            var obj3 = MusicNum.Parse( "0/16" );

            var result = obj1.Delta( obj2 ); // 1/4
            Assert.IsTrue( 1 == result.Numerator );
            Assert.IsTrue( 4 == result.Denumerator );

            result = obj2.Delta( obj1 );     // 1/4
            Assert.IsTrue( 1 == result.Numerator );
            Assert.IsTrue( 4 == result.Denumerator );

            result = obj1.Delta( obj3 );     // 8/16
            Assert.IsTrue( 8 == result.Numerator );
            Assert.IsTrue( 16 == result.Denumerator );

            result = obj3.Delta( obj1 );   // 8/16
            Assert.IsTrue( 8 == result.Numerator );
            Assert.IsTrue( 16 == result.Denumerator );

            result = obj1.Delta( null );     // 1/2
            Assert.IsTrue( 1 == result.Numerator );
            Assert.IsTrue( 2 == result.Denumerator );

        }

        [TestMethod()]
        public void ToStringTest()
        {
            var obj1 = MusicNum.Parse( "1/2" );
            var obj2 = MusicNum.Parse( "0" );
            var obj3 = MusicNum.Parse( "0/16" );


            String result = obj1.ToString();
            Assert.IsTrue( 0 == String.Compare( result, "1/2" ), "1" );

            result = obj2.ToString();
            Assert.IsTrue( 0 == String.Compare( result, "0" ), "2" );

            result = obj3.ToString();
            Assert.IsTrue( 0 == String.Compare( result, "0" ), "3" );
        }        
    }
}