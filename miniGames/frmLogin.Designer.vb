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
        CType(imgStatusGreen, ComponentModel.ISupportInitialize).BeginInit()
        CType(imgStatusRed, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' chkRemember
        ' 
        chkRemember.AutoSize = True
        chkRemember.Font = New Font("黑体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        chkRemember.Location = New Point(53, 126)
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
        Label1.Location = New Point(23, 26)
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
        cmd_tempInto.Location = New Point(23, 160)
        cmd_tempInto.Name = "cmd_tempInto"
        cmd_tempInto.Size = New Size(75, 75)
        cmd_tempInto.TabIndex = 2
        cmd_tempInto.Text = "游客进入"
        cmd_tempInto.UseVisualStyleBackColor = True
        ' 
        ' input_username
        ' 
        input_username.Cursor = Cursors.IBeam
        input_username.Font = New Font("黑体", 15.75F)
        input_username.Location = New Point(98, 22)
        input_username.Name = "input_username"
        input_username.Size = New Size(182, 31)
        input_username.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("黑体", 15F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        Label2.Location = New Point(43, 76)
        Label2.Name = "Label2"
        Label2.Size = New Size(49, 20)
        Label2.TabIndex = 4
        Label2.Text = "密码"
        ' 
        ' input_password
        ' 
        input_password.Cursor = Cursors.IBeam
        input_password.Font = New Font("黑体", 15.75F)
        input_password.Location = New Point(98, 72)
        input_password.Name = "input_password"
        input_password.Size = New Size(182, 31)
        input_password.TabIndex = 5
        ' 
        ' lb_reg
        ' 
        lb_reg.AutoSize = True
        lb_reg.Cursor = Cursors.Hand
        lb_reg.FlatStyle = FlatStyle.Flat
        lb_reg.Font = New Font("黑体", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(134))
        lb_reg.ForeColor = SystemColors.ActiveCaption
        lb_reg.Location = New Point(167, 127)
        lb_reg.Name = "lb_reg"
        lb_reg.Size = New Size(113, 12)
        lb_reg.TabIndex = 6
        lb_reg.Text = "没有账户？点击注册"
        ' 
        ' cmd_login
        ' 
        cmd_login.Cursor = Cursors.Hand
        cmd_login.FlatStyle = FlatStyle.Flat
        cmd_login.Location = New Point(104, 160)
        cmd_login.Name = "cmd_login"
        cmd_login.Size = New Size(176, 75)
        cmd_login.TabIndex = 7
        cmd_login.Text = "登录"
        cmd_login.UseVisualStyleBackColor = True
        ' 
        ' imgStatusGreen
        ' 
        imgStatusGreen.Image = CType(resources.GetObject("imgStatusGreen.Image"), Image)
        imgStatusGreen.Location = New Point(25, 243)
        imgStatusGreen.Name = "imgStatusGreen"
        imgStatusGreen.Size = New Size(14, 14)
        imgStatusGreen.SizeMode = PictureBoxSizeMode.Zoom
        imgStatusGreen.TabIndex = 8
        imgStatusGreen.TabStop = False
        imgStatusGreen.Visible = False
        ' 
        ' imgStatusRed
        ' 
        imgStatusRed.Image = CType(resources.GetObject("imgStatusRed.Image"), Image)
        imgStatusRed.Location = New Point(25, 244)
        imgStatusRed.Name = "imgStatusRed"
        imgStatusRed.Size = New Size(14, 14)
        imgStatusRed.SizeMode = PictureBoxSizeMode.Zoom
        imgStatusRed.TabIndex = 9
        imgStatusRed.TabStop = False
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = True
        lblStatus.ForeColor = Color.Red
        lblStatus.Location = New Point(42, 242)
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(80, 17)
        lblStatus.TabIndex = 10
        lblStatus.Text = "服务器维护中"
        ' 
        ' frmLogin
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(304, 269)
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
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "登录"
        CType(imgStatusGreen, ComponentModel.ISupportInitialize).EndInit()
        CType(imgStatusRed, ComponentModel.ISupportInitialize).EndInit()
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
End Class
