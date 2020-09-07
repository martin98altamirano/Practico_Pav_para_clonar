using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PAV_3K2_3_NEWWARESOFT.Utils
{
    public class FormUtils
    {
        //Ayuda para cargar los ComboBox
        public static void CargarCombo(ref ComboBox cb, BindingSource conector, string displayMember, string valueMember)
        {
            cb.DataSource = conector;
            cb.DisplayMember = displayMember;
            cb.ValueMember = valueMember;
        }
    }
}
