Imports UtangQBO
Public Interface IWallet
    Inherits ICrud(Of Wallet)
    Function UpdateWalletBalance(ByVal UserID As Integer, ByVal Amount As Decimal, ByVal OperationFlag As String) As Integer
End Interface

