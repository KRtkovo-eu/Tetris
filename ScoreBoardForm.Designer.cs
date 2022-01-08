
namespace Tetris
{
    partial class ScoreBoardForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.noSelectButton1 = new Tetris.NoSelectButton();
            this.highScoreList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Tetris.Properties.Resources.tetrisRibbon;
            this.pictureBox1.Location = new System.Drawing.Point(2, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(355, 30);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // noSelectButton1
            // 
            this.noSelectButton1.Location = new System.Drawing.Point(275, 314);
            this.noSelectButton1.Name = "noSelectButton1";
            this.noSelectButton1.Size = new System.Drawing.Size(72, 25);
            this.noSelectButton1.TabIndex = 1000;
            this.noSelectButton1.Text = "&Close";
            this.noSelectButton1.UseVisualStyleBackColor = true;
            this.noSelectButton1.Click += new System.EventHandler(this.noSelectButton1_Click);
            // 
            // highScoreList
            // 
            this.highScoreList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.highScoreList.HideSelection = false;
            this.highScoreList.Location = new System.Drawing.Point(2, 41);
            this.highScoreList.Name = "highScoreList";
            this.highScoreList.Size = new System.Drawing.Size(355, 267);
            this.highScoreList.TabIndex = 1001;
            this.highScoreList.UseCompatibleStateImageBehavior = false;
            this.highScoreList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Score";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Cool Name";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cool Quote";
            this.columnHeader3.Width = 300;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "End Level";
            // 
            // ScoreBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 351);
            this.Controls.Add(this.highScoreList);
            this.Controls.Add(this.noSelectButton1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ScoreBoardForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "High Scores Board";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private NoSelectButton noSelectButton1;
        private System.Windows.Forms.ListView highScoreList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}