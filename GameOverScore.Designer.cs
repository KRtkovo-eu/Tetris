
namespace Tetris
{
    partial class GameOverScore
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
            this.label1 = new System.Windows.Forms.Label();
            this.totalScoreLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.coolName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.coolQuote = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.noSelectButton1 = new Tetris.NoSelectButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 15);
            this.label1.TabIndex = 999;
            this.label1.Text = "Your Score:";
            // 
            // totalScoreLabel
            // 
            this.totalScoreLabel.AutoSize = true;
            this.totalScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.totalScoreLabel.ForeColor = System.Drawing.Color.Blue;
            this.totalScoreLabel.Location = new System.Drawing.Point(99, 9);
            this.totalScoreLabel.Name = "totalScoreLabel";
            this.totalScoreLabel.Size = new System.Drawing.Size(47, 15);
            this.totalScoreLabel.TabIndex = 999;
            this.totalScoreLabel.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 999;
            this.label2.Text = "Your Cool Name:";
            // 
            // coolName
            // 
            this.coolName.Location = new System.Drawing.Point(27, 95);
            this.coolName.Name = "coolName";
            this.coolName.Size = new System.Drawing.Size(242, 20);
            this.coolName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 15);
            this.label3.TabIndex = 999;
            this.label3.Text = "Your Cool Quote:";
            // 
            // coolQuote
            // 
            this.coolQuote.Location = new System.Drawing.Point(27, 147);
            this.coolQuote.Name = "coolQuote";
            this.coolQuote.Size = new System.Drawing.Size(242, 20);
            this.coolQuote.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Tetris.Properties.Resources.tetrisRibbon;
            this.pictureBox1.Location = new System.Drawing.Point(2, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 30);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(240, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 15);
            this.label4.TabIndex = 999;
            this.label4.Text = "Your Level:";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.levelLabel.ForeColor = System.Drawing.Color.Blue;
            this.levelLabel.Location = new System.Drawing.Point(324, 9);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(15, 15);
            this.levelLabel.TabIndex = 999;
            this.levelLabel.Text = "0";
            // 
            // noSelectButton1
            // 
            this.noSelectButton1.Location = new System.Drawing.Point(275, 95);
            this.noSelectButton1.Name = "noSelectButton1";
            this.noSelectButton1.Size = new System.Drawing.Size(72, 72);
            this.noSelectButton1.TabIndex = 1000;
            this.noSelectButton1.Text = "&OK";
            this.noSelectButton1.UseVisualStyleBackColor = true;
            this.noSelectButton1.Click += new System.EventHandler(this.noSelectButton1_Click);
            // 
            // GameOverScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 184);
            this.Controls.Add(this.noSelectButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.coolQuote);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.coolName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.totalScoreLabel);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GameOverScore";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game over!";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label totalScoreLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox coolName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox coolQuote;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label levelLabel;
        private NoSelectButton noSelectButton1;
    }
}