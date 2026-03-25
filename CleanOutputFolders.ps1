param (
  [switch] $clearVs
)

# This script will clean up extra files and binary files.
# Use $clearVs to also remove the .vs folders, if you are having trouble opening or building the project

$root = Get-Location
# Folders to remove
if ($clearVs) {
    $folders = @("bin", "obj", "node_modules", ".vs")
} else {
    $folders = @("bin", "obj", "node_modules")
}


foreach ($folder in $folders) {
    Get-ChildItem -Path $root -Recurse -Directory -Force -Filter $folder |
        ForEach-Object {
            Write-Host "Deleting $($_.FullName)"
            Remove-Item -Path $_.FullName -Recurse -Force -ErrorAction SilentlyContinue
        }
}