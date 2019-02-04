namespace PowerLine
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактированиеСхемыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактирвованиеОбъектовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимПросмотраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимПереносаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.отобажатьОбъектыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.использоватьСеткуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.увеличитьМасштабToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сделатьВсеЛинии10КВToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сделатьВесьПунктирДругимЦветомToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.конвертироватьСтаруюСхемуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.info1 = new PowerLine.Info();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem1,
            this.режимToolStripMenuItem,
            this.proToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1276, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem1
            // 
            this.файлToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.сохранитьToolStripMenuItem,
            this.toolStripSeparator,
            this.toolStripSeparator2,
            this.печатьToolStripMenuItem,
            this.toolStripSeparator3,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem1.Name = "файлToolStripMenuItem1";
            this.файлToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem1.Text = "&Файл";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(266, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(266, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(266, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.выходToolStripMenuItem.Text = "Вы&ход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // режимToolStripMenuItem
            // 
            this.режимToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактированиеСхемыToolStripMenuItem,
            this.редактирвованиеОбъектовToolStripMenuItem,
            this.режимПросмотраToolStripMenuItem,
            this.режимПереносаToolStripMenuItem,
            this.toolStripSeparator1,
            this.отобажатьОбъектыToolStripMenuItem,
            this.использоватьСеткуToolStripMenuItem,
            this.увеличитьМасштабToolStripMenuItem});
            this.режимToolStripMenuItem.Name = "режимToolStripMenuItem";
            this.режимToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.режимToolStripMenuItem.Text = "Режим";
            // 
            // редактированиеСхемыToolStripMenuItem
            // 
            this.редактированиеСхемыToolStripMenuItem.Checked = true;
            this.редактированиеСхемыToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.редактированиеСхемыToolStripMenuItem.Name = "редактированиеСхемыToolStripMenuItem";
            this.редактированиеСхемыToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.редактированиеСхемыToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.редактированиеСхемыToolStripMenuItem.Text = "Редактирование схемы";
            this.редактированиеСхемыToolStripMenuItem.Click += new System.EventHandler(this.ModeEvt);
            // 
            // редактирвованиеОбъектовToolStripMenuItem
            // 
            this.редактирвованиеОбъектовToolStripMenuItem.Name = "редактирвованиеОбъектовToolStripMenuItem";
            this.редактирвованиеОбъектовToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.редактирвованиеОбъектовToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.редактирвованиеОбъектовToolStripMenuItem.Text = "Редактирование объектов";
            this.редактирвованиеОбъектовToolStripMenuItem.Click += new System.EventHandler(this.ModeEvt);
            // 
            // режимПросмотраToolStripMenuItem
            // 
            this.режимПросмотраToolStripMenuItem.Name = "режимПросмотраToolStripMenuItem";
            this.режимПросмотраToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.режимПросмотраToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.режимПросмотраToolStripMenuItem.Text = "Режим просмотра";
            this.режимПросмотраToolStripMenuItem.Click += new System.EventHandler(this.ModeEvt);
            // 
            // режимПереносаToolStripMenuItem
            // 
            this.режимПереносаToolStripMenuItem.Name = "режимПереносаToolStripMenuItem";
            this.режимПереносаToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.режимПереносаToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.режимПереносаToolStripMenuItem.Text = "Режим переноса";
            this.режимПереносаToolStripMenuItem.Click += new System.EventHandler(this.ModeEvt);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(254, 6);
            // 
            // отобажатьОбъектыToolStripMenuItem
            // 
            this.отобажатьОбъектыToolStripMenuItem.Checked = true;
            this.отобажатьОбъектыToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.отобажатьОбъектыToolStripMenuItem.Name = "отобажатьОбъектыToolStripMenuItem";
            this.отобажатьОбъектыToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.отобажатьОбъектыToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.отобажатьОбъектыToolStripMenuItem.Text = "Отображать объекты";
            this.отобажатьОбъектыToolStripMenuItem.Click += new System.EventHandler(this.отобажатьОбъектыToolStripMenuItem_Click);
            // 
            // использоватьСеткуToolStripMenuItem
            // 
            this.использоватьСеткуToolStripMenuItem.Name = "использоватьСеткуToolStripMenuItem";
            this.использоватьСеткуToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.использоватьСеткуToolStripMenuItem.Text = "Использовать сетку";
            this.использоватьСеткуToolStripMenuItem.Click += new System.EventHandler(this.использоватьСеткуToolStripMenuItem_Click);
            // 
            // увеличитьМасштабToolStripMenuItem
            // 
            this.увеличитьМасштабToolStripMenuItem.Name = "увеличитьМасштабToolStripMenuItem";
            this.увеличитьМасштабToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.увеличитьМасштабToolStripMenuItem.Text = "Увеличить масштаб...";
            this.увеличитьМасштабToolStripMenuItem.Click += new System.EventHandler(this.увеличитьМасштабToolStripMenuItem_Click);
            // 
            // proToolStripMenuItem
            // 
            this.proToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сделатьВсеЛинии10КВToolStripMenuItem,
            this.сделатьВесьПунктирДругимЦветомToolStripMenuItem,
            this.конвертироватьСтаруюСхемуToolStripMenuItem});
            this.proToolStripMenuItem.Name = "proToolStripMenuItem";
            this.proToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.proToolStripMenuItem.Text = "Pro";
            // 
            // сделатьВсеЛинии10КВToolStripMenuItem
            // 
            this.сделатьВсеЛинии10КВToolStripMenuItem.Name = "сделатьВсеЛинии10КВToolStripMenuItem";
            this.сделатьВсеЛинии10КВToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.сделатьВсеЛинии10КВToolStripMenuItem.Text = "Сделать все линии 10 кВ";
            this.сделатьВсеЛинии10КВToolStripMenuItem.Click += new System.EventHandler(this.сделатьВсеЛинии10КВToolStripMenuItem_Click);
            // 
            // сделатьВесьПунктирДругимЦветомToolStripMenuItem
            // 
            this.сделатьВесьПунктирДругимЦветомToolStripMenuItem.Name = "сделатьВесьПунктирДругимЦветомToolStripMenuItem";
            this.сделатьВесьПунктирДругимЦветомToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.сделатьВесьПунктирДругимЦветомToolStripMenuItem.Text = "Сделать весь пунктир другим цветом";
            this.сделатьВесьПунктирДругимЦветомToolStripMenuItem.Click += new System.EventHandler(this.сделатьВесьПунктирДругимЦветомToolStripMenuItem_Click);
            // 
            // конвертироватьСтаруюСхемуToolStripMenuItem
            // 
            this.конвертироватьСтаруюСхемуToolStripMenuItem.Name = "конвертироватьСтаруюСхемуToolStripMenuItem";
            this.конвертироватьСтаруюСхемуToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.конвертироватьСтаруюСхемуToolStripMenuItem.Text = "Конвертировать старую схему";
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.Color.DarkOrange;
            this.lineShape1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.SelectionColor = System.Drawing.SystemColors.Control;
            this.lineShape1.X1 = 1;
            this.lineShape1.X2 = 1;
            this.lineShape1.Y1 = 1;
            this.lineShape1.Y2 = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Файл со схемой|*.sch;*.shm";
            this.openFileDialog1.Title = "Выберите путь для загрузки расположения линий";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Файл со схемой|*.sch";
            this.saveFileDialog1.Title = "Выберите файл для сохранения схемы";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.info1);
            this.panel1.Controls.Add(this.shapeContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1276, 974);
            this.panel1.TabIndex = 4;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CLICK);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DOWN);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MOVE);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UP);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer2.Size = new System.Drawing.Size(1276, 974);
            this.shapeContainer2.TabIndex = 0;
            this.shapeContainer2.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("создатьToolStripMenuItem.Image")));
            this.создатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.создатьToolStripMenuItem.Text = "&Создать схему";
            this.создатьToolStripMenuItem.Click += new System.EventHandler(this.создатьToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("открытьToolStripMenuItem.Image")));
            this.открытьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.открытьToolStripMenuItem.Text = "&Открыть схему";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(269, 22);
            this.toolStripMenuItem1.Text = "&Сохранить схему";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("сохранитьToolStripMenuItem.Image")));
            this.сохранитьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.сохранитьToolStripMenuItem.Text = "&Сохранить схему как...";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("печатьToolStripMenuItem.Image")));
            this.печатьToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(269, 22);
            this.печатьToolStripMenuItem.Text = "&Печать";
            this.печатьToolStripMenuItem.Click += new System.EventHandler(this.печатьToolStripMenuItem_Click);
            // 
            // info1
            // 
            this.info1.BackColor = System.Drawing.SystemColors.Info;
            this.info1.Location = new System.Drawing.Point(800, 397);
            this.info1.Name = "info1";
            this.info1.Size = new System.Drawing.Size(243, 70);
            this.info1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1276, 998);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Расстановка элементов";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DOWN);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MOVE);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UP);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem режимToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактированиеСхемыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактирвованиеОбъектовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимПросмотраToolStripMenuItem;
        private Info info1;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem отобажатьОбъектыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сделатьВсеЛинии10КВToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сделатьВесьПунктирДругимЦветомToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem использоватьСеткуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимПереносаToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem конвертироватьСтаруюСхемуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem увеличитьМасштабToolStripMenuItem;
    }
}

