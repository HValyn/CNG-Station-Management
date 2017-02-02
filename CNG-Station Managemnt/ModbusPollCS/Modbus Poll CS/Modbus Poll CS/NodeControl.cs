using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modbus_Poll_CS
{
    public partial class NodeControl : UserControl
    {
        public NodeControl()
        {
            InitializeComponent();
            
        }
        public void setStatus(String status) 
        {
            statusTextBox.Text = status;
            statusTextBox.TextAlign = HorizontalAlignment.Center;
        }
        public void setUnitRate(Double tempValue) 
        {
            tempValue = tempValue / 100;
            UnitRate.Text = String.Format("{0:0.00}", tempValue);
            UnitRate.TextAlign = HorizontalAlignment.Center;
        }
        public void setFilledQTY(Double tempValue)
        {
            tempValue = tempValue / 100;
            FilledQTY.Text = String.Format("{0:0.00}", tempValue);
            FilledQTY.TextAlign = HorizontalAlignment.Center;
        }
        public void setFilledAMT(Double tempValue)
        {
            tempValue = tempValue / 100;
            FilledAMT.Text = String.Format("{0:0.00}", tempValue);
            FilledAMT.TextAlign = HorizontalAlignment.Center;
        }
        public void setTotAMT(Double tempValue)
        {
            tempValue = tempValue / 100;
            TotAMT.Text = String.Format("{0:0.00}", tempValue);
            TotAMT.TextAlign = HorizontalAlignment.Center;
        }
        public void setTotQTY(Double tempValue)
        {
            tempValue = tempValue / 100;
            TotQTY.Text = String.Format("{0:0.00}", tempValue);
            TotQTY.TextAlign = HorizontalAlignment.Center;
        }
        public void setbackColor(Color backColor) 
        {
            groupBox4.BackColor = backColor;
        }
        public void setIndexandFuelType(int index, String Fuel) 
        {
            indexLabel.Text = index.ToString();
            fuelTypeLabel.Text = Fuel;
        }
    }
}
