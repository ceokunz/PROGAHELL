using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_2
{
    public class IconItem
    {
        public string IconPath { get; set; }
        public string IconName { get; set; }

        public IconItem(string iconPath)
        {
            IconPath = iconPath;

            string[] m = iconPath.Split(new char[] { '\\' });

            IconName = m.Last();
        }
    }
}
