﻿using ProductManagement.Localization;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace ProductManagement
{
    /* This module directly depends on EF Core by its design.
     * In this way, we can directly use EF Core async LINQ extension methods.
     */
    [DependsOn(
        typeof(ProductManagementDomainSharedModule),
        typeof(AbpEntityFrameworkCoreModule) 
        )]
    public class ProductManagementDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<VirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<ProductManagementDomainModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources.Get<ProductManagementResource>().AddVirtualJson("/MyCompanyName/ProductManagement/Localization/Domain");
            });

            Configure<ExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("ProductManagement", typeof(ProductManagementResource));
            });
        }
    }
}
