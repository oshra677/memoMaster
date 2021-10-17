param($installPath, $toolsPath, $package, $project)
[System.Reflection.Assembly]::LoadFile("$($installPath)\lib\net35\System.Data.CData.Gmail.dll")
[System.Data.CData.Gmail.Nuget]::CheckNugetLicense("nuget")