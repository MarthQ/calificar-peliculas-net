namespace CalificarPeliculas.WindowsForms
{
    partial class CriticaListForm
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
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            btn_2 = new Button();
            btn_1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(748, 331);
            dataGridView1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_2);
            panel1.Controls.Add(btn_1);
            panel1.Location = new Point(766, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 331);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // btn_2
            // 
            btn_2.Location = new Point(49, 62);
            btn_2.Name = "btn_2";
            btn_2.Size = new Size(94, 29);
            btn_2.TabIndex = 1;
            btn_2.Text = "button2";
            btn_2.UseVisualStyleBackColor = true;
            // 
            // btn_1
            // 
            btn_1.Location = new Point(49, 17);
            btn_1.Name = "btn_1";
            btn_1.Size = new Size(94, 29);
            btn_1.TabIndex = 0;
            btn_1.Text = "Agregar";
            btn_1.UseVisualStyleBackColor = true;
            btn_1.Click += button1_Click;
            // 
            // CriticaListForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(968, 355);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Name = "CriticaListForm";
            Text = "CriticaListForm";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Button btn_1;
        private Button btn_2;
    }
}