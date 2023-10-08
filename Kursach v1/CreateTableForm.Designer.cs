
namespace Kursach_v1
{
    partial class CreateTableForm
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
            this.TableCreationLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.NameLabel = new System.Windows.Forms.Label();
            this.textBoxTableName = new System.Windows.Forms.TextBox();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.Names = new System.Windows.Forms.Label();
            this.richTextBoxNames = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // TableCreationLabel
            // 
            this.TableCreationLabel.AutoSize = true;
            this.TableCreationLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TableCreationLabel.Font = new System.Drawing.Font("Montserrat", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableCreationLabel.Location = new System.Drawing.Point(27, 1);
            this.TableCreationLabel.Name = "TableCreationLabel";
            this.TableCreationLabel.Size = new System.Drawing.Size(145, 23);
            this.TableCreationLabel.TabIndex = 6;
            this.TableCreationLabel.Text = "Table creation";
            // 
            // MenuStrip
            // 
            this.MenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.MenuStrip.Font = new System.Drawing.Font("Montserrat", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MenuStrip.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(484, 24);
            this.MenuStrip.TabIndex = 7;
            this.MenuStrip.Text = "MenuStrip";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(167)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Montserrat", 9.818182F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(435, -3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 27);
            this.button1.TabIndex = 8;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NameLabel.Font = new System.Drawing.Font("Montserrat", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(12, 42);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(74, 23);
            this.NameLabel.TabIndex = 9;
            this.NameLabel.Text = "Name:";
            // 
            // textBoxTableName
            // 
            this.textBoxTableName.Font = new System.Drawing.Font("Montserrat", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTableName.Location = new System.Drawing.Point(92, 42);
            this.textBoxTableName.Name = "textBoxTableName";
            this.textBoxTableName.Size = new System.Drawing.Size(195, 26);
            this.textBoxTableName.TabIndex = 10;
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.FlatAppearance.BorderSize = 2;
            this.ApplyBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ApplyBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.ApplyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ApplyBtn.Font = new System.Drawing.Font("Montserrat", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ApplyBtn.Location = new System.Drawing.Point(182, 248);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(119, 44);
            this.ApplyBtn.TabIndex = 13;
            this.ApplyBtn.Text = "Create";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // Names
            // 
            this.Names.AutoSize = true;
            this.Names.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Names.Font = new System.Drawing.Font("Montserrat", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Names.Location = new System.Drawing.Point(12, 89);
            this.Names.Name = "Names";
            this.Names.Size = new System.Drawing.Size(194, 23);
            this.Names.TabIndex = 15;
            this.Names.Text = "Names of columns:";
            // 
            // richTextBoxNames
            // 
            this.richTextBoxNames.Font = new System.Drawing.Font("Montserrat", 9.163636F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxNames.Location = new System.Drawing.Point(16, 115);
            this.richTextBoxNames.Name = "richTextBoxNames";
            this.richTextBoxNames.Size = new System.Drawing.Size(456, 127);
            this.richTextBoxNames.TabIndex = 16;
            this.richTextBoxNames.Text = "";
            // 
            // CreateTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(484, 304);
            this.Controls.Add(this.richTextBoxNames);
            this.Controls.Add(this.Names);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.textBoxTableName);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.TableCreationLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateTableForm";
            this.Text = "CreateTableForm";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TableCreationLabel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox textBoxTableName;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.Label Names;
        private System.Windows.Forms.RichTextBox richTextBoxNames;
    }
}