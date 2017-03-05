using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPrint
{
    public class DateColumn : DataGridViewColumn
    {
        private bool showupdown = true;

        public bool Showupdown
        {
            get { return showupdown; }
            set { showupdown = value; }
        }

        public DateColumn() : base(new DateCell()) { }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DateCell)))
                    throw new InvalidCastException("列中只能使用日期单元格");
                base.CellTemplate = value;
            }
        }
    }

}
