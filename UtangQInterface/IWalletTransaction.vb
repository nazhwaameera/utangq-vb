Imports UtangQBO

Public Interface IWalletTransaction

    Function CreateWalletTransaction(obj As WalletTransacations) As Integer
    Function ReadWalletTransactions(ByVal id As Integer) As List(Of WalletTransacations)

End Interface
