Imports MySqlConnector ' 需通过NuGet安装MySqlConnector包
Imports System.Data

Public Class clsDatabase
    ' 替换VB6的CONN_STRING
    Private Const CONN_STRING As String = "Server=110.42.63.233;Database=pg;Uid=pg;Pwd=123456;Port=3306;"
    Private m_conn As MySqlConnection
    Public Property LastError As String = ""
    Public Property IsConnected As Boolean = False

    ' 初始化（替换Class_Initialize）
    Public Sub New()
        m_conn = New MySqlConnection(CONN_STRING)
    End Sub

    ' 打开连接（替换OpenConnection）
    Public Function OpenConnection() As Boolean
        Try
            If m_conn.State = ConnectionState.Open Then
                Return True
            End If
            m_conn.Open()
            IsConnected = True
            Return True
        Catch ex As Exception
            LastError = $"连接失败: {ex.Message}"
            IsConnected = False
            Return False
        End Try
    End Function

    ' 执行查询（替换ExecuteQuery，返回DataTable而非Recordset）
    Public Function ExecuteQuery(sql As String) As DataTable
        Dim dt As New DataTable()
        Try
            If Not OpenConnection() Then Return Nothing
            Using cmd As New MySqlCommand(sql, m_conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            Return dt
        Catch ex As Exception
            LastError = $"查询失败: {ex.Message} SQL: {sql}"
            Return Nothing
        End Try
    End Function

    ' 执行增删改（替换ExecuteNonQuery）
    Public Function ExecuteNonQuery(sql As String) As Boolean
        Try
            If Not OpenConnection() Then Return False
            Using cmd As New MySqlCommand(sql, m_conn)
                cmd.ExecuteNonQuery()
            End Using
            Return True
        Catch ex As Exception
            LastError = $"执行失败: {ex.Message} SQL: {sql}"
            Return False
        End Try
    End Function

    ' 转义SQL（保留原有逻辑）
    Public Function EscapeSQL(str As String) As String
        If String.IsNullOrEmpty(str) Then Return ""
        Return str.Replace("'", "''").Replace("\", "\\")
    End Function
End Class