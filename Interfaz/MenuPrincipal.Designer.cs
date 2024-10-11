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
            this.btnproductos.Location = new System.Drawing.Point(44, 73);
            this.btnproductos.Margin = new System.Windows.Forms.Padding(2);
            this.btnproductos.Name = "btnproductos";
            this.btnproductos.Size = new System.Drawing.Size(93, 32);
            this.btnproductos.TabIndex = 0;
            this.btnproductos.Text = "Productos";
            this.btnproductos.UseVisualStyleBackColor = true;
            this.btnproductos.Click += new System.EventHandler(this.btnproductos_Click);
            // 
            // btnproveedores
            // 
            this.btnproveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnproveedores.Location = new System.Drawing.Point(44, 127);
            this.btnproveedores.Margin = new System.Windows.Forms.Padding(2);
            this.btnproveedores.Name = "btnproveedores";
            this.btnproveedores.Size = new System.Drawing.Size(93, 32);
            this.btnproveedores.TabIndex = 1;
            this.btnproveedores.Text = "Proveedores";
            this.btnproveedores.UseVisualStyleBackColor = true;
            this.btnproveedores.Click += new System.EventHandler(this.btnproveedores_Click);
            // 
            // btnclientes
            // 
            this.btnclientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclientes.Location = new System.Drawing.Point(44, 184);
            this.btnclientes.Margin = new System.Windows.Forms.Padding(2);
            this.btnclientes.Name = "btnclientes";
            this.btnclientes.Size = new System.Drawing.Size(93, 32);
            this.btnclientes.TabIndex = 2;
            this.btnclientes.Text = "Clientes";
            this.btnclientes.UseVisualStyleBackColor = true;
            this.btnclientes.Click += new System.EventHandler(this.btnclientes_Click);
            // 
            // btndetallesventas
            // 
            this.btndetallesventas.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndetallesventas.Location = new System.Drawing.Point(44, 240);
            this.btndetallesventas.Margin = new System.Windows.Forms.Padding(2);
            this.btndetallesventas.Name = "btndetallesventas";
            this.btndetallesventas.Size = new System.Drawing.Size(93, 32);
            this.btndetallesventas.TabIndex = 3;
            this.btndetallesventas.Text = "Ventas";
            this.btndetallesventas.UseVisualStyleBackColor = true;
            this.btndetallesventas.Click += new System.EventHandler(this.btndetallesventas_Click);
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(188, 321);
            this.Controls.Add(this.btndetallesventas);
            this.Controls.Add(this.btnclientes);
            this.Controls.Add(this.btnproveedores);
            this.Controls.Add(this.btnproductos);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnproductos;
        private System.Windows.Forms.Button btnproveedores;
        private System.Windows.Forms.Button btnclientes;
        private System.Windows.Forms.Button btndetallesventas;
    }
}