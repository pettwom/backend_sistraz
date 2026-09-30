using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace backend_trazabilidad.Converters
{
    public sealed class OctanoDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException("Se esperaba una fecha como texto.");

            var texto = reader.GetString() ?? "";

            var coincidencia = Regex.Match(
                texto,
                @"^/Date\((?<ms>-?\d+)(?:[+-]\d{4})?\)/$");

            if (coincidencia.Success &&
                long.TryParse(
                    coincidencia.Groups["ms"].Value,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out var milisegundos))
            {
                return DateTimeOffset
                    .FromUnixTimeMilliseconds(milisegundos)
                    .UtcDateTime;
            }

            // También acepta fechas ISO.
            if (reader.TryGetDateTime(out var fecha))
                return fecha;

            throw new JsonException($"Fecha de Octano no válida: {texto}");
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}