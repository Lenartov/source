﻿
namespace Doctoral_accounting
{
    partial class Main
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
            this.blocksBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UsernameView = new System.Windows.Forms.TextBox();
            this.patientText = new System.Windows.Forms.TextBox();
            this.diagnosesText = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.Treatment = new System.Windows.Forms.Label();
            this.treatmentText = new System.Windows.Forms.TextBox();
            this.analizesText = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.patientSurNameText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comentaryText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.blocksBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chainBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // blocksBindingSource
            // 
            this.blocksBindingSource.DataMember = "Blocks";
            this.blocksBindingSource.DataSource = this.chainBindingSource;
            // 
            // chainBindingSource
            // 
            this.chainBindingSource.DataSource = typeof(Blockchain.Chain);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 135);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(366, 438);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // UsernameView
            // 
            this.UsernameView.Location = new System.Drawing.Point(12, 75);
            this.UsernameView.Name = "UsernameView";
            this.UsernameView.ReadOnly = true;
            this.UsernameView.Size = new System.Drawing.Size(245, 20);
            this.UsernameView.TabIndex = 1;
            // 
            // patientText
            // 
            this.patientText.Location = new System.Drawing.Point(24, 25);
            this.patientText.Name = "patientText";
            this.patientText.Size = new System.Drawing.Size(193, 20);
            this.patientText.TabIndex = 2;
            // 
            // diagnosesText
            // 
            this.diagnosesText.Location = new System.Drawing.Point(24, 120);
            this.diagnosesText.Name = "diagnosesText";
            this.diagnosesText.Size = new System.Drawing.Size(473, 20);
            this.diagnosesText.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Treatment);
            this.panel1.Controls.Add(this.treatmentText);
            this.panel1.Controls.Add(this.analizesText);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.patientSurNameText);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comentaryText);
            this.panel1.Controls.Add(this.patientText);
            this.panel1.Controls.Add(this.diagnosesText);
            this.panel1.Location = new System.Drawing.Point(384, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 505);
            this.panel1.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 436);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(24, 452);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(422, 449);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Save new";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // Treatment
            // 
            this.Treatment.AutoSize = true;
            this.Treatment.Location = new System.Drawing.Point(21, 395);
            this.Treatment.Name = "Treatment";
            this.Treatment.Size = new System.Drawing.Size(55, 13);
            this.Treatment.TabIndex = 13;
            this.Treatment.Text = "Treatment";
            // 
            // treatmentText
            // 
            this.treatmentText.Location = new System.Drawing.Point(24, 411);
            this.treatmentText.Name = "treatmentText";
            this.treatmentText.Size = new System.Drawing.Size(473, 20);
            this.treatmentText.TabIndex = 12;
            // 
            // analizesText
            // 
            this.analizesText.AutoSize = true;
            this.analizesText.Location = new System.Drawing.Point(21, 275);
            this.analizesText.Name = "analizesText";
            this.analizesText.Size = new System.Drawing.Size(49, 13);
            this.analizesText.TabIndex = 11;
            this.analizesText.Text = "Analyzes";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 291);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(473, 92);
            this.textBox1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Coment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Diagnos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Patient Surname";
            // 
            // patientSurNameText
            // 
            this.patientSurNameText.Location = new System.Drawing.Point(24, 67);
            this.patientSurNameText.Name = "patientSurNameText";
            this.patientSurNameText.Size = new System.Drawing.Size(193, 20);
            this.patientSurNameText.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Patient Name";
            // 
            // comentaryText
            // 
            this.comentaryText.AllowDrop = true;
            this.comentaryText.Location = new System.Drawing.Point(24, 163);
            this.comentaryText.Multiline = true;
            this.comentaryText.Name = "comentaryText";
            this.comentaryText.Size = new System.Drawing.Size(473, 100);
            this.comentaryText.TabIndex = 4;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(898, 585);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.UsernameView);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Main";
            this.Sizable = false;
            this.Text = "Doctoral accounting";
            ((System.ComponentModel.ISupportInitialize)(this.blocksBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chainBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource blocksBindingSource;
        private System.Windows.Forms.BindingSource chainBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox UsernameView;
        private System.Windows.Forms.TextBox patientText;
        private System.Windows.Forms.TextBox diagnosesText;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox comentaryText;
        private System.Windows.Forms.TextBox patientSurNameText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label analizesText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Treatment;
        private System.Windows.Forms.TextBox treatmentText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

