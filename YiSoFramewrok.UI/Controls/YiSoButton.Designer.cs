using System.ComponentModel;

namespace YiSoFramework.UI
{
    partial class YiSoButton
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextLable
            // 
            this.TextLable.AutoSize = true;
            this.TextLable.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextLable.Location = new System.Drawing.Point(12, 10);
            this.TextLable.Name = "TextLable";
            this.TextLable.Size = new System.Drawing.Size(78, 15);
            this.TextLable.TabIndex = 0;
            this.TextLable.Text = "YiSoBUTTON";
            this.TextLable.FontChanged += new System.EventHandler(this.TextLable_FontChanged);
            this.TextLable.SizeChanged += new System.EventHandler(this.TextLable_SizeChanged);
            this.TextLable.Click += new System.EventHandler(this.TextLable_Click);
            // 
            // YiSoButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TextLable);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "YiSoButton";
            this.Size = new System.Drawing.Size(99, 36);
            this.Load += new System.EventHandler(this.YiSoButton_Load);
            this.FontChanged += new System.EventHandler(this.YiSoDefaultButton_FontChanged);
            this.SizeChanged += new System.EventHandler(this.YiSoDefaultButton_SizeChanged);
            this.Click += new System.EventHandler(this.YiSoButton_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.YiSoButton_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.YiSoButton_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TextLable;
    }
}
