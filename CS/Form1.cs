using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraLayout;
using DevExpress.XtraEditors;


namespace WindowsApplication56 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            // TODO: This line of code loads data into the 'nwindDataSet.Categories' table. You can move, or remove it, as needed.
            this.categoriesTableAdapter.Fill(this.nwindDataSet.Categories);

            dataLayoutControl1.RetrieveFields();

            foreach (BaseLayoutItem baseItem in dataLayoutControl1.Items) {
                LayoutControlItem item = baseItem as LayoutControlItem;
                if (item != null && item.Control.DataBindings.Count > 0)
                    if (item.Control.DataBindings[0].BindingMemberInfo.BindingField == "Description") {
                        dataLayoutControl1.BeginUpdate();
                        Control prevControl = item.Control;
                        Binding binding = prevControl.DataBindings[0];
                        prevControl.DataBindings.Clear();
                        dataLayoutControl1.Controls.Remove(prevControl);
                        Control newControl = new MemoEdit();
                        newControl.Name = "myMemoEdit";
                        // Bind the new control to the same field as the previous control.
                        newControl.DataBindings.Add(new Binding(binding.PropertyName, binding.DataSource,
                            binding.BindingMemberInfo.BindingField, binding.FormattingEnabled));
                        dataLayoutControl1.Controls.Add(newControl);
                        item.Control = newControl;
                        prevControl.Dispose();
                        dataLayoutControl1.EndUpdate();
                        // Change the item's size after the EndUpdate method.
                        item.Size = new Size(100, 80);
                        break;
                    }
            }
        }
    }
}