namespace GSIntaller
{
    partial class Input
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.server_checkbox = new System.Windows.Forms.CheckBox();
            this.client_checkbox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.download_checkbox = new System.Windows.Forms.RadioButton();
            this.update_checkbox = new System.Windows.Forms.RadioButton();
            this.restore_checkbox = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.open_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OpenZipFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.server_checkbox);
            this.groupBox2.Controls.Add(this.client_checkbox);
            this.groupBox2.Location = new System.Drawing.Point(56, 260);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(295, 91);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Path";
            // 
            // server_checkbox
            // 
            this.server_checkbox.AutoSize = true;
            this.server_checkbox.Location = new System.Drawing.Point(169, 44);
            this.server_checkbox.Name = "server_checkbox";
            this.server_checkbox.Size = new System.Drawing.Size(60, 16);
            this.server_checkbox.TabIndex = 3;
            this.server_checkbox.Text = "Server";
            this.server_checkbox.UseVisualStyleBackColor = true;
            // 
            // client_checkbox
            // 
            this.client_checkbox.AutoSize = true;
            this.client_checkbox.Location = new System.Drawing.Point(52, 44);
            this.client_checkbox.Name = "client_checkbox";
            this.client_checkbox.Size = new System.Drawing.Size(56, 16);
            this.client_checkbox.TabIndex = 2;
            this.client_checkbox.Text = "Client";
            this.client_checkbox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.download_checkbox);
            this.groupBox1.Controls.Add(this.update_checkbox);
            this.groupBox1.Controls.Add(this.restore_checkbox);
            this.groupBox1.Location = new System.Drawing.Point(266, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 92);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Installation type";
            // 
            // download_checkbox
            // 
            this.download_checkbox.AutoSize = true;
            this.download_checkbox.Location = new System.Drawing.Point(23, 42);
            this.download_checkbox.Name = "download_checkbox";
            this.download_checkbox.Size = new System.Drawing.Size(56, 16);
            this.download_checkbox.TabIndex = 6;
            this.download_checkbox.TabStop = true;
            this.download_checkbox.Text = "Install";
            this.download_checkbox.UseVisualStyleBackColor = true;
            // 
            // update_checkbox
            // 
            this.update_checkbox.AutoSize = true;
            this.update_checkbox.Location = new System.Drawing.Point(105, 42);
            this.update_checkbox.Name = "update_checkbox";
            this.update_checkbox.Size = new System.Drawing.Size(62, 16);
            this.update_checkbox.TabIndex = 7;
            this.update_checkbox.TabStop = true;
            this.update_checkbox.Text = "Update";
            this.update_checkbox.UseVisualStyleBackColor = true;
            // 
            // restore_checkbox
            // 
            this.restore_checkbox.AutoSize = true;
            this.restore_checkbox.Location = new System.Drawing.Point(196, 42);
            this.restore_checkbox.Name = "restore_checkbox";
            this.restore_checkbox.Size = new System.Drawing.Size(66, 16);
            this.restore_checkbox.TabIndex = 8;
            this.restore_checkbox.TabStop = true;
            this.restore_checkbox.Text = "Restore";
            this.restore_checkbox.UseVisualStyleBackColor = true;
            // 
            // open_button
            // 
            this.open_button.Location = new System.Drawing.Point(212, 24);
            this.open_button.Name = "open_button";
            this.open_button.Size = new System.Drawing.Size(25, 23);
            this.open_button.TabIndex = 28;
            this.open_button.Text = "..";
            this.open_button.UseVisualStyleBackColor = true;
            this.open_button.Click += new System.EventHandler(this.open_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OpenZipFile});
            this.dataGridView1.Location = new System.Drawing.Point(60, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(177, 174);
            this.dataGridView1.TabIndex = 29;
            // 
            // OpenZipFile
            // 
            this.OpenZipFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OpenZipFile.HeaderText = "";
            this.OpenZipFile.Name = "OpenZipFile";
            this.OpenZipFile.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(58, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "open file";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(117, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 2);
            this.label2.TabIndex = 30;
            // 
            // Step1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.open_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Step1";
            this.Size = new System.Drawing.Size(600, 250);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton download_checkbox;
        private System.Windows.Forms.RadioButton update_checkbox;
        private System.Windows.Forms.RadioButton restore_checkbox;
        private System.Windows.Forms.CheckBox server_checkbox;
        private System.Windows.Forms.CheckBox client_checkbox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button open_button;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ColorDialog colorDialog2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpenZipFile;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
    }
}
