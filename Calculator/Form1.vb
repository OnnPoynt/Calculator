Public Class Form1
    Dim assign_input As Double = 0
    Dim operation As String
    Dim found_expression As Boolean = False
    Dim firstnum, secondnum, q As String

    Private Sub Numbers_Click(sender As Object, e As EventArgs) Handles Btn9.Click, Btn8.Click, Btn7.Click, Btn6.Click, Btn5.Click, Btn4.Click, Btn3.Click, Btn2.Click, Btn1.Click, Btn0.Click, BtnPoint.Click
        Dim b As Button = sender
        If ((TxtDisplay.Text = "0") Or (found_expression)) Then
            TxtDisplay.Clear()
            TxtDisplay.Text = b.Text
            found_expression = False
        ElseIf (b.Text = ".") Then
            If (Not TxtDisplay.Text.Contains(".")) Then
                TxtDisplay.Text = TxtDisplay.Text + b.Text
            End If
        Else
            TxtDisplay.Text = TxtDisplay.Text + b.Text
        End If
    End Sub
End Class
