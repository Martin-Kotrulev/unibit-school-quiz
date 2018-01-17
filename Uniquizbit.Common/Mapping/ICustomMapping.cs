namespace Uniquizbit.Common.Mapping
{
    using AutoMapper;

    public interface ICustomMapping
    {
        void ConfigureMapping(Profile mapper);
    }
}