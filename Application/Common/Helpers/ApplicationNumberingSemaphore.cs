namespace Core.Common.Helpers;

public static class ApplicationNumberingSemaphore
{
    public static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
}
