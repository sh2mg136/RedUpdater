namespace RedDeploy
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && ( components != null ))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtPathMainSource = new System.Windows.Forms.TextBox();
            this.btnSetPathMainSource = new System.Windows.Forms.Button();
            this.btnSetDestinationPath = new System.Windows.Forms.Button();
            this.txtPathMainDestination = new System.Windows.Forms.TextBox();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnCompare = new DevExpress.XtraEditors.SimpleButton();
            this.btnDeploy = new DevExpress.XtraEditors.SimpleButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPathMainSource
            // 
            this.txtPathMainSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtPathMainSource.Location = new System.Drawing.Point(12, 12);
            this.txtPathMainSource.Name = "txtPathMainSource";
            this.txtPathMainSource.Size = new System.Drawing.Size(461, 22);
            this.txtPathMainSource.TabIndex = 0;
            this.txtPathMainSource.Text = "c:\\RedExpressPro-merged\\bin\\Debug\\";
            // 
            // btnSetPathMainSource
            // 
            this.btnSetPathMainSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetPathMainSource.Location = new System.Drawing.Point(479, 11);
            this.btnSetPathMainSource.Name = "btnSetPathMainSource";
            this.btnSetPathMainSource.Size = new System.Drawing.Size(45, 23);
            this.btnSetPathMainSource.TabIndex = 1;
            this.btnSetPathMainSource.Text = "...";
            this.btnSetPathMainSource.UseVisualStyleBackColor = true;
            this.btnSetPathMainSource.Click += new System.EventHandler(this.btnSetPathMainSource_Click);
            // 
            // btnSetDestinationPath
            // 
            this.btnSetDestinationPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetDestinationPath.Location = new System.Drawing.Point(479, 39);
            this.btnSetDestinationPath.Name = "btnSetDestinationPath";
            this.btnSetDestinationPath.Size = new System.Drawing.Size(45, 23);
            this.btnSetDestinationPath.TabIndex = 5;
            this.btnSetDestinationPath.Text = "...";
            this.btnSetDestinationPath.UseVisualStyleBackColor = true;
            this.btnSetDestinationPath.Click += new System.EventHandler(this.btnSetDestinationPath_Click);
            // 
            // txtPathMainDestination
            // 
            this.txtPathMainDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtPathMainDestination.Location = new System.Drawing.Point(12, 40);
            this.txtPathMainDestination.Name = "txtPathMainDestination";
            this.txtPathMainDestination.Size = new System.Drawing.Size(461, 22);
            this.txtPathMainDestination.TabIndex = 4;
            this.txtPathMainDestination.Text = "z:\\RedExpress.4.0\\maxx\\";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(545, 12);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(667, 191);
            this.gridControl1.TabIndex = 8;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // gridControl2
            // 
            this.gridControl2.Location = new System.Drawing.Point(12, 209);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(1200, 401);
            this.gridControl2.TabIndex = 9;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView2.OptionsView.ShowDetailButtons = false;
            this.gridView2.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView2_CustomDrawCell);
            // 
            // btnCompare
            // 
            this.btnCompare.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCompare.Appearance.Options.UseFont = true;
            this.btnCompare.Location = new System.Drawing.Point(12, 117);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(63, 51);
            this.btnCompare.TabIndex = 10;
            this.btnCompare.Text = "<>";
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // btnDeploy
            // 
            this.btnDeploy.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDeploy.Appearance.Options.UseFont = true;
            this.btnDeploy.Enabled = false;
            this.btnDeploy.Location = new System.Drawing.Point(81, 117);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(63, 51);
            this.btnDeploy.TabIndex = 11;
            this.btnDeploy.Text = ">>>";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 616);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1200, 11);
            this.progressBar1.TabIndex = 12;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 639);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnDeploy);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.gridControl2);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnSetDestinationPath);
            this.Controls.Add(this.txtPathMainDestination);
            this.Controls.Add(this.btnSetPathMainSource);
            this.Controls.Add(this.txtPathMainSource);
            this.Name = "FormMain";
            this.Text = "RedDeploy 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPathMainSource;
        private System.Windows.Forms.Button btnSetPathMainSource;
        private System.Windows.Forms.Button btnSetDestinationPath;
        private System.Windows.Forms.TextBox txtPathMainDestination;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.SimpleButton btnCompare;
        private DevExpress.XtraEditors.SimpleButton btnDeploy;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

