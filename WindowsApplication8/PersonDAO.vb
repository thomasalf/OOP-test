Imports MySql.Data.MySqlClient
Imports System.Configuration



Public Class PersonDAO


    Public Function query(sql As String) As DataTable
        Dim data As New DataTable
        Dim conn As MySqlConnection = New MySqlConnection
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("mysql").ConnectionString
        Try
            conn.Open()
            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = New MySqlCommand(sql, conn)
            adapter.Fill(data)
            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Feil ved oppkobling til database: " & ex.Message)
        Finally
            conn.Dispose()
        End Try
        Return data
    End Function



    Public Function kundedataSQL(inndata As Kunde)
        Dim sql As String = "INSERT INTO pdk_kunde SET kfornavn = '" _
                             & inndata.getFornavn() & "', ketternavn = '" _
                             & inndata.getEtternavn() & "', kadresse = '" _
                             & inndata.getGateadresse() & ", " & inndata.getPostnummer() & "', kepost = '" _
                             & inndata.getEpost() & "', ktelefon = '" _
                             & inndata.getTelefon() & "';"
        Return sql
    End Function


End Class
