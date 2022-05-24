
namespace Blockchain
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.UsernameView = new System.Windows.Forms.TextBox();
            this.UriView = new System.Windows.Forms.TextBox();
            this.PortView = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prevHashDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blockTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.blocksBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.chainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.blocksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.datasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blocksBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chainBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blocksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.datasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(637, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 134);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add new block";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(637, 278);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 123);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(618, 316);
            this.listBox1.TabIndex = 2;
            // 
            // UsernameView
            // 
            this.UsernameView.Enabled = false;
            this.UsernameView.Location = new System.Drawing.Point(70, 12);
            this.UsernameView.Name = "UsernameView";
            this.UsernameView.Size = new System.Drawing.Size(100, 20);
            this.UsernameView.TabIndex = 3;
            // 
            // UriView
            // 
            this.UriView.Enabled = false;
            this.UriView.Location = new System.Drawing.Point(500, 12);
            this.UriView.Name = "UriView";
            this.UriView.Size = new System.Drawing.Size(196, 20);
            this.UriView.TabIndex = 4;
            // 
            // PortView
            // 
            this.PortView.Enabled = false;
            this.PortView.Location = new System.Drawing.Point(734, 12);
            this.PortView.Name = "PortView";
            this.PortView.Size = new System.Drawing.Size(54, 20);
            this.PortView.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(702, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(474, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Uri";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Username";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(619, 20);
            this.textBox2.TabIndex = 10;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(636, 52);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(151, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Search by block hash";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(637, 79);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(150, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "Search by user public key";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(637, 108);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(150, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "Search by username";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.dataDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.hashDataGridViewTextBoxColumn,
            this.prevHashDataGridViewTextBoxColumn,
            this.userDataGridViewTextBoxColumn,
            this.blockTypeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.blocksBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 89);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.ReadOnly = true;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(618, 349);
            this.dataGridView1.TabIndex = 14;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataDataGridViewTextBoxColumn
            // 
            this.dataDataGridViewTextBoxColumn.DataPropertyName = "Data";
            this.dataDataGridViewTextBoxColumn.HeaderText = "Data";
            this.dataDataGridViewTextBoxColumn.Name = "dataDataGridViewTextBoxColumn";
            this.dataDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // hashDataGridViewTextBoxColumn
            // 
            this.hashDataGridViewTextBoxColumn.DataPropertyName = "Hash";
            this.hashDataGridViewTextBoxColumn.HeaderText = "Hash";
            this.hashDataGridViewTextBoxColumn.Name = "hashDataGridViewTextBoxColumn";
            this.hashDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // prevHashDataGridViewTextBoxColumn
            // 
            this.prevHashDataGridViewTextBoxColumn.DataPropertyName = "PrevHash";
            this.prevHashDataGridViewTextBoxColumn.HeaderText = "PrevHash";
            this.prevHashDataGridViewTextBoxColumn.Name = "prevHashDataGridViewTextBoxColumn";
            this.prevHashDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // userDataGridViewTextBoxColumn
            // 
            this.userDataGridViewTextBoxColumn.DataPropertyName = "User";
            this.userDataGridViewTextBoxColumn.HeaderText = "User";
            this.userDataGridViewTextBoxColumn.Name = "userDataGridViewTextBoxColumn";
            this.userDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // blockTypeDataGridViewTextBoxColumn
            // 
            this.blockTypeDataGridViewTextBoxColumn.DataPropertyName = "BlockType";
            this.blockTypeDataGridViewTextBoxColumn.HeaderText = "BlockType";
            this.blockTypeDataGridViewTextBoxColumn.Name = "blockTypeDataGridViewTextBoxColumn";
            this.blockTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // blocksBindingSource1
            // 
            this.blocksBindingSource1.DataMember = "Blocks";
            this.blocksBindingSource1.DataSource = this.chainBindingSource;
            // 
            // chainBindingSource
            // 
            this.chainBindingSource.DataSource = typeof(Blockchain.Chain);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(637, 249);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(150, 23);
            this.button6.TabIndex = 15;
            this.button6.Text = "Find file to add";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(636, 157);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(150, 23);
            this.button7.TabIndex = 16;
            this.button7.Text = "Show my data";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(636, 186);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(150, 23);
            this.button8.TabIndex = 17;
            this.button8.Text = "Show all data";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(318, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Reload";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // blocksBindingSource
            // 
            this.blocksBindingSource.DataMember = "Blocks";
            this.blocksBindingSource.DataSource = this.chainBindingSource;
            // 
            // datasBindingSource
            // 
            this.datasBindingSource.DataMember = "Datas";
            this.datasBindingSource.DataSource = this.chainBindingSource;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortView);
            this.Controls.Add(this.UriView);
            this.Controls.Add(this.UsernameView);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Blockchain";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blocksBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chainBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blocksBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox UsernameView;
        public System.Windows.Forms.TextBox UriView;
        public System.Windows.Forms.TextBox PortView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.BindingSource chainBindingSource;
        private System.Windows.Forms.BindingSource blocksBindingSource;
        private System.Windows.Forms.BindingSource datasBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hashDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn prevHashDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn blockTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource blocksBindingSource1;
    }
}

