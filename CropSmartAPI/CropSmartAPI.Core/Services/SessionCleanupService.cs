using CropSmartAPI.Core.SessionObjects;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CropSmartAPI.Core.Services;

public class SessionCleanupService : IHostedService, IDisposable
{
    private readonly ISessionList _sessionList;
    private readonly System.Timers.Timer _timer;
    private readonly int _sessionLifetimeMinutes;
    private readonly int _checkIntervalMinutes;

    public SessionCleanupService(ISessionList sessionList, int sessionLifetimeMinutes, int checkIntervalMinutes)
    {
        this._sessionList = sessionList;
        this._sessionLifetimeMinutes = sessionLifetimeMinutes;
        this._checkIntervalMinutes = checkIntervalMinutes;

        //Initialize timer
        _timer = new System.Timers.Timer(_checkIntervalMinutes * 60 * 1000); // to miliseconds
        _timer.Elapsed += TimerElapsed;
        _timer.Start();
    }

    public void Dispose()
    {
        _timer.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer.Stop();
        return Task.CompletedTask;
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        var result = RemoveOvertimeSessions(_sessionLifetimeMinutes);
    }

    private Task<bool> RemoveOvertimeSessions(int sessionLifetimeMinutes)
    {
        DateTime currentTime = DateTime.Now;
        var sessionsToRemove = _sessionList.Sessions.Where(session =>
           (currentTime - session.LastOperationTime).TotalMinutes > sessionLifetimeMinutes).ToList();

        foreach (var session in sessionsToRemove)
        {
            _sessionList.Sessions.Remove(session);
        }

        return Task.FromResult(sessionsToRemove.Count > 0);
    }
}
