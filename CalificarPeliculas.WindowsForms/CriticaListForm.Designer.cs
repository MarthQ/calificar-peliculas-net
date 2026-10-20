namespace CalificarPeliculas.WindowsForms
{
    partial class CriticaListForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMaestros;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox comboUsuario;
        private System.Windows.Forms.Label lblContenido;
        private System.Windows.Forms.ComboBox comboContenido;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridView dgvCriticas;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelActions;

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
            this.panelMaestros = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.comboUsuario = new System.Windows.Forms.ComboBox();
            this.lblContenido = new System.Windows.Forms.Label();
            this.comboContenido = new System.Windows.Forms.ComboBox();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvCriticas = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelActions = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriticas)).BeginInit();
            SuspendLayout();
            // 
            // panelMaestros
            // 
            this.panelMaestros.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            this.panelMaestros.Location = new System.Drawing.Point(12, 12);
            this.panelMaestros.Name = "panelMaestros";
            this.panelMaestros.Size = new System.Drawing.Size(760, 70);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Location = new System.Drawing.Point(0, 4);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Text = "Usuario:";
            // 
            // comboUsuario
            // 
            this.comboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUsuario.Location = new System.Drawing.Point(0, 28);
            this.comboUsuario.Name = "comboUsuario";
            this.comboUsuario.Size = new System.Drawing.Size(250, 28);
            this.comboUsuario.SelectedIndexChanged += new System.EventHandler(this.comboMaestro_SelectedIndexChanged);
            // 
            // lblContenido
            // 
            this.lblContenido.AutoSize = true;
            this.lblContenido.Location = new System.Drawing.Point(270, 4);
            this.lblContenido.Name = "lblContenido";
            this.lblContenido.Text = "Contenido:";
            // 
            // comboContenido
            // 
            this.comboContenido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboContenido.Location = new System.Drawing.Point(270, 28);
            this.comboContenido.Name = "comboContenido";
            this.comboContenido.Size = new System.Drawing.Size(320, 28);
            this.comboContenido.SelectedIndexChanged += new System.EventHandler(this.comboMaestro_SelectedIndexChanged);
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(610, 26);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(140, 30);
            this.btnLimpiarFiltros.Text = "Ver todas";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = true;
            this.btnLimpiarFiltros.Click += new System.EventHandler(this.btnLimpiarFiltros_Click);
            // 
            // dgvCriticas
            // 
            this.dgvCriticas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCriticas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCriticas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCriticas.Location = new System.Drawing.Point(12, 90);
            this.dgvCriticas.MultiSelect = false;
            this.dgvCriticas.Name = "dgvCriticas";
            this.dgvCriticas.ReadOnly = true;
            this.dgvCriticas.RowHeadersVisible = false;
            this.dgvCriticas.RowTemplate.Height = 25;
            this.dgvCriticas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCriticas.Size = new System.Drawing.Size(760, 270);
            this.dgvCriticas.TabIndex = 0;
            this.dgvCriticas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCriticas_CellDoubleClick);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 368);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Text = "Críticas: 0";
            // 
            // panelActions
            // 
            this.panelActions.Location = new System.Drawing.Point(790, 90);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(120, 296);
            this.panelActions.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(10, 40);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(100, 30);
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(10, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(10, 240);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // CriticaListForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(920, 400);
            this.panelMaestros.Controls.Add(this.lblUsuario);
            this.panelMaestros.Controls.Add(this.comboUsuario);
            this.panelMaestros.Controls.Add(this.lblContenido);
            this.panelMaestros.Controls.Add(this.comboContenido);
            this.panelMaestros.Controls.Add(this.btnLimpiarFiltros);
            Controls.Add(this.panelMaestros);
            Controls.Add(this.dgvCriticas);
            Controls.Add(this.lblTotal);
            this.panelActions.Controls.Add(this.btnAdd);
            this.panelActions.Controls.Add(this.btnEdit);
            this.panelActions.Controls.Add(this.btnDelete);
            this.panelActions.Controls.Add(this.btnClose);
            Controls.Add(this.panelActions);
            Name = "CriticaListForm";
            Text = "Críticas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCriticas)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
