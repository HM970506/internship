namespace GSIntaller
{
    partial class InstallPrepration
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
            this.GV_input = new System.Windows.Forms.DataGridView();
            this.GV_exist = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SubSystem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuildVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GV_input)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_exist)).BeginInit();
            this.SuspendLayout();
            // 
            // GV_input
            // 
            this.GV_input.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GV_input.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GV_input.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV_input.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubSystem,
            this.BuildVersion,
            this.Path});
            this.GV_input.Location = new System.Drawing.Point(18, 38);
            this.GV_input.Name = "GV_input";
            this.GV_input.RowHeadersVisible = false;
            this.GV_input.RowTemplate.Height = 23;
            this.GV_input.Size = new System.Drawing.Size(276, 198);
            this.GV_input.TabIndex = 30;
            // 
            // GV_exist
            // 
            this.GV_exist.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GV_exist.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.GV_exist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV_exist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4});
            this.GV_exist.Location = new System.Drawing.Point(300, 38);
            this.GV_exist.Name = "GV_exist";
            this.GV_exist.RowHeadersVisible = false;
            this.GV_exist.RowTemplate.Height = 23;
            this.GV_exist.Size = new System.Drawing.Size(284, 198);
            this.GV_exist.TabIndex = 31;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 12);
            this.label1.TabIndex = 32;
            this.label1.Text = "Target subsystem";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 33;
            this.label2.Text = "Installed subsystem";
            // 
            // SubSystem
            // 
            this.SubSystem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SubSystem.HeaderText = "SubSystem";
            this.SubSystem.Name = "SubSystem";
            this.SubSystem.Width = 95;
            // 
            // BuildVersion
            // 
            this.BuildVersion.HeaderText = "BuildVersion";
            this.BuildVersion.Name = "BuildVersion";
            // 
            // Path
            // 
            this.Path.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.Width = 55;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.HeaderText = "SubSystem";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 95;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "BuildVersion";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn4.HeaderText = "Path";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 55;
            // 
            // Step2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GV_exist);
            this.Controls.Add(this.GV_input);
            this.Name = "Step2";
            this.Size = new System.Drawing.Size(600, 250);
            ((System.ComponentModel.ISupportInitialize)(this.GV_input)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GV_exist)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView GV_input;
        private System.Windows.Forms.DataGridView GV_exist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuildVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
