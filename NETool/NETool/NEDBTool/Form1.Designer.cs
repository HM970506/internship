namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.server_input = new System.Windows.Forms.TextBox();
            this.db_input = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.Label();
            this.id_input = new System.Windows.Forms.TextBox();
            this.pw_input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.connect_button = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.table_label = new System.Windows.Forms.Label();
            this.table_input = new System.Windows.Forms.TextBox();
            this.unconnect_button = new System.Windows.Forms.Button();
            this.isconnect_label = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucXmlRichTextBox1 = new CustomXmlViewer.ucXmlRichTextBox();
            this.up_button = new System.Windows.Forms.Button();
            this.down_button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.search_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // server_input
            // 
            this.server_input.Location = new System.Drawing.Point(71, 12);
            this.server_input.Name = "server_input";
            this.server_input.Size = new System.Drawing.Size(398, 21);
            this.server_input.TabIndex = 0;
            // 
            // db_input
            // 
            this.db_input.Location = new System.Drawing.Point(71, 38);
            this.db_input.Name = "db_input";
            this.db_input.Size = new System.Drawing.Size(151, 21);
            this.db_input.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Server";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "DB Name";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(378, 200);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 12);
            this.label.TabIndex = 13;
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Location = new System.Drawing.Point(481, 14);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(22, 12);
            this.id.TabIndex = 14;
            this.id.Text = "uid";
            // 
            // id_input
            // 
            this.id_input.Location = new System.Drawing.Point(509, 11);
            this.id_input.Name = "id_input";
            this.id_input.Size = new System.Drawing.Size(125, 21);
            this.id_input.TabIndex = 3;
            // 
            // pw_input
            // 
            this.pw_input.Location = new System.Drawing.Point(509, 40);
            this.pw_input.Name = "pw_input";
            this.pw_input.Size = new System.Drawing.Size(125, 21);
            this.pw_input.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(480, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "pwd";
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(656, 11);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(101, 23);
            this.connect_button.TabIndex = 5;
            this.connect_button.Text = "connect";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // save_button
            // 
            this.save_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.save_button.Location = new System.Drawing.Point(1291, 47);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(112, 23);
            this.save_button.TabIndex = 9;
            this.save_button.Text = "save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // table_label
            // 
            this.table_label.AutoSize = true;
            this.table_label.Location = new System.Drawing.Point(228, 43);
            this.table_label.Name = "table_label";
            this.table_label.Size = new System.Drawing.Size(75, 12);
            this.table_label.TabIndex = 26;
            this.table_label.Text = "Table Name";
            // 
            // table_input
            // 
            this.table_input.Location = new System.Drawing.Point(309, 39);
            this.table_input.Name = "table_input";
            this.table_input.Size = new System.Drawing.Size(160, 21);
            this.table_input.TabIndex = 2;
            // 
            // unconnect_button
            // 
            this.unconnect_button.Location = new System.Drawing.Point(656, 41);
            this.unconnect_button.Name = "unconnect_button";
            this.unconnect_button.Size = new System.Drawing.Size(101, 23);
            this.unconnect_button.TabIndex = 27;
            this.unconnect_button.Text = "unconnect";
            this.unconnect_button.UseVisualStyleBackColor = true;
            this.unconnect_button.Click += new System.EventHandler(this.unconnect_button_Click);
            // 
            // isconnect_label
            // 
            this.isconnect_label.AutoSize = true;
            this.isconnect_label.Location = new System.Drawing.Point(774, 47);
            this.isconnect_label.Name = "isconnect_label";
            this.isconnect_label.Size = new System.Drawing.Size(64, 12);
            this.isconnect_label.TabIndex = 21;
            this.isconnect_label.Text = "unconnect";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(455, 470);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datagridview1_cellclick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 558);
            this.splitter1.TabIndex = 30;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(3, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 558);
            this.splitter2.TabIndex = 31;
            this.splitter2.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(26, 76);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucXmlRichTextBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1377, 470);
            this.splitContainer1.SplitterDistance = 458;
            this.splitContainer1.TabIndex = 32;
            // 
            // ucXmlRichTextBox1
            // 
            this.ucXmlRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucXmlRichTextBox1.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucXmlRichTextBox1.Location = new System.Drawing.Point(-1, 0);
            this.ucXmlRichTextBox1.Name = "ucXmlRichTextBox1";
            this.ucXmlRichTextBox1.Size = new System.Drawing.Size(913, 467);
            this.ucXmlRichTextBox1.TabIndex = 29;
            this.ucXmlRichTextBox1.Text = "";
            this.ucXmlRichTextBox1.Xml = "";
            // 
            // up_button
            // 
            this.up_button.Enabled = false;
            this.up_button.Location = new System.Drawing.Point(935, 15);
            this.up_button.Name = "up_button";
            this.up_button.Size = new System.Drawing.Size(75, 23);
            this.up_button.TabIndex = 34;
            this.up_button.Text = "up";
            this.up_button.UseVisualStyleBackColor = true;
            this.up_button.Click += new System.EventHandler(this.up_button_Click);
            // 
            // down_button
            // 
            this.down_button.Enabled = false;
            this.down_button.Location = new System.Drawing.Point(1016, 15);
            this.down_button.Name = "down_button";
            this.down_button.Size = new System.Drawing.Size(75, 23);
            this.down_button.TabIndex = 35;
            this.down_button.Text = "down";
            this.down_button.UseVisualStyleBackColor = true;
            this.down_button.Click += new System.EventHandler(this.down_button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(876, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "font size";
            // 
            // search_button
            // 
            this.search_button.Enabled = false;
            this.search_button.Location = new System.Drawing.Point(1040, 47);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(75, 23);
            this.search_button.TabIndex = 37;
            this.search_button.Text = "highlight";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(878, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 21);
            this.textBox1.TabIndex = 38;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1415, 558);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.down_button);
            this.Controls.Add(this.up_button);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.save_button);
            this.Controls.Add(this.unconnect_button);
            this.Controls.Add(this.table_label);
            this.Controls.Add(this.table_input);
            this.Controls.Add(this.isconnect_label);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.pw_input);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.id_input);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.db_input);
            this.Controls.Add(this.server_input);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "NEDBTool";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox server_input;
        private System.Windows.Forms.TextBox db_input;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label id;
        private System.Windows.Forms.TextBox id_input;
        private System.Windows.Forms.TextBox pw_input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Label table_label;
        private System.Windows.Forms.TextBox table_input;
        private System.Windows.Forms.Button unconnect_button;
        private System.Windows.Forms.Label isconnect_label;
        private System.Windows.Forms.DataGridView dataGridView1;
        private CustomXmlViewer.ucXmlRichTextBox ucXmlRichTextBox1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button up_button;
        private System.Windows.Forms.Button down_button;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    }
}

