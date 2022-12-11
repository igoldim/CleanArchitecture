using Application.Abstractions.Messaging;
using Domain.Exceptions;
using Dapper;
using System.Data;

namespace Application.Webinars.Queries.GetWebinarById
{
    internal sealed class GetWebinarQueryHandler: IQueryHandler<GetWebinarByIdQuery, WebinarResponse>
    {
        private readonly IDbConnection _dbConnection;

        public GetWebinarQueryHandler(IDbConnection dbConnection) => _dbConnection = dbConnection;

        public async Task<WebinarResponse> Handle(
            GetWebinarByIdQuery request, 
            CancellationToken cancellationToken)
        {
            const string sql = @"SELECT * FROM ""Webinars"" WHERE ""Id"" = @WebinarId";

            var webinar = await _dbConnection.QueryFirstOrDefaultAsync<WebinarResponse>(
                sql,
                new { request.WebinarId });

            if (webinar is null)
            {
                throw new WebinarNotFoundExcption(request.WebinarId);
            }

            return webinar;
        }
    }
}
