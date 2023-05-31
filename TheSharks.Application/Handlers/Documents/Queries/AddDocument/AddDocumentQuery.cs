using MediatR;
using Microsoft.AspNetCore.Http;
using TheSharks.Contracts.Models.Common;

namespace TheSharks.Application.Handlers.Documents.Queries.AddDocument;

public class AddDocumentQuery : IRequest<BaseIdModel>
{
    public string Name { get; set; }
    public bool IsImportant { get; set; }
    public IFormFile File { get; set; }
}