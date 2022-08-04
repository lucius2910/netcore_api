using Business.Core.Extensions;
using Business.Core.Interfaces;
using Framework.Core.Abstractions;
using Framework.Core.Collections;
using Framework.Core.Helpers;

namespace Seisankanri.Api.Extensions
{
    public static class ResponseExtensions
    {
        /// <summary>
        /// Converts the specified source to <see cref="IResponse{T}"/> from <see cref="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="cancellationToken">
        /// <param name="ls"></param>
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>

        /// <returns>An instance of the inherited from <see cref="IResponse{T}"/> interface.</returns>
        public static IResponse<T> ToResponse<T>(this T source, ILocalizeServices ls, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var data = source;
            if (data != null)
                return new BaseResponse<T>(data, ResponseCode.Success, ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.I_005) );
            else
                return new BaseResponse<T>(data, ResponseCode.NotFound, ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_005));
        }

        /// <summary>
        /// Converts the specified source to <see cref="IResponse{T}"/> from <see cref="Task{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="cancellationToken">
        /// <param name="ls"></param>
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>An instance of the inherited from <see cref="IResponse{T}"/> interface.</returns>
        public static async Task<IResponse<T>> ToResponse<T>(this Task<T> source, ILocalizeServices ls, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var data = await source;
            if (data != null)
                return new BaseResponse<T>(data, ResponseCode.Success, ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001));
            else
                return new BaseResponse<T>(data, ResponseCode.NotFound, ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_005));
        }

        /// <summary>
        /// Converts the specified source to <see cref="IResponse{T}"/> .
        /// </summary>
        /// <typeparam name="T">The type of the source.</typeparam>
        /// <param name="source">The source to paging.</param>
        /// <param name="cancellationToken">
        /// <param name="ls"></param>
        ///     A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>An instance of the inherited from <see cref="IResponse{T}"/> interface.</returns>
        public static async Task<IResponse<T>> ToResponse<T>(this IQueryable<T> source, ILocalizeServices ls, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            var data = source.FirstOrDefault();
            if (data != null)
                return new BaseResponse<T>(data, ResponseCode.Success, ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_001));
            else
                return new BaseResponse<T>(data, ResponseCode.NotFound, ls.Get(Modules.CORE, Screen.MESSAGE, MessageKey.E_005));
        }
    }
}
