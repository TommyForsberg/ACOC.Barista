using ACOC.Barista.Contracts.http;
using ACOC.Barista.Models;
using AutoMapper;

internal class CustomerMappingProfile: Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<LifeCycleEventDTO, LifeCycleEvent>();

        CreateMap<ProductTemplateDTO, ProductTemplate>();
    

        
    }
}