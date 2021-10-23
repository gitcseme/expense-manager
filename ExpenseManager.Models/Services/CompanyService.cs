using System.Threading.Tasks;
using ExpenseManager.Models.Entities;
using ExpenseManager.Models.UnitOfWorks;

namespace ExpenseManager.Models.Services
{
	public class CompanyService : ICompanyService
	{
		private ICompanyUnitOfWork _companyUnitOfWork { get; }
		public CompanyService(ICompanyUnitOfWork companyUnitOfWork)
		{
			_companyUnitOfWork = companyUnitOfWork;
		}

    	public async Task<Company> CreateAsync(Company entity)
		{
			await _companyUnitOfWork.CompanyRepository.AddAsync(entity);
			await _companyUnitOfWork.SaveAsync();
			return entity;
		}

		public async Task DeleteAsync(Company entity)
		{
			await _companyUnitOfWork.CompanyRepository.RemoveAsync(entity);
			await _companyUnitOfWork.SaveAsync();
		}

		public void Dispose()
		{
			_companyUnitOfWork.Dispose();
		}

		public async Task EditAsync(Company entity)
		{
			await _companyUnitOfWork.CompanyRepository.EditAsync(entity);
			await _companyUnitOfWork.SaveAsync();
		}

		public Company Get(int Id)
		{
			return _companyUnitOfWork.CompanyRepository.Get(Id);
		}

		public async Task<Company> GetAsync(int Id)
		{
			return await _companyUnitOfWork.CompanyRepository.GetAsync(Id);
		}
	}
}