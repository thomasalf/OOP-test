Imports MySql.Data.MySqlClient
Imports System.Configuration



Public Class SykkelDAO

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



    Public Function lagreNySykkeldataSQL(inndata As Sykkel)
        Dim sql As String = "INSERT IGNORE INTO pdk_sykkelmerke SET merke = '" _
                            & inndata.getSykkelMerke() & "'; INSERT IGNORE INTO pdk_status SET statusID = '" _
                            & inndata.getSykkelStatus() & "';INSERT IGNORE INTO pdk_sykkelmodell SET merke = '" _
                            & inndata.getSykkelMerke() & "', modell = '" _
                            & inndata.getSykkelModell() & "'; INSERT INTO pdk_sykkel SET merke = '" _
                             & inndata.getSykkelMerke() & "', modell = '" _
                             & inndata.getSykkelModell() & "', sykkeltype = '" _
                             & inndata.getSykkelType() & "', postnr = '" _
                             & inndata.getSykkelHjemsted() & "', statusID = '" _
                             & inndata.getSykkelStatus() & "',transportorID = '" _
                             & inndata.getSykkelTransportor() & "';"
        Return sql
    End Function

    Public Function slettSykkeldataSQL(sykkelID As Integer)
        Dim sql As String = "DELETE FROM pdk_sykkel " _
                                & "WHERE sykkelID = '" & sykkelID & "';"
        Return sql
    End Function

    Public Function velgAlleSykler()
        Dim sql As String = "SELECT * FROM pdk_sykkel;"
        Return sql
    End Function

    Public Function endreSykkeldataSQL(inndata As Sykkel, sykkelIDlabel As Integer)
        Dim sql As String = "UPDATE pdk_sykkel " _
                                & "SET merke = '" & inndata.getSykkelMerke() _
                                & "', modell = '" & inndata.getSykkelModell() _
                                & "', sykkeltype = '" & inndata.getSykkelType() _
                                & "' WHERE sykkelID = '" & sykkelIDlabel & "';"
        Return sql
    End Function





End Class


