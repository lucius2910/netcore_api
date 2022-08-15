﻿namespace Application.Core.Interfaces
{
    public interface ILogServices
    {
        Task WriteLogAction(string path, string method, string ip, string body = null);
        Task WriteLogException(string function, string message, string stackTrace);
    }
}
