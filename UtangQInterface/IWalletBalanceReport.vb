Imports UtangQBO

Public Interface IWalletBalanceReport
    Function GetWalletBalanceReportById(ByVal id As Integer) As List(Of WalletBalanceReport)
End Interface
