namespace FSR.NET;

public enum FSRVkPresentModeKHR : int
{
    ImmediateKhr = 0,

    MailboxKhr = 1,

    FifoKhr = 2,

    FifoRelaxedKhr = 3,

    SharedDemandRefreshKhr = 1000111000,

    SharedContinuousRefreshKhr = 1000111001,

    FifoLatestReadyKhr = 1000361000,

    FifoLatestReadyExt = 1000361000,

    MaxEnumKhr = 2147483647,
}
