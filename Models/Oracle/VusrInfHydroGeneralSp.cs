namespace backend_trazabilidad.Models.Oracle;

// Vista ANH_HYD.VUSR_INF_HYDRO_GENERAL_SP. NUMBER(p,0) se lee como decimal
// para evitar desbordamientos; los campos sin precisión definida también.
public sealed class VusrInfHydroGeneralSp
{
    public decimal? IdEntidadPadre { get; set; }
    public decimal? IdUsuarioAsignado { get; set; }
    public string DenominacionPadre { get; set; } = null!;
    public decimal IdEntidad { get; set; }
    public decimal IdConsumidor { get; set; }
    public string Denominacion { get; set; } = null!;
    public string? Objeto { get; set; }
    public decimal? Telefonos { get; set; }
    public string? AmbitoOperacionPadre { get; set; }
    public string? AmbitoOperacion { get; set; }
    public decimal? IdTipoSociedad { get; set; }
    public string? TipoSociedad { get; set; }
    public decimal? IdTipoSociedadPadre { get; set; }
    public string? TipoSociedadPadre { get; set; }
    public string? NombresPropietario { get; set; }
    public string? CiPropietario { get; set; }
    public string? NombresRepresentante { get; set; }
    public string? CiRepresentante { get; set; }
    public string? Nit { get; set; }
    public decimal? IdTipoDocumento { get; set; }
    public string? RepresentanteLegal { get; set; }
    public string? Direccion { get; set; }
    public decimal? IdDepartamento { get; set; }
    public string? Departamento { get; set; }
    public decimal? IdMunicipio { get; set; }
    public decimal? IdLocalidad { get; set; }
    public string? Municipio { get; set; }
    public string? Localidad { get; set; }
    public decimal? IdMunicipioPadre { get; set; }
    public decimal? IdLocalidadPadre { get; set; }
    public string? MunicipioPadre { get; set; }
    public string? LocalidadPadre { get; set; }
    public decimal? Latitud { get; set; }
    public decimal? Longitud { get; set; }
    public decimal? IdActividad { get; set; }
    public string Actividad { get; set; } = null!;
    public string? Licencia { get; set; }
    public string? LicVigencia { get; set; }
    public string? LicVencimiento { get; set; }
    public string? LicProd { get; set; }
    public string? LicIdProd { get; set; }
    public string? InfProvSw { get; set; }
    public string? NroHydro { get; set; }
}
