
namespace HandyMike
{
    partial class User_Satisfaction
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Satisfaction));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.reviewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.handyMikeData = new HandyMike.HandyMikeData();
            this.reviewTableAdapter = new HandyMike.HandyMikeDataTableAdapters.ReviewTableAdapter();
            this.label4 = new System.Windows.Forms.Label();
            this.reviewBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reviewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.handyMikeData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reviewBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(61, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(246, 378);
            this.dataGridView1.TabIndex = 0;
            this.toolTip1.SetToolTip(this.dataGridView1, "Person name and rating");
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // reviewBindingSource
            // 
            this.reviewBindingSource.DataMember = "Review";
            this.reviewBindingSource.DataSource = this.handyMikeData;
            // 
            // handyMikeData
            // 
            this.handyMikeData.DataSetName = "HandyMikeData";
            this.handyMikeData.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reviewTableAdapter
            // 
            this.reviewTableAdapter.ClearBeforeFill = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.Location = new System.Drawing.Point(369, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 29);
            this.label4.TabIndex = 46;
            this.label4.Text = "Select month";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // reviewBindingSource1
            // 
            this.reviewBindingSource1.DataMember = "Review";
            this.reviewBindingSource1.DataSource = this.handyMikeData;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(619, 103);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "one";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(300, 300);
            this.chart1.TabIndex = 47;
            this.chart1.Text = "chart1";
            this.toolTip1.SetToolTip(this.chart1, "Ratings on a pie chart");
            this.chart1.Click += new System.EventHandler(this.chart1_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Location = new System.Drawing.Point(290, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 41);
            this.label1.TabIndex = 48;
            this.label1.Text = "USER SATISFACTION";
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(211, 511);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(168, 23);
            this.button6.TabIndex = 54;
            this.button6.Text = "Exit";
            this.toolTip1.SetToolTip(this.button6, "Exit");
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(12, 511);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(168, 23);
            this.button3.TabIndex = 53;
            this.button3.Text = "Back";
            this.toolTip1.SetToolTip(this.button3, "Back to reports page");
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(795, 402);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(124, 74);
            this.richTextBox1.TabIndex = 55;
            this.richTextBox1.Text = "1 : Poor Service \n2 : Weak Service\n3 : Good Service\n4 : Very Good Service\n5 : Exc" +
    "ellent Service";
            this.toolTip1.SetToolTip(this.richTextBox1, "Rating Legend");
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // User_Satisfaction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(969, 546);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "User_Satisfaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Satisfaction";
            this.toolTip1.SetToolTip(this, "User satifaction from ratings");
            this.Load += new System.EventHandler(this.User_Satisfaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reviewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.handyMikeData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reviewBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private HandyMikeData handyMikeData;
        private System.Windows.Forms.BindingSource reviewBindingSource;
        private HandyMikeDataTableAdapters.ReviewTableAdapter reviewTableAdapter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingSource reviewBindingSource1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}