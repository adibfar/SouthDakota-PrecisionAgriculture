using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PAS.Application.Extensions;
using PAS.Application.Interfaces.Services;
using PAS.Application.Responses.Audit;
using PAS.Infrastructure.Contexts;
using PAS.Infrastructure.Models.Audit;
using PAS.Infrastructure.Specifications;
using PAS.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PAS.Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        private readonly PasContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;

        public AuditService(
            IMapper mapper,
            PasContext context,
            IExcelService excelService)
        {
            _mapper = mapper;
            _context = context;
            _excelService = excelService;
        }

        public async Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId)
        {
            var trails = await _context.AuditTrails.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
            var mappedLogs = _mapper.Map<List<AuditResponse>>(trails);
            return await Result<IEnumerable<AuditResponse>>.SuccessAsync(mappedLogs);
        }

        public async Task<IResult<string>> ExportToExcelAsync(string userId, string searchString = "", bool searchInOldValues = false, bool searchInNewValues = false)
        {
            var auditSpec = new AuditFilterSpecification(userId, searchString, searchInOldValues, searchInNewValues);
            var trails = await _context.AuditTrails
                .Specify(auditSpec)
                .OrderByDescending(a => a.DateTime)
                .ToListAsync();
            var data = await _excelService.ExportAsync(trails, sheetName: "Audit trails",
                mappers: new Dictionary<string, Func<Audit, object>>
                {
                    { "Table Name", item => item.TableName },
                    { "Type", item => item.Type },
                    { "Date Time (Local)", item => DateTime.SpecifyKind(item.DateTime, DateTimeKind.Utc).ToLocalTime().ToString("G", CultureInfo.CurrentCulture) },
                    { "Date Time (UTC)", item => item.DateTime.ToString("G", CultureInfo.CurrentCulture) },
                    { "Primary Key", item => item.PrimaryKey },
                    { "Old Values", item => item.OldValues },
                    { "New Values", item => item.NewValues },
                });

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}