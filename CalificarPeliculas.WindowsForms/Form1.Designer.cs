using System;
using System.Windows.Forms;
using System.Drawing;

namespace CalificarPeliculas.WindowsForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuGenerosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuContenidosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuLogoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuRegisterToolStripMenuItem;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuGenerosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuContenidosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRegisterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMain = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGenerosToolStripMenuItem,
            this.menuContenidosToolStripMenuItem,
            this.menuRegisterToolStripMenuItem,
            this.menuLogoutToolStripMenuItem});
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuGenerosToolStripMenuItem
            // 
            this.menuGenerosToolStripMenuItem.Name = "menuGenerosToolStripMenuItem";
            this.menuGenerosToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.menuGenerosToolStripMenuItem.Text = "Géneros";
            this.menuGenerosToolStripMenuItem.Click += new System.EventHandler(this.btnGeneros_Click);
            // 
            // menuContenidosToolStripMenuItem
            // 
            this.menuContenidosToolStripMenuItem.Name = "menuContenidosToolStripMenuItem";
            this.menuContenidosToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.menuContenidosToolStripMenuItem.Text = "Contenidos";
            this.menuContenidosToolStripMenuItem.Click += new System.EventHandler(this.btnContenidos_Click);
            // 
            // menuRegisterToolStripMenuItem
            // 
            this.menuRegisterToolStripMenuItem.Name = "menuRegisterToolStripMenuItem";
            this.menuRegisterToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.menuRegisterToolStripMenuItem.Text = "Registro";
            this.menuRegisterToolStripMenuItem.Click += new System.EventHandler(this.menuRegisterToolStripMenuItem_Click);
            // 
            // menuLogoutToolStripMenuItem
            // 
            this.menuLogoutToolStripMenuItem.Name = "menuLogoutToolStripMenuItem";
            this.menuLogoutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.menuLogoutToolStripMenuItem.Text = "Logout";
            this.menuLogoutToolStripMenuItem.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelMain
            // 
            this.panelMain.Location = new Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new Size(800, 426);
            this.panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            this.MainMenuStrip = this.menuStrip1;
            Controls.Add(this.menuStrip1);
            Controls.Add(this.panelMain);
            Name = "Form1";
            Text = "Home";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
