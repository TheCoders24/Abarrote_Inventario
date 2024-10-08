namespace Interfaz
{
    partial class MenuPrincipal
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
            this.btnproductos = new System.Windows.Forms.Button();
            this.btnproveedores = new System.Windows.Forms.Button();
            this.btnclientes = new System.Windows.Forms.Button();
            this.btndetallesventas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnproductos
            // 
            this.btnproductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnproductos.Location = new System.Drawing.Point(58, 90);
            this.btnproductos.Name = "btnproductos";
            this.btnproductos.Size = new System.Drawing.Size(124, 39);
            this.btnproductos.TabIndex = 0;
            this.btnproductos.Text = "Productos";
            this.btnproductos.UseVisualStyleBackColor = true;
            // 
            // btnproveedores
            // 
            this.btnproveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnproveedores.Location = new System.Drawing.Point(58, 156);
            this.btnproveedores.Name = "btnproveedores";
            this.btnproveedores.Size = new System.Drawing.Size(124, 39);
            this.btnproveedores.TabIndex = 1;
            this.btnproveedores.Text = "Proveedores";
            this.btnproveedores.UseVisualStyleBackColor = true;
            // 
            // btnclientes
            // 
            this.btnclientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclientes.Location = new System.Drawing.Point(58, 227);
            this.btnclientes.Name = "btnclientes";
            this.btnclientes.Size = new System.Drawing.Size(124, 39);
            this.btnclientes.TabIndex = 2;
            this.btnclientes.Text = "Clientes";
            this.btnclientes.UseVisualStyleBackColor = true;
            // 
            // btndetallesventas
            // 
            this.btndetallesventas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndetallesventas.Location = new System.Drawing.Point(58, 296);
            this.btndetallesventas.Name = "btndetallesventas";
            this.btndetallesventas.Size = new System.Drawing.Size(124, 39);
            this.btndetallesventas.TabIndex = 3;
            this.btndetallesventas.Text = "DetallesVentas";
            this.btndetallesventas.UseVisualStyleBackColor = true;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(250, 395);
            this.Controls.Add(this.btndetallesventas);
            this.Controls.Add(this.btnclientes);
            this.Controls.Add(this.btnproveedores);
            this.Controls.Add(this.btnproductos);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnproductos;
        private System.Windows.Forms.Button btnproveedores;
        private System.Windows.Forms.Button btnclientes;
        private System.Windows.Forms.Button btndetallesventas;
    }
}