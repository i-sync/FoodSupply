using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPrint
{
    public class DateCell : DataGridViewTextBoxCell
    {
        private DateEdit ne;
        public DateCell() : base() { }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            ne = DataGridView.EditingControl as DateEdit;
            ne.Value = Convert.ToDateTime(this.Value);
            if (ne != null)
            {
                //ne.Format = DateTimePickerFormat.Custom;
                ne.CustomFormat = dataGridViewCellStyle.Format;
                //ne.ShowUpDown = ((DateColumn)this.OwningColumn).Showupdown;
            }
        }
        public override Type EditType
        {
            get
            {
                return typeof(DateEdit);
            }
        }
        public override Type ValueType
        {
            get
            {
                return typeof(DateTime);
            }
        }
        protected override object GetValue(int rowIndex)
        {
            return base.GetValue(rowIndex);
        }
        public override object DefaultNewRowValue
        {
            get
            {
                return DateTime.Now;
            }
        }
    }

}
