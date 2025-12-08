Public Class frmPersonalCenter
    Private Sub cmdBack_Click(sender As Object, e As EventArgs) Handles cmdBack.Click
        Me.Close()
    End Sub

    Private Sub cmdLogOut_Click(sender As Object, e As EventArgs) Handles cmdLogOut.Click
        frmLogin.Show()
        Me.Close()
    End Sub
End Class