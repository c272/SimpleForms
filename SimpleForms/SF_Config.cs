using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleForms
{
    public static class SF_Config
    {
        //Private property and get-set to return the default winforms icon.
        private static Icon defaultFormIcon;
        public static Icon DefaultFormIcon
        {
            get
            {
                if (defaultFormIcon == null)
                {
                    defaultFormIcon = (Icon)typeof(Form).
                         GetProperty("DefaultIcon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null, null);
                }
                return defaultFormIcon;
            }
        }

        //Private property and get-set to return the current set icon.
        private static Icon globalIcon = null;
        public static Icon GetIcon
        {
            get
            {
                if (globalIcon==null)
                {
                    return DefaultFormIcon;
                } else
                {
                    return globalIcon;
                }
            }
        }

        //Sets the icon for all SimpleForms forms. All new forms will have this icon upon creation.
        public static void SetIcon(Icon i)
        {
            globalIcon = i;
        }
    }
}
