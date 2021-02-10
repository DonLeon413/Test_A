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
    public sealed class MusicNum
    {
        // числитель
        public UInt32 Numerator
        {
            get;
            private set;
        }

        // знаменатель
        public UInt32 Denumerator
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new String ToString()
        {
            if( 0 == this.Denumerator )
            {
                return String.Format( "Numerator: {0} Denumerator: {1}",
                                      this.Numerator, this.Denumerator );
            }

            if( 0 == this.Numerator % this.Denumerator && this.Numerator >= this.Denumerator )
            {
                return String.Format( "{0}", this.Numerator / this.Denumerator );
            }

            return ( 0 != this.Numerator ?
                        String.Format( "{0}/{1}", this.Numerator, this.Denumerator )
                        :
                        "0" );
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="denumerator"></param>
        /// <param name="numerator"></param>
        private MusicNum( UInt32 numerator, UInt32 denumerator )
        {
            this.Denumerator = denumerator;
            this.Numerator = numerator;
        }


        /// <summary>
        /// 
        /// </summary>
        public static MusicNum From( UInt32 note )
        {
            if(note > 0 && note< 17 && note.IsPowerOfTwo() )
            {
                return new MusicNum( 1, note );
            }

            throw new MusNoteException( String.Format("Note '{0}' incorrect.", note ) );
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static MusicNum Parse( String str )
        {
            str = str.Trim();

            if( String.IsNullOrWhiteSpace( str ) )
            {
                throw new MusNoteException( "String empty." );
            }

            var str_tokens = str.Split( new char[] { '/', '\\' } );
            var tokens = str_tokens.Where( t => false == String.IsNullOrWhiteSpace( t ) )
                                   .Select( t => UInt32.Parse( t ) )
                                   .ToArray();

            if( tokens != null && ( tokens.Length == 1 || ( tokens.Length == 2 
                                                            && tokens[1].IsPowerOfTwo() ) ) )
            {
                return new MusicNum( tokens[0], (tokens.Length == 1 ? 1 : tokens[1] ) );
            }

            throw new MusNoteException( String.Format( "Error parse '{0}' to MusicNumber/", str ) );

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mn1"></param>
        /// <param name="mn2"></param>
        /// <returns></returns>
        public static MusicNum operator+ ( MusicNum mn1, MusicNum mn2 )
        {
            if( ReferenceEquals( mn1, null ) )
            {
                return new MusicNum( mn2.Numerator, mn2.Denumerator );
            }

            if( ReferenceEquals( mn2, null ) )
            {
                return new MusicNum( mn1.Numerator, mn1.Denumerator );
            }

            var data = Prepare( mn1, mn2 );
            
            return new MusicNum( data.Item1 + data.Item2, data.Item3 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mn1"></param>
        /// <param name="mn2"></param>
        /// <returns></returns>
        public static Boolean operator == ( MusicNum mn1, MusicNum mn2 )
        {
            if( ReferenceEquals( mn1, mn2 ) )
            {
                return true;
            }

            if( ReferenceEquals( mn1, null ) || ReferenceEquals( mn2, null ) )
            {
                return false;
            }

            var data = Prepare( mn1, mn2 );

            return ( data.Item1 == data.Item2 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherObj"></param>
        /// <returns></returns>
        public MusicNum Delta( MusicNum otherObj )
        {
            if( ReferenceEquals( null, otherObj ) )
            {
                return new MusicNum( this.Numerator, this.Denumerator );
            }

            var data = Prepare( this, otherObj );
            
            return new MusicNum( ( data.Item1 > data.Item2 ? data.Item1 - data.Item2 :  
                                                                data.Item2 - data.Item1 ),
                                      data.Item3 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mn1"></param>
        /// <param name="mn2"></param>
        /// <returns></returns>
        public static Boolean operator != ( MusicNum mn1, MusicNum mn2 )
        {
            if( ReferenceEquals( mn1, mn2 ) ) 
            {
                return false;
            }

            if( ReferenceEquals( mn1, null ) || ReferenceEquals( mn2, null ) )
            {
                return true;
            }

            var data = Prepare( mn1, mn2 );

            return ( data.Item1 != data.Item2 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mn1"></param>
        /// <param name="mn2"></param>
        /// <returns></returns>
        public static Boolean operator < ( MusicNum mn1, MusicNum mn2 )
        {
            if( ReferenceEquals( mn1, mn2 ) )
            {
                return false;
            }

            if( ReferenceEquals( mn1, null ) )   // null < value
            {
                return true;
            }

            if( ReferenceEquals( mn2, null ) )    // value < null
            {
                return false;
            }

            var data = Prepare( mn1, mn2 );

            return ( data.Item1 < data.Item2 );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mn1"></param>
        /// <param name="mn2"></param>
        /// <returns></returns>
        public static Boolean operator > ( MusicNum mn1, MusicNum mn2 )
        {
            if( ReferenceEquals( mn1, mn2 ) )
            {
                return false;
            }

            if( ReferenceEquals( mn1, null ) )    // null > value
            {
                return false;
            }

            if( ReferenceEquals( mn2, null ) )     // value > null 
            {
                return true;
            }

            var data = Prepare( mn1, mn2 );

            return ( data.Item1 > data.Item2 );
        }

        /// <summary>
        /// может кому понадобится
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public override bool Equals( object otherObj )
        {

            var second = (MusicNum)otherObj;

            if( ReferenceEquals( null, second ) )
            {
                return false;
            }

            var data = Prepare( this, second );

            return ( data.Item1 == data.Item2 );
        }

        /// <summary>
        /// по простому чтобы варнингов не было
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Numerator.GetHashCode();
        }

        #region UTILS
        /// <summary>
        /// очень простое вычисление общего знаменателя
        /// тут все кратно степени двойки поэтому просто выбираем наибольший
        /// </summary>
        /// <param name="denominator1"></param>
        /// <param name="denominator2"></param>
        /// <returns></returns>
        private static UInt32 CalcDenominator( UInt32 denominator1, UInt32 denominator2 )
        {
            return Math.Max( denominator1, denominator2 );
        }

        /// <summary>
        /// тут все понятно
        /// </summary>
        /// <param name="mn1"></param>
        /// <param name="mn2"></param>
        /// <returns></returns>
        private static Tuple<UInt32,UInt32, UInt32> Prepare( MusicNum mn1, MusicNum mn2 )
        {
            UInt32 denominator = CalcDenominator( mn1.Denumerator, mn2.Denumerator );

            UInt32 numerator1 = mn1.Numerator * ( denominator / mn1.Denumerator );
            UInt32 numerator2 = mn2.Numerator * ( denominator / mn2.Denumerator );

            return new Tuple<UInt32, UInt32, UInt32>( numerator1, numerator2, denominator );
        } 
        
        /// <summary>
        /// Нормализация музыкальной дроби по простому
        /// </summary>
        /// <returns></returns>
        public MusicNum Normalize()
        {
            var numerator = this.Numerator;
            var denumerator = this.Denumerator;

            // denumerator всегда делится на 2
            while( numerator > 1 && denumerator > 1 && denumerator % 2 == 0 && numerator % 2 == 0 )
            {
                numerator /= 2;
                denumerator /= 2;
            }

            return new MusicNum( numerator, denumerator );
        }
        #endregion
    }
}
