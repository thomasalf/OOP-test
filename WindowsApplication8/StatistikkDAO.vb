Imports MySql.Data.MySqlClient
Imports System.Configuration

Public Class StatistikkDAO

    'Funksjon for å hente ut bestillinger
    Public Function visBestillingerSQL(utdata As String)
        Dim sql As String = "SELECT b.bookingID, b.uttid, b.inntid,b.kundeID, " _
        & "CONCAT( k.kfornavn,  ' ', k.ketternavn) AS kunde, b.betalt," _
        & "CONCAT( s.fornavn,  ' ', s.etternavn) AS selger FROM pdk_booking b," _
        & "pdk_kunde k, pdk_ansatt s WHERE b.kundeID = k.kundeID and b.selgerID = s.selgerID;"
        Return sql
    End Function

    'Funksjon for å hente prisnøkkel på sykkel
    Public Function visPrisnoklerSykkel(ByVal utdata As String)
        Dim sql As String = "SELECT prisID, prisprosent from pdk_prisnokkel"
        Return sql
    End Function

    'Funksjon for å hente ut priser på utstyr
    Public Function visPrisUtstyr(ByVal utdata As String)
        Dim sql As String = "SELECT  utstyrID, utstyrstype, dagspris from pdk_ekstrautstyr"
        Return sql
    End Function




End Class
