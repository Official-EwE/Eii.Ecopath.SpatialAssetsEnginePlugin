# Eii.Ecopath.SpatialAssetsEnginePlugin
A stripped-down version of the STDF, without DotSpatial and GDAL, meant for running the STDF in automation exercises such as the EwERunner and SURIMI

## Package output

The package automatically places `Eii.Ecopath.SpatialAssetsEnginePlugin.dll` and its accompanying symbols and XML documentation (when copied) in `Plugins\SpatialAssetsEnginePlugin\` beneath the consuming project's build output. SDK-style publish output uses the same layout, including publishing with `--no-build`. PackageReference consumers also inherit these rules through transitive package references.

Dependencies keep their normal output locations. The host must load the plugin from this subdirectory; installing the package does not configure assembly probing. The NuGet cache layout is unchanged.
