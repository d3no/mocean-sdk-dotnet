namespace MoceanTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.button_2 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Verify = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Check Pricing";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CheckPricing);
            // 
            // button_2
            // 
            this.button_2.Location = new System.Drawing.Point(113, 93);
            this.button_2.Name = "button_2";
            this.button_2.Size = new System.Drawing.Size(160, 39);
            this.button_2.TabIndex = 1;
            this.button_2.Text = "Send SMS";
            this.button_2.UseVisualStyleBackColor = true;
            this.button_2.Click += new System.EventHandler(this.SendMessage);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(113, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(160, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "Send Flash SMS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SendFlashMessage);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(113, 205);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(160, 39);
            this.button3.TabIndex = 3;
            this.button3.Text = "Search SMS Status";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.SearchMessageStatus);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(314, 148);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(160, 39);
            this.button4.TabIndex = 4;
            this.button4.Text = "Check Balance";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.CheckBalance);
            // 
            // Verify
            // 
            this.Verify.Location = new System.Drawing.Point(512, 93);
            this.Verify.Name = "Verify";
            this.Verify.Size = new System.Drawing.Size(160, 39);
            this.Verify.TabIndex = 5;
            this.Verify.Text = "Verify";
            this.Verify.UseVisualStyleBackColor = true;
            this.Verify.Click += new System.EventHandler(this.Verification);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(512, 148);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(160, 39);
            this.button5.TabIndex = 6;
            this.button5.Text = "Check Verify";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.CheckVerify);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.Verify);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button_2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Verify;
        private System.Windows.Forms.Button button5;
    }
}

