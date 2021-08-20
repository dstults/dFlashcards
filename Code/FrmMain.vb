
Public Class FrmMain

    Private VersionSplash As String = "2021-08-20 / v1.1.0"
    'private fG As Graphics = Me.CreateGraphics

    Private ShowCards As Boolean

    Private CurrentCard As ClsCard = Nothing
    Private CurrentSide As CardSide = CardSide.Unset
    Private LastToggledTest As CardSide = CardSide.Unset

    Private TestLeft As Boolean = True
    Private TestMiddle As Boolean = True
    Private TestRight As Boolean = True

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
        If CardDeck.Cards.Count > 0 Then
            CurrentCard = CardDeck.Cards(0)
            NextCard()
        Else
            CurrentCard = Nothing
        End If
        UpdateDisplay()

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
                CardDeck.Rescore()
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


    Private Sub UpdateDisplay()
        Dim enableButtons As Boolean = CurrentCard IsNot Nothing
        BtnBack.Visible = enableButtons
        BtnBack.Enabled = enableButtons
        BtnNext.Visible = enableButtons
        BtnNext.Enabled = enableButtons
        BtnShow.Visible = enableButtons
        BtnShow.Enabled = enableButtons
        btnGood.Visible = enableButtons
        btnGood.Enabled = enableButtons
        btnSlow.Visible = enableButtons
        btnSlow.Enabled = enableButtons
        btnBad.Visible = enableButtons
        btnBad.Enabled = enableButtons

        If Not enableButtons Then
            LblLeft.Text = ""
            LblMid.Text = ""
            LblRight.Text = ""

        Else
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

        BtnLeft_Tested.Enabled = enableButtons
        BtnMiddle_Tested.Enabled = enableButtons
        BtnRight_Tested.Enabled = enableButtons
        Select Case TestLeft
            Case True
                BtnLeft_Tested.Text = "TEST"
            Case False
                BtnLeft_Tested.Text = "ignore"
        End Select
        Select Case TestMiddle
            Case True
                BtnMiddle_Tested.Text = "TEST"
            Case False
                BtnMiddle_Tested.Text = "ignore"
        End Select
        Select Case TestRight
            Case True
                BtnRight_Tested.Text = "TEST"
            Case False
                BtnRight_Tested.Text = "ignore"
        End Select

        BtnShow.Focus()
        BtnShow.Select()
    End Sub

    Private Sub BtnLeft_Tested_Click(sender As Object, e As EventArgs) Handles BtnLeft_Tested.Click
        LastToggledTest = CardSide.Left
        TestLeft = Not TestLeft
        CheckTestEnablings()
        UpdateDisplay()
    End Sub

    Private Sub BtnMiddle_Tested_Click(sender As Object, e As EventArgs) Handles BtnMiddle_Tested.Click
        LastToggledTest = CardSide.Middle
        TestMiddle = Not TestMiddle
        CheckTestEnablings()
        UpdateDisplay()
    End Sub

    Private Sub BtnRight_Tested_Click(sender As Object, e As EventArgs) Handles BtnRight_Tested.Click
        LastToggledTest = CardSide.Right
        TestRight = Not TestRight
        CheckTestEnablings()
        UpdateDisplay()
    End Sub

    Private Sub CheckTestEnablings()
        ' If all tests are disabled...
        If Not TestLeft And Not TestMiddle And Not TestRight Then
            Select Case LastToggledTest
                Case CardSide.Left
                    TestMiddle = Not TestMiddle
                    TestRight = Not TestRight
                Case CardSide.Middle
                    TestLeft = Not TestLeft
                    TestRight = Not TestRight
                Case CardSide.Right
                    TestLeft = Not TestLeft
                    TestMiddle = Not TestMiddle
            End Select
        End If
    End Sub

End Class
