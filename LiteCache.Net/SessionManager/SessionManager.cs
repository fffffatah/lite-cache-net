﻿using System.Collections.Concurrent;

namespace LiteCache.Net.SessionManager;

public class SessionManager(Configs configs) : ISessionManager
{
    private readonly Configs _configs = configs ?? throw new ArgumentNullException(nameof(configs));
    private readonly ConcurrentDictionary<string, DateTime> _sessions = new();

    public async Task<bool> Validate(string id)
    {
        return await Task.Run(() =>
        {
            var response = _sessions.TryGetValue(id, out var sessionTimeout);

            return response && DateTime.Now >= sessionTimeout;
        });
    }

    public async Task<string> Create()
    {
        var guid = Guid.NewGuid().ToString();
        
        var response = await Task.Run(() =>
            _sessions.TryAdd(guid, DateTime.Now.AddSeconds(_configs.SessionTimeout)));

        return response ? guid : String.Empty;
    }
}