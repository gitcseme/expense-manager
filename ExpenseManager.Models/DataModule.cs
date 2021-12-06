using ExpenseManager.Models.Contexts;
using ExpenseManager.Models.Repositories;
using ExpenseManager.Models.Services;
using ExpenseManager.Models.UnitOfWorks;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Models.Seeds;

namespace ExpenseManager.Models
{
    public class DataModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public DataModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>()
                .As<ICategoryRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseRepository>()
                .As<IExpenseRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IncomeRepository>()
                .As<IIncomeRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>()
                .As<ICompanyRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryUnitOfWork>()
                .As<ICategoryUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<IncomeUnitOfWork>()
                .As<IIncomeUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyUnitOfWork>()
                .As<ICompanyUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseUnitOfWork>()
                .As<IExpenseUnitOfWork>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryService>()
                .As<ICategoryService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<IncomeService>()
                .As<IIncomeService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>()
                .As<ICompanyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseService>()
                .As<IExpenseService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DataContextSeed>().SingleInstance();

            base.Load(builder);
        }
    }
}
