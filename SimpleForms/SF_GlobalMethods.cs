using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForms
{
    public class SF
    {
        //Functions for returning an integer from an object/string (overloads).
        public static int Int(object o)
        {
            return Convert.ToInt32(o);
        }
        public static int Int(string s)
        {
            return Convert.ToInt32(s);
        }

        //Functions for returning a string from an object.
        public static string String(object o)
        {
            return (string)o;
        }

        //Functions for returning a bool from an object/string (overloads).
        public static bool Bool(object o, bool acceptYesNo=false)
        {
            try
            {
                return bool.Parse((string)o);
            } catch
            {
                if (acceptYesNo)
                {
                    if (((string)o).ToLower()=="yes")
                    {
                        return true;
                    } else if (((string)o).ToLower()=="no")
                    {
                        return false;
                    } else
                    {
                        throw new Exception("Error parsing bool.");
                    }
                } else
                {
                    throw new Exception("Error parsing bool.");
                }
            }
        }
        public static bool Bool(string s, bool acceptYesNo = false)
        {
            try
            {
                return bool.Parse(s);
            }
            catch
            {
                if (acceptYesNo)
                {
                    if (s.ToLower() == "yes")
                    {
                        return true;
                    }
                    else if (s.ToLower() == "no")
                    {
                        return false;
                    }
                    else
                    {
                        throw new Exception("Error parsing bool.");
                    }
                }
                else
                {
                    throw new Exception("Error parsing bool.");
                }
            }
        }
    }
}
