Imports System.Data
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms

Public Class frmRegister
    ' VB.NET 使用 New 创建对象，无需 Set
    Private m_user As New clsUser()

    ' 公共属性用于向主窗体返回结果
    ' 使用私有字段 + 公共属性
    Public RegistrationSuccess As Boolean = False
    Public RegisteredUsername As String = ""

    ' 定义字符集（A-Z 大写字母 + 1-9 数字，排除0避免混淆）
    Private ReadOnly _charSet As Char() = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".ToCharArray()
    Private ReadOnly _random As Random = New Random(Guid.NewGuid().GetHashCode())

    ' 窗体加载事件 - 对应 VB6 的 Form_Load
    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' VB6: Me.Caption → VB.NET: Me.Text
        Me.Text = "注册"
        lblAuths.Text = GenerateRandomCode()
        ' 初始化控件
        InitControls()
    End Sub

    ' 窗体关闭事件 - 对应 VB6 的 Form_Unload
    Private Sub frmRegister_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' VB6: Set m_user = Nothing → VB.NET: 可置为 Nothing（非必需）
        m_user = Nothing
    End Sub

    ' ====== 初始化控件 ======
    Private Sub InitControls()
        ' 设置 Tab 键顺序
        txtUsername.TabIndex = 1
        txtPassword.TabIndex = 2
        txtConfirmPassword.TabIndex = 3
        txtEmail.TabIndex = 4
        cmdRegister.TabIndex = 5
        cmdCancel.TabIndex = 6

        ' 设置密码字符
        txtPassword.PasswordChar = "o"
        txtConfirmPassword.PasswordChar = "o"

        ' 焦点设置
        txtUsername.Focus()
    End Sub

    ' ====== 注册按钮事件 ======
    Private Sub cmdRegister_Click(sender As Object, e As EventArgs) Handles cmdRegister.Click
        Try
            ' 获取输入
            Dim username As String = txtUsername.Text.Trim()
            Dim password As String = txtPassword.Text
            Dim confirmPassword As String = txtConfirmPassword.Text
            Dim email As String = txtEmail.Text.Trim()
            Dim auth As String = txtAuth.Text

            ' 验证输入
            If Not ValidateInput(username, password, confirmPassword, email, auth) Then
                Exit Sub
            End If

            ' 禁用界面（类似 VB6 的 DisableControls）
            DisableControls()
            Me.Cursor = Cursors.WaitCursor  ' VB6: Screen.MousePointer = vbHourglass
            Application.DoEvents()           ' VB6: DoEvents

            ' 执行注册
            If m_user.Register(username, password, email) Then
                ' 注册成功
                RegistrationSuccess = True
                RegisteredUsername = username

                MessageBox.Show($"注册成功！{vbCrLf}用户名: {username}{vbCrLf}" &
                               If(email <> "", $"邮箱: {email}", ""),
                               "注册成功", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' 关闭窗体（类似 VB6 的 Unload Me）
                Me.DialogResult = DialogResult.OK
                Me.Close()
            Else
                ' 注册失败
                MessageBox.Show($"注册失败：{vbCrLf}{m_user.LastError}",
                               "注册失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                RegistrationSuccess = False
            End If

        Catch ex As Exception
            ' 错误处理
            MessageBox.Show($"注册过程中发生错误：{ex.Message}",
                           "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RegistrationSuccess = False
        Finally
            ' 恢复界面（类似 VB6 的 EnableControls）
            EnableControls()
            Me.Cursor = Cursors.Default  ' VB6: Screen.MousePointer = vbDefault
        End Try
    End Sub

    ' ====== 取消按钮事件 ======
    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        RegistrationSuccess = False
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' ====== 验证输入 ======
    Private Function ValidateInput(username As String, password As String,
                                 confirmPassword As String, email As String, auth As String) As Boolean

        ' 验证用户名
        If String.IsNullOrEmpty(username) Then
            MessageBox.Show("请输入用户名！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsername.Focus()
            txtUsername.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        If username.Length < 3 Then
            MessageBox.Show("用户名至少需要3个字符！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsername.Focus()
            txtUsername.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        If username.Length > 50 Then
            MessageBox.Show("用户名最多50个字符！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsername.Focus()
            txtUsername.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        ' 验证密码
        If String.IsNullOrEmpty(password) Then
            MessageBox.Show("请输入密码！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            txtPassword.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        If password.Length < 6 Then
            MessageBox.Show("密码至少需要6个字符！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            txtPassword.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        ' 验证确认密码
        If String.IsNullOrEmpty(confirmPassword) Then
            MessageBox.Show("请确认密码！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtConfirmPassword.Focus()
            txtPassword.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        If password <> confirmPassword Then
            MessageBox.Show("两次输入的密码不一致！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'txtPassword.Text = ""
            'txtConfirmPassword.Text = ""
            txtPassword.Focus()
            txtPassword.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        ' 验证邮箱
        If Not String.IsNullOrEmpty(email) Then
            If email.Length > 100 Then
                MessageBox.Show("邮箱最多100个字符！", "提示",
                               MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmail.Focus()
                txtEmail.BackColor = Color.LightCoral
                cleanColor.Enabled = True
                Return False
            End If

            ' 可选：添加邮箱格式验证
            If Not IsValidEmail(email) Then
                MessageBox.Show("邮箱格式不正确！", "提示",
                               MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmail.Focus()
                txtEmail.BackColor = Color.LightCoral
                cleanColor.Enabled = True
                Return False
            End If
        End If

        '验证码
        If String.IsNullOrEmpty(auth) Then
            MessageBox.Show("请输入验证码！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtUsername.Focus()
            txtAuth.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        If auth <> lblAuths.Text Then
            MessageBox.Show("验证码错误！", "提示",
                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtAuth.Focus()
            txtAuth.BackColor = Color.LightCoral
            cleanColor.Enabled = True
            Return False
        End If

        Return True
    End Function

    ' ====== 邮箱格式验证 ======
    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    ' ====== 禁用控件 ======
    Private Sub DisableControls()
        txtUsername.Enabled = False
        txtPassword.Enabled = False
        txtConfirmPassword.Enabled = False
        txtEmail.Enabled = False
        cmdRegister.Enabled = False
        cmdCancel.Enabled = False
    End Sub

    ' ====== 启用控件 ======
    Private Sub EnableControls()
        txtUsername.Enabled = True
        txtPassword.Enabled = True
        txtConfirmPassword.Enabled = True
        txtEmail.Enabled = True
        cmdRegister.Enabled = True
        cmdCancel.Enabled = True
    End Sub

    ' ====== 键盘事件 ======
    Private Sub txtUsername_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsername.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then  ' VB6: vbKeyReturn
            txtPassword.Focus()
            e.Handled = True  ' 阻止默认的叮声
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            txtConfirmPassword.Focus()
            e.Handled = True
        End If
    End Sub

    Private Sub txtConfirmPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtConfirmPassword.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            txtEmail.Focus()
            e.Handled = True
        End If
    End Sub

    Private Sub txtEmail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEmail.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            cmdRegister.PerformClick()  ' 触发按钮点击事件
            e.Handled = True
        End If
    End Sub

    ' ====== 窗体显示事件（可选） ======
    Private Sub frmRegister_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtUsername.Focus()
        txtUsername.SelectAll()  ' 选中所有文本
    End Sub

    ''' <summary>
    ''' 生成指定长度的随机字符组合
    ''' </summary>
    ''' <param name="length">组合长度，默认4位</param>
    ''' <returns>随机字符组合字符串</returns>
    'Private Function GenerateRandomCode(Optional length As Integer = 4, Optional authLabel As Label = Nothing) As String
    Private Function GenerateRandomCode(Optional length As Integer = 4) As String
        If length <= 0 Then
            Throw New ArgumentOutOfRangeException(NameOf(length), "长度必须大于0")
        End If

        Dim sb As New StringBuilder()
        For i As Integer = 0 To length - 1
            ' 随机获取字符集中的一个字符
            Dim randomIndex As Integer = _random.Next(0, _charSet.Length)
            sb.Append(_charSet(randomIndex))
        Next

        'Dim result As String = sb.ToString()

        'If authLabel IsNot Nothing Then
        ' 跨线程安全检查（若在非UI线程调用需处理）
        'If authLabel.InvokeRequired Then
        'authLabel.Invoke(Sub() authLabel.Text = result)
        'Else
        'authLabel.Text = result
        'End If
        'End If

        Return sb.ToString()
    End Function

    Private Sub cleanColor_Tick(sender As Object, e As EventArgs) Handles cleanColor.Tick
        txtUsername.BackColor = Color.White
        txtPassword.BackColor = Color.White
        txtConfirmPassword.BackColor = Color.White
        txtEmail.BackColor = Color.White
        txtAuth.BackColor = Color.White
        cleanColor.Enabled = False
    End Sub
End Class