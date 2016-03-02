using System;
using System.Net;

namespace Netsparker.RequestManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.grpRequest = new System.Windows.Forms.GroupBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtRawRequest = new System.Windows.Forms.TextBox();
            this.dgvResponses = new System.Windows.Forms.DataGridView();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.grpRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResponses)).BeginInit();
            this.statusBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.scMain);
            this.pnlMain.Location = new System.Drawing.Point(12, 27);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(763, 506);
            this.pnlMain.TabIndex = 2;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.grpRequest);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.dgvResponses);
            this.scMain.Panel2.Controls.Add(this.txtResponse);
            this.scMain.Size = new System.Drawing.Size(763, 506);
            this.scMain.SplitterDistance = 215;
            this.scMain.TabIndex = 1;
            // 
            // grpRequest
            // 
            this.grpRequest.Controls.Add(this.btnSend);
            this.grpRequest.Controls.Add(this.txtRawRequest);
            this.grpRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRequest.Location = new System.Drawing.Point(0, 0);
            this.grpRequest.Name = "grpRequest";
            this.grpRequest.Size = new System.Drawing.Size(763, 215);
            this.grpRequest.TabIndex = 1;
            this.grpRequest.TabStop = false;
            this.grpRequest.Text = "Request";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(642, 182);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(113, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtRawRequest
            // 
            this.txtRawRequest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRawRequest.AutoCompleteCustomSource.AddRange(new string[] {
            "CacheControl",
            "Connection",
            "Date",
            "KeepAlive",
            "Pragma",
            "Trailer",
            "TransferEncoding",
            "Upgrade",
            "Via",
            "Warning",
            "Allow",
            "ContentLength",
            "ContentType",
            "ContentEncoding",
            "ContentLanguage",
            "ContentLocation",
            "ContentMd5",
            "ContentRange",
            "Expires",
            "LastModified",
            "Accept",
            "AcceptCharset",
            "AcceptEncoding",
            "AcceptLanguage",
            "Authorization",
            "Cookie",
            "Expect",
            "From",
            "Host",
            "IfMatch",
            "IfModifiedSince",
            "IfNoneMatch",
            "IfRange",
            "IfUnmodifiedSince",
            "MaxForwards",
            "ProxyAuthorization",
            "Referer",
            "Range",
            "Te",
            "Translate",
            "UserAgent"});
            this.txtRawRequest.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtRawRequest.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRawRequest.Location = new System.Drawing.Point(12, 19);
            this.txtRawRequest.Multiline = true;
            this.txtRawRequest.Name = "txtRawRequest";
            this.txtRawRequest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRawRequest.Size = new System.Drawing.Size(743, 157);
            this.txtRawRequest.TabIndex = 0;
            this.txtRawRequest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRawRequest_KeyDown);
            // 
            // dgvResponses
            // 
            this.dgvResponses.AllowUserToAddRows = false;
            this.dgvResponses.AllowUserToDeleteRows = false;
            this.dgvResponses.AllowUserToResizeRows = false;
            this.dgvResponses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvResponses.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvResponses.CausesValidation = false;
            this.dgvResponses.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvResponses.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvResponses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResponses.Location = new System.Drawing.Point(12, 13);
            this.dgvResponses.MultiSelect = false;
            this.dgvResponses.Name = "dgvResponses";
            this.dgvResponses.ReadOnly = true;
            this.dgvResponses.RowHeadersVisible = false;
            this.dgvResponses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResponses.ShowCellErrors = false;
            this.dgvResponses.ShowCellToolTips = false;
            this.dgvResponses.ShowEditingIcon = false;
            this.dgvResponses.ShowRowErrors = false;
            this.dgvResponses.Size = new System.Drawing.Size(282, 259);
            this.dgvResponses.TabIndex = 1;
            this.dgvResponses.SelectionChanged += new System.EventHandler(this.dgvResponses_SelectionChanged);
            // 
            // txtResponse
            // 
            this.txtResponse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResponse.Location = new System.Drawing.Point(300, 13);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(455, 259);
            this.txtResponse.TabIndex = 0;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 536);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(787, 22);
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusBar";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(772, 17);
            this.lblStatus.Spring = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(787, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 558);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.pnlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "RequestManager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.grpRequest.ResumeLayout(false);
            this.grpRequest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResponses)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.GroupBox grpRequest;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtRawRequest;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.DataGridView dgvResponses;
    }
}