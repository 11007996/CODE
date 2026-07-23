
Imports System.IO
Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim sourceFile As String = "D:\a.txt"
            Dim destFile As String = "C:\Users\mh.guo\Desktop\c.txt"
            File.Copy(sourceFile, destFile, True)
        Catch ex As Exception
            MessageBox.Show(ex.Message)

        End Try

    End Sub
End Class
