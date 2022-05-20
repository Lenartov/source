
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.UsernameView = new System.Windows.Forms.TextBox();
            this.UriView = new System.Windows.Forms.TextBox();
            this.PortView = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(602, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 20);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add new block";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 249);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(584, 20);
            this.textBox1.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 285);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(776, 160);
            this.listBox1.TabIndex = 2;
            // 
            // UsernameView
            // 
            this.UsernameView.Enabled = false;
            this.UsernameView.Location = new System.Drawing.Point(12, 12);
            this.UsernameView.Name = "UsernameView";
            this.UsernameView.Size = new System.Drawing.Size(100, 20);
            this.UsernameView.TabIndex = 3;
            // 
            // UriView
            // 
            this.UriView.Enabled = false;
            this.UriView.Location = new System.Drawing.Point(144, 12);
            this.UriView.Name = "UriView";
            this.UriView.Size = new System.Drawing.Size(211, 20);
            this.UriView.TabIndex = 4;
            // 
            // Port
            // 
            this.PortView.Enabled = false;
            this.PortView.Location = new System.Drawing.Point(361, 12);
            this.PortView.Name = "Port";
            this.PortView.Size = new System.Drawing.Size(211, 20);
            this.PortView.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.PortView);
            this.Controls.Add(this.UriView);
            this.Controls.Add(this.UsernameView);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

