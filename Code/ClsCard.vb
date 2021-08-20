Public Class ClsCard

    Public ReadOnly Left As String
    Public Left_Score As Integer
    Public Left_BadHistory As Boolean

    Public ReadOnly Middle As String
    Public Middle_Score As Integer
    Public Middle_BadHistory As Boolean

    Public ReadOnly Right As String
    Public Right_Score As Integer
    Public Right_BadHistory As Boolean

    Public Sub New(left As String, right As String)
        Me.Left = left
        Me.Right = right
    End Sub
    Public Sub New(left As String, middle As String, right As String)
        Me.Left = left
        Me.Middle = middle
        Me.Right = right
    End Sub

    Public Function LeftNeedsTesting() As Boolean
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
