using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music
{
    /// <summary>
    /// 
    /// </summary>
    public static class MathUtils
    {
        /// <summary>
        /// Яыляется ли число степенью двойки
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static Boolean IsPowerOfTwo( this UInt32 _this )
        {            
            return ( _this != 0 && ( _this & ( _this - 1 ) ) == 0 );            
        }
    }
}
