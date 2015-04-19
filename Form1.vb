Public Class Form1

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        TabControl1.SelectTab(7)
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        TabControl1.SelectTab(1)
    End Sub

  
    Private Sub Side3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Side3ToolStripMenuItem.Click
        TabControl1.SelectTab(2)
    End Sub

    Private Sub SøkMedlemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SøkMedlemToolStripMenuItem.Click
        MsgBox("Fant ingen registrerte Medlemmer med navn: " & ToolStripTextBox1.Text, MsgBoxStyle.Information, "Medlemsregister")
    End Sub


   
    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        TabControl1.SelectTab(3)
    End Sub

   

    Private Sub UtstyrToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UtstyrToolStripMenuItem.Click
        TabControl1.SelectTab(4)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectTab(5)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.SelectTab(6)
    End Sub

    Private Sub LoginToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoginToolStripMenuItem.Click
        TabControl1.SelectTab(0)
    End Sub
End Class
