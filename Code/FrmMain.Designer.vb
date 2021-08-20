<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.BtnOpen = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.LblLeft = New System.Windows.Forms.Label()
        Me.LblMid = New System.Windows.Forms.Label()
        Me.LblRight = New System.Windows.Forms.Label()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.btnGood = New System.Windows.Forms.Button()
        Me.btnSlow = New System.Windows.Forms.Button()
        Me.BtnShow = New System.Windows.Forms.Button()
        Me.BtnNext = New System.Windows.Forms.Button()
        Me.btnBad = New System.Windows.Forms.Button()
        Me.BtnLeft_Tested = New System.Windows.Forms.Button()
        Me.BtnMiddle_Tested = New System.Windows.Forms.Button()
        Me.BtnRight_Tested = New System.Windows.Forms.Button()
        Me.lblAbout = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BtnAlwaysShow = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'BtnOpen
        '
        Me.BtnOpen.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnOpen.Location = New System.Drawing.Point(6, 7)
        Me.BtnOpen.Name = "BtnOpen"
        Me.BtnOpen.Size = New System.Drawing.Size(87, 82)
        Me.BtnOpen.TabIndex = 0
        Me.BtnOpen.TabStop = False
        Me.BtnOpen.Text = "Open"
        Me.BtnOpen.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Text files|*.txt|All files|*.*"
        Me.OpenFileDialog1.InitialDirectory = "Application.StartupPath"
        '
        'LblLeft
        '
        Me.LblLeft.BackColor = System.Drawing.Color.Black
        Me.LblLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblLeft.Font = New System.Drawing.Font("SimSun", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LblLeft.ForeColor = System.Drawing.Color.White
        Me.LblLeft.Location = New System.Drawing.Point(7, 110)
        Me.LblLeft.Name = "LblLeft"
        Me.LblLeft.Size = New System.Drawing.Size(281, 178)
        Me.LblLeft.TabIndex = 4
        Me.LblLeft.Text = "! Test !"
        Me.LblLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblMid
        '
        Me.LblMid.BackColor = System.Drawing.Color.Black
        Me.LblMid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMid.Font = New System.Drawing.Font("SimSun", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LblMid.ForeColor = System.Drawing.Color.White
        Me.LblMid.Location = New System.Drawing.Point(310, 110)
        Me.LblMid.Name = "LblMid"
        Me.LblMid.Size = New System.Drawing.Size(281, 178)
        Me.LblMid.TabIndex = 5
        Me.LblMid.Text = "! Test !"
        Me.LblMid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblRight
        '
        Me.LblRight.BackColor = System.Drawing.Color.Black
        Me.LblRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblRight.Font = New System.Drawing.Font("SimSun", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.LblRight.ForeColor = System.Drawing.Color.White
        Me.LblRight.Location = New System.Drawing.Point(609, 110)
        Me.LblRight.Name = "LblRight"
        Me.LblRight.Size = New System.Drawing.Size(281, 178)
        Me.LblRight.TabIndex = 6
        Me.LblRight.Text = "! Test !"
        Me.LblRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnBack
        '
        Me.BtnBack.Font = New System.Drawing.Font("SimSun", 14.0!)
        Me.BtnBack.Location = New System.Drawing.Point(7, 309)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(85, 85)
        Me.BtnBack.TabIndex = 7
        Me.BtnBack.TabStop = False
        Me.BtnBack.Text = "Force Back" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "([)"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'btnGood
        '
        Me.btnGood.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnGood.Font = New System.Drawing.Font("SimSun", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnGood.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnGood.Location = New System.Drawing.Point(199, 308)
        Me.btnGood.Name = "btnGood"
        Me.btnGood.Size = New System.Drawing.Size(163, 85)
        Me.btnGood.TabIndex = 9
        Me.btnGood.TabStop = False
        Me.btnGood.Text = "Good" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(S)"
        Me.btnGood.UseVisualStyleBackColor = False
        '
        'btnSlow
        '
        Me.btnSlow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnSlow.Font = New System.Drawing.Font("SimSun", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnSlow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnSlow.Location = New System.Drawing.Point(368, 308)
        Me.btnSlow.Name = "btnSlow"
        Me.btnSlow.Size = New System.Drawing.Size(163, 85)
        Me.btnSlow.TabIndex = 10
        Me.btnSlow.TabStop = False
        Me.btnSlow.Text = "Slow" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(D)"
        Me.btnSlow.UseVisualStyleBackColor = False
        '
        'BtnShow
        '
        Me.BtnShow.Font = New System.Drawing.Font("SimSun", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnShow.Location = New System.Drawing.Point(195, 301)
        Me.BtnShow.Name = "BtnShow"
        Me.BtnShow.Size = New System.Drawing.Size(514, 96)
        Me.BtnShow.TabIndex = 8
        Me.BtnShow.TabStop = False
        Me.BtnShow.Text = "Show" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(A/Space)"
        Me.BtnShow.UseVisualStyleBackColor = True
        '
        'BtnNext
        '
        Me.BtnNext.Font = New System.Drawing.Font("SimSun", 14.0!)
        Me.BtnNext.Location = New System.Drawing.Point(809, 309)
        Me.BtnNext.Name = "BtnNext"
        Me.BtnNext.Size = New System.Drawing.Size(85, 85)
        Me.BtnNext.TabIndex = 12
        Me.BtnNext.TabStop = False
        Me.BtnNext.Text = "Force Next" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(])"
        Me.BtnNext.UseVisualStyleBackColor = True
        '
        'btnBad
        '
        Me.btnBad.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnBad.Font = New System.Drawing.Font("SimSun", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.btnBad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnBad.Location = New System.Drawing.Point(537, 308)
        Me.btnBad.Name = "btnBad"
        Me.btnBad.Size = New System.Drawing.Size(163, 85)
        Me.btnBad.TabIndex = 11
        Me.btnBad.TabStop = False
        Me.btnBad.Text = "Bad" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(F)"
        Me.btnBad.UseVisualStyleBackColor = False
        '
        'BtnLeft_Tested
        '
        Me.BtnLeft_Tested.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BtnLeft_Tested.Enabled = False
        Me.BtnLeft_Tested.Font = New System.Drawing.Font("SimSun", 8.0!)
        Me.BtnLeft_Tested.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnLeft_Tested.Location = New System.Drawing.Point(199, 95)
        Me.BtnLeft_Tested.Name = "BtnLeft_Tested"
        Me.BtnLeft_Tested.Size = New System.Drawing.Size(77, 29)
        Me.BtnLeft_Tested.TabIndex = 1
        Me.BtnLeft_Tested.TabStop = False
        Me.BtnLeft_Tested.Text = "TEST"
        Me.BtnLeft_Tested.UseVisualStyleBackColor = False
        '
        'BtnMiddle_Tested
        '
        Me.BtnMiddle_Tested.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BtnMiddle_Tested.Enabled = False
        Me.BtnMiddle_Tested.Font = New System.Drawing.Font("SimSun", 8.0!)
        Me.BtnMiddle_Tested.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnMiddle_Tested.Location = New System.Drawing.Point(503, 95)
        Me.BtnMiddle_Tested.Name = "BtnMiddle_Tested"
        Me.BtnMiddle_Tested.Size = New System.Drawing.Size(77, 29)
        Me.BtnMiddle_Tested.TabIndex = 2
        Me.BtnMiddle_Tested.TabStop = False
        Me.BtnMiddle_Tested.Text = "TEST"
        Me.BtnMiddle_Tested.UseVisualStyleBackColor = False
        '
        'BtnRight_Tested
        '
        Me.BtnRight_Tested.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.BtnRight_Tested.Enabled = False
        Me.BtnRight_Tested.Font = New System.Drawing.Font("SimSun", 8.0!)
        Me.BtnRight_Tested.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnRight_Tested.Location = New System.Drawing.Point(801, 95)
        Me.BtnRight_Tested.Name = "BtnRight_Tested"
        Me.BtnRight_Tested.Size = New System.Drawing.Size(77, 29)
        Me.BtnRight_Tested.TabIndex = 3
        Me.BtnRight_Tested.TabStop = False
        Me.BtnRight_Tested.Text = "TEST"
        Me.BtnRight_Tested.UseVisualStyleBackColor = False
        '
        'lblAbout
        '
        Me.lblAbout.BackColor = System.Drawing.Color.Transparent
        Me.lblAbout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAbout.Font = New System.Drawing.Font("SimSun", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblAbout.Location = New System.Drawing.Point(872, 4)
        Me.lblAbout.Name = "lblAbout"
        Me.lblAbout.Size = New System.Drawing.Size(27, 26)
        Me.lblAbout.TabIndex = 13
        Me.lblAbout.Text = "@"
        Me.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 30
        '
        'BtnAlwaysShow
        '
        Me.BtnAlwaysShow.Font = New System.Drawing.Font("SimSun", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.BtnAlwaysShow.Location = New System.Drawing.Point(124, 21)
        Me.BtnAlwaysShow.Name = "BtnAlwaysShow"
        Me.BtnAlwaysShow.Size = New System.Drawing.Size(171, 34)
        Me.BtnAlwaysShow.TabIndex = 14
        Me.BtnAlwaysShow.TabStop = False
        Me.BtnAlwaysShow.Text = "Always Show"
        Me.BtnAlwaysShow.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(901, 399)
        Me.Controls.Add(Me.BtnAlwaysShow)
        Me.Controls.Add(Me.BtnShow)
        Me.Controls.Add(Me.lblAbout)
        Me.Controls.Add(Me.BtnRight_Tested)
        Me.Controls.Add(Me.BtnMiddle_Tested)
        Me.Controls.Add(Me.BtnLeft_Tested)
        Me.Controls.Add(Me.BtnNext)
        Me.Controls.Add(Me.btnBad)
        Me.Controls.Add(Me.btnSlow)
        Me.Controls.Add(Me.btnGood)
        Me.Controls.Add(Me.BtnBack)
        Me.Controls.Add(Me.LblRight)
        Me.Controls.Add(Me.LblMid)
        Me.Controls.Add(Me.LblLeft)
        Me.Controls.Add(Me.BtnOpen)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmMain"
        Me.Text = "Darren's Flashcards (2019)"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BtnOpen As Button
    Friend WithEvents LblLeft As Label
    Friend WithEvents LblMid As Label
    Friend WithEvents LblRight As Label
    Friend WithEvents BtnBack As Button
    Friend WithEvents btnGood As Button
    Friend WithEvents btnSlow As Button
    Friend WithEvents BtnShow As Button
    Friend WithEvents BtnNext As Button
    Friend WithEvents btnBad As Button
    Friend WithEvents BtnLeft_Tested As Button
    Friend WithEvents BtnMiddle_Tested As Button
    Friend WithEvents BtnRight_Tested As Button
    Friend WithEvents lblAbout As Label
    Friend WithEvents BtnAlwaysShow As Button
End Class
