using System.Globalization;
namespace CrudTestMicroservice.WebApi.Common.Mappings;

public class GeneralMapping : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
    {
        config.NewConfig<string, decimal>()
            .MapWith(src => decimal.Parse(src));
        
        config.NewConfig<decimal, string>()
            .MapWith(src => src.ToString("R", new CultureInfo("en-us")));

        config.NewConfig<decimal?, string>()
            .MapWith(src => src == null ? string.Empty : src.Value.ToString("R", new CultureInfo("en-us")));

        config.NewConfig<string, decimal?>()
            .MapWith(src => string.IsNullOrEmpty(src) ? null : decimal.Parse(src));
        
        config.NewConfig<Guid, string>()
            .MapWith(src => src == Guid.Empty ? string.Empty : src.ToString());
        
        config.NewConfig<string, Guid>()
            .MapWith(src => string.IsNullOrEmpty(src) ? Guid.Empty : Guid.Parse(src));
        
        config.NewConfig<string, Guid?>()
            .MapWith(src => string.IsNullOrEmpty(src) ? null : Guid.Parse(src));

        config.NewConfig<Timestamp, DateTime>()
            .MapWith(src => src.ToDateTime());

        config.NewConfig<Timestamp, DateTime?>()
            .MapWith(src => src == null ? null : src.ToDateTime());

        config.NewConfig<DateTime, Timestamp>()
            .MapWith(src => Timestamp.FromDateTime(DateTime.SpecifyKind(src, DateTimeKind.Utc)));

        config.NewConfig<DateTime?, Timestamp>()
           .MapWith(src => src.HasValue ? Timestamp.FromDateTime(DateTime.SpecifyKind(src.Value, DateTimeKind.Utc)) : null);

        config.NewConfig<Duration, TimeSpan>()
           .MapWith(src => src.ToTimeSpan());

        config.NewConfig<Duration, TimeSpan?>()
           .MapWith(src => src == null ? null : src.ToTimeSpan());

        config.NewConfig<TimeSpan, Duration>()
            .MapWith(src => Duration.FromTimeSpan(src));

        config.NewConfig<TimeSpan?, Duration>()
            .MapWith(src => src.HasValue ? Duration.FromTimeSpan(src.Value) : null);

        config.Default
            .UseDestinationValue(member => member.SetterModifier == AccessModifier.None &&
                                           member.Type.IsGenericType &&
                                           member.Type.GetGenericTypeDefinition() == typeof(Google.Protobuf.Collections.RepeatedField<>));

        config.NewConfig<Google.Protobuf.ByteString, byte[]>()
            .MapWith(src => src.ToByteArray());

        config.NewConfig<byte[], Google.Protobuf.ByteString>()
            .MapWith(src => Google.Protobuf.ByteString.CopyFrom(src));
    }
}
