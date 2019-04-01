namespace YiSoFramework.UI
{
    partial class YiSoPagesContainer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.yiSoDragger1 = new YiSoFramework.UI.YiSoDragger(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // yiSoDragger1
            // 
            this.yiSoDragger1.Fixed = false;
            this.yiSoDragger1.HorizontalMoving = false;
            this.yiSoDragger1.TargetControl = null;
            this.yiSoDragger1.UseAsFormDrager = false;
            this.yiSoDragger1.VerticalMoving = true;
            this.yiSoDragger1.ControlReleased += new System.EventHandler(this.YiSoDragger_ControlReleased);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // YiSoPagesContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "YiSoPagesContainer";
            this.Size = new System.Drawing.Size(297, 240);
            this.Load += new System.EventHandler(this.YiSoPagesContainer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private YiSoDragger yiSoDragger1;
        private System.Windows.Forms.Timer timer1;
    }
}
