using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LabelPrint
{

    public delegate void DataGridviewCheckboxHeaderCellEventHander(object sender, DataGridviewCheckboxHeaderEventHander e);

    public class DataGridviewCheckboxHeaderCell : DataGridViewColumnHeaderCell
    {
        private Point checkBoxLocation;
        private Size checkBoxSize;
        private bool isChecked = false;
        private Point cellLocation = new Point();
        private System.Windows.Forms.VisualStyles.CheckBoxState cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;


        public event DataGridviewCheckboxHeaderCellEventHander OnCheckBoxClicked;


        protected override void Paint(Graphics g,
                                          Rectangle clipBounds,
                                          Rectangle cellBounds,
                                          int rowIndex,
                                          DataGridViewElementStates dataGridViewElementState,
                                          object value,
                                          object formattedValue,
                                          string errorText,
                                          DataGridViewCellStyle cellStyle,
                                          DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                          DataGridViewPaintParts paintParts)
        {
            base.Paint(g, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            Point p = new Point();
            Size s = CheckBoxRenderer.GetGlyphSize(g, System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
            //列头checkbox的X坐标
            p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2) - 1;
            //列头checkbox的Y坐标
            p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
            cellLocation = cellBounds.Location;
            checkBoxLocation = p;
            checkBoxSize = s;
            if (isChecked)
                cbState = System.Windows.Forms.VisualStyles.CheckBoxState.CheckedNormal;
            else
                cbState = System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
            //绘制复选框
            CheckBoxRenderer.DrawCheckBox(g, checkBoxLocation, cbState);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Point p = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
            if (p.X >= checkBoxLocation.X && p.X <= checkBoxLocation.X + checkBoxSize.Width && p.Y >= checkBoxLocation.Y && p.Y <= checkBoxLocation.Y + checkBoxSize.Height)
            {
                isChecked = !isChecked;

                //获取列头checkbox的选择状态
                DataGridviewCheckboxHeaderEventHander ex = new DataGridviewCheckboxHeaderEventHander();
                ex.CheckedState = isChecked;

                //此处不代表选择的列头checkbox，只是作为参数传递。应该列头checkbox是绘制出来的，无法获得它的实例
                object sender = new object();

                if (OnCheckBoxClicked != null)
                {
                    //触发单击事件
                    OnCheckBoxClicked(sender, ex);
                    this.DataGridView.InvalidateCell(this);
                }

            }
            base.OnMouseClick(e);
        }
    }
}
