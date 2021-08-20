
Public Class FrmMain

    Private LastToggledTest As Integer
    Private VersionSplash As String = "2021-08-20 / v1.1.0"
    'Public fG As Graphics = Me.CreateGraphics
    Public TestLeft As Boolean = True
    Public TestMiddle As Boolean = True
    Public TestRight As Boolean = True

    Private Sub FrmMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub FlashMan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        OpenFileDialog1.InitialDirectory = Application.StartupPath
        UpdateDisplay()

    End Sub

    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        Dim optionPicked As Integer
        optionPicked = OpenFileDialog1.ShowDialog

        If optionPicked = vbCancel Then
            'MsgBox("Operation cancelled!")
            Return
        End If
        If optionPicked <> vbOK Then
            MsgBox("Unknown dialogue operation!" & vbNewLine & "(Code: " & Format(optionPicked, "0") & ")")
            Return
        End If
        If Dir(OpenFileDialog1.FileName) = "" Then
            MsgBox("File not found:" & vbNewLine & OpenFileDialog1.FileName)
            Return
        End If

        CardDeck.LoadCardsFromFile(OpenFileDialog1.FileName)
        CurrentCard = CardDeck.Cards(0)
        NextCard()
        UpdateDisplay()

    End Sub

    Private Sub CheckTestEnablings()
        ' If all tests are disabled...
        If Not TestLeft And Not TestMiddle And Not TestRight Then
            Select Case LastToggledTest
                Case CardSide.Left
                    ToggleMiddle()
                    ToggleRight()
                Case CardSide.Middle
                    ToggleLeft()
                    ToggleRight()
                Case CardSide.Right
                    ToggleLeft()
                    ToggleMiddle()
            End Select
        End If
    End Sub

    Private Sub UpdateDisplay()
        If CurrentCard Is Nothing Then
            BtnBack.Visible = False
            BtnBack.Enabled = False
            BtnNext.Visible = False
            BtnNext.Enabled = False
            BtnShow.Visible = False
            BtnShow.Enabled = False
            btnGood.Visible = False
            btnGood.Enabled = False
            btnSlow.Visible = False
            btnSlow.Enabled = False
            btnBad.Visible = False
            btnBad.Enabled = False
            LblLeft.Text = ""
            LblMid.Text = ""
            LblRight.Text = ""
        Else
            BtnBack.Visible = True
            BtnBack.Enabled = True
            BtnNext.Visible = True
            BtnNext.Enabled = True
            'btnShow.Visible = True
            'btnShow.Enabled = True
            btnGood.Visible = True
            btnGood.Enabled = True
            btnSlow.Visible = True
            btnSlow.Enabled = True
            btnBad.Visible = True
            btnBad.Enabled = True
            Select Case ShowCards
                Case True
                    BtnShow.Enabled = False
                    BtnShow.Visible = False
                    SetLabelText(LblLeft, CurrentCard.Left)
                    SetLabelText(LblMid, CurrentCard.Middle)
                    SetLabelText(LblRight, CurrentCard.Right)
                Case False
                    BtnShow.Enabled = True
                    BtnShow.Visible = True
                    LblLeft.Text = ""
                    LblMid.Text = ""
                    LblRight.Text = ""
                    Select Case CurrentSide
                        Case CardSide.Left
                            SetLabelText(LblLeft, CurrentCard.Left)
                        Case CardSide.Middle
                            SetLabelText(LblMid, CurrentCard.Middle)
                        Case CardSide.Right
                            SetLabelText(LblRight, CurrentCard.Right)
                    End Select
            End Select
        End If
        BtnShow.Focus()
        BtnShow.Select()
    End Sub

    Private Sub FrmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case 219
                DoBack()
            Case 221
                DoNext()
            Case 32, 65
                DoShowCards()
            Case 83
                DoGood()
            Case 68
                DoSlow()
            Case 70
                DoBad()
                'Case Else
                'MsgBox("keyCode: " & e.KeyCode & vbNewLine & "keyData: " & e.KeyData & vbNewLine & "keyValue: " & e.KeyValue)
        End Select
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As EventArgs) Handles BtnShow.Click
        DoShowCards()
    End Sub

    Private Sub DoShowCards()
        If BtnShow.Enabled Then
            ShowCards = True
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnGood_Click(sender As Object, e As EventArgs) Handles btnGood.Click
        DoGood()
    End Sub

    Private Sub DoGood()
        If btnGood.Enabled Then
            Select Case CurrentSide
                Case CardSide.Left
                    CurrentCard.Left_Score += 1
                    If CurrentCard.Left_Score > 1 Then CurrentCard.Left_Score = 1
                Case CardSide.Middle
                    CurrentCard.Middle_Score += 1
                    If CurrentCard.Middle_Score > 1 Then CurrentCard.Middle_Score = 1
                Case CardSide.Right
                    CurrentCard.Right_Score += 1
                    If CurrentCard.Right_Score > 1 Then CurrentCard.Right_Score = 1
            End Select
            NextCard()
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnSlow_Click(sender As Object, e As EventArgs) Handles btnSlow.Click
        DoSlow()
    End Sub

    Private Sub DoSlow()
        If btnSlow.Enabled Then
            ' Doesn't modify score
            NextCard()
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnBad_Click(sender As Object, e As EventArgs) Handles btnBad.Click
        DoBad()
    End Sub

    Private Sub DoBad()
        If btnBad.Enabled Then
            Select Case CurrentSide
                Case CardSide.Left
                    CurrentCard.Left_Score -= 1
                    CurrentCard.Left_BadHistory = True
                    If CurrentCard.Left_Score < -1 Then CurrentCard.Left_Score = -1
                Case CardSide.Middle
                    CurrentCard.Middle_Score -= 1
                    CurrentCard.Middle_BadHistory = True
                    If CurrentCard.Middle_Score < -1 Then CurrentCard.Middle_Score = -1
                Case CardSide.Right
                    CurrentCard.Right_Score -= 1
                    CurrentCard.Right_BadHistory = True
                    If CurrentCard.Right_Score < -1 Then CurrentCard.Right_Score = -1
            End Select
            NextCard()
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        DoBack()
    End Sub

    Private Sub DoBack()
        If BtnBack.Enabled Then
            Dim nextIndex = CardDeck.Cards.IndexOf(CurrentCard) - 1
            If nextIndex < 0 Then nextIndex = CardDeck.Cards.Count - 1
            CurrentCard = CardDeck.Cards(nextIndex)
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        DoNext()
    End Sub

    Private Sub DoNext()
        If BtnNext.Enabled Then
            Dim nextIndex = CardDeck.Cards.IndexOf(CurrentCard) + 1
            If nextIndex >= CardDeck.Cards.Count Then nextIndex = 0
            CurrentCard = CardDeck.Cards(nextIndex)
            UpdateDisplay()
        End If
    End Sub

    Private Sub BtnLeft_Tested_Click(sender As Object, e As EventArgs) Handles BtnLeft_Tested.Click
        LastToggledTest = CardSide.Left
        ToggleLeft()
        CheckTestEnablings()
    End Sub

    Private Sub BtnMiddle_Tested_Click(sender As Object, e As EventArgs) Handles BtnMiddle_Tested.Click
        LastToggledTest = CardSide.Middle
        ToggleMiddle()
        CheckTestEnablings()
    End Sub

    Private Sub BtnRight_Tested_Click(sender As Object, e As EventArgs) Handles BtnRight_Tested.Click
        LastToggledTest = CardSide.Right
        ToggleRight()
        CheckTestEnablings()
    End Sub

    Private Sub ToggleLeft()
        If BtnLeft_Tested.Enabled Then
            TestLeft = Not TestLeft
            Update_BtnLeftTest_Display()
        End If
    End Sub
    Private Sub Update_BtnLeftTest_Display()
        Select Case TestLeft
            Case True
                BtnLeft_Tested.Text = "TEST"
            Case False
                BtnLeft_Tested.Text = "ignore"
        End Select
    End Sub

    Private Sub ToggleMiddle()
        If BtnMiddle_Tested.Enabled Then
            TestMiddle = Not TestMiddle
            Update_BtnMiddleTest_Display()
        End If
    End Sub
    Private Sub Update_BtnMiddleTest_Display()
        Select Case TestMiddle
            Case True
                BtnMiddle_Tested.Text = "TEST"
            Case False
                BtnMiddle_Tested.Text = "ignore"
        End Select
    End Sub

    Private Sub ToggleRight()
        If BtnRight_Tested.Enabled Then
            TestRight = Not TestRight
            Update_BtnRightTest_Display()
        End If
    End Sub
    Private Sub Update_BtnRightTest_Display()
        Select Case TestRight
            Case True
                BtnRight_Tested.Text = "TEST"
            Case False
                BtnRight_Tested.Text = "ignore"
        End Select
    End Sub

    Private Sub LblAbout_Click(sender As Object, e As EventArgs) Handles lblAbout.Click
        MsgBox("Darren's Flashcards! (dFlashcards.exe)" & vbNewLine & vbNewLine & "Made with VB.net in VS 2017 Community Edition." & vbNewLine & "Major Version Date: 2019" & vbNewLine & vbNewLine & "For any questions please contact:" & vbNewLine & "Darren" & vbNewLine & "drankof@gmail.com", vbOKOnly, VersionSplash)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim fG As Graphics = Me.CreateGraphics
        drawTiledBG(fG, blueGridPic, ptOffset, -2, 1)
    End Sub

    Public Sub NextCard()
        ShowCards = False

        Dim nextIndex As Integer = CardDeck.Cards.IndexOf(CurrentCard)
        Dim nextCard As ClsCard
        Dim nextSides As New HashSet(Of Integer)
        Do
            nextIndex += 1
            If nextIndex = CardDeck.Cards.Count Then nextIndex = 0
            nextCard = CardDeck.Cards(nextIndex)
            If nextCard Is CurrentCard Then
                MsgBox("You have memorized all the cards!" & vbNewLine & vbNewLine & "Now do it again!", vbOKOnly, "Congratulations!")
                CardDeck.Shuffle()
            Else
                If TestLeft And nextCard.LeftNeedsTesting Then nextSides.Add(CardSide.Left)
                If TestMiddle And nextCard.Middle_NeedsTesting Then nextSides.Add(CardSide.Middle)
                If TestRight And nextCard.Right_NeedsTesting Then nextSides.Add(CardSide.Right)
                If nextSides.Count > 0 Then
                    CurrentSide = nextSides(Int(Rnd() * nextSides.Count))
                    CurrentCard = nextCard
                    Exit Do
                End If
            End If
        Loop

    End Sub

    Private Sub DeleteCard(cardID As Integer, ByRef theDeck() As ClsCard, ByRef deckTotal As Integer)
        deckTotal = deckTotal - 1
        For intA As Integer = cardID To deckTotal
            theDeck(intA) = theDeck(intA + 1)
        Next
        ReDim Preserve theDeck(deckTotal)
    End Sub

End Class
