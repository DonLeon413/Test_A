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
    public class MusNoteException:
            Exception
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="message"></param>
        public MusNoteException( String message ):
              base( message )
        {
        }
    }
}
