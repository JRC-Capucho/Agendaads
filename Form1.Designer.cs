namespace AgendaADS;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
    [Obsolete]
    private void InitializeComponent()
        {
            this.btStart = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btStart
            // 
            this.btStart.Font = new System.Drawing.Font("Bauhaus 93", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStart.Location = new System.Drawing.Point(12, 56);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(96, 35);
            this.btStart.TabIndex = 0;
            this.btStart.Text = "Começar";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btStop
            // 
            this.btStop.Font = new System.Drawing.Font("Bauhaus 93", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btStop.Location = new System.Drawing.Point(185, 56);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(96, 35);
            this.btStop.TabIndex = 1;
            this.btStop.Text = "Encerrar";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 103);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btStart);
            this.Name = "Form";
            this.ShowIcon = false;
            this.Text = "Agenda 3ADS";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

    #endregion
    private System.Windows.Forms.Button btStart;
    private System.Windows.Forms.Button btStop;
}
