Public Class clsUser
    Private m_db As New clsDatabase()
    ' 成员变量保留，去掉VB6的Let/Get属性，改用.NET自动属性
    Public Property UserID As Long = 0
    Public Property Username As String = ""
    Public Property Email As String = ""
    Public Property CreateTime As Date = Date.Now
    Public Property IsActive As Boolean = False
    Public Property LastError As String = ""

    ' 登录方法（替换VB6的Recordset为DataTable）
    Public Function Login(username As String, password As String) As Boolean
        Try
            ' 1. 输入验证（保留原有逻辑）
            If String.IsNullOrWhiteSpace(username) Then
                LastError = "用户名不能为空"
                Return False
            End If
            If String.IsNullOrWhiteSpace(password) Then
                LastError = "密码不能为空"
                Return False
            End If

            ' 2. 查询用户是否存在（替换ADODB.Recordset）
            Dim sql As String = $"SELECT COUNT(*) as cnt FROM vb_user WHERE username = '{m_db.EscapeSQL(username)}' AND password = MD5('{m_db.EscapeSQL(password)}')"
            Dim dt As DataTable = m_db.ExecuteQuery(sql)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                LastError = "查询失败: " & m_db.LastError
                Return False
            End If

            Dim count As Long = CLng(dt.Rows(0)("cnt"))
            If count = 0 Then
                LastError = "用户名或密码错误"
                Return False
            End If

            ' 3. 获取用户信息
            sql = $"SELECT id, username, email, create_time, isActive FROM vb_user WHERE username = '{m_db.EscapeSQL(username)}'"
            dt = m_db.ExecuteQuery(sql)
            If dt.Rows.Count = 0 Then
                LastError = "获取用户信息失败"
                Return False
            End If

            ' 填充用户信息（替换Recordset取值）
            UserID = CLng(dt.Rows(0)("id"))
            username = CStr(dt.Rows(0)("username"))
            Email = If(IsDBNull(dt.Rows(0)("email")), "", CStr(dt.Rows(0)("email")))
            CreateTime = If(IsDBNull(dt.Rows(0)("create_time")), Date.Now, CDate(dt.Rows(0)("create_time")))
            IsActive = CBool(dt.Rows(0)("isActive"))

            LastError = "登录成功"
            Return True
        Catch ex As Exception
            LastError = $"系统错误: {ex.Message}"
            Return False
        End Try
    End Function

    ' 注册/修改密码等方法可按此逻辑迁移，核心是替换Recordset为DataTable
End Class