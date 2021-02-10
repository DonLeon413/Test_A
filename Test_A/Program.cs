using Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_A
{
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main( string[] args )
        {
            while( true )
            {
                Console.WriteLine( "\n\n--------------------" );

                MusicNum beat = ReadBeat();
                if( null == beat )
                {
                    break;
                }
                                
                String raw_data = ReadRawData();
                if( String.IsNullOrWhiteSpace( raw_data ) )
                {
                    continue;
                }

                String[] takts = RawDataToStrTakts( raw_data );
                if( takts == null || takts.Length == 0 )
                {
                    continue;
                }

                AnalyzeTakts( takts, beat );
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="takts"></param>
        private static void AnalyzeTakts( IEnumerable<String> takts, MusicNum beat )
        {

            Int32 count_takts = 0;
            MusicNum beat_duration = null;

            foreach( String takt in takts )
            {
                beat_duration = null;
                try
                {
                    Console.WriteLine( String.Format( "--- {0} -- Takt: {1} -----", count_takts++, takt ) );

                    foreach( UInt32 note in ParseTakt( takt ) )
                    {
                        beat_duration = beat_duration + MusicNum.From( note );                        
                    }
                    
                    if( beat == beat_duration )
                    {
                        Console.WriteLine( "Beat duration: {0} - OK!", beat_duration.ToString() );
                    }
                    else
                    {
                        Console.WriteLine( "Beat duration: {0}, delta: {1}{2} ",
                                             beat_duration.ToString(),
                                             ( ( beat < beat_duration ? "+" : "-" ) ),
                                             beat_duration.Delta( beat ).Normalize().ToString() );
                    }

                }catch( Exception ex )
                {
                    Console.WriteLine( String.Format( "Error: {0}", ex.Message ) );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strRawTakt"></param>
        /// <returns></returns>
        private static IEnumerable<UInt32> ParseTakt( String strRawTakt )
        {
            strRawTakt = strRawTakt.Trim();
            return strRawTakt.Split( new char[] { ' ' } )
                              .Select( t => t.Trim() )
                              .Where( t => false == String.IsNullOrWhiteSpace( t ) )
                              .Select( t => UInt32.Parse( t ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        private static String[] RawDataToStrTakts( String rawData )
        {
            String[] result = null;
            rawData = rawData.Trim();

            if( false == String.IsNullOrWhiteSpace( rawData ) )
            {
                result = rawData.Split( new char[] { '|' } )
                                 .Where( t => false == String.IsNullOrWhiteSpace( t ) )
                                 .Select( t => t.Trim() )
                                 .ToArray();                
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static String ReadRawData()
        {
            Console.WriteLine( "Takts data ( Sample: 4 2 8|2 2 16|8 8 8 8 8 or empty ):" );
            return Console.ReadLine().Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static MusicNum ReadBeat()
        {
              
            Console.WriteLine( "length beat ( Sample: 3/4 or 1 or 2/8 or empty ):" );
            String str_beat = Console.ReadLine();

            if( false == String.IsNullOrWhiteSpace( str_beat ) )
            {
                try
                {
                    return MusicNum.Parse( str_beat );
                }
                catch( Exception ex )
                {
                    Console.WriteLine( String.Format( "Error:{0}", ex.Message ) );
                }
            }

            return null;
        }
    }
}
