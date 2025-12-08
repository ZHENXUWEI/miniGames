Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml

Public Class AppConfig
    Private Shared ReadOnly ConfigDir As String = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), "LYGames")
    Private Shared ReadOnly ConfigPath As String = Path.Combine(ConfigDir, "user_config.cfg")
    Private Const EncryptKey As String = "VB.NET_Login_Key_2025_LYGames"

    ''' <summary>
    ''' 确保配置文件目录存在
    ''' </summary>
    Private Shared Sub EnsureConfigDirectory()
        If Not Directory.Exists(ConfigDir) Then
            Directory.CreateDirectory(ConfigDir)
        End If
    End Sub

    ''' <summary>
    ''' 保存用户凭据到配置文件
    ''' </summary>
    Public Shared Sub SaveCredentials(username As String, password As String, rememberPassword As Boolean)
        EnsureConfigDirectory()

        Try
            Dim xmlDoc As New XmlDocument()
            Dim rootElement As XmlElement

            ' 如果文件存在则加载，否则创建新文档
            If File.Exists(ConfigPath) Then
                xmlDoc.Load(ConfigPath)
                rootElement = xmlDoc.DocumentElement
            Else
                ' 创建新的XML结构
                Dim decl As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", Nothing)
                xmlDoc.AppendChild(decl)

                rootElement = xmlDoc.CreateElement("LYGamesConfig")
                xmlDoc.AppendChild(rootElement)
            End If

            ' 更新或创建节点
            SetXmlValue(xmlDoc, rootElement, "Username", If(String.IsNullOrEmpty(username), "", username))
            SetXmlValue(xmlDoc, rootElement, "RememberPassword", rememberPassword.ToString().ToLower())

            If rememberPassword AndAlso Not String.IsNullOrEmpty(password) Then
                ' 加密保存密码
                SetXmlValue(xmlDoc, rootElement, "Password", EncryptString(password, EncryptKey))
            Else
                ' 清除密码
                SetXmlValue(xmlDoc, rootElement, "Password", "")
            End If

            ' 保存时间戳
            SetXmlValue(xmlDoc, rootElement, "LastUpdated", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

            ' 保存到文件
            xmlDoc.Save(ConfigPath)
        Catch ex As Exception
            MessageBox.Show($"保存配置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' 从配置文件加载用户凭据
    ''' </summary>
    Public Shared Function LoadCredentials() As (Username As String, Password As String, RememberPassword As Boolean)
        EnsureConfigDirectory()

        If Not File.Exists(ConfigPath) Then
            Return (String.Empty, String.Empty, False)
        End If

        Try
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(ConfigPath)

            Dim username As String = GetXmlValue(xmlDoc, "Username")
            Dim encryptedPassword As String = GetXmlValue(xmlDoc, "Password")
            Dim rememberPasswordStr As String = GetXmlValue(xmlDoc, "RememberPassword")

            Dim password As String = String.Empty
            Dim rememberPassword As Boolean = False

            ' 解析记住密码选项
            Boolean.TryParse(rememberPasswordStr, rememberPassword)

            ' 解密密码
            If rememberPassword AndAlso Not String.IsNullOrEmpty(encryptedPassword) Then
                Try
                    password = DecryptString(encryptedPassword, EncryptKey)
                Catch
                    ' 解密失败，返回空密码
                    password = String.Empty
                End Try
            End If

            Return (username, password, rememberPassword)
        Catch ex As Exception
            MessageBox.Show($"读取配置失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return (String.Empty, String.Empty, False)
        End Try
    End Function

    ''' <summary>
    ''' 清除保存的凭据
    ''' </summary>
    Public Shared Sub ClearCredentials()
        If File.Exists(ConfigPath) Then
            Try
                SaveCredentials("", "", False)
            Catch ex As Exception
                MessageBox.Show($"清除凭据失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 获取配置文件路径（用于调试）
    ''' </summary>
    Public Shared Function GetConfigPath() As String
        Return ConfigPath
    End Function

    ''' <summary>
    ''' 获取配置目录路径
    ''' </summary>
    Public Shared Function GetConfigDirectory() As String
        Return ConfigDir
    End Function

#Region "辅助方法"
    Private Shared Sub SetXmlValue(xmlDoc As XmlDocument, root As XmlElement, key As String, value As String)
        Dim node As XmlNode = root.SelectSingleNode(key)
        If node Is Nothing Then
            node = xmlDoc.CreateElement(key)
            root.AppendChild(node)
        End If
        node.InnerText = value
    End Sub

    Private Shared Function GetXmlValue(xmlDoc As XmlDocument, key As String) As String
        Dim node As XmlNode = xmlDoc.DocumentElement.SelectSingleNode(key)
        Return If(node IsNot Nothing, node.InnerText, String.Empty)
    End Function

    ''' <summary>
    ''' AES加密字符串
    ''' </summary>
    Private Shared Function EncryptString(plainText As String, key As String) As String
        Using aesAlg As Aes = Aes.Create()
            Dim keyBytes As Byte() = New SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(key))
            Array.Resize(keyBytes, aesAlg.KeySize \ 8)
            aesAlg.Key = keyBytes
            aesAlg.IV = New Byte(15) {}

            Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
            Using msEncrypt As New MemoryStream()
                Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                    Using swEncrypt As New StreamWriter(csEncrypt)
                        swEncrypt.Write(plainText)
                    End Using
                End Using
                Return Convert.ToBase64String(msEncrypt.ToArray())
            End Using
        End Using
    End Function

    ''' <summary>
    ''' AES解密字符串
    ''' </summary>
    Private Shared Function DecryptString(cipherText As String, key As String) As String
        Using aesAlg As Aes = Aes.Create()
            Dim keyBytes As Byte() = New SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(key))
            Array.Resize(keyBytes, aesAlg.KeySize \ 8)
            aesAlg.Key = keyBytes
            aesAlg.IV = New Byte(15) {}

            Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)
            Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)

            Using msDecrypt As New MemoryStream(cipherBytes)
                Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                    Using srDecrypt As New StreamReader(csDecrypt)
                        Return srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        End Using
    End Function
#End Region
End Class