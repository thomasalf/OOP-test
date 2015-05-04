Imports MySql.Data.MySqlClient
Imports System.Configuration



Public Class PersonDAO

    'Funksjon for kobling til database
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



    Public Function lagreKundedataSQL(inndata As Kunde)
        Dim sql As String = "INSERT INTO pdk_kunde SET kfornavn = '" _
                             & inndata.getFornavn() & "', ketternavn = '" _
                             & inndata.getEtternavn() & "', kadresse = '" _
                             & inndata.getGateadresse() & "', kepost = '" _
                             & inndata.getEpost() & "', ktelefon = '" _
                             & inndata.getTelefon() & "';"
        Return sql
    End Function

    Public Function endreKundedataSQL(inndata As Kunde, kundeIDlabel As Integer)
        Dim sql As String = "UPDATE pdk_kunde " _
                                & "SET kfornavn = '" & inndata.getFornavn() _
                                & "', ketternavn = '" & inndata.getEtternavn() _
                                & "', kadresse = '" & inndata.getGateadresse() _
                                & "', kepost = '" & inndata.getEpost() _
                                & "', ktelefon = '" & inndata.getTelefon() _
                                & "' WHERE kundeID = '" & kundeIDlabel & "';"
        Return sql
    End Function

    Public Function slettKundedataSQL(kundeIDlabel As Integer)
        Dim sql As String = "DELETE FROM pdk_kunde " _
                                & "WHERE kundeID = '" & kundeIDlabel & "';"
        Return sql
    End Function

    Public Function velgAlleKunder()
        Dim sql As String = "SELECT * FROM pdk_kunde;"
        Return sql
    End Function

    Public Function velgAlleAnsatte()
        Dim sql As String = "SELECT * FROM pdk_ansatt;"
        Return sql
    End Function


End Class


