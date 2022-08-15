﻿using Application.Core.Contracts;

namespace Application.Core.Interfaces
{
    public interface ICalendarService
    {
        Task<List<CalendarResponse>> GetByCompany(CalendarSearchRequest request);
        Task<int> Create(CalendarRequest request);
        Task<int> Update(Guid id, CalendarRequest request);
    }
}