using AutoMapper;
using Business.Core.Extensions;
using Business.Core.Interfaces;
using Business.Logs.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Microsoft.Extensions.Configuration;
using Seisankanri.Model.Entities;

namespace Business.Logs.Services
{
    public class LogService : BaseServices, ILogService
    {
        private readonly IRepository<LogAction> logActionRepository;
        private readonly IRepository<LogException> logExceptionRepository;
        private readonly IServiceContext serviceContext;
        private readonly IUserServices userServices;
        private readonly ILocalizeServices ls;
        private readonly IConfiguration configuration;

        public LogService(IUnitOfWork unitOfWork, IMapper mapper, IServiceContext serviceContext, IUserServices userServices, ILocalizeServices ls, IConfiguration configuration) : base(unitOfWork, mapper)
        {
            logActionRepository = _unitOfWork.GetRepository<LogAction>();
            logExceptionRepository = _unitOfWork.GetRepository<LogException>();
            this.serviceContext = serviceContext;
            this.userServices = userServices;
            this.ls = ls;
            this.configuration = configuration;
        }

        public async Task WriteLogAction(string path, string method, string ip, string body = "")
        {
            string message = RenderMessageLog(method, path);

            await logActionRepository.AddEntityAsync(new LogAction
            {
                path = path,
                method = method,
                body = body,
                message = message,
                ip = ip
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task WriteLogException(string function, string message, string? stackTrace)
        {
            await logExceptionRepository.AddEntityAsync(new LogException
            {
                function = function,
                message = message,
                stack_trace = stackTrace
            });

            await _unitOfWork.SaveChangesAsync();
        }

        private string RenderMessageLog(string method, string path)
        {
            var userName = userServices.GetUserNameById((Guid)serviceContext._userId);
            var message = ls.Get(Modules.LOG, "Common", "MessageLog");
            string newMess = string.Format(message, userName, method, path);

            return newMess;
        }
    }
}
