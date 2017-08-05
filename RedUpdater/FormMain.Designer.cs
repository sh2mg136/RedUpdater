namespace RedUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.gridControlMain = new DevExpress.XtraGrid.GridControl();
            this.gridViewMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlPlugins = new DevExpress.XtraGrid.GridControl();
            this.gridViewPlugins = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.progressBarControlMain = new DevExpress.XtraEditors.ProgressBarControl();
            this.txtSourcePath = new DevExpress.XtraEditors.TextEdit();
            this.txtDestinationPath = new DevExpress.XtraEditors.TextEdit();
            this.btnSetSourcePath = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSetDestinationPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRunRed = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPlugins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlugins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlMain.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourcePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestinationPath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlMain
            // 
            this.gridControlMain.Location = new System.Drawing.Point(12, 102);
            this.gridControlMain.MainView = this.gridViewMain;
            this.gridControlMain.Name = "gridControlMain";
            this.gridControlMain.Size = new System.Drawing.Size(938, 211);
            this.gridControlMain.TabIndex = 0;
            this.gridControlMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMain});
            // 
            // gridViewMain
            // 
            this.gridViewMain.GridControl = this.gridControlMain;
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewMain.OptionsBehavior.ReadOnly = true;
            this.gridViewMain.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            this.gridViewMain.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewMain_CustomDrawCell);
            // 
            // gridControlPlugins
            // 
            this.gridControlPlugins.Location = new System.Drawing.Point(12, 319);
            this.gridControlPlugins.MainView = this.gridViewPlugins;
            this.gridControlPlugins.Name = "gridControlPlugins";
            this.gridControlPlugins.Size = new System.Drawing.Size(938, 199);
            this.gridControlPlugins.TabIndex = 2;
            this.gridControlPlugins.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewPlugins});
            // 
            // gridViewPlugins
            // 
            this.gridViewPlugins.GridControl = this.gridControlPlugins;
            this.gridViewPlugins.Name = "gridViewPlugins";
            this.gridViewPlugins.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridViewPlugins.OptionsBehavior.ReadOnly = true;
            this.gridViewPlugins.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewPlugins.OptionsView.ShowGroupPanel = false;
            this.gridViewPlugins.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridViewPlugins_CustomDrawCell);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdate.Image")));
            this.btnUpdate.Location = new System.Drawing.Point(12, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(162, 32);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Обновить программу";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // progressBarControlMain
            // 
            this.progressBarControlMain.Location = new System.Drawing.Point(12, 86);
            this.progressBarControlMain.Name = "progressBarControlMain";
            this.progressBarControlMain.Size = new System.Drawing.Size(937, 10);
            this.progressBarControlMain.TabIndex = 4;
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Location = new System.Drawing.Point(361, 20);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSourcePath.Properties.Appearance.Options.UseFont = true;
            this.txtSourcePath.Size = new System.Drawing.Size(538, 22);
            this.txtSourcePath.TabIndex = 7;
            // 
            // txtDestinationPath
            // 
            this.txtDestinationPath.Location = new System.Drawing.Point(361, 58);
            this.txtDestinationPath.Name = "txtDestinationPath";
            this.txtDestinationPath.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtDestinationPath.Properties.Appearance.Options.UseFont = true;
            this.txtDestinationPath.Size = new System.Drawing.Size(538, 22);
            this.txtDestinationPath.TabIndex = 8;
            // 
            // btnSetSourcePath
            // 
            this.btnSetSourcePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetSourcePath.Location = new System.Drawing.Point(905, 19);
            this.btnSetSourcePath.Name = "btnSetSourcePath";
            this.btnSetSourcePath.Size = new System.Drawing.Size(45, 23);
            this.btnSetSourcePath.TabIndex = 9;
            this.btnSetSourcePath.Text = "...";
            this.btnSetSourcePath.UseVisualStyleBackColor = true;
            this.btnSetSourcePath.Click += new System.EventHandler(this.btnSetDestinationPath_Click);
            // 
            // btnSetDestinationPath
            // 
            this.btnSetDestinationPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetDestinationPath.Location = new System.Drawing.Point(905, 57);
            this.btnSetDestinationPath.Name = "btnSetDestinationPath";
            this.btnSetDestinationPath.Size = new System.Drawing.Size(45, 23);
            this.btnSetDestinationPath.TabIndex = 10;
            this.btnSetDestinationPath.Text = "...";
            this.btnSetDestinationPath.UseVisualStyleBackColor = true;
            this.btnSetDestinationPath.Click += new System.EventHandler(this.btnSetDestinationPath_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(358, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Путь к обновлениям программы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(358, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Путь к установленной копии RedExpress";
            // 
            // btnRunRed
            // 
            this.btnRunRed.Enabled = false;
            this.btnRunRed.Image = ((System.Drawing.Image)(resources.GetObject("btnRunRed.Image")));
            this.btnRunRed.Location = new System.Drawing.Point(180, 12);
            this.btnRunRed.Name = "btnRunRed";
            this.btnRunRed.Size = new System.Drawing.Size(162, 32);
            this.btnRunRed.TabIndex = 13;
            this.btnRunRed.Text = "Запустить RedExpress";
            this.btnRunRed.Click += new System.EventHandler(this.btnRunRed_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 530);
            this.Controls.Add(this.btnRunRed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSetDestinationPath);
            this.Controls.Add(this.btnSetSourcePath);
            this.Controls.Add(this.txtDestinationPath);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.progressBarControlMain);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.gridControlPlugins);
            this.Controls.Add(this.gridControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обновление RedExpress";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPlugins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewPlugins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControlMain.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSourcePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDestinationPath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;
        private DevExpress.XtraGrid.GridControl gridControlPlugins;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewPlugins;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControlMain;
        private DevExpress.XtraEditors.TextEdit txtSourcePath;
        private DevExpress.XtraEditors.TextEdit txtDestinationPath;
        private System.Windows.Forms.Button btnSetSourcePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnSetDestinationPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnRunRed;
    }
}