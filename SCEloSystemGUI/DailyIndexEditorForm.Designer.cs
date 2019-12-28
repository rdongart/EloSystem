namespace SCEloSystemGUI
{
    partial class DailyIndexEditorForm
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
            this.tLPMatchIndexEditorMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tLPMatchIndexEditorMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tLPMatchIndexEditorMain
            // 
            this.tLPMatchIndexEditorMain.ColumnCount = 1;
            this.tLPMatchIndexEditorMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPMatchIndexEditorMain.Controls.Add(this.btnCancel, 0, 1);
            this.tLPMatchIndexEditorMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLPMatchIndexEditorMain.Location = new System.Drawing.Point(6, 6);
            this.tLPMatchIndexEditorMain.Margin = new System.Windows.Forms.Padding(0);
            this.tLPMatchIndexEditorMain.Name = "tLPMatchIndexEditorMain";
            this.tLPMatchIndexEditorMain.RowCount = 2;
            this.tLPMatchIndexEditorMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tLPMatchIndexEditorMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tLPMatchIndexEditorMain.Size = new System.Drawing.Size(972, 350);
            this.tLPMatchIndexEditorMain.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(879, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DailyIndexEditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(984, 362);
            this.Controls.Add(this.tLPMatchIndexEditorMain);
            this.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DailyIndexEditorForm";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Text = "Match Index Editor";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DailyIndexEditorForm_KeyPress);
            this.tLPMatchIndexEditorMain.ResumeLayout(false);
            this.tLPMatchIndexEditorMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tLPMatchIndexEditorMain;
        private System.Windows.Forms.Button btnCancel;
    }
}