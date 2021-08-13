namespace GSIntaller
{
    partial class Install
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.script_textbox = new System.Windows.Forms.TextBox();
            this.state_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 209);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(548, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // script_textbox
            // 
            this.script_textbox.BackColor = System.Drawing.SystemColors.Window;
            this.script_textbox.Location = new System.Drawing.Point(23, 12);
            this.script_textbox.Multiline = true;
            this.script_textbox.Name = "script_textbox";
            this.script_textbox.ReadOnly = true;
            this.script_textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.script_textbox.Size = new System.Drawing.Size(548, 166);
            this.script_textbox.TabIndex = 5;
            this.script_textbox.TextChanged += new System.EventHandler(this.script_textbox_TextChanged);
            // 
            // state_label
            // 
            this.state_label.AutoSize = true;
            this.state_label.Location = new System.Drawing.Point(21, 189);
            this.state_label.Name = "state_label";
            this.state_label.Size = new System.Drawing.Size(25, 12);
            this.state_label.TabIndex = 6;
            this.state_label.Text = "test";
            this.state_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Install
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.state_label);
            this.Controls.Add(this.script_textbox);
            this.Controls.Add(this.progressBar1);
            this.Name = "Install";
            this.Size = new System.Drawing.Size(600, 250);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TextBox script_textbox;
        public System.Windows.Forms.Label state_label;
    }
}
