using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LabelPrint
{
    public class DateEdit : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView grid;
        private bool valuechanged = false;
        int rowindex;
        public DateEdit()
            : base()
        {
            //设置控件的最大日期与格式"yyyy-MM-dd"
            this.MaxDate = DateTime.Now;
            this.Format = DateTimePickerFormat.Short;
        }
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToString("yyyy-MM-dd");
            }
            set
            {
                if (value != null)
                    this.Value = Convert.ToDateTime(value);
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowindex;
            }
            set
            {
                rowindex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valuechanged ;
            }
            set
            {
                valuechanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {

        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            valuechanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}
