Public Class Kunde
    Inherits Person

    Private rabatt As String = "0"






    Private stilling As String
    Private lønnstrinn As Integer

    Private Const MIN As Integer = 1
    Private Const MAX As Integer = 99

    'Get- og Set-funksjoner
    Public Property Stillingen() As String
        Get
            Return stilling
        End Get
        Set(ByVal value As String)
            value = stilling
        End Set
    End Property


    Public Property Lønnstrinnet() As Integer
        Get
            Return lønnstrinn
        End Get
        Set(ByVal value As Integer)
            If value < MIN Then
                lønnstrinn = MIN
            ElseIf value > MAX Then
                lønnstrinn = MAX
            Else
                lønnstrinn = value
            End If
        End Set
    End Property

    'Konstruktører/Constructors
    Public Sub New(ByVal etFornavn As String, _
                   etEtternavn As String, _
                   enStilling As String, _
                   etLønnstrinn As Integer)
        MyBase.New(etFornavn, etEtternavn)
        stilling = enStilling
        lønnstrinn = etLønnstrinn
    End Sub



    Public Sub New(ByVal etFornavn As String, _
           etEtternavn As String, _
           enFødselsdato As Date, _
           enTelefon As String, _
           enAdresse As String, _
           enEpost As String, _
           enStilling As String, _
           etLønnstrinn As Integer)
        MyBase.New(etFornavn, etEtternavn, enFødselsdato, enTelefon, enAdresse, enEpost)
        stilling = enStilling
        lønnstrinn = etLønnstrinn
    End Sub

    Public Sub New(ByVal etFornavn As String, _
           etEtternavn As String, _
           etLønnstrinn As Integer)
        MyBase.New(etFornavn, etEtternavn)
        lønnstrinn = etLønnstrinn
    End Sub

    'ToString()-funksjon
    Public Overrides Function ToString() As String
        Return MyBase.ToString() & " Stilling: " & stilling & " Lønnstrinn: " & lønnstrinn
    End Function


End Class

