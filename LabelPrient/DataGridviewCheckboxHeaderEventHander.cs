﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelPrint
{
    public class DataGridviewCheckboxHeaderEventHander : EventArgs
    {
        private bool checkedState = false;

        public bool CheckedState
        {
            get { return checkedState; }
            set { checkedState = value; }
        }
    }

}
