namespace PosAttDataGenerator
{
    partial class PosAttDataGenerator
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.time_textbox = new System.Windows.Forms.TextBox();
            this.interval_textbox = new System.Windows.Forms.TextBox();
            this.inputpath_label = new System.Windows.Forms.Label();
            this.outputpath_label = new System.Windows.Forms.Label();
            this.time_label = new System.Windows.Forms.Label();
            this.interval_label = new System.Windows.Forms.Label();
            this.transfer_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // time_textbox
            // 
            this.time_textbox.Location = new System.Drawing.Point(111, 160);
            this.time_textbox.Name = "time_textbox";
            this.time_textbox.Size = new System.Drawing.Size(187, 21);
            this.time_textbox.TabIndex = 2;
            // 
            // interval_textbox
            // 
            this.interval_textbox.Location = new System.Drawing.Point(111, 190);
            this.interval_textbox.Name = "interval_textbox";
            this.interval_textbox.Size = new System.Drawing.Size(187, 21);
            this.interval_textbox.TabIndex = 3;
            // 
            // inputpath_label
            // 
            this.inputpath_label.AutoSize = true;
            this.inputpath_label.Location = new System.Drawing.Point(127, 39);
            this.inputpath_label.Name = "inputpath_label";
            this.inputpath_label.Size = new System.Drawing.Size(25, 12);
            this.inputpath_label.TabIndex = 7;
            this.inputpath_label.Text = "test";
            // 
            // outputpath_label
            // 
            this.outputpath_label.AutoSize = true;
            this.outputpath_label.Location = new System.Drawing.Point(127, 85);
            this.outputpath_label.Name = "outputpath_label";
            this.outputpath_label.Size = new System.Drawing.Size(25, 12);
            this.outputpath_label.TabIndex = 8;
            this.outputpath_label.Text = "test";
            // 
            // time_label
            // 
            this.time_label.AutoSize = true;
            this.time_label.Location = new System.Drawing.Point(36, 163);
            this.time_label.Name = "time_label";
            this.time_label.Size = new System.Drawing.Size(53, 12);
            this.time_label.TabIndex = 9;
            this.time_label.Text = "start시각";
            // 
            // interval_label
            // 
            this.interval_label.AutoSize = true;
            this.interval_label.Location = new System.Drawing.Point(36, 190);
            this.interval_label.Name = "interval_label";
            this.interval_label.Size = new System.Drawing.Size(29, 12);
            this.interval_label.TabIndex = 10;
            this.interval_label.Text = "간격";
            // 
            // transfer_button
            // 
            this.transfer_button.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.transfer_button.Location = new System.Drawing.Point(353, 163);
            this.transfer_button.Name = "transfer_button";
            this.transfer_button.Size = new System.Drawing.Size(75, 23);
            this.transfer_button.TabIndex = 12;
            this.transfer_button.Text = "변환";
            this.transfer_button.UseVisualStyleBackColor = true;
            this.transfer_button.Click += new System.EventHandler(this.transfer_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(302, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Hz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "input folder:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "output folder:";
            // 
            // PosAttDataGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 242);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.transfer_button);
            this.Controls.Add(this.interval_label);
            this.Controls.Add(this.time_label);
            this.Controls.Add(this.outputpath_label);
            this.Controls.Add(this.inputpath_label);
            this.Controls.Add(this.interval_textbox);
            this.Controls.Add(this.time_textbox);
            this.Name = "PosAttDataGenerator";
            this.Text = "PosAttDataGenerator";
            this.Load += new System.EventHandler(this.PosAttDataGenerator_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox time_textbox;
        private System.Windows.Forms.TextBox interval_textbox;
        private System.Windows.Forms.Label inputpath_label;
        private System.Windows.Forms.Label outputpath_label;
        private System.Windows.Forms.Label time_label;
        private System.Windows.Forms.Label interval_label;
        private System.Windows.Forms.Button transfer_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

