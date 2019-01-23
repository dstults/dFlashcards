
Module ModEffects

    Public BlueGridPic As Image = Global.dFlashcards.My.Resources.Resources.ShadesOfBlue
    Public PtOffset As Point

    Public Sub DrawTiledBG(ByRef theGFX As Graphics, ByRef theBG As Image, ByRef theOffset As Point, ByRef moveX As Integer, ByRef moveY As Integer)

        For intA As Integer = 1 To CInt(Int(theGFX.VisibleClipBounds.Width / theBG.Width) + 2)
            For intB As Integer = 1 To CInt(Int(theGFX.VisibleClipBounds.Height / theBG.Height) + 2)
                theGFX.DrawImage(theBG, CInt(theOffset.X + (intA - 2) * theBG.Width), CInt(theOffset.Y + (intB - 2) * theBG.Height))
            Next
        Next
        theOffset.X = theOffset.X + moveX
        If theOffset.X < 0 Then theOffset.X = theBG.Width
        If theOffset.X > theBG.Width Then theOffset.X = 0
        theOffset.Y = theOffset.Y + moveY
        If theOffset.Y < 0 Then theOffset.Y = theBG.Height
        If theOffset.Y > theBG.Height Then theOffset.Y = 0

    End Sub

    Public Sub SetLabelText(theLabel As Label, theText As String)

        If theText = "" Then theText = "-"
        Dim f As Font = theLabel.Font
        Select Case theText.Length
            Case 0
                Return
            Case Is <= 2
                theLabel.Font = New Font(f.Name, 72)
            Case Is <= 6
                theLabel.Font = New Font(f.Name, 48)
            Case Is <= 32
                theLabel.Font = New Font(f.Name, 24)
            Case Is <= 112
                theLabel.Font = New Font(f.Name, 14)
            Case Is <= 190
                theLabel.Font = New Font(f.Name, 10)
            Case Is <= 350
                theLabel.Font = New Font(f.Name, 8)
            Case Else
                theLabel.Font = New Font(f.Name, 6)
        End Select
        theLabel.Text = theText

    End Sub

End Module
