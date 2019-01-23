Public Class ClsCard

    Public Left As String
    Public Left_Score As Integer
    Public Left_BadHistory As Boolean

    Public Middle As String
    Public Middle_Score As Integer
    Public Middle_BadHistory As Boolean

    Public Right As String
    Public Right_Score As Integer
    Public Right_BadHistory As Boolean

    Public Function Left_NeedsTesting() As Boolean
        If Left = "" Then Return False
        Return Left_Score <= 0
    End Function
    Public Function Middle_NeedsTesting() As Boolean
        If Middle = "" Then Return False
        Return Middle_Score <= 0
    End Function
    Public Function Right_NeedsTesting() As Boolean
        If Right = "" Then Return False
        Return Right_Score <= 0
    End Function

End Class
