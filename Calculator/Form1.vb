Public Class Form1
    Dim assign_input As Double = 0
    Dim operation As String
    Dim found_expression As Boolean = False
    Dim firstnum, secondnum, q As String

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.NumPad0
                Btn0.PerformClick()
            Case Keys.NumPad1
                Btn1.PerformClick()
            Case Keys.NumPad2
                Btn2.PerformClick()
            Case Keys.NumPad3
                Btn3.PerformClick()
            Case Keys.NumPad4
                Btn4.PerformClick()
            Case Keys.NumPad5
                Btn5.PerformClick()
            Case Keys.NumPad6
                Btn6.PerformClick()
            Case Keys.NumPad7
                Btn7.PerformClick()
            Case Keys.NumPad8
                Btn8.PerformClick()
            Case Keys.NumPad9
                Btn9.PerformClick()
            Case Keys.Decimal
                BtnPoint.PerformClick()
        End Select

        Select Case e.KeyCode
            Case Keys.Add
                BtnPlus.PerformClick()
            Case Keys.Subtract
                BtnMinus.PerformClick()
            Case Keys.Multiply
                BtnMultiply.PerformClick()
            Case Keys.Divide
                BtnEquals.PerformClick()
            Case Keys.Enter
                BtnDivide.PerformClick()
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub BtnCE_Click(sender As Object, e As EventArgs) Handles BtnCE.Click
        TxtDisplay.Text = "0"
        lblEquation.Text = ""
    End Sub

    Private Sub BtnC_Click(sender As Object, e As EventArgs) Handles BtnC.Click
        TxtDisplay.Text = "0"
    End Sub

    Private Sub Numbers_Click(sender As Object, e As EventArgs) Handles Btn9.Click, Btn8.Click, Btn7.Click, Btn6.Click, Btn5.Click, Btn4.Click, Btn3.Click, Btn2.Click, Btn1.Click, Btn0.Click, BtnPoint.Click
        Dim b As Button = sender
        Dim keyValue As String = b.Text

        If ((TxtDisplay.Text = "0") Or (found_expression)) Then
            TxtDisplay.Clear()
            TxtDisplay.Text = keyValue
            found_expression = False
        ElseIf (keyValue = ".") Then
            If (Not TxtDisplay.Text.Contains(".")) Then
                TxtDisplay.Text = TxtDisplay.Text + keyValue
            End If
        Else
            TxtDisplay.Text = TxtDisplay.Text + keyValue
        End If
    End Sub

    Private Sub operation_Click(sender As Object, e As EventArgs) Handles BtnEquals.Click, BtnMultiply.Click, BtnMinus.Click, BtnPlus.Click
        Dim b As Button = sender
        If (assign_input <> 0) Then
            BtnDivide.PerformClick()
            found_expression = True
            operation = b.Text
            lblEquation.Text = lblEquation.Text & "  " & assign_input & " " & operation
        Else
            operation = b.Text
            assign_input = Double.Parse(TxtDisplay.Text)
            found_expression = True
            lblEquation.Text = assign_input & "  " & operation
        End If
    End Sub

    Private Sub BtnEquals_Click(sender As Object, e As EventArgs) Handles BtnDivide.Click
        'lblEquation.Text = assign_input & operation & TxtDisplay.Text & " ="
        Select Case operation
            Case "+"
                TxtDisplay.Text = (assign_input + Double.Parse(TxtDisplay.Text)).ToString()
            Case "-"
                TxtDisplay.Text = (assign_input - Double.Parse(TxtDisplay.Text)).ToString()
            Case "×"
                TxtDisplay.Text = (assign_input * Double.Parse(TxtDisplay.Text)).ToString()
            Case "/"
                TxtDisplay.Text = (assign_input / Double.Parse(TxtDisplay.Text)).ToString()
        End Select
        assign_input = Double.Parse(TxtDisplay.Text)
        operation = ""
    End Sub

    Private Sub BtnPercent_Click(sender As Object, e As EventArgs) Handles BtnPercent.Click
        Dim a As Double
        a = Convert.ToDouble(TxtDisplay.Text) / Convert.ToDouble(100)
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub BtnSqrt_Click(sender As Object, e As EventArgs) Handles BtnSqrt.Click
        Dim a As Double
        lblEquation.Text = "√ (" & (TxtDisplay.Text) & ")"
        a = Math.Sqrt(TxtDisplay.Text)
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub BtnX2_Click(sender As Object, e As EventArgs) Handles BtnX2.Click
        Dim a As Double
        a = Convert.ToDouble(TxtDisplay.Text) * Convert.ToDouble(TxtDisplay.Text)
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub Btn1x_Click(sender As Object, e As EventArgs) Handles Btn1x.Click
        Dim a As Double
        a = Convert.ToDouble(1.0 / Convert.ToDouble(TxtDisplay.Text))
        TxtDisplay.Text = System.Convert.ToString(a)
    End Sub

    Private Sub BtnBack_Click(sender As Object, e As EventArgs) Handles BtnBack.Click
        'Backspace
        If TxtDisplay.Text.Length > 0 Then
            TxtDisplay.Text = TxtDisplay.Text.Remove(TxtDisplay.Text.Length - 1, 1)
        End If
        If (TxtDisplay.Text = "") Then
            TxtDisplay.Text = "0"
        End If
    End Sub

    Private Sub BtnPlusMinus_Click(sender As Object, e As EventArgs) Handles BtnPlusMinus.Click
        If (TxtDisplay.Text.Contains("-")) Then
            TxtDisplay.Text = TxtDisplay.Text.Remove(0, 1)
        Else
            TxtDisplay.Text = $"-{TxtDisplay.Text}"
        End If
    End Sub
End Class

