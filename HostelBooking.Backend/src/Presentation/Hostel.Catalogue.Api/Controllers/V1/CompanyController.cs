using Hostel.Catalogue.Application.Commands.Companies.Create;
using Hostel.Catalogue.Application.Commands.Companies.Delete;
using Hostel.Catalogue.Application.Commands.Companies.Update;
using Hostel.Catalogue.Application.Queries.Companies.GetCompanyById;
using Hostel.Catalogue.Application.Queries.Companies.GetListCompanies;
using Hostel.Catalogue.Domain.Entities;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Const;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hostel.Catalogue.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICommandHandler<CreateCompany> _createCompanyHandler;
        private readonly ICommandHandler<UpdateCompany> _updateCompanyHandler;
        private readonly ICommandHandler<DeleteCompany> _deleteCompanyHandler;
        private readonly IQueryHandler<GetCompanies, IEnumerable<Company>> _getCompaniesHandler;
        private readonly IQueryHandler<GetCompany, Company> _getCompanyHandler;

        public CompanyController(ICommandHandler<CreateCompany> createCompanyHandle,
                              ICommandHandler<UpdateCompany> updateCompanyHandler,
                              ICommandHandler<DeleteCompany> deleteCompanyHandler,
                              IQueryHandler<GetCompanies, IEnumerable<Company>> getCompaniesHandler,
                              IQueryHandler<GetCompany, Company> getCompanyHandler)
        {
            _createCompanyHandler = createCompanyHandle;
            _updateCompanyHandler = updateCompanyHandler;
            _deleteCompanyHandler = deleteCompanyHandler;
            _getCompaniesHandler = getCompaniesHandler;
            _getCompanyHandler = getCompanyHandler;
        }

        /// <summary>
        /// Get all available companies
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(Routes.GetCompanies)]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies([FromQuery] GetCompanies query)
        {
            var rooms = _getCompaniesHandler.HandleAsync(query);
            return Ok(rooms);
        }

        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(Routes.GetRoomById)]
        public async Task<ActionResult<Company>> GetCompanyById([FromQuery] GetCompany query)
        {
            var room = _getCompanyHandler.HandleAsync(new GetCompany { CompanyId = query.CompanyId });
            return Ok(room);
        }

        /// <summary>
        /// Create company
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPost(Routes.CreateRoom)]
        public async Task<ActionResult> CreateCompany([FromBody] CreateCompany request, CancellationToken cancellationToken)
        {
            var room = _createCompanyHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }

        /// <summary>
        /// Update company
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpPut(Routes.UpdateRoom)]
        public async Task<ActionResult> UpdateCompany([FromBody] UpdateCompany request, CancellationToken cancellationToken)
        {
            var room = _updateCompanyHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }

        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [HttpDelete(Routes.DeleteRoom)]
        public async Task<ActionResult> DeleteCompany([FromBody] DeleteCompany request, CancellationToken cancellationToken)
        {
            var room = _deleteCompanyHandler.HandleAsync(request, cancellationToken);
            return Ok(room);
        }
    }
}
