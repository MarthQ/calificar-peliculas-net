namespace CalificarPeliculas.WindowsForms
{
    partial class CriticaEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox comboUsuario;
        private System.Windows.Forms.Label lblContenido;
        private System.Windows.Forms.ComboBox comboContenido;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Label lblCalificacion;
        private System.Windows.Forms.NumericUpDown numCalificacion;
        private System.Windows.Forms.Label lblReacciones;
        private System.Windows.Forms.NumericUpDown numReacciones;
        private System.Windows.Forms.Label lblTexto;
        private System.Windows.Forms.TextBox txtTexto;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUsuario = new System.Windows.Forms.Label();
            this.comboUsuario = new System.Windows.Forms.ComboBox();
            this.lblContenido = new System.Windows.Forms.Label();
            this.comboContenido = new System.Windows.Forms.ComboBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.lblCalificacion = new System.Windows.Forms.Label();
            this.numCalificacion = new System.Windows.Forms.NumericUpDown();
            this.lblReacciones = new System.Windows.Forms.Label();
            this.numReacciones = new System.Windows.Forms.NumericUpDown();
            this.lblTexto = new System.Windows.Forms.Label();
            this.txtTexto = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numCalificacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReacciones)).BeginInit();
            SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(12, 15);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario:";
            // 
            // comboUsuario
            // 
            this.comboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUsuario.Location = new System.Drawing.Point(120, 12);
            this.comboUsuario.Name = "comboUsuario";
            this.comboUsuario.Size = new System.Drawing.Size(360, 28);
            // 
            // lblContenido
            // 
            this.lblContenido.AutoSize = true;
            this.lblContenido.Location = new System.Drawing.Point(12, 52);
            this.lblContenido.Name = "lblContenido";
            this.lblContenido.Text = "Contenido:";
            // 
            // comboContenido
            // 
            this.comboContenido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboContenido.Location = new System.Drawing.Point(120, 49);
            this.comboContenido.Name = "comboContenido";
            this.comboContenido.Size = new System.Drawing.Size(360, 28);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(12, 89);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Text = "Fecha:";
            // 
            // dateFecha
            // 
            this.dateFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFecha.Location = new System.Drawing.Point(120, 86);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(150, 27);
            // 
            // lblCalificacion
            // 
            this.lblCalificacion.AutoSize = true;
            this.lblCalificacion.Location = new System.Drawing.Point(12, 126);
            this.lblCalificacion.Name = "lblCalificacion";
            this.lblCalificacion.Text = "Calificación:";
            // 
            // numCalificacion
            // 
            this.numCalificacion.DecimalPlaces = 1;
            this.numCalificacion.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            this.numCalificacion.Location = new System.Drawing.Point(120, 123);
            this.numCalificacion.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.numCalificacion.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numCalificacion.Name = "numCalificacion";
            this.numCalificacion.Size = new System.Drawing.Size(80, 27);
            // 
            // lblReacciones
            // 
            this.lblReacciones.AutoSize = true;
            this.lblReacciones.Location = new System.Drawing.Point(240, 126);
            this.lblReacciones.Name = "lblReacciones";
            this.lblReacciones.Text = "Reacciones:";
            // 
            // numReacciones
            // 
            this.numReacciones.Location = new System.Drawing.Point(340, 123);
            this.numReacciones.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numReacciones.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numReacciones.Name = "numReacciones";
            this.numReacciones.Size = new System.Drawing.Size(140, 27);
            // 
            // lblTexto
            // 
            this.lblTexto.AutoSize = true;
            this.lblTexto.Location = new System.Drawing.Point(12, 163);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Text = "Texto:";
            // 
            // txtTexto
            // 
            this.txtTexto.Location = new System.Drawing.Point(120, 160);
            this.txtTexto.Multiline = true;
            this.txtTexto.Name = "txtTexto";
            this.txtTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTexto.Size = new System.Drawing.Size(360, 120);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(120, 295);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 30);
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(380, 295);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CriticaEditForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(500, 340);
            Controls.Add(this.lblUsuario);
            Controls.Add(this.comboUsuario);
            Controls.Add(this.lblContenido);
            Controls.Add(this.comboContenido);
            Controls.Add(this.lblFecha);
            Controls.Add(this.dateFecha);
            Controls.Add(this.lblCalificacion);
            Controls.Add(this.numCalificacion);
            Controls.Add(this.lblReacciones);
            Controls.Add(this.numReacciones);
            Controls.Add(this.lblTexto);
            Controls.Add(this.txtTexto);
            Controls.Add(this.btnOk);
            Controls.Add(this.btnCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Name = "CriticaEditForm";
            Text = "Editar Crítica";
            ((System.ComponentModel.ISupportInitialize)(this.numCalificacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReacciones)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
