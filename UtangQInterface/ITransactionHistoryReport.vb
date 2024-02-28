Imports UtangQBO

Public Interface ITransactionHistoryReport
    Function GetTransactionHistoryReportById(ByVal id As Integer) As List(Of TransactionsHistoryReport)
End Interface
