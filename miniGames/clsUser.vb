Imports MySqlConnector

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
    ' ========== 注册方法 ==========
    Public Function Register(username As String, password As String,
                            Optional email As String = "") As Boolean
        Try
            ' 1. 验证注册信息
            If Not ValidateRegisterInput(username, password, email) Then
                Return False
            End If

            ' 2. 检查用户名是否已存在
            If IsUsernameExists(username) Then
                LastError = "用户名已存在"
                Return False
            End If

            ' 3. 检查邮箱是否已存在（如果提供了邮箱）
            If Not String.IsNullOrEmpty(email) AndAlso IsEmailExists(email) Then
                LastError = "邮箱已被注册"
                Return False
            End If

            ' 4. 构建插入SQL - 使用参数化查询防止SQL注入
            Dim sql As String
            If String.IsNullOrEmpty(email) Then
                sql = "INSERT INTO vb_user (username, password, email, isActive, create_time) " &
                      "VALUES (@username, MD5(@password), NULL, 1, NOW())"
            Else
                sql = "INSERT INTO vb_user (username, password, email, isActive, create_time) " &
                      "VALUES (@username, MD5(@password), @email, 1, NOW())"
            End If

            ' 5. 执行插入
            If ExecuteParameterizedQuery(sql, username, password, email) Then
                ' 获取新用户ID
                Dim getIDsql As String = "SELECT LAST_INSERT_ID()"
                Dim dt As DataTable = m_db.ExecuteQuery(getIDsql)

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim newID As Object = dt.Rows(0)(0)
                    If newID IsNot Nothing AndAlso Not IsDBNull(newID) Then
                        UserID = CLng(newID)
                        username = username
                        email = email
                        IsActive = True
                        CreateTime = DateTime.Now
                        LastError = "注册成功"
                        Return True
                    End If
                End If

                ' 插入成功但获取ID失败
                LastError = "注册成功，但获取用户信息失败"
                Return True
            Else
                LastError = $"注册失败: {m_db.LastError}"
                Return False
            End If

        Catch ex As Exception
            LastError = $"注册过程出错: {ex.Message}"
            Return False
        End Try
    End Function

    ' ========== 修改密码 ==========
    Public Function ChangePassword(username As String, oldPassword As String,
                              newPassword As String) As Boolean
        Try
            ' 1. 验证旧密码
            If Not Login(username, oldPassword) Then
                LastError = "原密码错误"
                Return False
            End If

            ' 2. 验证新密码
            If String.IsNullOrEmpty(newPassword) Then
                LastError = "新密码不能为空"
                Return False
            End If

            If newPassword.Length < 6 Then
                LastError = "新密码至少需要6个字符"
                Return False
            End If

            ' 3. 新旧密码不能相同
            If oldPassword = newPassword Then
                LastError = "新密码不能与旧密码相同"
                Return False
            End If

            ' 4. 更新密码 - 使用参数化查询
            Dim sql As String = "UPDATE vb_user SET password = MD5(@newPassword) " &
                           "WHERE username = @username"

            If Not m_db.OpenConnection() Then
                LastError = $"数据库连接失败: {m_db.LastError}"
                Return False
            End If

            Using cmd As New MySqlCommand(sql, m_db.Connection)
                cmd.Parameters.AddWithValue("@newPassword", newPassword)
                cmd.Parameters.AddWithValue("@username", username)

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                If rowsAffected > 0 Then
                    LastError = "密码修改成功"
                    Return True
                Else
                    LastError = $"密码修改失败，未找到用户或密码未更改"
                    Return False
                End If
            End Using

        Catch ex As Exception
            LastError = $"修改密码出错: {ex.Message}"
            Return False
        End Try
    End Function

    ' ========== 参数化查询辅助方法 ==========
    Private Function ExecuteParameterizedQuery(sql As String, username As String,
                                              password As String, email As String) As Boolean
        Try
            If Not m_db.OpenConnection() Then
                LastError = $"数据库连接失败: {m_db.LastError}"
                Return False
            End If

            Using cmd As New MySqlCommand(sql, m_db.Connection)
                cmd.Parameters.AddWithValue("@username", username)
                cmd.Parameters.AddWithValue("@password", password)

                If Not String.IsNullOrEmpty(email) Then
                    cmd.Parameters.AddWithValue("@email", email)
                End If

                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected > 0
            End Using
        Catch ex As Exception
            LastError = $"执行SQL失败: {ex.Message}"
            Return False
        End Try
    End Function

    ' ========== 验证注册输入 ==========
    Private Function ValidateRegisterInput(username As String, password As String,
                                          email As String) As Boolean
        ' 验证用户名
        If String.IsNullOrWhiteSpace(username) Then
            LastError = "用户名不能为空"
            Return False
        End If

        If username.Length < 3 Then
            LastError = "用户名至少需要3个字符"
            Return False
        End If

        If username.Length > 50 Then
            LastError = "用户名最多50个字符"
            Return False
        End If

        ' 验证密码
        If String.IsNullOrWhiteSpace(password) Then
            LastError = "密码不能为空"
            Return False
        End If

        If password.Length < 6 Then
            LastError = "密码至少需要6个字符"
            Return False
        End If

        ' 验证邮箱（可选）
        If Not String.IsNullOrEmpty(email) Then
            If email.Length > 100 Then
                LastError = "邮箱最多100个字符"
                Return False
            End If

            ' 邮箱格式验证
            If Not IsValidEmail(email) Then
                LastError = "邮箱格式不正确"
                Return False
            End If
        End If

        Return True
    End Function

    ' ========== 邮箱格式验证 ==========
    Private Function IsValidEmail(email As String) As Boolean
        Try
            Dim addr As New System.Net.Mail.MailAddress(email)
            Return addr.Address = email
        Catch
            Return False
        End Try
    End Function

    ' ========== 检查用户名是否存在 ==========
    Private Function IsUsernameExists(username As String) As Boolean
        Try
            Dim sql As String = $"SELECT COUNT(*) FROM vb_user WHERE username = '{m_db.EscapeSQL(username)}'"
            Dim dt As DataTable = m_db.ExecuteQuery(sql)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0)(0)) > 0
            End If
            Return False
        Catch
            Return False
        End Try
    End Function

    ' ========== 检查邮箱是否存在 ==========
    Private Function IsEmailExists(email As String) As Boolean
        Try
            Dim sql As String = $"SELECT COUNT(*) FROM vb_user WHERE email = '{m_db.EscapeSQL(email)}'"
            Dim dt As DataTable = m_db.ExecuteQuery(sql)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0)(0)) > 0
            End If
            Return False
        Catch
            Return False
        End Try
    End Function
End Class