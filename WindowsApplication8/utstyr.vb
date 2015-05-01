Public Class utstyr
    Private utId As String = "" 'Idnr til utstyret
    Private utType As String = "" 'Beskrivelse av type utstyr
    Private utPris As String = "" 'Dagspris utleie av utstyret
    Private utAntall As String = "" 'Antall innkjøpte

    'tester property
    Public Property utstyrId() As String
        Get
            Return utId
        End Get
        Set(ByVal value As String)
            utId = value
        End Set
    End Property

    Public Property utstyrType() As String
        Get
            Return utType
        End Get
        Set(ByVal value As String)
            utType = value
        End Set
    End Property

    Public Property utstyrPris() As String
        Get
            Return utPris
        End Get
        Set(ByVal value As String)
            utPris = value
        End Set
    End Property

    Public Property utstyrAntall() As String
        Get
            Return utAntall
        End Get
        Set(ByVal value As String)
            utAntall = value
        End Set
    End Property



    'Her er en Constructor for å registrere nytt utstyr
    'Public Sub New(ByVal utId As String, _
    'ByVal utType As String)
    '   utstyrsId = utId
    '  utstyrsType = utType
    'End Sub


End Class
