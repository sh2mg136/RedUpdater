using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace CommonTypes
{

    public class ConfigHelper
    {

        private static ConfigHelper instance;

        private ConfigHelper() { }

        public static ConfigHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigHelper();
                }
                return instance;
            }
        }


        #region Saving

        /// <summary>
        /// Сохранение в реестр
        /// </summary>
        /// <param name="key">Раздел реестра</param>
        /// <param name="name">Параметр</param>
        /// <param name="val">Значение</param>
        public static void Save( string key, string name, string val )
        {
            RegistryKey RegKey = Registry.CurrentUser;
            try
            {
                RegKey = RegKey.OpenSubKey( key, true );
                if (RegKey == null)
                {
                    RegKey = Registry.CurrentUser;
                    RegKey = RegKey.CreateSubKey( key );
                }
                RegKey.SetValue( name, val );
                RegKey.Close();
            }
            catch { }
        }


        public static void Save( string key, string name, bool val )
        {
            RegistryKey RegKey = Registry.CurrentUser;
            try
            {
                RegKey = RegKey.OpenSubKey( key, true );
                if (RegKey == null)
                {
                    RegKey = Registry.CurrentUser;
                    RegKey = RegKey.CreateSubKey( key );
                }
                RegKey.SetValue( name, val );
                RegKey.Close();
            }
            catch { }
        }


        public static void Save( string key, string name, int val )
        {
            RegistryKey RegKey = Registry.CurrentUser;
            try
            {
                RegKey = RegKey.OpenSubKey( key, true );
                if (RegKey == null)
                {
                    RegKey = Registry.CurrentUser;
                    RegKey = RegKey.CreateSubKey( key );
                }
                RegKey.SetValue( name, val );
                RegKey.Close();
            }
            catch { }
        }


        public static void Save( string key, string name, object val )
        {
            RegistryKey RegKey = Registry.CurrentUser;
            try
            {
                RegKey = RegKey.OpenSubKey( key, true );
                if (RegKey == null)
                {
                    RegKey = Registry.CurrentUser;
                    RegKey = RegKey.CreateSubKey( key );
                }
                if (val != null) RegKey.SetValue( name, val );
                RegKey.Close();
            }
            catch { }
        }

        #endregion


        #region Loading


        public static string Load( string key, string name, string defaultVal )
        {
            string val = defaultVal;
            RegistryKey regKey = Registry.CurrentUser;
            try
            {
                regKey = regKey.OpenSubKey( key, true );
                if (regKey != null)
                {
                    val = (string)regKey.GetValue( name, defaultVal );
                    regKey.Close();
                }
                else
                {
                    regKey = Registry.CurrentUser.CreateSubKey( key );
                    regKey.SetValue( name, defaultVal );
                }
            }
            catch
            {
                val = defaultVal;
            }
            finally
            {
                regKey.Dispose();
            }

            return val;
        }


        /// <summary>
        /// Читает значение реестра
        /// </summary>
        /// <param name="key">Ключ реестра</param>
        /// <param name="name">Имя параметра</param>
        /// <param name="defaultValue">Значение по-умолчанию</param>
        /// <returns></returns>
        /*
        public static bool Load( string key, string name, bool defaultValue )
        {
            bool val = defaultValue;
            RegistryKey regKey = Registry.CurrentUser;
            try
            {
                regKey = regKey.OpenSubKey( key, true );
                if (regKey != null)
                {
                    object obj = regKey.GetValue( name );
                    if (obj != null)
                        val = Convert.ToBoolean( obj );
                    else
                        regKey.SetValue( name, defaultValue );

                    regKey.Close();
                }
                else
                {
                    regKey.CreateSubKey( key );
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message );
            }
            finally
            {
                regKey.Dispose();
            }
            return val;
        }
        */

        public static bool Load( string key, string name, bool defaultValue )
        {
            bool val = defaultValue;
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey( key, true ))
                {
                    if (regKey != null)
                    {
                        object obj = regKey.GetValue( name, val );
                        val = Convert.ToBoolean( obj );
                    }
                    else
                    {
                        Save( key, name, defaultValue );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine( ex.Message );
            }
            return val;
        }


        public static int Load( string key, string name, int defaultValue )
        {
            int val = defaultValue;
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey( key, true ))
                {
                    if (regKey != null)
                    {
                        object obj = regKey.GetValue( name, val );
                        val = Convert.ToInt32( obj );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine( ex.Message );
            }
            return val;
        }


        public static object Load( string key, string name )
        {
            object result = null;
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey( key, true ))
                {
                    if (regKey != null)
                    {
                        result = regKey.GetValue( name, null );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine( ex.Message );
            }
            return result;
        }


        public static object Load( string key, string name, object defaultVal )
        {
            object val = defaultVal;
            RegistryKey regKey = Registry.CurrentUser;
            try
            {
                regKey = regKey.OpenSubKey( key, true );
                if (regKey != null)
                {
                    val = regKey.GetValue( name, defaultVal );
                    regKey.Close();
                }
                else
                {
                    regKey = Registry.CurrentUser.CreateSubKey( key );
                    regKey.SetValue( name, defaultVal );
                }
            }
            catch
            {
                val = defaultVal;
            }
            finally
            {
                regKey.Dispose();
            }

            return val;
        }


        #endregion


        #region Saving-Loading-Password


        internal static byte[] my_key = new byte[24] { 23, 12, 54, 25, 3, 0, 64, 34, 234, 43, 77, 65, 34, 65, 43, 35, 234, 0, 45, 2, 46, 76, 23, 43 };
        internal static byte[] my_iv = new byte[8] { 98, 34, 65, 91, 3, 65, 119, 5 };


        public static void SavePassword( string key, string name, string data )
        {
            try
            {
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes( data );
                ICryptoTransform transform = tDESalg.CreateEncryptor( my_key, my_iv );
                byte[] resultArray = transform.TransformFinalBlock( toEncryptArray, 0, toEncryptArray.Length );
                RegistryKey RegKey = Registry.CurrentUser;
                RegKey = RegKey.OpenSubKey( key, true );
                RegKey.SetValue( name, resultArray );
                RegKey.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine( "A Cryptographic error occurred: {0}", e.Message );
            }
            catch (Exception e)
            {
                Console.WriteLine( "Error: {0}", e.Message );
            }

        }


        public static string LoadPassword( string key, string name )
        {
            try
            {
                RegistryKey RegKey = Registry.CurrentUser;
                RegKey = RegKey.OpenSubKey( key, true );
                object obj = RegKey.GetValue( name, null );
                RegKey.Close();
                byte[] toEncryptArray = (byte[])obj;
                if (toEncryptArray != null)
                {
                    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                    ICryptoTransform transform = tdes.CreateDecryptor( my_key, my_iv );
                    byte[] resultArray = transform.TransformFinalBlock( toEncryptArray, 0, toEncryptArray.Length );
                    tdes.Clear();
                    return UTF8Encoding.UTF8.GetString( resultArray );
                }
                else
                    return string.Empty;


            }
            catch
            {
                return string.Empty;
            }

        }


        #endregion




        /// <summary>
        /// Сохранение строки в реестре
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subkey"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public static void Save( string key, string subkey, string name, string val )
        {
            Save( System.IO.Path.Combine( key, subkey ), name, val );
        }

        public static void Save( string key, string subkey, string name, bool val )
        {
            Save( System.IO.Path.Combine( key, subkey ), name, val );
        }

        public static void Save( string key, string subkey, string name, int val )
        {
            Save( System.IO.Path.Combine( key, subkey ), name, val );
        }

        public static void Save( string key, string subkey, string name, object val )
        {
            Save( System.IO.Path.Combine( key, subkey ), name, val );
        }



        public static string Load( string key, string subkey, string name, string defaultValue )
        {
            return Load( System.IO.Path.Combine( key, subkey ), name, defaultValue );
        }

        public static bool Load( string key, string subkey, string name, bool defaultValue )
        {
            return Load( System.IO.Path.Combine( key, subkey ), name, defaultValue );
        }

        public static object Load( string key, string subkey, string name, object defaultValue )
        {
            return Load( System.IO.Path.Combine( key, subkey ), name );
        }

        public static object Load( string key, string subkey, string name, int defaultValue )
        {
            return Load( System.IO.Path.Combine( key, subkey ), name );
        }

    }

}
