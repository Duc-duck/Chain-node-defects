param($installPath, $toolsPath, $package, $project)
$project.Object.References | where-object { $_.Name -eq 'Basler.Pylon' } | ForEach-Object { $_.Remove() }
