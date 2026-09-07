namespace CalificarPeliculas.WindowsForms
{
    partial class ContenidoEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DateTimePicker dateFecha;
        private System.Windows.Forms.Label lblDirector;
        private System.Windows.Forms.TextBox txtDirector;
        private System.Windows.Forms.Label lblUrlImagen;
        private System.Windows.Forms.TextBox txtUrlImagen;
        private System.Windows.Forms.Label lblPuntuacion;
        private System.Windows.Forms.TextBox txtPuntuacion;
        private System.Windows.Forms.Label lblNumEpisodio;
        private System.Windows.Forms.NumericUpDown numNumEpisodio;
        private System.Windows.Forms.Label lblNumTemporada;
        private System.Windows.Forms.NumericUpDown numNumTemporada;
        private System.Windows.Forms.Label lblCantTemporadas;
        private System.Windows.Forms.NumericUpDown numCantTemporadas;
        private System.Windows.Forms.Label lblDuracion;
        private System.Windows.Forms.TextBox txtDuracion;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.ComboBox comboTipo;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.ComboBox comboGenero;
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
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblDirector = new System.Windows.Forms.Label();
            this.txtDirector = new System.Windows.Forms.TextBox();
            this.lblUrlImagen = new System.Windows.Forms.Label();
            this.txtUrlImagen = new System.Windows.Forms.TextBox();
            this.lblPuntuacion = new System.Windows.Forms.Label();
            this.txtPuntuacion = new System.Windows.Forms.TextBox();
            this.lblNumEpisodio = new System.Windows.Forms.Label();
            this.numNumEpisodio = new System.Windows.Forms.NumericUpDown();
            this.lblNumTemporada = new System.Windows.Forms.Label();
            this.numNumTemporada = new System.Windows.Forms.NumericUpDown();
            this.lblCantTemporadas = new System.Windows.Forms.Label();
            this.numCantTemporadas = new System.Windows.Forms.NumericUpDown();
            this.dateFecha = new System.Windows.Forms.DateTimePicker();
            this.lblDuracion = new System.Windows.Forms.Label();
            this.txtDuracion = new System.Windows.Forms.TextBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.comboTipo = new System.Windows.Forms.ComboBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.comboGenero = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(12, 15);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.AutoSize = true;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(90, 12);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(300, 27);
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.Location = new System.Drawing.Point(12, 50);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(80, 23);
            this.lblDescripcion.Text = "Descripción:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(90, 47);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(300, 80);
            // 
            // lblDirector
            // 
            this.lblDirector.Location = new System.Drawing.Point(12, 136);
            this.lblDirector.Name = "lblDirector";
            this.lblDirector.AutoSize = true;
            this.lblDirector.Text = "Director:";
            // 
            // txtDirector
            // 
            this.txtDirector.Location = new System.Drawing.Point(120, 132);
            this.txtDirector.Name = "txtDirector";
            this.txtDirector.Size = new System.Drawing.Size(270, 27);
            // 
            // lblUrlImagen
            // 
            this.lblUrlImagen.Location = new System.Drawing.Point(12, 170);
            this.lblUrlImagen.Name = "lblUrlImagen";
            this.lblUrlImagen.AutoSize = true;
            this.lblUrlImagen.Text = "URL imagen:";
            // 
            // txtUrlImagen
            // 
            this.txtUrlImagen.Location = new System.Drawing.Point(120, 166);
            this.txtUrlImagen.Name = "txtUrlImagen";
            this.txtUrlImagen.Size = new System.Drawing.Size(270, 27);
            // 
            // lblPuntuacion
            // 
            this.lblPuntuacion.Location = new System.Drawing.Point(12, 204);
            this.lblPuntuacion.Name = "lblPuntuacion";
            this.lblPuntuacion.AutoSize = true;
            this.lblPuntuacion.Text = "Puntuación promedio:";
            // 
            // txtPuntuacion
            // 
            this.txtPuntuacion.Location = new System.Drawing.Point(180, 200);
            this.txtPuntuacion.Name = "txtPuntuacion";
            this.txtPuntuacion.Size = new System.Drawing.Size(80, 27);
            // 
            // lblNumEpisodio
            // 
            this.lblNumEpisodio.Location = new System.Drawing.Point(12, 240);
            this.lblNumEpisodio.Name = "lblNumEpisodio";
            this.lblNumEpisodio.AutoSize = true;
            this.lblNumEpisodio.Text = "N° episodio:";
            // 
            // numNumEpisodio
            // 
            this.numNumEpisodio.Location = new System.Drawing.Point(120, 236);
            this.numNumEpisodio.Name = "numNumEpisodio";
            this.numNumEpisodio.Size = new System.Drawing.Size(80, 27);
            this.numNumEpisodio.Minimum = 0;
            this.numNumEpisodio.Maximum = 10000;
            // 
            // lblNumTemporada
            // 
            this.lblNumTemporada.Location = new System.Drawing.Point(220, 240);
            this.lblNumTemporada.Name = "lblNumTemporada";
            this.lblNumTemporada.AutoSize = true;
            this.lblNumTemporada.Text = "N° temporada:";
            // 
            // numNumTemporada
            // 
            this.numNumTemporada.Location = new System.Drawing.Point(340, 236);
            this.numNumTemporada.Name = "numNumTemporada";
            this.numNumTemporada.Size = new System.Drawing.Size(80, 27);
            this.numNumTemporada.Minimum = 0;
            this.numNumTemporada.Maximum = 1000;
            // 
            // lblCantTemporadas
            // 
            this.lblCantTemporadas.Location = new System.Drawing.Point(12, 276);
            this.lblCantTemporadas.Name = "lblCantTemporadas";
            this.lblCantTemporadas.AutoSize = true;
            this.lblCantTemporadas.Text = "Cant. temporadas:";
            // 
            // numCantTemporadas
            // 
            this.numCantTemporadas.Location = new System.Drawing.Point(140, 272);
            this.numCantTemporadas.Name = "numCantTemporadas";
            this.numCantTemporadas.Size = new System.Drawing.Size(80, 27);
            this.numCantTemporadas.Minimum = 0;
            this.numCantTemporadas.Maximum = 1000;
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(12, 312);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.AutoSize = true;
            this.lblFecha.Text = "Fecha:";
            // 
            // dateFecha
            // 
            this.dateFecha.Location = new System.Drawing.Point(120, 308);
            this.dateFecha.Name = "dateFecha";
            this.dateFecha.Size = new System.Drawing.Size(200, 27);
            // 
            // lblDuracion
            // 
            this.lblDuracion.Location = new System.Drawing.Point(12, 352);
            this.lblDuracion.Name = "lblDuracion";
            this.lblDuracion.AutoSize = true;
            this.lblDuracion.Text = "Duración (HH:mm):";
            // 
            // txtDuracion
            // 
            this.txtDuracion.Location = new System.Drawing.Point(150, 348);
            this.txtDuracion.Name = "txtDuracion";
            this.txtDuracion.Size = new System.Drawing.Size(140, 27);
            // 
            // lblTipo
            // 
            this.lblTipo.Location = new System.Drawing.Point(12, 392);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.AutoSize = true;
            this.lblTipo.Text = "Tipo:";
            // 
            // comboTipo
            // 
            this.comboTipo.Location = new System.Drawing.Point(120, 388);
            this.comboTipo.Name = "comboTipo";
            this.comboTipo.Size = new System.Drawing.Size(180, 28);
            this.comboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipo.SelectedIndexChanged += new System.EventHandler(this.comboTipo_SelectedIndexChanged);
            // 
            // lblGenero
            // 
            this.lblGenero.Location = new System.Drawing.Point(12, 432);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.AutoSize = true;
            this.lblGenero.Text = "Género:";
            // 
            // comboGenero
            // 
            this.comboGenero.Location = new System.Drawing.Point(120, 428);
            this.comboGenero.Name = "comboGenero";
            this.comboGenero.Size = new System.Drawing.Size(200, 28);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(120, 468);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 30);
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(260, 468);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ContenidoEditForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(520, 520);
            Controls.Add(this.lblNombre);
            Controls.Add(this.txtNombre);
            Controls.Add(this.lblDescripcion);
            Controls.Add(this.txtDescripcion);
            Controls.Add(this.lblDirector);
            Controls.Add(this.txtDirector);
            Controls.Add(this.lblUrlImagen);
            Controls.Add(this.txtUrlImagen);
            Controls.Add(this.lblPuntuacion);
            Controls.Add(this.txtPuntuacion);
            Controls.Add(this.lblNumEpisodio);
            Controls.Add(this.numNumEpisodio);
            Controls.Add(this.lblNumTemporada);
            Controls.Add(this.numNumTemporada);
            Controls.Add(this.lblCantTemporadas);
            Controls.Add(this.numCantTemporadas);
            Controls.Add(this.lblFecha);
            Controls.Add(this.dateFecha);
            Controls.Add(this.lblDuracion);
            Controls.Add(this.txtDuracion);
            Controls.Add(this.lblTipo);
            Controls.Add(this.comboTipo);
            Controls.Add(this.lblGenero);
            Controls.Add(this.comboGenero);
            Controls.Add(this.btnOk);
            Controls.Add(this.btnCancel);
            Name = "ContenidoEditForm";
            Text = "Editar Contenido";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
