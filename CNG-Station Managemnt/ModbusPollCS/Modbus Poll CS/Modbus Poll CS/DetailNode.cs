using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modbus_Poll_CS
{
    public partial class DetailNode : Form
    {
        public DetailNode()
        {
            InitializeComponent();
        }

        private void transactionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.transactionBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cNGDatabaseDataSet);

        }

        private void DetailNode_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cNGDatabaseDataSet.Transaction' table. You can move, or remove it, as needed.
            this.transactionTableAdapter1.Fill(this.cNGDatabaseDataSet.Transaction);
            // TODO: This line of code loads data into the 'cNGDatabaseDataSet.Transaction' table. You can move, or remove it, as needed.
            //this.transactionTableAdapter.Fill(this.cNGDatabaseDataSet.Transaction);

           /* this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();*/
        }

        private void transactionBindingNavigatorSaveItem_Click_1(object sender, EventArgs e)
        {
            this.Validate();
            this.transactionBindingSource.EndEdit();
            this.tableAdapterManager1.UpdateAll(this.cNGDatabaseDataSet);

        }
    }
}
