<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        chkRemember = New CheckBox()
        Label1 = New Label()
        cmd_tempInto = New Button()
        input_username = New TextBox()
        Label2 = New Label()
        input_password = New TextBox()
        lb_reg = New Label()
        cmd_login = New Button()
        imgStatusGreen = New PictureBox()
        imgStatusRed = New PictureBox()
        lblStatus = New Label()
        Timer1 = New Timer(components)
        imgVersionOrange = New PictureBox()
        imgVersionGreen = New PictureBox()
        lblVersion = New Label()
        ProgressBar1 = New ProgressBar()
        lblBar = New Label()
        CType(imgStatusGreen, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgStatusRed, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgVersionOrange, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgVersionGreen, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' chkRemember
        ' 
        chkRemember.AutoSize = True
        chkRemember.Font = New Font("黑体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        chkRemember.Location = New Point(35, 149)
        chkRemember.Name = "chkRemember"
        chkRemember.Size = New Size(72, 16)
        chkRemember.TabIndex = 0
        chkRemember.Text = "记住密码"
        chkRemember.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("黑体", 15F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Label1.Location = New Point(26, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(69, 20)
        Label1.TabIndex = 1
        Label1.Text = "用户名"
        ' 
        ' cmd_tempInto
        ' 
        cmd_tempInto.Cursor = Cursors.No
        cmd_tempInto.Enabled = False
        cmd_tempInto.FlatStyle = FlatStyle.Flat
        cmd_tempInto.Location = New Point(26, 189)
        cmd_tempInto.Name = "cmd_tempInto"
        cmd_tempInto.Size = New Size(86, 89)
        cmd_tempInto.TabIndex = 2
        cmd_tempInto.Text = "游客进入"
        cmd_tempInto.UseVisualStyleBackColor = True
        ' 
        ' input_username
        ' 
        input_username.Cursor = Cursors.IBeam
        input_username.Font = New Font("黑体", 15.75F)
        input_username.Location = New Point(112, 26)
        input_username.Name = "input_username"
        input_username.Size = New Size(207, 31)
        input_username.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("黑体", 15F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Label2.Location = New Point(50, 90)
        Label2.Name = "Label2"
        Label2.Size = New Size(49, 20)
        Label2.TabIndex = 4
        Label2.Text = "密码"
        ' 
        ' input_password
        ' 
        input_password.Cursor = Cursors.IBeam
        input_password.Font = New Font("黑体", 15.75F)
        input_password.Location = New Point(112, 84)
        input_password.Name = "input_password"
        input_password.Size = New Size(207, 31)
        input_password.TabIndex = 5
        ' 
        ' lb_reg
        ' 
        lb_reg.AutoSize = True
        lb_reg.Cursor = Cursors.Hand
        lb_reg.FlatStyle = FlatStyle.Flat
        lb_reg.Font = New Font("黑体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lb_reg.ForeColor = SystemColors.ActiveCaption
        lb_reg.Location = New Point(190, 150)
        lb_reg.Name = "lb_reg"
        lb_reg.Size = New Size(113, 12)
        lb_reg.TabIndex = 6
        lb_reg.Text = "没有账户？点击注册"
        ' 
        ' cmd_login
        ' 
        cmd_login.Cursor = Cursors.Hand
        cmd_login.FlatStyle = FlatStyle.Flat
        cmd_login.Location = New Point(118, 189)
        cmd_login.Name = "cmd_login"
        cmd_login.Size = New Size(202, 89)
        cmd_login.TabIndex = 7
        cmd_login.Text = "登录"
        cmd_login.UseVisualStyleBackColor = True
        ' 
        ' imgStatusGreen
        ' 
        imgStatusGreen.Image = CType(resources.GetObject("imgStatusGreen.Image"), Image)
        imgStatusGreen.Location = New Point(29, 286)
        imgStatusGreen.Name = "imgStatusGreen"
        imgStatusGreen.Size = New Size(16, 17)
        imgStatusGreen.SizeMode = PictureBoxSizeMode.Zoom
        imgStatusGreen.TabIndex = 8
        imgStatusGreen.TabStop = False
        imgStatusGreen.Visible = False
        ' 
        ' imgStatusRed
        ' 
        imgStatusRed.Image = CType(resources.GetObject("imgStatusRed.Image"), Image)
        imgStatusRed.Location = New Point(29, 286)
        imgStatusRed.Name = "imgStatusRed"
        imgStatusRed.Size = New Size(16, 17)
        imgStatusRed.SizeMode = PictureBoxSizeMode.Zoom
        imgStatusRed.TabIndex = 9
        imgStatusRed.TabStop = False
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.ForeColor = Color.Red
        lblStatus.Location = New Point(48, 283)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(93, 20)
        lblStatus.TabIndex = 10
        lblStatus.Text = "服务器维护中"
        ' 
        ' imgVersionOrange
        ' 
        imgVersionOrange.Image = CType(resources.GetObject("imgVersionOrange.Image"), Image)
        imgVersionOrange.Location = New Point(176, 286)
        imgVersionOrange.Name = "imgVersionOrange"
        imgVersionOrange.Size = New Size(16, 17)
        imgVersionOrange.SizeMode = PictureBoxSizeMode.Zoom
        imgVersionOrange.TabIndex = 11
        imgVersionOrange.TabStop = False
        imgVersionOrange.Visible = False
        ' 
        ' imgVersionGreen
        ' 
        imgVersionGreen.Image = CType(resources.GetObject("imgVersionGreen.Image"), Image)
        imgVersionGreen.Location = New Point(176, 286)
        imgVersionGreen.Name = "imgVersionGreen"
        imgVersionGreen.Size = New Size(16, 17)
        imgVersionGreen.SizeMode = PictureBoxSizeMode.Zoom
        imgVersionGreen.TabIndex = 12
        imgVersionGreen.TabStop = False
        imgVersionGreen.Visible = False
        ' 
        ' lblVersion
        ' 
        lblVersion.AutoSize = True
        lblVersion.Enabled = False
        lblVersion.ForeColor = Color.Red
        lblVersion.Location = New Point(194, 283)
        lblVersion.Name = "lblVersion"
        lblVersion.Size = New Size(79, 20)
        lblVersion.TabIndex = 13
        lblVersion.Text = "版本需更新"
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Location = New Point(0, 0)
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(350, 19)
        ProgressBar1.TabIndex = 14
        ProgressBar1.Visible = False
        ' 
        ' lblBar
        ' 
        lblBar.BackColor = Color.Transparent
        lblBar.Font = New Font("Microsoft YaHei UI", 7.5F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lblBar.Location = New Point(50, 22)
        lblBar.Name = "lblBar"
        lblBar.Size = New Size(253, 16)
        lblBar.TabIndex = 15
        lblBar.Text = "lblBar"
        lblBar.TextAlign = ContentAlignment.MiddleCenter
        lblBar.Visible = False
        ' 
        ' frmLogin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(347, 317)
        Controls.Add(lblBar)
        Controls.Add(ProgressBar1)
        Controls.Add(lblVersion)
        Controls.Add(imgVersionGreen)
        Controls.Add(imgVersionOrange)
        Controls.Add(lblStatus)
        Controls.Add(imgStatusRed)
        Controls.Add(imgStatusGreen)
        Controls.Add(cmd_login)
        Controls.Add(lb_reg)
        Controls.Add(input_password)
        Controls.Add(Label2)
        Controls.Add(input_username)
        Controls.Add(cmd_tempInto)
        Controls.Add(Label1)
        Controls.Add(chkRemember)
        Font = New Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "登录"
        CType(imgStatusGreen, ComponentModel.ISupportInitialize).EndInit()
        CType(imgStatusRed, ComponentModel.ISupportInitialize).EndInit()
        CType(imgVersionOrange, ComponentModel.ISupportInitialize).EndInit()
        CType(imgVersionGreen, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents chkRemember As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cmd_tempInto As Button
    Friend WithEvents input_username As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents input_password As TextBox
    Friend WithEvents lb_reg As Label
    Friend WithEvents cmd_login As Button
    Friend WithEvents imgStatusGreen As PictureBox
    Friend WithEvents imgStatusRed As PictureBox
    Friend WithEvents lblStatus As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents imgVersionOrange As PictureBox
    Friend WithEvents imgVersionGreen As PictureBox
    Friend WithEvents lblVersion As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents lblBar As Label
End Class
