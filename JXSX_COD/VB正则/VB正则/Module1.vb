Imports System.Text.RegularExpressions
Module Module1

    Sub Main()
        'Dim reg3 As String = "[1-9a-z]+"
        Dim reg3 As String = "(昌硕)|(立臻)|(世硕)"
        Dim CusStr As String = "12345立臻6789asdfgh"
        Dim mc3 As String = Regex.Match(CusStr, reg3).ToString().Trim()
        Console.WriteLine(mc3)
        Console.ReadKey()

    End Sub

End Module