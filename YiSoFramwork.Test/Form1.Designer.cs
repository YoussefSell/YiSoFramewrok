namespace YiSoFramwork.Test
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Btn_Trigger = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Panel();
            this.Btn_ChangeValue = new System.Windows.Forms.Button();
            this.Txt_Value = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.yiSoAnimator1 = new YiSoFramewrok.UI.YiSoAnimator(this.components);
            this.yiSoButton1 = new YiSoFramework.UI.YiSoButton();
            this.yiSoPagesContainer1 = new YiSoFramework.UI.YiSoPagesContainer();
            this.yiSoEllipser1 = new YiSoFramework.UI.YiSoEllipser(this.components);
            this.yiSoDragger1 = new YiSoFramework.UI.YiSoDragger(this.components);
            this.SuspendLayout();
            // 
            // Btn_Trigger
            // 
            this.Btn_Trigger.Location = new System.Drawing.Point(12, 47);
            this.Btn_Trigger.Name = "Btn_Trigger";
            this.Btn_Trigger.Size = new System.Drawing.Size(75, 23);
            this.Btn_Trigger.TabIndex = 1;
            this.Btn_Trigger.Text = "Trigger";
            this.Btn_Trigger.UseVisualStyleBackColor = true;
            this.Btn_Trigger.Click += new System.EventHandler(this.Btn_Trigger_Click);
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.Location = new System.Drawing.Point(0, 0);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(1106, 41);
            this.Header.TabIndex = 3;
            // 
            // Btn_ChangeValue
            // 
            this.Btn_ChangeValue.Location = new System.Drawing.Point(13, 78);
            this.Btn_ChangeValue.Name = "Btn_ChangeValue";
            this.Btn_ChangeValue.Size = new System.Drawing.Size(154, 23);
            this.Btn_ChangeValue.TabIndex = 5;
            this.Btn_ChangeValue.Text = "Change Value";
            this.Btn_ChangeValue.UseVisualStyleBackColor = true;
            this.Btn_ChangeValue.Click += new System.EventHandler(this.Btn_ChangeValue_Click);
            // 
            // Txt_Value
            // 
            this.Txt_Value.Location = new System.Drawing.Point(13, 107);
            this.Txt_Value.Name = "Txt_Value";
            this.Txt_Value.Size = new System.Drawing.Size(154, 20);
            this.Txt_Value.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(13, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(154, 277);
            this.panel1.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "DeTrigger";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Btn_DeTrigger_Click);
            // 
            // yiSoAnimator1
            // 
            this.yiSoAnimator1.Duration = 1000;
            this.yiSoAnimator1.Easing = YiSoFramework.Animation._Easing.Linear;
            this.yiSoAnimator1.Effect = YiSoFramework.Animation._Effect.LeftAnchoredWidthEffect;
            this.yiSoAnimator1.Loop = 1;
            this.yiSoAnimator1.Reverse = false;
            this.yiSoAnimator1.TargetControl = this.panel1;
            this.yiSoAnimator1.ValueToReach = 0;
            // 
            // yiSoButton1
            // 
            this.yiSoButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
            this.yiSoButton1.BorderRadius = 5;
            this.yiSoButton1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
            this.yiSoButton1.ButtonText = "YiSoBUTTON";
            this.yiSoButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yiSoButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.yiSoButton1.HoverdColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(217)))));
            this.yiSoButton1.Location = new System.Drawing.Point(556, 207);
            this.yiSoButton1.Name = "yiSoButton1";
            this.yiSoButton1.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(116)))), ((int)(((byte)(217)))));
            this.yiSoButton1.Size = new System.Drawing.Size(110, 36);
            this.yiSoButton1.TabIndex = 10;
            this.yiSoButton1.TextColor = System.Drawing.Color.White;
            this.yiSoButton1.Click += new System.EventHandler(this.yiSoButton1_Click);
            // 
            // yiSoPagesContainer1
            // 
            this.yiSoPagesContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yiSoPagesContainer1.HoveHorizontal = true;
            this.yiSoPagesContainer1.Location = new System.Drawing.Point(176, 47);
            this.yiSoPagesContainer1.Name = "yiSoPagesContainer1";
            this.yiSoPagesContainer1.Navigator = null;
            this.yiSoPagesContainer1.Size = new System.Drawing.Size(349, 367);
            this.yiSoPagesContainer1.TabIndex = 9;
            // 
            // yiSoEllipser1
            // 
            this.yiSoEllipser1.Radius = 5;
            this.yiSoEllipser1.TargetControl = null;
            // 
            // yiSoDragger1
            // 
            this.yiSoDragger1.Fixed = false;
            this.yiSoDragger1.HorizontalMoving = true;
            this.yiSoDragger1.TargetControl = this.Header;
            this.yiSoDragger1.UseAsFormDrager = true;
            this.yiSoDragger1.VerticalMoving = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 669);
            this.Controls.Add(this.yiSoButton1);
            this.Controls.Add(this.yiSoPagesContainer1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Txt_Value);
            this.Controls.Add(this.Btn_ChangeValue);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.Btn_Trigger);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Btn_Trigger;
        private System.Windows.Forms.Panel Header;
        private System.Windows.Forms.Button Btn_ChangeValue;
        private System.Windows.Forms.TextBox Txt_Value;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private YiSoFramework.UI.YiSoPagesContainer yiSoPagesContainer1;
        private YiSoFramework.UI.YiSoButton yiSoButton1;
        private YiSoFramework.UI.YiSoEllipser yiSoEllipser1;
        private YiSoFramework.UI.YiSoDragger yiSoDragger1;
        private YiSoFramewrok.UI.YiSoAnimator yiSoAnimator1;
    }
}

