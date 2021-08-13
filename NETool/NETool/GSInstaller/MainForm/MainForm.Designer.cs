namespace GSIntaller
{
    partial class MainForm
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
            this.next_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.previous_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.exit_buttion = new System.Windows.Forms.Button();
            this.testimage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.testimage)).BeginInit();
            this.SuspendLayout();
            // 
            // next_button
            // 
            this.next_button.Location = new System.Drawing.Point(477, 346);
            this.next_button.Name = "next_button";
            this.next_button.Size = new System.Drawing.Size(85, 23);
            this.next_button.TabIndex = 5;
            this.next_button.Text = "Next";
            this.next_button.UseVisualStyleBackColor = true;
            this.next_button.Click += new System.EventHandler(this.next_button_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(-8, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(600, 2);
            this.label2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(-8, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(600, 2);
            this.label3.TabIndex = 10;
            // 
            // previous_button
            // 
            this.previous_button.Location = new System.Drawing.Point(386, 346);
            this.previous_button.Name = "previous_button";
            this.previous_button.Size = new System.Drawing.Size(85, 23);
            this.previous_button.TabIndex = 11;
            this.previous_button.Text = "Previous";
            this.previous_button.UseVisualStyleBackColor = true;
            this.previous_button.Visible = false;
            this.previous_button.Click += new System.EventHandler(this.before_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(30, 346);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(85, 23);
            this.cancel_button.TabIndex = 12;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Visible = false;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // exit_buttion
            // 
            this.exit_buttion.Location = new System.Drawing.Point(477, 346);
            this.exit_buttion.Name = "exit_buttion";
            this.exit_buttion.Size = new System.Drawing.Size(85, 23);
            this.exit_buttion.TabIndex = 13;
            this.exit_buttion.Text = "Exit";
            this.exit_buttion.UseVisualStyleBackColor = true;
            this.exit_buttion.Visible = false;
            this.exit_buttion.Click += new System.EventHandler(this.exit_buttion_Click);
            // 
            // testimage
            // 
            this.testimage.Location = new System.Drawing.Point(1, 1);
            this.testimage.Name = "testimage";
            this.testimage.Size = new System.Drawing.Size(581, 68);
            this.testimage.TabIndex = 16;
            this.testimage.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 391);
            this.Controls.Add(this.testimage);
            this.Controls.Add(this.exit_buttion);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.previous_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.next_button);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.testimage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button next_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button previous_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button exit_buttion;
        private System.Windows.Forms.PictureBox testimage;
    }
}

