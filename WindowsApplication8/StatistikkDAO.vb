Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class StatistikkDAO

    Public Function visBestillingerSQL(utdata As String)
        Dim sql As String = "SELECT b.bookingID, b.uttid, b.inntid,b.kundeID, " _
        & "CONCAT( k.kfornavn,  ' ', k.ketternavn) AS kunde, b.betalt," _
        & "CONCAT( s.fornavn,  ' ', s.etternavn) AS selger FROM pdk_booking b," _
        & "pdk_kunde k, pdk_ansatt s WHERE b.kundeID = k.kundeID and b.selgerID = s.selgerID;"
        Return sql
    End Function

End Class
