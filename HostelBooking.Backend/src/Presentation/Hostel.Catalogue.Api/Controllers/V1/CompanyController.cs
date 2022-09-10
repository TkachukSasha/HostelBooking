using Hostel.Catalogue.Application.Commands.Companies.Create;
using Hostel.Catalogue.Application.Commands.Companies.Delete;
using Hostel.Catalogue.Application.Commands.Companies.Update;
using Hostel.Catalogue.Application.Dto.Company;
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
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICommandHandler<CreateCompany, CompanyReturnDto> _createCompanyHandler;
        private readonly ICommandHandler<UpdateCompany, CompanyReturnDto> _updateCompanyHandler;
        private readonly ICommandHandler<DeleteCompany, CompanyReturnDto> _deleteCompanyHandler;
        private readonly IQueryHandler<GetCompanies, IEnumerable<Company>> _getCompaniesHandler;
        private readonly IQueryHandler<GetCompany, Company> _getCompanyHandler;

        public CompanyController(ICommandHandler<CreateCompany, CompanyReturnDto> createCompanyHandle,
                              ICommandHandler<UpdateCompany, CompanyReturnDto> updateCompanyHandler,
                              ICommandHandler<DeleteCompany, CompanyReturnDto> deleteCompanyHandler,
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
        [Route(Routes.GetCompanies)]
        [HttpGet]
        //[Cached(600)]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies([FromQuery] GetCompanies query)
        {
            var companies = await _getCompaniesHandler.HandleAsync(query);
            return Ok(companies);
        }

        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route(Routes.GetCompanyById)]
        [HttpGet]
        public async Task<ActionResult<Company>> GetCompany([FromQuery] GetCompany query)
        {
            var company = await _getCompanyHandler.HandleAsync(new GetCompany { CompanyId = query.CompanyId });
            return Ok(company);
        }

        /// <summary>
        /// Create company
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route(Routes.CreateCompany)]
        [HttpPost]
        public async Task<ActionResult<CompanyReturnDto>> CreateCompany([FromBody] CreateCompany request, CancellationToken cancellationToken)
        {
            var company = await _createCompanyHandler.HandleAsync(request, cancellationToken);
            return Ok(company);
        }

        /// <summary>
        /// Update company
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route((Routes.UpdateCompany))]
        [HttpPut]
        public async Task<ActionResult<CompanyReturnDto>> UpdateCompany([FromBody] UpdateCompany request, CancellationToken cancellationToken)
        {
            var company = await _updateCompanyHandler.HandleAsync(request, cancellationToken);
            return Ok(company);
        }

        /// <summary>
        /// Delete company
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Route(Routes.DeleteCompany)]
        [HttpDelete]
        public async Task<ActionResult<CompanyReturnDto>> DeleteCompany([FromBody] DeleteCompany request, CancellationToken cancellationToken)
        {
            var company = await _deleteCompanyHandler.HandleAsync(request, cancellationToken);
            return Ok(company);
        }
    }
}
