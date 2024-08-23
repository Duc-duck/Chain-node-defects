param($installPath, $toolsPath, $package, $project)
$pylonDll = Join-Path $installPath "lib\native\Basler.Pylon.dll"
$project.Object.References.Add($pylonDll)
