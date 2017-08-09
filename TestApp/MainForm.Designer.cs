namespace TestApp
{
	partial class MainForm
	{
		/// <summary>
		/// Требуется переменная конструктора.
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
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnGo = new System.Windows.Forms.Button();
			this.txtAddress = new System.Windows.Forms.TextBox();
			this.btnPrint = new System.Windows.Forms.Button();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tabs = new System.Windows.Forms.TabControl();
			this.tab1 = new System.Windows.Forms.TabPage();
			this.tab2 = new System.Windows.Forms.TabPage();
			this.btnGCCollect = new System.Windows.Forms.Button();
			this.btnTestParser = new System.Windows.Forms.Button();
			this.statusStrip.SuspendLayout();
			this.tabs.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(12, 12);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 0;
			this.btnGo.Text = "Go!";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// txtAddress
			// 
			this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAddress.Location = new System.Drawing.Point(93, 14);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(421, 20);
			this.txtAddress.TabIndex = 2;
			this.txtAddress.Text = "about:blank";
			this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(12, 41);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(75, 23);
			this.btnPrint.TabIndex = 3;
			this.btnPrint.Text = "Print";
			this.btnPrint.UseVisualStyleBackColor = true;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
			this.statusStrip.Location = new System.Drawing.Point(0, 325);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(526, 22);
			this.statusStrip.TabIndex = 4;
			// 
			// lblStatus
			// 
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(52, 17);
			this.lblStatus.Text = "lblStatus";
			// 
			// tabs
			// 
			this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabs.Controls.Add(this.tab1);
			this.tabs.Controls.Add(this.tab2);
			this.tabs.Location = new System.Drawing.Point(12, 70);
			this.tabs.Name = "tabs";
			this.tabs.SelectedIndex = 0;
			this.tabs.Size = new System.Drawing.Size(502, 252);
			this.tabs.TabIndex = 5;
			// 
			// tab1
			// 
			this.tab1.Location = new System.Drawing.Point(4, 22);
			this.tab1.Name = "tab1";
			this.tab1.Padding = new System.Windows.Forms.Padding(3);
			this.tab1.Size = new System.Drawing.Size(494, 226);
			this.tab1.TabIndex = 0;
			this.tab1.Text = "Tab 1";
			this.tab1.UseVisualStyleBackColor = true;
			// 
			// tab2
			// 
			this.tab2.Location = new System.Drawing.Point(4, 22);
			this.tab2.Name = "tab2";
			this.tab2.Padding = new System.Windows.Forms.Padding(3);
			this.tab2.Size = new System.Drawing.Size(494, 226);
			this.tab2.TabIndex = 1;
			this.tab2.Text = "Tab 2";
			this.tab2.UseVisualStyleBackColor = true;
			// 
			// btnGCCollect
			// 
			this.btnGCCollect.Location = new System.Drawing.Point(93, 40);
			this.btnGCCollect.Name = "btnGCCollect";
			this.btnGCCollect.Size = new System.Drawing.Size(75, 23);
			this.btnGCCollect.TabIndex = 6;
			this.btnGCCollect.Text = "Collect";
			this.btnGCCollect.UseVisualStyleBackColor = true;
			this.btnGCCollect.Click += new System.EventHandler(this.btnGCCollect_Click);
			// 
			// btnTestParser
			// 
			this.btnTestParser.Location = new System.Drawing.Point(174, 41);
			this.btnTestParser.Name = "btnTestParser";
			this.btnTestParser.Size = new System.Drawing.Size(75, 23);
			this.btnTestParser.TabIndex = 7;
			this.btnTestParser.Text = "Parse";
			this.btnTestParser.UseVisualStyleBackColor = true;
			this.btnTestParser.Click += new System.EventHandler(this.btnTestParser_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(526, 347);
			this.Controls.Add(this.btnTestParser);
			this.Controls.Add(this.btnGCCollect);
			this.Controls.Add(this.tabs);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.btnGo);
			this.Name = "MainForm";
			this.Text = "TestApp";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.tabs.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.TextBox txtAddress;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel lblStatus;
		private System.Windows.Forms.TabControl tabs;
		private System.Windows.Forms.TabPage tab1;
		private System.Windows.Forms.TabPage tab2;
		private System.Windows.Forms.Button btnGCCollect;
		private System.Windows.Forms.Button btnTestParser;
	}
}

