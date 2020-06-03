<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlCommand
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserControlCommand))
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.PanelSendCommand = New System.Windows.Forms.Panel()
        Me.dgvParameters = New System.Windows.Forms.DataGridView()
        Me.ColumnИндекс = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnЗначениеПараметра = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnТип = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnОписание = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ComboBoxTasks = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxURLSend = New System.Windows.Forms.TextBox()
        Me.PanelStatusSend = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButtonIsClient = New System.Windows.Forms.RadioButton()
        Me.RadioButtonIsServer = New System.Windows.Forms.RadioButton()
        Me.TextBoxPipeServerStatus = New System.Windows.Forms.TextBox()
        Me.SendTextBox = New System.Windows.Forms.TextBox()
        Me.PanelSend = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelSend = New System.Windows.Forms.TableLayoutPanel()
        Me.ListTaskSend = New System.Windows.Forms.ListView()
        Me.SplitContainerSend = New System.Windows.Forms.SplitContainer()
        Me.DataGridSend = New System.Windows.Forms.DataGridView()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TableLayoutPanelForm = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonSend = New System.Windows.Forms.Button()
        Me.ButtonListen = New System.Windows.Forms.Button()
        Me.PanelReceive = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelReceive = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainerRecieve = New System.Windows.Forms.SplitContainer()
        Me.ReceiveTextBox = New System.Windows.Forms.TextBox()
        Me.DataGridReceive = New System.Windows.Forms.DataGridView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ListTaskReceive = New System.Windows.Forms.ListView()
        Me.PanelRecive = New System.Windows.Forms.Panel()
        Me.LabelError = New System.Windows.Forms.Label()
        Me.TextBoxError = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxURLReceive = New System.Windows.Forms.TextBox()
        Me.timeStampLabel = New System.Windows.Forms.Label()
        Me.TimeStampTextBox = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PanelSendCommand.SuspendLayout()
        CType(Me.dgvParameters, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelStatusSend.SuspendLayout()
        Me.PanelSend.SuspendLayout()
        Me.TableLayoutPanelSend.SuspendLayout()
        CType(Me.SplitContainerSend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerSend.Panel1.SuspendLayout()
        Me.SplitContainerSend.Panel2.SuspendLayout()
        Me.SplitContainerSend.SuspendLayout()
        CType(Me.DataGridSend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanelForm.SuspendLayout()
        Me.PanelReceive.SuspendLayout()
        Me.TableLayoutPanelReceive.SuspendLayout()
        CType(Me.SplitContainerRecieve, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerRecieve.Panel1.SuspendLayout()
        Me.SplitContainerRecieve.Panel2.SuspendLayout()
        Me.SplitContainerRecieve.SuspendLayout()
        CType(Me.DataGridReceive, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelRecive.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label80
        '
        Me.Label80.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label80.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label80.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label80.ForeColor = System.Drawing.Color.Blue
        Me.Label80.Location = New System.Drawing.Point(6, 39)
        Me.Label80.Name = "Label80"
        Me.Label80.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label80.Size = New System.Drawing.Size(55, 20)
        Me.Label80.TabIndex = 23
        Me.Label80.Text = "Статус:"
        '
        'PanelSendCommand
        '
        Me.PanelSendCommand.Controls.Add(Me.dgvParameters)
        Me.PanelSendCommand.Controls.Add(Me.ComboBoxTasks)
        Me.PanelSendCommand.Controls.Add(Me.Label2)
        Me.PanelSendCommand.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSendCommand.Location = New System.Drawing.Point(3, 83)
        Me.PanelSendCommand.Name = "PanelSendCommand"
        Me.PanelSendCommand.Size = New System.Drawing.Size(372, 214)
        Me.PanelSendCommand.TabIndex = 30
        '
        'dgvParameters
        '
        Me.dgvParameters.AllowUserToAddRows = False
        Me.dgvParameters.AllowUserToDeleteRows = False
        Me.dgvParameters.AllowUserToResizeColumns = False
        Me.dgvParameters.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.dgvParameters.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvParameters.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.dgvParameters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvParameters.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvParameters.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvParameters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvParameters.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnИндекс, Me.ColumnЗначениеПараметра, Me.ColumnТип, Me.ColumnОписание})
        Me.dgvParameters.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgvParameters.Location = New System.Drawing.Point(0, 33)
        Me.dgvParameters.MultiSelect = False
        Me.dgvParameters.Name = "dgvParameters"
        Me.dgvParameters.RowHeadersVisible = False
        Me.dgvParameters.Size = New System.Drawing.Size(372, 181)
        Me.dgvParameters.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.dgvParameters, "Таблица редактирования полей выбранной команды")
        '
        'ColumnИндекс
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColumnИндекс.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColumnИндекс.FillWeight = 108.4038!
        Me.ColumnИндекс.HeaderText = "Индекс"
        Me.ColumnИндекс.Name = "ColumnИндекс"
        Me.ColumnИндекс.ReadOnly = True
        Me.ColumnИндекс.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColumnИндекс.Width = 80
        '
        'ColumnЗначениеПараметра
        '
        Me.ColumnЗначениеПараметра.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ColumnЗначениеПараметра.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColumnЗначениеПараметра.FillWeight = 73.83957!
        Me.ColumnЗначениеПараметра.HeaderText = "Значение"
        Me.ColumnЗначениеПараметра.Name = "ColumnЗначениеПараметра"
        Me.ColumnЗначениеПараметра.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColumnТип
        '
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue
        Me.ColumnТип.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColumnТип.FillWeight = 74.15202!
        Me.ColumnТип.HeaderText = "Тип"
        Me.ColumnТип.Name = "ColumnТип"
        Me.ColumnТип.ReadOnly = True
        Me.ColumnТип.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColumnОписание
        '
        Me.ColumnОписание.HeaderText = "Описание"
        Me.ColumnОписание.Name = "ColumnОписание"
        Me.ColumnОписание.ReadOnly = True
        Me.ColumnОписание.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ComboBoxTasks
        '
        Me.ComboBoxTasks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxTasks.FormattingEnabled = True
        Me.ComboBoxTasks.Location = New System.Drawing.Point(130, 6)
        Me.ComboBoxTasks.Name = "ComboBoxTasks"
        Me.ComboBoxTasks.Size = New System.Drawing.Size(239, 21)
        Me.ComboBoxTasks.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.ComboBoxTasks, "Выбор команды для редактирования и отправки на Клиент")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(4, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Команда для посылки"
        '
        'TextBoxURLSend
        '
        Me.TextBoxURLSend.AcceptsReturn = True
        Me.TextBoxURLSend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxURLSend.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TextBoxURLSend.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxURLSend.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxURLSend.ForeColor = System.Drawing.Color.Maroon
        Me.TextBoxURLSend.Location = New System.Drawing.Point(4, 20)
        Me.TextBoxURLSend.MaxLength = 0
        Me.TextBoxURLSend.Name = "TextBoxURLSend"
        Me.TextBoxURLSend.ReadOnly = True
        Me.TextBoxURLSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxURLSend.Size = New System.Drawing.Size(365, 13)
        Me.TextBoxURLSend.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TextBoxURLSend, "Сетевой адрес контейнера команды посылаемой на Клиент")
        '
        'PanelStatusSend
        '
        Me.PanelStatusSend.Controls.Add(Me.Label1)
        Me.PanelStatusSend.Controls.Add(Me.TextBoxURLSend)
        Me.PanelStatusSend.Controls.Add(Me.RadioButtonIsClient)
        Me.PanelStatusSend.Controls.Add(Me.RadioButtonIsServer)
        Me.PanelStatusSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelStatusSend.Location = New System.Drawing.Point(3, 3)
        Me.PanelStatusSend.Name = "PanelStatusSend"
        Me.PanelStatusSend.Size = New System.Drawing.Size(372, 74)
        Me.PanelStatusSend.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(372, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Посылка"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'RadioButtonIsClient
        '
        Me.RadioButtonIsClient.AutoSize = True
        Me.RadioButtonIsClient.Checked = True
        Me.RadioButtonIsClient.Enabled = False
        Me.RadioButtonIsClient.ForeColor = System.Drawing.Color.Blue
        Me.RadioButtonIsClient.Location = New System.Drawing.Point(9, 45)
        Me.RadioButtonIsClient.Name = "RadioButtonIsClient"
        Me.RadioButtonIsClient.Size = New System.Drawing.Size(61, 17)
        Me.RadioButtonIsClient.TabIndex = 25
        Me.RadioButtonIsClient.TabStop = True
        Me.RadioButtonIsClient.Text = "Клиент"
        Me.RadioButtonIsClient.UseVisualStyleBackColor = True
        '
        'RadioButtonIsServer
        '
        Me.RadioButtonIsServer.AutoSize = True
        Me.RadioButtonIsServer.Enabled = False
        Me.RadioButtonIsServer.ForeColor = System.Drawing.Color.Blue
        Me.RadioButtonIsServer.Location = New System.Drawing.Point(76, 45)
        Me.RadioButtonIsServer.Name = "RadioButtonIsServer"
        Me.RadioButtonIsServer.Size = New System.Drawing.Size(62, 17)
        Me.RadioButtonIsServer.TabIndex = 26
        Me.RadioButtonIsServer.TabStop = True
        Me.RadioButtonIsServer.Text = "Сервер"
        Me.RadioButtonIsServer.UseVisualStyleBackColor = True
        '
        'TextBoxPipeServerStatus
        '
        Me.TextBoxPipeServerStatus.AcceptsReturn = True
        Me.TextBoxPipeServerStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPipeServerStatus.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TextBoxPipeServerStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxPipeServerStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxPipeServerStatus.ForeColor = System.Drawing.Color.Maroon
        Me.TextBoxPipeServerStatus.Location = New System.Drawing.Point(67, 39)
        Me.TextBoxPipeServerStatus.MaxLength = 0
        Me.TextBoxPipeServerStatus.Name = "TextBoxPipeServerStatus"
        Me.TextBoxPipeServerStatus.ReadOnly = True
        Me.TextBoxPipeServerStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxPipeServerStatus.Size = New System.Drawing.Size(301, 13)
        Me.TextBoxPipeServerStatus.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.TextBoxPipeServerStatus, "Статус контейнера для команды посылаемой на Клиент")
        '
        'SendTextBox
        '
        Me.SendTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SendTextBox.Location = New System.Drawing.Point(0, 0)
        Me.SendTextBox.Multiline = True
        Me.SendTextBox.Name = "SendTextBox"
        Me.SendTextBox.ReadOnly = True
        Me.SendTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.SendTextBox.Size = New System.Drawing.Size(372, 107)
        Me.SendTextBox.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.SendTextBox, "Текст команды посылаемой на Клиент в формате XML")
        Me.SendTextBox.WordWrap = False
        '
        'PanelSend
        '
        Me.PanelSend.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelSend.Controls.Add(Me.TableLayoutPanelSend)
        Me.PanelSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSend.Location = New System.Drawing.Point(3, 3)
        Me.PanelSend.Name = "PanelSend"
        Me.PanelSend.Size = New System.Drawing.Size(378, 722)
        Me.PanelSend.TabIndex = 31
        '
        'TableLayoutPanelSend
        '
        Me.TableLayoutPanelSend.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TableLayoutPanelSend.ColumnCount = 1
        Me.TableLayoutPanelSend.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelSend.Controls.Add(Me.PanelStatusSend, 0, 0)
        Me.TableLayoutPanelSend.Controls.Add(Me.PanelSendCommand, 0, 1)
        Me.TableLayoutPanelSend.Controls.Add(Me.ListTaskSend, 0, 3)
        Me.TableLayoutPanelSend.Controls.Add(Me.SplitContainerSend, 0, 2)
        Me.TableLayoutPanelSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelSend.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelSend.Name = "TableLayoutPanelSend"
        Me.TableLayoutPanelSend.RowCount = 4
        Me.TableLayoutPanelSend.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80.0!))
        Me.TableLayoutPanelSend.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 220.0!))
        Me.TableLayoutPanelSend.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelSend.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanelSend.Size = New System.Drawing.Size(378, 722)
        Me.TableLayoutPanelSend.TabIndex = 31
        '
        'ListTaskSend
        '
        Me.ListTaskSend.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListTaskSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListTaskSend.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ListTaskSend.ForeColor = System.Drawing.Color.Blue
        Me.ListTaskSend.GridLines = True
        Me.ListTaskSend.Location = New System.Drawing.Point(3, 525)
        Me.ListTaskSend.Name = "ListTaskSend"
        Me.ListTaskSend.Size = New System.Drawing.Size(372, 194)
        Me.ListTaskSend.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.ListTaskSend, "Список всех команд посылаемых на Клиент в текущем сеансе работы")
        Me.ListTaskSend.UseCompatibleStateImageBehavior = False
        Me.ListTaskSend.View = System.Windows.Forms.View.Details
        '
        'SplitContainerSend
        '
        Me.SplitContainerSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerSend.Location = New System.Drawing.Point(3, 303)
        Me.SplitContainerSend.Name = "SplitContainerSend"
        Me.SplitContainerSend.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerSend.Panel1
        '
        Me.SplitContainerSend.Panel1.Controls.Add(Me.SendTextBox)
        '
        'SplitContainerSend.Panel2
        '
        Me.SplitContainerSend.Panel2.Controls.Add(Me.DataGridSend)
        Me.SplitContainerSend.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainerSend.Size = New System.Drawing.Size(372, 216)
        Me.SplitContainerSend.SplitterDistance = 107
        Me.SplitContainerSend.TabIndex = 31
        '
        'DataGridSend
        '
        Me.DataGridSend.AllowUserToAddRows = False
        Me.DataGridSend.AllowUserToDeleteRows = False
        Me.DataGridSend.AllowUserToResizeRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.GhostWhite
        Me.DataGridSend.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridSend.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridSend.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridSend.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridSend.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridSend.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DataGridSend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridSend.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridSend.Location = New System.Drawing.Point(0, 20)
        Me.DataGridSend.MultiSelect = False
        Me.DataGridSend.Name = "DataGridSend"
        Me.DataGridSend.RowHeadersVisible = False
        Me.DataGridSend.Size = New System.Drawing.Size(372, 85)
        Me.DataGridSend.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.DataGridSend, "Значения параметров команды посылаемой Клиенту")
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(372, 20)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Посылаемая задача"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TableLayoutPanelForm
        '
        Me.TableLayoutPanelForm.BackColor = System.Drawing.Color.Silver
        Me.TableLayoutPanelForm.ColumnCount = 2
        Me.TableLayoutPanelForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelForm.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanelForm.Controls.Add(Me.ButtonSend, 0, 1)
        Me.TableLayoutPanelForm.Controls.Add(Me.ButtonListen, 1, 1)
        Me.TableLayoutPanelForm.Controls.Add(Me.PanelReceive, 1, 0)
        Me.TableLayoutPanelForm.Controls.Add(Me.PanelSend, 0, 0)
        Me.TableLayoutPanelForm.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelForm.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelForm.Name = "TableLayoutPanelForm"
        Me.TableLayoutPanelForm.RowCount = 2
        Me.TableLayoutPanelForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelForm.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.TableLayoutPanelForm.Size = New System.Drawing.Size(768, 768)
        Me.TableLayoutPanelForm.TabIndex = 36
        '
        'ButtonSend
        '
        Me.ButtonSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSend.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonSend.Cursor = System.Windows.Forms.Cursors.Default
        Me.ButtonSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ButtonSend.Image = CType(resources.GetObject("ButtonSend.Image"), System.Drawing.Image)
        Me.ButtonSend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonSend.Location = New System.Drawing.Point(235, 731)
        Me.ButtonSend.Name = "ButtonSend"
        Me.ButtonSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ButtonSend.Size = New System.Drawing.Size(146, 34)
        Me.ButtonSend.TabIndex = 35
        Me.ButtonSend.Text = "Послать команду"
        Me.ButtonSend.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.ButtonSend, "Послать Клиенту выбранную команду для исполнения")
        Me.ButtonSend.UseVisualStyleBackColor = False
        '
        'ButtonListen
        '
        Me.ButtonListen.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonListen.Image = CType(resources.GetObject("ButtonListen.Image"), System.Drawing.Image)
        Me.ButtonListen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonListen.Location = New System.Drawing.Point(619, 731)
        Me.ButtonListen.Name = "ButtonListen"
        Me.ButtonListen.Size = New System.Drawing.Size(146, 34)
        Me.ButtonListen.TabIndex = 34
        Me.ButtonListen.Text = "Установить связь"
        Me.ButtonListen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.ButtonListen, "Влючить режим прослушивания сети для принятия команд")
        Me.ButtonListen.UseVisualStyleBackColor = True
        '
        'PanelReceive
        '
        Me.PanelReceive.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelReceive.Controls.Add(Me.TableLayoutPanelReceive)
        Me.PanelReceive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelReceive.Location = New System.Drawing.Point(387, 3)
        Me.PanelReceive.Name = "PanelReceive"
        Me.PanelReceive.Size = New System.Drawing.Size(378, 722)
        Me.PanelReceive.TabIndex = 32
        '
        'TableLayoutPanelReceive
        '
        Me.TableLayoutPanelReceive.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TableLayoutPanelReceive.ColumnCount = 1
        Me.TableLayoutPanelReceive.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelReceive.Controls.Add(Me.SplitContainerRecieve, 0, 1)
        Me.TableLayoutPanelReceive.Controls.Add(Me.ListTaskReceive, 0, 2)
        Me.TableLayoutPanelReceive.Controls.Add(Me.PanelRecive, 0, 0)
        Me.TableLayoutPanelReceive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanelReceive.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanelReceive.Name = "TableLayoutPanelReceive"
        Me.TableLayoutPanelReceive.RowCount = 3
        Me.TableLayoutPanelReceive.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300.0!))
        Me.TableLayoutPanelReceive.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelReceive.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanelReceive.Size = New System.Drawing.Size(378, 722)
        Me.TableLayoutPanelReceive.TabIndex = 26
        '
        'SplitContainerRecieve
        '
        Me.SplitContainerRecieve.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerRecieve.Location = New System.Drawing.Point(3, 303)
        Me.SplitContainerRecieve.Name = "SplitContainerRecieve"
        Me.SplitContainerRecieve.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerRecieve.Panel1
        '
        Me.SplitContainerRecieve.Panel1.Controls.Add(Me.ReceiveTextBox)
        '
        'SplitContainerRecieve.Panel2
        '
        Me.SplitContainerRecieve.Panel2.Controls.Add(Me.DataGridReceive)
        Me.SplitContainerRecieve.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainerRecieve.Size = New System.Drawing.Size(372, 216)
        Me.SplitContainerRecieve.SplitterDistance = 107
        Me.SplitContainerRecieve.TabIndex = 32
        '
        'ReceiveTextBox
        '
        Me.ReceiveTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReceiveTextBox.Location = New System.Drawing.Point(0, 0)
        Me.ReceiveTextBox.Multiline = True
        Me.ReceiveTextBox.Name = "ReceiveTextBox"
        Me.ReceiveTextBox.ReadOnly = True
        Me.ReceiveTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ReceiveTextBox.Size = New System.Drawing.Size(372, 107)
        Me.ReceiveTextBox.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.ReceiveTextBox, "Текст команды пришедшей от Сервера в формате XML")
        Me.ReceiveTextBox.WordWrap = False
        '
        'DataGridReceive
        '
        Me.DataGridReceive.AllowUserToAddRows = False
        Me.DataGridReceive.AllowUserToDeleteRows = False
        Me.DataGridReceive.AllowUserToResizeRows = False
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.GhostWhite
        Me.DataGridReceive.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridReceive.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridReceive.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridReceive.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DataGridReceive.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridReceive.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridReceive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridReceive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridReceive.Location = New System.Drawing.Point(0, 20)
        Me.DataGridReceive.MultiSelect = False
        Me.DataGridReceive.Name = "DataGridReceive"
        Me.DataGridReceive.RowHeadersVisible = False
        Me.DataGridReceive.Size = New System.Drawing.Size(372, 85)
        Me.DataGridReceive.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.DataGridReceive, "Значения параметров команды принятой от Сервера")
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(0, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(372, 20)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Задача на исполнение"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ListTaskReceive
        '
        Me.ListTaskReceive.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ListTaskReceive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListTaskReceive.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ListTaskReceive.ForeColor = System.Drawing.Color.Blue
        Me.ListTaskReceive.GridLines = True
        Me.ListTaskReceive.Location = New System.Drawing.Point(3, 525)
        Me.ListTaskReceive.Name = "ListTaskReceive"
        Me.ListTaskReceive.Size = New System.Drawing.Size(372, 194)
        Me.ListTaskReceive.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.ListTaskReceive, "Список всех пришедших команд от Сервера в текущем сеансе работы")
        Me.ListTaskReceive.UseCompatibleStateImageBehavior = False
        Me.ListTaskReceive.View = System.Windows.Forms.View.Details
        '
        'PanelRecive
        '
        Me.PanelRecive.BackColor = System.Drawing.Color.WhiteSmoke
        Me.PanelRecive.Controls.Add(Me.Label80)
        Me.PanelRecive.Controls.Add(Me.LabelError)
        Me.PanelRecive.Controls.Add(Me.TextBoxError)
        Me.PanelRecive.Controls.Add(Me.Label6)
        Me.PanelRecive.Controls.Add(Me.TextBoxPipeServerStatus)
        Me.PanelRecive.Controls.Add(Me.TextBoxURLReceive)
        Me.PanelRecive.Controls.Add(Me.timeStampLabel)
        Me.PanelRecive.Controls.Add(Me.TimeStampTextBox)
        Me.PanelRecive.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelRecive.Location = New System.Drawing.Point(3, 3)
        Me.PanelRecive.Name = "PanelRecive"
        Me.PanelRecive.Size = New System.Drawing.Size(372, 294)
        Me.PanelRecive.TabIndex = 33
        '
        'LabelError
        '
        Me.LabelError.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelError.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LabelError.Cursor = System.Windows.Forms.Cursors.Default
        Me.LabelError.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.LabelError.ForeColor = System.Drawing.Color.Blue
        Me.LabelError.Location = New System.Drawing.Point(12, 84)
        Me.LabelError.Name = "LabelError"
        Me.LabelError.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LabelError.Size = New System.Drawing.Size(356, 22)
        Me.LabelError.TabIndex = 30
        Me.LabelError.Text = "Ошибка:"
        Me.LabelError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TextBoxError
        '
        Me.TextBoxError.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxError.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBoxError.Location = New System.Drawing.Point(0, 113)
        Me.TextBoxError.Margin = New System.Windows.Forms.Padding(2)
        Me.TextBoxError.Multiline = True
        Me.TextBoxError.Name = "TextBoxError"
        Me.TextBoxError.ReadOnly = True
        Me.TextBoxError.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxError.Size = New System.Drawing.Size(372, 181)
        Me.TextBoxError.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.TextBoxError, "Текст ошибки соединения с Сервером по сети")
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(372, 17)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Прием"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBoxURLReceive
        '
        Me.TextBoxURLReceive.AcceptsReturn = True
        Me.TextBoxURLReceive.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxURLReceive.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TextBoxURLReceive.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxURLReceive.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxURLReceive.ForeColor = System.Drawing.Color.Maroon
        Me.TextBoxURLReceive.Location = New System.Drawing.Point(6, 20)
        Me.TextBoxURLReceive.MaxLength = 0
        Me.TextBoxURLReceive.Name = "TextBoxURLReceive"
        Me.TextBoxURLReceive.ReadOnly = True
        Me.TextBoxURLReceive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBoxURLReceive.Size = New System.Drawing.Size(363, 13)
        Me.TextBoxURLReceive.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TextBoxURLReceive, "Сетевой адрес контейнера команды пришедшей от Сервера")
        '
        'timeStampLabel
        '
        Me.timeStampLabel.AutoSize = True
        Me.timeStampLabel.BackColor = System.Drawing.Color.Transparent
        Me.timeStampLabel.ForeColor = System.Drawing.Color.Blue
        Me.timeStampLabel.Location = New System.Drawing.Point(6, 61)
        Me.timeStampLabel.Name = "timeStampLabel"
        Me.timeStampLabel.Size = New System.Drawing.Size(58, 13)
        Me.timeStampLabel.TabIndex = 26
        Me.timeStampLabel.Text = "Получено:"
        '
        'TimeStampTextBox
        '
        Me.TimeStampTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TimeStampTextBox.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TimeStampTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TimeStampTextBox.ForeColor = System.Drawing.Color.Maroon
        Me.TimeStampTextBox.Location = New System.Drawing.Point(67, 61)
        Me.TimeStampTextBox.Name = "TimeStampTextBox"
        Me.TimeStampTextBox.ReadOnly = True
        Me.TimeStampTextBox.Size = New System.Drawing.Size(301, 13)
        Me.TimeStampTextBox.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.TimeStampTextBox, "Время получения команды от Клиента")
        '
        'UserControlCommand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanelForm)
        Me.Name = "UserControlCommand"
        Me.Size = New System.Drawing.Size(768, 768)
        Me.PanelSendCommand.ResumeLayout(False)
        Me.PanelSendCommand.PerformLayout()
        CType(Me.dgvParameters, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelStatusSend.ResumeLayout(False)
        Me.PanelStatusSend.PerformLayout()
        Me.PanelSend.ResumeLayout(False)
        Me.TableLayoutPanelSend.ResumeLayout(False)
        Me.SplitContainerSend.Panel1.ResumeLayout(False)
        Me.SplitContainerSend.Panel1.PerformLayout()
        Me.SplitContainerSend.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerSend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerSend.ResumeLayout(False)
        CType(Me.DataGridSend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanelForm.ResumeLayout(False)
        Me.PanelReceive.ResumeLayout(False)
        Me.TableLayoutPanelReceive.ResumeLayout(False)
        Me.SplitContainerRecieve.Panel1.ResumeLayout(False)
        Me.SplitContainerRecieve.Panel1.PerformLayout()
        Me.SplitContainerRecieve.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerRecieve, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerRecieve.ResumeLayout(False)
        CType(Me.DataGridReceive, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelRecive.ResumeLayout(False)
        Me.PanelRecive.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents Label80 As Windows.Forms.Label
    Friend WithEvents PanelSendCommand As Windows.Forms.Panel
    Friend WithEvents dgvParameters As Windows.Forms.DataGridView
    Friend WithEvents ColumnИндекс As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnЗначениеПараметра As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnТип As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnОписание As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolTip1 As Windows.Forms.ToolTip
    Friend WithEvents ComboBoxTasks As Windows.Forms.ComboBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Public WithEvents TextBoxURLSend As Windows.Forms.TextBox
    Friend WithEvents PanelStatusSend As Windows.Forms.Panel
    Public WithEvents Label1 As Windows.Forms.Label
    Public WithEvents TextBoxPipeServerStatus As Windows.Forms.TextBox
    Friend WithEvents SendTextBox As Windows.Forms.TextBox
    Friend WithEvents PanelSend As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanelSend As Windows.Forms.TableLayoutPanel
    Public WithEvents ListTaskSend As Windows.Forms.ListView
    Friend WithEvents SplitContainerSend As Windows.Forms.SplitContainer
    Friend WithEvents DataGridSend As Windows.Forms.DataGridView
    Public WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents TableLayoutPanelForm As Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonSend As Windows.Forms.Button
    Public WithEvents ButtonListen As Windows.Forms.Button
    Friend WithEvents PanelReceive As Windows.Forms.Panel
    Friend WithEvents TableLayoutPanelReceive As Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainerRecieve As Windows.Forms.SplitContainer
    Friend WithEvents ReceiveTextBox As Windows.Forms.TextBox
    Friend WithEvents DataGridReceive As Windows.Forms.DataGridView
    Public WithEvents Label7 As Windows.Forms.Label
    Friend WithEvents ListTaskReceive As Windows.Forms.ListView
    Friend WithEvents PanelRecive As Windows.Forms.Panel
    Public WithEvents LabelError As Windows.Forms.Label
    Public WithEvents Label6 As Windows.Forms.Label
    Public WithEvents TextBoxURLReceive As Windows.Forms.TextBox
    Private WithEvents timeStampLabel As Windows.Forms.Label
    Friend WithEvents RadioButtonIsServer As Windows.Forms.RadioButton
    Friend WithEvents RadioButtonIsClient As Windows.Forms.RadioButton
    Private WithEvents TimeStampTextBox As Windows.Forms.TextBox
    Public WithEvents TextBoxError As Windows.Forms.TextBox
End Class
